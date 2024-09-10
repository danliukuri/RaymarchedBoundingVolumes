#pragma once

#include "../../Data/Enumerations/OperationTypeEnumeration.cginc"
#include "../../Data/Variables/RaymarchingGlobalVariables.cginc"
#include "../../Data/Structures/RaymarchingDataStructures.cginc"
#include "../Functions/BoolianOperators.cginc"
#include "../Functions/SmoothBoolianOperators.cginc"

SDFData applyOperation(const OperationData operation, const SDFData sdf1, const SDFData sdf2)
{
    SDFData sdf = _DefaultSDFData;
    if (sdf1.distance >= _FarClippingPlane || sdf2.distance >= _FarClippingPlane)
    {
        sdf.distance = unionSDF(sdf1.distance, sdf2.distance);
        return sdf;
    }

    UNITY_BRANCH
    switch (operation.type)
    {
        default:
        case OPERATION_TYPE_UNION:
            sdf.distance = unionSDF(sdf1.distance, sdf2.distance);
            break;
        case OPERATION_TYPE_SUBTRACT:
            sdf.distance = subtractSDF(sdf1.distance, sdf2.distance);
            break;
        case OPERATION_TYPE_BLEND:
            sdf = blendSDF(sdf1, sdf2, operation.blendStrength);
            break;
        case OPERATION_TYPE_INTERSECT:
            sdf.distance = intersectSDF(sdf1.distance, sdf2.distance);
            break;
    }
    return sdf;
}