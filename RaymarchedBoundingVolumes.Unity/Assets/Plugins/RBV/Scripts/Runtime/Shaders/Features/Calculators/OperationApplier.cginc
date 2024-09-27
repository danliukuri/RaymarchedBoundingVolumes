#pragma once

#include "../../Data/Enumerations/OperationTypeEnumeration.cginc"
#include "../../Data/Variables/RaymarchingGlobalVariables.cginc"
#include "../../Data/Structures/RaymarchingDataStructures.cginc"
#include "../../Data/Variables/OperationTypeRelatedVariables.cginc"
#include "../Functions/BoolianOperators.cginc"
#include "../Functions/ColorCombinationOperators.cginc"

fixed3 calculateColor(const SDFData sdf1, const SDFData sdf2)
{
    bool isSdf1Closer = sdf1.distance < sdf2.distance;
    return sdf1.color * isSdf1Closer + sdf2.color * !isSdf1Closer;
}

SDFData applyOperation(const OperationData operation, const SDFData sdf1, const SDFData sdf2)
{
    SDFData sdf;

    if (sdf1.distance >= _FarClippingPlane || sdf2.distance >= _FarClippingPlane)
    {
        sdf.distance = unionSDF(sdf1.distance, sdf2.distance);
        sdf.color    = calculateColor(sdf1, sdf2);
        return sdf;
    }

    UNITY_BRANCH
    switch (operation.type)
    {
        default:
        case OPERATION_TYPE_UNION:
            sdf.distance = unionSDF(sdf1.distance, sdf2.distance);
            sdf.color = calculateColor(sdf1, sdf2);
            break;
        case OPERATION_TYPE_SUBTRACT:
            sdf.distance = subtractSDF(sdf1.distance, sdf2.distance);
            sdf.color = calculateColor(sdf1, sdf2);
            break;
        case OPERATION_TYPE_INTERSECT:
            sdf.distance = intersectSDF(sdf1.distance, sdf2.distance);
            sdf.color = calculateColor(sdf1, sdf2);
            break;
        case OPERATION_TYPE_XOR:
            sdf.distance = xorSDF(sdf1.distance, sdf2.distance);
            sdf.color = calculateColor(sdf1, sdf2);
            break;
        case OPERATION_TYPE_SMOOTH_UNION:
            float smoothUnionRadius = _RaymarchingSmoothUnionOperationData[operation.typeDataIndex].radius;
            sdf.distance = smoothUnionSDF(sdf1.distance, sdf2.distance, smoothUnionRadius);
            sdf.color    = smoothUnionColor(sdf1, sdf2, smoothUnionRadius);
            break;
        case OPERATION_TYPE_SMOOTH_SUBTRACT:
            float smoothSubtractRadius = _RaymarchingSmoothSubtractOperationData[operation.typeDataIndex].radius;
            sdf.distance = smoothSubtractSDF(sdf1.distance, sdf2.distance, smoothSubtractRadius);
            sdf.color    = smoothUnionColor(sdf1, sdf2, smoothSubtractRadius);
            break;
        case OPERATION_TYPE_SMOOTH_INTERSECT:
            float smoothIntersectionRadius = _RaymarchingSmoothIntersectOperationData[operation.typeDataIndex].radius;
            sdf.distance = smoothIntersectSDF(sdf1.distance, sdf2.distance, smoothIntersectionRadius);
            sdf.color    = smoothUnionColor(sdf1, sdf2, smoothIntersectionRadius);
            break;
        case OPERATION_TYPE_SMOOTH_XOR:
            float smoothXorOuterRadius = _RaymarchingSmoothXorOperationData[operation.typeDataIndex].outerRadius;
            float smoothXorInnerRadius = _RaymarchingSmoothXorOperationData[operation.typeDataIndex].innerRadius;
            sdf.distance = smoothXorSDF(sdf1.distance, sdf2.distance, smoothXorOuterRadius, smoothXorInnerRadius);
            sdf.color = smoothUnionColor(sdf1, sdf2, smoothXorOuterRadius);
            break;
        case OPERATION_TYPE_CHAMFER_UNION:
            float chamferUnionRadius = _RaymarchingChamferUnionOperationData[operation.typeDataIndex].radius;
            sdf.distance = chamferUnionSDF(sdf1.distance, sdf2.distance, chamferUnionRadius);
            sdf.color    = smoothUnionColor(sdf1, sdf2, chamferUnionRadius);
            break;
        case OPERATION_TYPE_CHAMFER_SUBTRACT:
            float chamferSubtractRadius = _RaymarchingChamferSubtractOperationData[operation.typeDataIndex].radius;
            sdf.distance = chamferSubtractSDF(sdf1.distance, sdf2.distance, chamferSubtractRadius);
            sdf.color    = smoothUnionColor(sdf1, sdf2, chamferSubtractRadius);
            break;
        case OPERATION_TYPE_CHAMFER_INTERSECT:
            float chamferIntersectRadius = _RaymarchingChamferIntersectOperationData[operation.typeDataIndex].radius;
            sdf.distance = chamferIntersectSDF(sdf1.distance, sdf2.distance, chamferIntersectRadius);
            sdf.color    = smoothUnionColor(sdf1, sdf2, chamferIntersectRadius);
            break;
        case OPERATION_TYPE_CHAMFER_XOR:
            float chamferXorOuterRadius = _RaymarchingChamferXorOperationData[operation.typeDataIndex].outerRadius;
            float chamferXorInnerRadius = _RaymarchingChamferXorOperationData[operation.typeDataIndex].innerRadius;
            sdf.distance = chamferXorSDF(sdf1.distance, sdf2.distance, chamferXorOuterRadius, chamferXorInnerRadius);
            sdf.color = smoothUnionColor(sdf1, sdf2, chamferXorOuterRadius);
            break;
        case OPERATION_TYPE_STAIRS_UNION:
            float stairsUnionRadius = _RaymarchingStairsUnionOperationData[operation.typeDataIndex].radius;
            float stairsUnionCount = _RaymarchingStairsUnionOperationData[operation.typeDataIndex].count;
            sdf.distance           = stairsUnionSDF(sdf1.distance, sdf2.distance, stairsUnionRadius, stairsUnionCount);
            sdf.color              = smoothUnionColor(sdf1, sdf2, stairsUnionRadius);
            break;
        case OPERATION_TYPE_STAIRS_SUBTRACT:
            float stairsSubtractRadius = _RaymarchingStairsSubtractOperationData[operation.typeDataIndex].radius;
            float stairsSubtractCount = _RaymarchingStairsSubtractOperationData[operation.typeDataIndex].count;
            sdf.distance = stairsSubtractSDF(sdf1.distance, sdf2.distance, stairsSubtractRadius, stairsSubtractCount);
            sdf.color = smoothUnionColor(sdf1, sdf2, stairsSubtractRadius);
            break;
    }
    return sdf;
}