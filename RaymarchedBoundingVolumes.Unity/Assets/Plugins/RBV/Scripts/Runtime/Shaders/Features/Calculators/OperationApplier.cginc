#pragma once

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
        case 0:
            sdf.distance = unionSDF(sdf1.distance, sdf2.distance);
            break;
        case 1:
            sdf.distance = subtractSDF(sdf1.distance, sdf2.distance);
            break;
        case 2:
            sdf = blendSDF(sdf1, sdf2, operation.blendStrength);
            break;
        case 3:
            sdf.distance = intersectSDF(sdf1.distance, sdf2.distance);
            break;
    }
    return sdf;
}
