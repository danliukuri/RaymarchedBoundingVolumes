#pragma once

#include "2DSDFs.cginc"
#include "ModificationOperations.cginc"

float calculateCubeSDF(const float3 position, const float3 halfDimensions)
{
    float3 distance        = abs(position) - halfDimensions;
    float  outsideDistance = length(max(distance, 0.0));
    float  insideDistance  = min(max(distance.x, max(distance.y, distance.z)), 0.0);
    return outsideDistance + insideDistance;
}

float calculateSphereSDF(const float3 position, const float radius)
{
    return length(position) - radius;
}

/**
 * Calculate the signed distance of a point to an ellipsoid.
 *
 * For more information on ellipsoid SDF calculations, visit:
 * <a href="https://iquilezles.org/articles/ellipsoids">Inigo Quilez - Ellipsoid SDF</a>
 *
 * @param position The 3D position vector to calculate the distance from.
 * @param radii The radii of the ellipsoid in the X, Y, and Z axes.
 * @return The signed distance of the point to the ellipsoid.
 */
float calculateEllipsoidSDF(const float3 position, const float3 radii)
{
    float3 normalizedPosition = position / radii;
    float  normalizedDistance = length(normalizedPosition);
    float  outsideDistance    = normalizedDistance - 1.0;
    float  gradientLength     = length(normalizedPosition / radii);
    return normalizedDistance * outsideDistance / gradientLength;
}

float calculateCapsuleSDF(const float3 position, const float halfHeight, const float radius)
{
    return calculateSphereSDF(elongateY(position, halfHeight), radius);
}

float calculateEllipsoidalCapsuleSDF(const float3 position, const float halfHeight, const float3 radii)
{
    return calculateEllipsoidSDF(elongateY(position, halfHeight), radii);
}

float calculateCylinderSDF(const float3 position, const float height, const float radius)
{
    return extrudeY(position, calculateCircleSDF(position.xz, radius), height);
}

float calculateEllipsoidalCylinderSDF(const float3 position, const float3 dimensions)
{
    return extrudeY(position, calculateEllipseSDF(position.xz, dimensions.xz), dimensions.y);
}

float calculatePlaneSDF(const float3 position, const float3 halfDimensions)
{
    return calculateCubeSDF(position, halfDimensions);
}

float calculateConeSDF(const float3 position, const float height, const float radius)
{
    float2 baseToHeight = float2(radius / height, -1.0);

    float2 projectedPosition = float2(length(position.xz), position.y - height * 0.5);
    float2 vectorToBase      = height * baseToHeight;

    float clampedProjectionFactor =
        clamp(dot(projectedPosition, vectorToBase) / dot(vectorToBase, vectorToBase), 0.0, 1.0);
    float2 clampedBaseIntersection =
        float2(clamp(projectedPosition.x / vectorToBase.x, 0.0, 1.0), 1.0);

    float2 distanceToSurfaceA = projectedPosition - vectorToBase * clampedProjectionFactor;
    float2 distanceToSurfaceB = projectedPosition - vectorToBase * clampedBaseIntersection;
    float  distanceSquared    =
        min(dot(distanceToSurfaceA, distanceToSurfaceA), dot(distanceToSurfaceB, distanceToSurfaceB));

    float signCorrection   = sign(vectorToBase.y);
    float sideDistance     = projectedPosition.x * vectorToBase.y - projectedPosition.y * vectorToBase.x;
    float heightDifference = projectedPosition.y - vectorToBase.y;
    float surfaceSign      = max(signCorrection * sideDistance, signCorrection * heightDifference);

    return sqrt(distanceSquared) * sign(surfaceSign);
}