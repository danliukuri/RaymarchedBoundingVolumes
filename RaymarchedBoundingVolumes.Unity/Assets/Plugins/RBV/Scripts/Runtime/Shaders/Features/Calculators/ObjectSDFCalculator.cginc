#pragma once

#include <UnityShaderVariables.cginc>

#include "Object3DRotator.cginc"
#include "../../Data/Enumerations/Object3DTypeEnumeration.cginc"
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
            distance = calculateCapsuleSDF(position, capsuleData.halfHeight, capsuleData.radius);
            break;
        case OBJECT_3D_TYPE_ELLIPTIC_CAPSULE:
            EllipticCapsuleData ellipticCapsuleData = _RaymarchedEllipticCapsuleData[objectData.typeDataIndex];
            distance = calculateEllipticCapsuleSDF(position, ellipticCapsuleData.halfHeight, ellipticCapsuleData.radii);
            break;
        case OBJECT_3D_TYPE_CYLINDER:
            CylinderData cylinderData = _RaymarchedCylinderData[objectData.typeDataIndex];
            distance = calculateCylinderSDF(position, cylinderData.height, cylinderData.radius);
            break;
        case OBJECT_3D_TYPE_ELLIPTIC_CYLINDER:
            EllipticCylinderData ellipticCylinderData = _RaymarchedEllipticCylinderData[objectData.typeDataIndex];
            distance = calculateEllipticCylinderSDF(position, ellipticCylinderData.dimensions);
            break;
        case OBJECT_3D_TYPE_PLANE:
            distance = calculatePlaneSDF(position, _RaymarchedPlaneData[objectData.typeDataIndex].halfDimensions);
            break;
        case OBJECT_3D_TYPE_CONE:
            ConeData coneData = _RaymarchedConeData[objectData.typeDataIndex];
            distance = calculateConeSDF(position, coneData.height, coneData.radius);
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
                                               cappedTorusData.majorRadius, cappedTorusData.minorRadius);
            break;
        case OBJECT_3D_TYPE_REGULAR_PRISM:
            RegularPrismData regularPrismData = _RaymarchedRegularPrismData[objectData.typeDataIndex];
            distance = calculateRegularPrismSDF(position, regularPrismData.verticesCount,
                                                regularPrismData.circumradius, regularPrismData.length);
            break;
        case OBJECT_3D_TYPE_REGULAR_POLYHEDRON:
            RegularPolyhedronData regularPolyhedronData = _RaymarchedRegularPolyhedronData[objectData.typeDataIndex];
            distance = calculateRegularPolyhedronSDF(position, regularPolyhedronData.inscribedRadius,
                                                     regularPolyhedronData.activeBoundPlaneRange.start,
                                                     regularPolyhedronData.activeBoundPlaneRange.end);
            break;
    }
    distance *= transform.scale;

    const SDFData sdf = {_ObjectColor.rgb, distance};
    return sdf;
}