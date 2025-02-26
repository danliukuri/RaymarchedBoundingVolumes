﻿#pragma once

#include <UnityShaderVariables.cginc>

#include "Object3DRotator.cginc"
#include "../../Data/Enumerations/Object3DTypeEnumeration.cginc"
#include "../../Data/Variables/ObjectTypeRelatedVariables.cginc"
#include "../../Data/Structures/RaymarchingDataStructures.cginc"
#include "../Functions/SDFs.cginc"

float calculateObjectSDF(float3 position, ObjectData objectData, ObjectTransform3D transform)
{
    position -= mul(unity_WorldToObject, float4(transform.position, 1));
    position = rotate3D(position, transform.rotation);
    position /= transform.scale;
    float distance;

    UNITY_BRANCH
    switch (objectData.type)
    {
        default:
        case OBJECT_3D_TYPE_CUBE:
            distance = calculateCubeSDF(position, _RaymarchedCubeData[objectData.typeDataIndex].halfDimensions);
            break;
        case OBJECT_3D_TYPE_SPHERE:
            distance = calculateSphereSDF(position, _RaymarchedSphereData[objectData.typeDataIndex].radius);
            break;
        case OBJECT_3D_TYPE_ELLIPSOID:
            distance = calculateEllipsoidSDF(position, _RaymarchedEllipsoidData[objectData.typeDataIndex].radii);
            break;
        case OBJECT_3D_TYPE_CAPSULE:
            CapsuleData capsuleData = _RaymarchedCapsuleData[objectData.typeDataIndex];
            distance = calculateCapsuleSDF(position, capsuleData.halfHeight, capsuleData.base.radius);
            break;
        case OBJECT_3D_TYPE_ELLIPTIC_CAPSULE:
            EllipticCapsuleData ellipticCapsuleData = _RaymarchedEllipticCapsuleData[objectData.typeDataIndex];
            distance = calculateEllipticCapsuleSDF(position, ellipticCapsuleData.halfHeight,
                                                   ellipticCapsuleData.ellipsoid.radii);
            break;
        case OBJECT_3D_TYPE_CYLINDER:
            CylinderData cylinderData = _RaymarchedCylinderData[objectData.typeDataIndex];
            distance = calculateCylinderSDF(position, cylinderData.height, cylinderData.base.radius);
            break;
        case OBJECT_3D_TYPE_ELLIPTIC_CYLINDER:
            EllipticCylinderData ellipticCylinderData = _RaymarchedEllipticCylinderData[objectData.typeDataIndex];
            distance = calculateEllipticCylinderSDF(position, ellipticCylinderData.dimensions);
            break;
        case OBJECT_3D_TYPE_PLANE:
            distance = calculatePlaneSDF(position, _RaymarchedPlaneData[objectData.typeDataIndex].halfDimensions);
            break;
        case OBJECT_3D_TYPE_CONE:
            CapsuleData coneData = _RaymarchedConeData[objectData.typeDataIndex];
            distance = calculateConeSDF(position, coneData.halfHeight, coneData.base.radius);
            break;
        case OBJECT_3D_TYPE_CAPPED_CONE:
            CappedConeData cappedConeData = _RaymarchedCappedConeData[objectData.typeDataIndex];
            distance = calculateCappedConeSDF(position, cappedConeData.halfHeight,
                                              cappedConeData.topBaseRadius, cappedConeData.bottomBaseRadius);
            break;
        case OBJECT_3D_TYPE_TORUS:
            TorusData torusData = _RaymarchedTorusData[objectData.typeDataIndex];
            distance = calculateTorusSDF(position, torusData.majorRadius, torusData.minorRadius);
            break;
        case OBJECT_3D_TYPE_CAPPED_TORUS:
            CappedTorusData cappedTorusData = _RaymarchedCappedTorusData[objectData.typeDataIndex];
            distance = calculateCappedTorusSDF(position, cappedTorusData.capAngle,
                                               cappedTorusData.torus.majorRadius, cappedTorusData.torus.minorRadius);
            break;
        case OBJECT_3D_TYPE_REGULAR_PRISM:
            RegularPrismData regularPrismData = _RaymarchedRegularPrismData[objectData.typeDataIndex];
            distance = calculateRegularPrismSDF(position, regularPrismData.verticesCount,
                                                regularPrismData.circumradius, regularPrismData.halfLength);
            break;
        case OBJECT_3D_TYPE_REGULAR_POLYHEDRON:
            RegularPolyhedronData regularPolyhedronData = _RaymarchedRegularPolyhedronData[objectData.typeDataIndex];
            distance = calculateRegularPolyhedronSDF(position, regularPolyhedronData.inscribedRadius,
                                                     regularPolyhedronData.activeBoundPlaneRange.start,
                                                     regularPolyhedronData.activeBoundPlaneRange.end);
            break;
    }
    distance *= transform.scale;

    return distance;
}