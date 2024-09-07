#pragma once

float calculateCircleSDF(const float2 position, const float radius)
{
    return length(position) - radius;
}

float calculateEllipseSDF(const float2 position, const float2 radii)
{
    float2 normalizedPosition = position / radii;
    float  normalizedDistance = length(normalizedPosition);
    float  outsideDistance    = normalizedDistance - 1.0;
    float  gradientLength     = length(normalizedPosition / radii);
    return normalizedDistance * outsideDistance / gradientLength;
}