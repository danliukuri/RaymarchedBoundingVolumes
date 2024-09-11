#pragma once

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


float extrude(const float sdf, const float verticalDistance)
{
    float2 distance        = float2(sdf, verticalDistance);
    float  outsideDistance = length(max(distance, 0.0));
    float  insideDistance  = min(max(distance.x, distance.y), 0.0);
    return outsideDistance + insideDistance;
}

float extrudeX(const float3 position, const float sdf, const float height)
{
    return extrude(sdf, abs(position.x) - height);
}

float extrudeY(const float3 position, const float sdf, const float height)
{
    return extrude(sdf, abs(position.y) - height);
}

float extrudeZ(const float3 position, const float sdf, const float height)
{
    return extrude(sdf, abs(position.z) - height);
}


float2 revolutionizeX(const float3 position, const float horizontalOffset = 0.0, const float verticalOffset = 0.0)
{
    return float2(length(position.yz) - horizontalOffset, position.x - verticalOffset);
}

float2 revolutionizeY(const float3 position, const float horizontalOffset = 0.0, const float verticalOffset = 0.0)
{
    return float2(length(position.xz) - horizontalOffset, position.y - verticalOffset);
}

float2 revolutionizeZ(const float3 position, const float horizontalOffset = 0.0, const float verticalOffset = 0.0)
{
    return float2(length(position.xy) - horizontalOffset, position.z - verticalOffset);
}