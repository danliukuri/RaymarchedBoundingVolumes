#pragma once

#include "ObjectSDFCalculator.cginc"
#include "OperationApplier.cginc"
#include "../../Data/Variables/RaymarchingGlobalVariables.cginc"
#include "../../Data/Enumerations/TransformTypeEnumeration.cginc"
#include "../../Data/Structures/RaymarchingDataStructures.cginc"
#include "../../Data/Macros/Stack.cginc"

#ifdef RBV_4D_ON
#include "../../../../RBV.4D/Scripts/Runtime/Shaders/Data/Variables/RaymarchingGlobalVariables.cginc"
#include "../../../../RBV.4D/Scripts/Runtime/Shaders/Features/Calculators/ObjectSDFCalculator4D.cginc"
#endif

SDFData calculateObjectSDF(const float3 position, int index)
{
    SDFData objectSDF;

    UNITY_BRANCH
    switch (_RaymarchedObjects[index].transformType)
    {
        default:
        case TRANSFORM_TYPE_THREE_DIMENSIONAL:
            objectSDF = calculateObjectSDF(position,
                                           _RaymarchedObjects[index],
                                           _RaymarchedObjectsThreeDimensionalTransforms[index]);
            break;
#ifdef RBV_4D_ON
        case TRANSFORM_TYPE_FOUR_DIMENSIONAL:
            objectSDF = calculateObjectSDF4D(position,
                                             _RaymarchedObjects[index],
                                             _RaymarchedObjectsFourDimensionalTransforms[index]);
            break;
#endif
    }

    return objectSDF;
}

SDFData calculateOperationSDF(const float3 position, const OperationData operation,
                              const int    objectsIndex, const int       childObjectsCount)
{
    SDFData sdf = {_ObjectColor.rgb, _FarClippingPlane};

    for (int i = objectsIndex; i < childObjectsCount + objectsIndex; i++)
        UNITY_BRANCH
        if (_RaymarchedObjects[i].isActive)
            sdf = applyOperation(operation, calculateObjectSDF(position, i), sdf);

    return sdf;
}

DEFINE_STACK(SDFData, LayerSDFStack, MAX_INHERITANCE_LEVEL)

SDFData calculateSDF(const float3 position)
{
    int objectsIndex  = 0;
    int previousLayer = -1;

    for (int i = 0; i < _RaymarchingOperationsCount; i++)
    {
        const OperationNodeData current   = _RaymarchingOperationNodes[i];
        const OperationData     operation = _RaymarchingOperations[i];

        SDFData currentLayerSDF = calculateOperationSDF(position, operation, objectsIndex, current.childObjectsCount);
        objectsIndex += current.childObjectsCount;

        UNITY_BRANCH
        if (current.childOperationsCount > 0)
            currentLayerSDF = applyOperation(operation, currentLayerSDF, popFromLayerSDFStack());

        for (int j = 0; j < current.layer - previousLayer; j++)
            pushToLayerSDFStack(_DefaultSDFData);

        const OperationData parentOperation = _RaymarchingOperations[current.parentIndex];
        pushToLayerSDFStack(applyOperation(parentOperation, currentLayerSDF, popFromLayerSDFStack()));

        previousLayer = current.layer;
    }

    return popFromLayerSDFStack();
}