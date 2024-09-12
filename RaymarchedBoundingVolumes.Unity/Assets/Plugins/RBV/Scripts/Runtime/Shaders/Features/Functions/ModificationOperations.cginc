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


float extrude(const float sdf, const float positionOnAxis, const float distanceAlongAxis)
{
    float2 distance        = float2(sdf, abs(positionOnAxis) - distanceAlongAxis);
    float  outsideDistance = length(max(distance, 0.0));
    float  insideDistance  = min(max(distance.x, distance.y), 0.0);
    return outsideDistance + insideDistance;
}

float extrude(const float sdf, const float2 positionOnAxes, const float2 distanceAlongAxes)
{
    float3 distance        = float3(sdf, abs(positionOnAxes) - distanceAlongAxes);
    float  outsideDistance = length(max(distance, 0.0));
    float  insideDistance  = min(max(distance.x, max(distance.y, distance.z)), 0.0);
    return outsideDistance + insideDistance;
}

float extrude(const float sdf, const float3 positionOnAxes, const float3 distanceAlongAxes)
{
    float4 distance        = float4(sdf, abs(positionOnAxes) - distanceAlongAxes);
    float  outsideDistance = length(max(distance, 0.0));
    float  insideDistance  = min(max(distance.x, max(distance.y, max(distance.z, distance.w))), 0.0);
    return outsideDistance + insideDistance;
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