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
        case 2:
            distance = calculateEllipsoidSDF(position, _RaymarchedEllipsoidData[objectData.typeDataIndex].radii);
            break;
        case 3:
            CapsuleData capsuleData = _RaymarchedCapsuleData[objectData.typeDataIndex];
            distance = calculateCapsuleSDF(position, capsuleData.halfHeight, capsuleData.radius);
            break;
        case 4:
            EllipsoidalCapsuleData ellipsoidalCapsuleData = _RaymarchedEllipsoidalCapsuleData[objectData.typeDataIndex];
            distance = calculateEllipsoidalCapsuleSDF(position,
                                                      ellipsoidalCapsuleData.halfHeight, ellipsoidalCapsuleData.radii);
            break;
        case 5:
            CylinderData cylinderData = _RaymarchedCylinderData[objectData.typeDataIndex];
            distance = calculateCylinderSDF(position, cylinderData.height, cylinderData.radius);
            break;
        case 6:
            EllipsoidalCylinderData ellipsoidalCylinderData =
                _RaymarchedEllipsoidalCylinderData[objectData.typeDataIndex];
            distance = calculateEllipsoidalCylinderSDF(position, ellipsoidalCylinderData.dimensions);
            break;
        case 7:
            distance = calculatePlaneSDF(position, _RaymarchedPlaneData[objectData.typeDataIndex].halfDimensions);
            break;
    }
    distance *= transform.scale;

    const SDFData sdf = {_ObjectColor.rgb, distance};
    return sdf;
}