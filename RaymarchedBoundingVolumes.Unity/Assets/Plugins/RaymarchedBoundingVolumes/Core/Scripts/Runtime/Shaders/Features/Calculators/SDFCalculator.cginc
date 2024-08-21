#pragma once

#include "../../Data/Variables/RaymarchingGlobalVariables.cginc"
#include "../../Data/Structures/RaymarchingDataStructures.cginc"
#include "../Functions/SDFs.cginc"
#include "OperationApplier.cginc"
#include "Assets/Plugins/RaymarchedBoundingVolumes/Core/Scripts/Runtime/Shaders/Data/Macros/Stack.cginc"

SDFData calculateObjectSDF(const float3 position, const int index)
{
    const float3 objectLocalPosition = mul(unity_WorldToObject, float4(_RaymarchedObjects[index].position, 1));
    const float distance = calculateSphereSDF(position - objectLocalPosition, 0.5);
    const SDFData sdf = {_ObjectColor.rgb, distance};
    return sdf;
}

SDFData calculateOperationSDF(const float3 position, const OperationData operation,
                              const int objectsIndex, const int childObjectsCount)
{
    SDFData sdf = {_ObjectColor.rgb, _FarClippingPlane};

    for (int i = objectsIndex; i < childObjectsCount + objectsIndex; i++)
        UNITY_BRANCH
        if (_RaymarchedObjects[i].isActive)
        {
            const SDFData objectSDF = calculateObjectSDF(position, i);
            sdf = applyOperation(operation, objectSDF, sdf);
        }

    return sdf;
}

DEFINE_STACK(SDFData, LayerSDFStack, MAX_INHERITANCE_LEVEL)

SDFData calculateSDF(const float3 position)
{
    int objectsIndex  =  0;
    int previousLayer = -1;

    for (int i = 0; i < _RaymarchingOperationsCount; i++)
    {
        const OperationNodeData current   = _RaymarchingOperationNodes[i];
        const OperationData     operation = _RaymarchingOperations    [i];

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