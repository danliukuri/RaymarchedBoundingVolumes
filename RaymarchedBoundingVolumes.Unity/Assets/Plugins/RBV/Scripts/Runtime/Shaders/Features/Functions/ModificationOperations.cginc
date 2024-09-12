#pragma once

#include "1DSDFs.cginc"
#include "2DSDFs.cginc"
#include "MathExtentions.cginc"

float calculateElongationDisplacement(float position, const float strength)
{
    float safeStrength = abs(strength);
    return clamp(position, -safeStrength, safeStrength);
}

float3 elongateX(float3 position, const float strength)
{
    position.x -= calculateElongationDisplacement(position.x, strength);
    return position;
}

float3 elongateY(float3 position, const float strength)
{
    position.y -= calculateElongationDisplacement(position.y, strength);
    return position;
}

float3 elongateZ(float3 position, const float strength)
{
    position.z -= calculateElongationDisplacement(position.z, strength);
    return position;
}


float extrudeOrigin(const float2 positionOnAxes, const float3 distanceAlongAxes)
{
    float2 distance        = float2(abs(positionOnAxes) - distanceAlongAxes);
    float  outsideDistance = length(max(distance, 0.0));
    float  insideDistance  = min(maxAxisOf2(distance), 0.0);
    return outsideDistance + insideDistance;
}

float extrudeOrigin(const float3 positionOnAxes, const float3 distanceAlongAxes)
{
    float3 distance        = float3(abs(positionOnAxes) - distanceAlongAxes);
    float  outsideDistance = length(max(distance, 0.0));
    float  insideDistance  = min(maxAxisOf3(distance), 0.0);
    return outsideDistance + insideDistance;
}

float extrude(const float sdf, const float positionOnAxis, const float distanceAlongAxis)
{
    float2 distance        = float2(sdf, calculateLineSDF(positionOnAxis, distanceAlongAxis));
    float  outsideDistance = length(max(distance, 0.0));
    float  insideDistance  = min(maxAxisOf2(distance), 0.0);
    return outsideDistance + insideDistance;
}

float extrude(const float sdf, const float2 positionOnAxes, const float2 distanceAlongAxes)
{
    float3 distance = float3(sdf,
                             calculateLineSDF(positionOnAxes.x, distanceAlongAxes.y),
                             calculateLineSDF(positionOnAxes.y, distanceAlongAxes.y));
    float outsideDistance = length(max(distance, 0.0));
    float insideDistance  = min(maxAxisOf3(distance), 0.0);
    return outsideDistance + insideDistance;
}

float extrude(const float sdf, const float3 positionOnAxes, const float3 distanceAlongAxes)
{
    float4 distance = float4(sdf,
                             calculateLineSDF(positionOnAxes.x, distanceAlongAxes.y),
                             calculateLineSDF(positionOnAxes.y, distanceAlongAxes.y),
                             calculateLineSDF(positionOnAxes.z, distanceAlongAxes.z));
    float outsideDistance = length(max(distance, 0.0));
    float insideDistance  = min(maxAxisOf4(distance), 0.0);
    return outsideDistance + insideDistance;
}


float2 revolutionizeX(const float3 position, const float horizontalOffset = 0.0, const float verticalOffset = 0.0)
{
    return float2(calculateCircleSDF(position.yz, horizontalOffset), position.x - verticalOffset);
}

float2 revolutionizeY(const float3 position, const float horizontalOffset = 0.0, const float verticalOffset = 0.0)
{
    return float2(calculateCircleSDF(position.xz, horizontalOffset), position.y - verticalOffset);
}

float2 revolutionizeZ(const float3 position, const float horizontalOffset = 0.0, const float verticalOffset = 0.0)
{
    return float2(calculateCircleSDF(position.xy, horizontalOffset), position.z - verticalOffset);
}