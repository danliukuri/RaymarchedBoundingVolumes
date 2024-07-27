#pragma once

#include <UnityShaderVariables.cginc>
#include "RaymarchingGlobalVariables.cginc"
#include "RaymarchingDataStructures.cginc"
#include "DistanceFunctions.cginc"
#include "BoolianOperators.cginc"
#include "SmoothBoolianOperators.cginc"

float calculateObjectSDF(const float3 position, const int index)
{
    const float3 objectLocalPosition =
        mul(unity_WorldToObject, float4(_RaymarchedObjects[index].position, 1));
    const float sdf = calculateSphereSDF(position - objectLocalPosition, 0.5);
    return sdf;
}

SDFData calculateOperationSDF(const float3 position, const OperationData operation, int objectIndexOffset)
{
    SDFData sdf = {_ObjectColor.rgb, _FarClippingPlane};

    for (int j = objectIndexOffset; j < operation.childCount + objectIndexOffset; j++)
        if (_RaymarchedObjects[j].isActive)
            switch (operation.type)
            {
            default:
            case 0:
                sdf.distanceToObject = unionSDF(sdf.distanceToObject, calculateObjectSDF(position, j));
                break;
            case 1:
                sdf.distanceToObject = subtractSDF(sdf.distanceToObject, calculateObjectSDF(position, j));
                break;
            case 2:
                const SDFData otherPixel = {_ObjectColor.rgb, calculateObjectSDF(position, j)};
                sdf = blendSDF(sdf, otherPixel, operation.blendStrength);
                break;
            }

    return sdf;
}

SDFData calculateSDF(const float3 position)
{
    SDFData sdf = calculateOperationSDF(position, _RaymarchingOperations[0], 0);
    float objectIndexOffset =  _RaymarchingOperations[0].childCount;

    for (int i = 1; i < _RaymarchingOperationsCount; i++)
    {
        const SDFData operationSDF = calculateOperationSDF(position, _RaymarchingOperations[i], objectIndexOffset);
        objectIndexOffset += _RaymarchingOperations[i].childCount;

        sdf.distanceToObject = unionSDF(sdf.distanceToObject, operationSDF.distanceToObject);
        if (operationSDF.distanceToObject < sdf.distanceToObject)
            sdf.pixelColor = operationSDF.pixelColor;
    }
    return sdf;
}