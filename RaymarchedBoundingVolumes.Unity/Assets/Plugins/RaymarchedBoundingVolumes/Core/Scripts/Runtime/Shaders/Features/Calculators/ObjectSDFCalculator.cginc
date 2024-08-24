#pragma once

#include <UnityShaderVariables.cginc>
#include "../../Data/Variables/RaymarchingGlobalVariables.cginc"
#include "../../Data/Variables/ObjectTypeRelatedVariables.cginc"
#include "../../Data/Structures/RaymarchingDataStructures.cginc"
#include "../Functions/SDFs.cginc"

SDFData calculateObjectSDF(float3 position, ObjectData objectData)
{
    position -= mul(unity_WorldToObject, float4(objectData.position, 1));

    float distance;
    const float unityMetricSystemMultiplier = 0.5;

    UNITY_BRANCH
    switch (objectData.type)
    {
        default:
        case 0:
            SphereData sphereData = _RaymarchedSphereData[objectData.typeRelatedDataIndex];
            distance = calculateSphereSDF(position, sphereData.radius * unityMetricSystemMultiplier);
            break;
        case 1:
            CubeData cubeData = _RaymarchedCubeData[objectData.typeRelatedDataIndex];
            distance = calculateCubeSDF(position, cubeData.size * unityMetricSystemMultiplier);
            break;
    }

    const SDFData sdf = {_ObjectColor.rgb, distance};
    return sdf;
}
