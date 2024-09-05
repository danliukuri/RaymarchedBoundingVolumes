#pragma once

#include <UnityShaderVariables.cginc>

#include "../../../../../../RBV/Scripts/Runtime/Shaders/Data/Variables/RaymarchingGlobalVariables.cginc"
#include "../../../../../../RBV/Scripts/Runtime/Shaders/Features/Calculators/Object3DRotator.cginc"

#include "../../Data/Enumerations/ObjectTypeEnumeration.cginc"
#include "../../Data/Structures/RaymarchingDataStructures.cginc"
#include "../../Data/Variables/ObjectTypeRelatedVariables.cginc"
#include "../Functions/SDFs.cginc"
#include "Object4DRotator.cginc"

SDFData calculateObjectSDF4D(float3 position, ObjectData objectData, ObjectTransform4D transform)
{
    position -= mul(unity_WorldToObject, float4(transform.position.xyz, 1));
    position = rotate3D(position, transform.rotation);

    float4 position4D = float4(position, transform.position.w);
    position4D = rotate4D(position4D, transform.rotation4D);
    position4D /= transform.scale;
    float distance;

    UNITY_BRANCH
    switch (objectData.type)
    {
        default:
        case OBJECT_TYPE_HYPERSPHERE:
            HypersphereData hypersphereData = _RaymarchedHypersphereData[objectData.typeDataIndex];
            distance = calculateHypersphereSDF(position4D, hypersphereData.radius);
            break;
        case OBJECT_TYPE_HYPERCUBE:
            HypercubeData hypercubeData = _RaymarchedHypercubeData[objectData.typeDataIndex];
            distance = calculateHypercubeSDF(position4D, hypercubeData.halfDimensions);
            break;
    }
    distance *= transform.scale;

    const SDFData sdf = {_ObjectColor.rgb, distance};
    return sdf;
}