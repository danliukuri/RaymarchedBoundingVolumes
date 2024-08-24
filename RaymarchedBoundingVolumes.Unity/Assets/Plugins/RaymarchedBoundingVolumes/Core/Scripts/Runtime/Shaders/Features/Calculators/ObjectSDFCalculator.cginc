#pragma once

#include <UnityShaderVariables.cginc>

#include "Object3DRotator.cginc"
#include "../../Data/Variables/RaymarchingGlobalVariables.cginc"
#include "../../Data/Variables/ObjectTypeRelatedVariables.cginc"
#include "../../Data/Structures/RaymarchingDataStructures.cginc"
#include "../Functions/SDFs.cginc"

SDFData calculateObjectSDF(float3 position, ObjectData objectData)
{
    position -= mul(unity_WorldToObject, float4(objectData.position, 1));
    position = rotate3D(position, objectData.rotation);

    float distance;

    UNITY_BRANCH
    switch (objectData.type)
    {
        default:
        case 0:
            distance = calculateSphereSDF(position, _RaymarchedSphereData[objectData.typeRelatedDataIndex].radius);
            break;
        case 1:
            distance = calculateCubeSDF(position, _RaymarchedCubeData[objectData.typeRelatedDataIndex].halfDimensions);
            break;
    }

    const SDFData sdf = {_ObjectColor.rgb, distance};
    return sdf;
}