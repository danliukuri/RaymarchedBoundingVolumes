#pragma once

#include <UnityShaderVariables.cginc>

#include "Object3DRotator.cginc"
#include "../../Data/Variables/RaymarchingGlobalVariables.cginc"
#include "../../Data/Variables/ObjectTypeRelatedVariables.cginc"
#include "../../Data/Structures/RaymarchingDataStructures.cginc"
#include "../Functions/SDFs.cginc"

SDFData calculateObjectSDF(float3 position, ObjectData objectData, ObjectTransform3D transform)
{
    position -= mul(unity_WorldToObject, float4(transform.position, 1));
    position = rotate3D(position, transform.rotation);
    position /= transform.scale;
    float distance;

    UNITY_BRANCH
    switch (objectData.type)
    {
        default:
        case 0:
            distance = calculateCubeSDF(position, _RaymarchedCubeData[objectData.typeDataIndex].halfDimensions);
            break;
        case 1:
            distance = calculateSphereSDF(position, _RaymarchedSphereData[objectData.typeDataIndex].radius);
            break;
    }
    distance *= transform.scale;

    const SDFData sdf = {_ObjectColor.rgb, distance};
    return sdf;
}