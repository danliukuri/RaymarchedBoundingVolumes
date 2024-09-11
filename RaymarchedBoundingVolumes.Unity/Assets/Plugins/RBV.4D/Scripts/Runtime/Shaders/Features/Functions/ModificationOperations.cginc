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


float extrudeX(const float4 position, const float sdf, const float height)
{
    return extrude(sdf, abs(position.x) - height);
}

float extrudeY(const float4 position, const float sdf, const float height)
{
    return extrude(sdf, abs(position.y) - height);
}

float extrudeZ(const float4 position, const float sdf, const float height)
{
    return extrude(sdf, abs(position.z) - height);
}

float extrudeW(const float4 position, const float sdf, const float height)
{
    return extrude(sdf, abs(position.w) - height);
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