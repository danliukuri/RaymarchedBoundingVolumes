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

/** @related _CartesianProductDefinition */
float cartesianProduct(const float2 distances)
{
    float outsideDistance = length(max(distances, 0.0));
    float insideDistance  = min(maxAxisOf2(distances), 0.0);
    return outsideDistance + insideDistance;
}

/** @related _CartesianProductDefinition */
float cartesianProduct(const float3 distances)
{
    float outsideDistance = length(max(distances, 0.0));
    float insideDistance  = min(maxAxisOf3(distances), 0.0);
    return outsideDistance + insideDistance;
}

/** @related _CartesianProductDefinition */
float cartesianProduct(const float4 distances)
{
    float outsideDistance = length(max(distances, 0.0));
    float insideDistance  = min(maxAxisOf4(distances), 0.0);
    return outsideDistance + insideDistance;
}

/** @related _CartesianProductDefinition */
float cartesianProduct(const float distance1, const float distance2)
{
    return cartesianProduct(float2(distance1, distance2));
}

/** @related _CartesianProductDefinition */
float cartesianProduct(const float distance1, const float distance2, const float distance3)
{
    return cartesianProduct(float3(distance1, distance2, distance3));
}

/** @related _CartesianProductDefinition */
float cartesianProduct(const float distance1, const float distance2, const float distance3, const float distance4)
{
    return cartesianProduct(float4(distance1, distance2, distance3, distance4));
}


float extrudeOrigin(const float2 positionOnAxes, const float2 distanceAlongAxes)
{
    return cartesianProduct(abs(positionOnAxes) - distanceAlongAxes);
}

float extrudeOrigin(const float3 positionOnAxes, const float3 distanceAlongAxes)
{
    return cartesianProduct(abs(positionOnAxes) - distanceAlongAxes);
}

float extrude(const float sdf, const float positionOnAxis, const float distanceAlongAxis)
{
    return cartesianProduct(sdf, calculateLineSDF(positionOnAxis, distanceAlongAxis));
}

float extrude(const float sdf, const float2 positionOnAxes, const float2 distanceAlongAxes)
{
    return cartesianProduct(sdf,
                            calculateLineSDF(positionOnAxes.x, distanceAlongAxes.y),
                            calculateLineSDF(positionOnAxes.y, distanceAlongAxes.y));
}

float extrude(const float sdf, const float3 positionOnAxes, const float3 distanceAlongAxes)
{
    return cartesianProduct(sdf,
                            calculateLineSDF(positionOnAxes.x, distanceAlongAxes.y),
                            calculateLineSDF(positionOnAxes.y, distanceAlongAxes.y),
                            calculateLineSDF(positionOnAxes.z, distanceAlongAxes.z));
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


float addRoundness(const float sdf, const float radius)
{
    return sdf - radius;
}