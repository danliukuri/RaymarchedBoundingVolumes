#pragma once

#include "UnityCG.cginc"

#include "1DSDFs.cginc"
#include "MathExtentions.cginc"

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

float calculateIsoscelesTriangleSDF(const float2 position, const float halfWidth, const float halfHeight)
{
    float2 adjustedPosition = float2(calculateLineSDF(position.x, halfWidth), position.y + halfHeight);

    float2 edgeVector       = float2(-halfWidth, halfHeight * 2.0);
    float  projectionFactor = clamp(dot(adjustedPosition, edgeVector) / dot2(edgeVector), 0.0, 1.0);
    float2 projectedPoint   = adjustedPosition - edgeVector * projectionFactor;
    float2 distanceToAxis   = float2(max(adjustedPosition.x, 0.0), -adjustedPosition.y);

    float distanceToEdge = min(dot2(projectedPoint), dot2(distanceToAxis));
    float signAdjustment = sign(max(maxAxisOf2(projectedPoint), distanceToAxis.y));

    return sqrt(distanceToEdge) * signAdjustment;
}

float calculateIsoscelesTrapezoidSDF(const float2 position,
                                     const float  bottomBaseHalfWidth, const float topBaseHalfWidth,
                                     const float  halfHeight)
{
    float absolutePositionX = abs(position.x);
    float bottomBaseX       = absolutePositionX - bottomBaseHalfWidth;
    float topBaseX          = absolutePositionX - topBaseHalfWidth;

    float2 bottomBase = float2(max(bottomBaseX, 0.0), -position.y - halfHeight);
    float2 topBase    = float2(max(topBaseX, 0.0), position.y - halfHeight);

    float2 baseProjectionVector = float2(bottomBaseX, position.y + halfHeight);

    float2 edgeVector       = float2(topBaseHalfWidth - bottomBaseHalfWidth, halfHeight * 2.0);
    float  projectionFactor = clamp(dot(baseProjectionVector, edgeVector) / dot2(edgeVector), 0.0, 1.0);
    float2 distanceToEdge   = baseProjectionVector - edgeVector * projectionFactor;

    float closestDistance = sqrt(min(min(dot2(bottomBase), dot2(topBase)), dot2(distanceToEdge)));
    float signAdjustment  = sign(max(max(bottomBase.y, topBase.y), distanceToEdge.x));
    return closestDistance * signAdjustment;
}