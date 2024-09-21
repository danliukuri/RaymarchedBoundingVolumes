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
float cartesianProduct(const float distance1, const float4 distances)
{
    float outsideDistance = length(max(max(distance1, 0.0), max(distances, 0.0)));
    float insideDistance  = min(max(distance1, maxAxisOf4(distances)), 0.0);
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

/** @related _CartesianProductDefinition */
float cartesianProduct(const float distance1, const float distance2, const float distance3, const float distance4,
                       const float distance5)
{
    return cartesianProduct(distance1, float4(distance2, distance3, distance4, distance5));
}


float extrudeOrigin(const float2 positionOnAxes, const float2 distanceAlongAxes)
{
    return cartesianProduct(calculateTwoLinesSDF(positionOnAxes, distanceAlongAxes));
}

float extrudeOrigin(const float3 positionOnAxes, const float3 distanceAlongAxes)
{
    return cartesianProduct(calculateThreeLinesSDF(positionOnAxes, distanceAlongAxes));
}

float extrude(const float sdf, const float positionOnAxis, const float distanceAlongAxis)
{
    return cartesianProduct(sdf, calculateLineSDF(positionOnAxis, distanceAlongAxis));
}

float extrude(const float sdf, const float2 positionOnAxes, const float2 distanceAlongAxes)
{
    return cartesianProduct(float3(sdf, calculateTwoLinesSDF(positionOnAxes, distanceAlongAxes)));
}

float extrude(const float sdf, const float3 positionOnAxes, const float3 distanceAlongAxes)
{
    return cartesianProduct(float4(sdf, calculateThreeLinesSDF(positionOnAxes, distanceAlongAxes)));
}


float2 revolve(const float sdfOfRevolving, const float positionOnAxis, const float axialOffset = 0.0)
{
    return float2(sdfOfRevolving, positionOnAxis + axialOffset);
}

float3 doubleRevolve(const float sdfOfRevolving, const float2 positionOnAxes, const float2 axialOffsets = 0.0)
{
    return float3(sdfOfRevolving, positionOnAxes + axialOffsets);
}

float2 revolveInCircle(const float2 position, const float positionOnAxis,
                       const float  radius      = 0.0,
                       const float  axialOffset = 0.0)
{
    return revolve(calculateCircleSDF(position, radius), positionOnAxis, axialOffset);
}

float2 revolveInCircleByX(const float3 position, const float radius = 0.0, const float axialOffset = 0.0)
{
    return revolveInCircle(position.yz, position.x, radius, axialOffset);
}

float2 revolveInCircleByY(const float3 position, const float radius = 0.0, const float axialOffset = 0.0)
{
    return revolveInCircle(position.xz, position.y, radius, axialOffset);
}

float2 revolveInCircleByZ(const float3 position, const float radius = 0.0, const float axialOffset = 0.0)
{
    return revolveInCircle(position.xy, position.z, radius, axialOffset);
}


float addRoundness(const float sdf, const float radius)
{
    return sdf - radius;
}