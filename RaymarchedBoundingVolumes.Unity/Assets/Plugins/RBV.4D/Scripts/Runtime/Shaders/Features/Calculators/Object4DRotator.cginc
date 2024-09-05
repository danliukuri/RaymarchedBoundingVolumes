#pragma once

float4 rotateInXwPlane(float4 position, float angle)
{
    position.xw = mul(position.xw, float2x2(cos(angle), +sin(angle), -sin(angle), cos(angle)));
    return position;
}

float4 rotateInYwPlane(float4 position, float angle)
{
    position.yw = mul(position.yw, float2x2(cos(angle), -sin(angle), +sin(angle), cos(angle)));
    return position;
}

float4 rotateInZwPlane(float4 position, float angle)
{
    position.zw = mul(position.zw, float2x2(cos(angle), -sin(angle), +sin(angle), cos(angle)));
    return position;
}

/**
 * Applies a 4D rotation to a position vector using 4D Euler angles.
 * Rotations are applied in the XW, YW, and ZW planes in sequence.
 * 
 * For more information on rotation matrices and rotations, visit:
 * <a href="https://en.wikipedia.org/wiki/Rotation_matrix">Rotation Matrix on Wikipedia</a>
 *
 * @param position The 4D position vector to be rotated.
 * @param rotation The 4D Euler angles (in radians) representing rotations in the XW, YW, and ZW planes.
 * @return The rotated 4D position vector.
 */
float4 rotate4D(float4 position, float3 rotation)
{
    position = rotateInXwPlane(position, rotation.x);
    position = rotateInYwPlane(position, rotation.y);
    position = rotateInZwPlane(position, rotation.z);
    return position;
}