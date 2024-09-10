#pragma once

#include "UnityCG.cginc"

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

float calculateRegularPolygonSDF(float2 position, float verticesCount, float circumradius)
{
    float segmentAngle     = UNITY_TWO_PI / verticesCount;
    float halfSegmentAngle = segmentAngle * 0.5;

    float angleRadians    = atan2(position.x, position.y);
    float angleMod        = fmod(abs(angleRadians), segmentAngle);
    float angleDifference = angleMod - halfSegmentAngle;

    float inscribedRadius = circumradius * cos(halfSegmentAngle);
    float radialDistance  = length(position);

    float xProjected = sin(angleDifference) * radialDistance;
    float yProjected = cos(angleDifference) * radialDistance - inscribedRadius;

    float insideDistance  = min(yProjected, 0.0);
    float cornerDistance  = circumradius * sin(halfSegmentAngle);
    float outsideDistance =
        length(float2(max(abs(xProjected) - cornerDistance, 0.0), yProjected)) * step(0.0, yProjected);

    return outsideDistance + insideDistance;
}