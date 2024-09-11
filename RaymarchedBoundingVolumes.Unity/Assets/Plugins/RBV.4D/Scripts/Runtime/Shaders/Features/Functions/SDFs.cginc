#pragma once

#include "ModificationOperations.cginc"

float calculateHypercubeSDF(const float4 position, const float4 halfDimensions)
{
    float4 distance        = abs(position) - halfDimensions;
    float  outsideDistance = length(max(distance, 0.0));
    float  insideDistance  = min(max(distance.x, max(distance.y, max(distance.z, distance.w))), 0.0);
    return outsideDistance + insideDistance;
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