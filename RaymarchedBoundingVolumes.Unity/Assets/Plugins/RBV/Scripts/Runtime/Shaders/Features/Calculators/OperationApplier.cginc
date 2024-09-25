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
            float radius = _RaymarchingSmoothUnionOperationData[operation.typeDataIndex].radius;
            sdf.distance = smoothUnionSDF(sdf1.distance, sdf2.distance, radius);
            sdf.color = smoothUnionColor(sdf1, sdf2, radius);
            break;
    }
    return sdf;
}