#pragma once

#include "../../../../../../RBV/Scripts/Runtime/Shaders/Features/Functions/ModificationOperations.cginc"

float4 elongateX(float4 position, const float strength)
{
    position.x -= calculateElongationDisplacement(position.x, strength);
    return position;
}

float4 elongateY(float4 position, const float strength)
{
    position.y -= calculateElongationDisplacement(position.y, strength);
    return position;
}

float4 elongateZ(float4 position, const float strength)
{
    position.z -= calculateElongationDisplacement(position.z, strength);
    return position;
}

float4 elongateW(float4 position, const float strength)
{
    position.w -= calculateElongationDisplacement(position.w, strength);
    return position;
}


float extrudeOrigin(const float4 positionOnAxes, const float4 distanceAlongAxes)
{
    return cartesianProduct(calculateFourLinesSDF(positionOnAxes, distanceAlongAxes));
}

float extrude(const float sdf, const float4 positionOnAxes, const float4 distanceAlongAxes)
{
    return cartesianProduct(sdf, calculateFourLinesSDF(positionOnAxes, distanceAlongAxes));
}


float2 revolutionizeX(const float4 position, const float horizontalOffset = 0.0, const float verticalOffset = 0.0)
{
    return float2(length(position.yzw) - horizontalOffset, position.x - verticalOffset);
}

float2 revolutionizeY(const float4 position, const float horizontalOffset = 0.0, const float verticalOffset = 0.0)
{
    return float2(length(position.xzw) - horizontalOffset, position.y - verticalOffset);
}

float2 revolutionizeZ(const float4 position, const float horizontalOffset = 0.0, const float verticalOffset = 0.0)
{
    return float2(length(position.xyw) - horizontalOffset, position.z - verticalOffset);
}

float2 revolutionizeW(const float4 position, const float horizontalOffset = 0.0, const float verticalOffset = 0.0)
{
    return float2(length(position.xyz) - horizontalOffset, position.w - verticalOffset);
}