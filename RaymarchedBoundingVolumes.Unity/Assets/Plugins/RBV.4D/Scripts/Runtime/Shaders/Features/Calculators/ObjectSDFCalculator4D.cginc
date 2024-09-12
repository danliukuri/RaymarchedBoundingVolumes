#pragma once

#include <UnityShaderVariables.cginc>

#include "../../../../../../RBV/Scripts/Runtime/Shaders/Data/Variables/RaymarchingGlobalVariables.cginc"
#include "../../../../../../RBV/Scripts/Runtime/Shaders/Features/Calculators/Object3DRotator.cginc"

#include "../../Data/Enumerations/Object4DTypeEnumeration.cginc"
#include "../../Data/Structures/RaymarchingDataStructures.cginc"
#include "../../Data/Variables/ObjectTypeRelatedVariables.cginc"
#include "../Functions/SDFs.cginc"
#include "Object4DRotator.cginc"

SDFData calculateObjectSDF4D(float3 position, ObjectData objectData, ObjectTransform4D transform)
{
    position -= mul(unity_WorldToObject, float4(transform.position.xyz, 1));
    position = rotate3D(position, transform.rotation);

    float4 position4D = float4(position, transform.position.w);
    position4D        = rotate4D(position4D, transform.rotation4D);
    position4D /= transform.scale;
    float distance;

    UNITY_BRANCH
    switch (objectData.type)
    {
        default:
        case OBJECT_4D_TYPE_HYPERCUBE:
            HypercubeData hypercubeData = _RaymarchedHypercubeData[objectData.typeDataIndex];
            distance = calculateHypercubeSDF(position4D, hypercubeData.halfDimensions);
            break;
        case OBJECT_4D_TYPE_HYPERSPHERE:
            HypersphereData hypersphereData = _RaymarchedHypersphereData[objectData.typeDataIndex];
            distance = calculateHypersphereSDF(position4D, hypersphereData.radius);
            break;
        case OBJECT_4D_TYPE_HYPERELLIPSOID:
            HyperellipsoidData hyperellipsoidData = _RaymarchedHyperellipsoidData[objectData.typeDataIndex];
            distance = calculateHyperellipsoidSDF(position4D, hyperellipsoidData.radii);
            break;
        case OBJECT_4D_TYPE_HYPERCAPSULE:
            HypercapsuleData hypercapsuleData = _RaymarchedHypercapsuleData[objectData.typeDataIndex];
            distance = calculateHypercapsuleSDF(position4D, hypercapsuleData.halfHeight, hypercapsuleData.radius);
            break;
        case OBJECT_4D_TYPE_ELLIPSOIDAL_HYPERCAPSULE:
            EllipsoidalHypercapsuleData ellipsoidalHypercapsuleData =
                _RaymarchedEllipsoidalHypercapsuleData[objectData.typeDataIndex];
            distance = calculateEllipsoidalHypercapsuleSDF(position4D,
                                                           ellipsoidalHypercapsuleData.halfHeight,
                                                           ellipsoidalHypercapsuleData.radii);
            break;
        case OBJECT_4D_TYPE_CUBICAL_CYLINDER:
            CubicalCylinderData cubicalCylinderData = _RaymarchedCubicalCylinderData[objectData.typeDataIndex];
            distance = calculateCubicalCylinderSDF(position4D, cubicalCylinderData.radius,
                                                   cubicalCylinderData.halfHeight, cubicalCylinderData.halfTrength);
            break;
    }
    distance *= transform.scale;

    const SDFData sdf = {_ObjectColor.rgb, distance};
    return sdf;
}