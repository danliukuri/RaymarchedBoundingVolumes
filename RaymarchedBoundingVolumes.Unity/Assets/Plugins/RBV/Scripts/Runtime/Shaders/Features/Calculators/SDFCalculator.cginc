#pragma once

#include "ObjectSDFCalculator.cginc"
#include "OperationApplier.cginc"
#include "../../Data/Variables/RaymarchingGlobalVariables.cginc"
#include "../../Data/Enumerations/TransformTypeEnumeration.cginc"
#include "../../Data/Structures/RaymarchingDataStructures.cginc"
#include "../../Data/Macros/Stack.cginc"

#ifdef RBV_4D_ON
#ifdef RBV_4D_IS_PACKAGE
#include "Packages/com.danliukuri.rbv.4d/Scripts/Runtime/Shaders/Data/Variables/RaymarchingGlobalVariables.cginc"
#include "Packages/com.danliukuri.rbv.4d/Scripts/Runtime/Shaders/Features/Calculators/ObjectSDFCalculator4D.cginc"
#else
#include "../../../../RBV.4D/Scripts/Runtime/Shaders/Data/Variables/RaymarchingGlobalVariables.cginc"
#include "../../../../RBV.4D/Scripts/Runtime/Shaders/Features/Calculators/ObjectSDFCalculator4D.cginc"
#endif
#endif

SDFData calculateObjectSDF(float3 position, ObjectData object, ObjectRenderingSettings renderingSettings)
{
    SDFData objectSDF;
    objectSDF.color = renderingSettings.color.rgb;

    UNITY_BRANCH
    switch (object.transformType)
    {
        default:
        case TRANSFORM_TYPE_THREE_DIMENSIONAL:
            ObjectTransform3D transform3D = _RaymarchedObjectsThreeDimensionalTransforms[object.transformDataIndex];
            objectSDF.distance = calculateObjectSDF(position, object, transform3D);
            break;
#ifdef RBV_4D_ON
        case TRANSFORM_TYPE_FOUR_DIMENSIONAL:
            ObjectTransform4D transform4D = _RaymarchedObjectsFourDimensionalTransforms[object.transformDataIndex];
            objectSDF.distance = calculateObjectSDF4D(position, object, transform4D);
            break;
#endif
    }

    return objectSDF;
}

SDFData calculateOperationSDF(const float3        position,
                              const OperationData operation,
                              const int           objectsIndex,
                              const int           childObjectsCount)
{
    SDFData operationSdf = _DefaultSDFData;

    for (int i = objectsIndex; i < childObjectsCount + objectsIndex; i++)
    {
        SDFData objectSDF = calculateObjectSDF(position, _RaymarchedObjects[i], _RaymarchedObjectsRenderingSettings[i]);
        operationSdf = applyOperation(operation, objectSDF, operationSdf);
    }

    return operationSdf;
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