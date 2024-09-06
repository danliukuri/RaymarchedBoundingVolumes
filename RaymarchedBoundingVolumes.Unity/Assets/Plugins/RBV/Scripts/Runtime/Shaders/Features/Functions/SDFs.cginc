#pragma once

#include "ModificationOperations.cginc"

float calculateCircleSDF(const float2 position, const float radius)
{
    return length(position) - radius;
}

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
    float2 displacement    = float2(calculateCircleSDF(position.xz, radius), abs(position.y) - height);
    float  outsideDistance = length(max(displacement, 0.0));
    float  insideDistance  = min(max(displacement.x, displacement.y), 0.0);
    return outsideDistance + insideDistance;
}