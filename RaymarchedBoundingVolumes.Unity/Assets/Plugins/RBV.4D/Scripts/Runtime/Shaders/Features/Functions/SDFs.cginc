#pragma once

#include "ModificationOperations.cginc"

float calculateHypercubeSDF(const float4 position, const float4 halfDimensions)
{
    return extrudeOrigin(position, halfDimensions);
}

float calculateHypersphereSDF(const float4 position, const float radius)
{
    return length(position) - radius;
}

float calculateHyperellipsoidSDF(const float4 position, const float4 radii)
{
    float4 normalizedPosition = position / radii;
    float  normalizedDistance = length(normalizedPosition);
    float  outsideDistance    = normalizedDistance - 1.0;
    float  gradientLength     = length(normalizedPosition / radii);
    return normalizedDistance * outsideDistance / gradientLength;
}

float calculateHypercapsuleSDF(const float4 position, const float halfHeight, const float radius)
{
    return calculateHypersphereSDF(elongateY(position, halfHeight), radius);
}

float calculateEllipsoidalHypercapsuleSDF(const float4 position, const float halfHeight, const float4 radii)
{
    return calculateHyperellipsoidSDF(elongateY(position, halfHeight), radii);
}

float calculateCubicalCylinderSDF(const float4 position,
                                  const float  radius, const float halfHeight, const float halfTrength)
{
    return extrude(calculateCircleSDF(position.xz, radius), position.yw, float2(halfHeight, halfTrength));
}

float calculateSphericalCylinderSDF(const float4 position, const float radius, const float halfTrength)
{
    return extrude(calculateSphereSDF(position.xyz, radius), position.w, halfTrength);
}