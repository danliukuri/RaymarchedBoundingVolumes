#pragma once

float3 rotate3DAroundX(float3 position, float angle)
{
    position.yz = mul(position.yz, float2x2(cos(angle), -sin(angle), +sin(angle), cos(angle)));
    return position;
}

float3 rotate3DAroundY(float3 position, float angle)
{
    position.xz = mul(position.xz, float2x2(cos(angle), +sin(angle), -sin(angle), cos(angle)));
    return position;
}

float3 rotate3DAroundZ(float3 position, float angle)
{
    position.xy = mul(position.xy, float2x2(cos(angle), -sin(angle), +sin(angle), cos(angle)));
    return position;
}

/**
 * Applies a 3D rotation to a position vector using Euler angles.
 * Rotations are applied around the X, Y, and Z axes in sequence.
 * 
 * For more information on rotation matrices and 3D rotations, visit:
 * <a href="https://en.wikipedia.org/wiki/Rotation_matrix#General_3D_rotations">Rotation Matrix on Wikipedia</a>
 *
 * @param position The 3D position vector to be rotated.
 * @param rotation The Euler angles (in radians) representing rotations around the X, Y, and Z axes.
 * @return The rotated 3D position vector.
 */
float3 rotate3D(float3 position, float3 rotation)
{
    position = rotate3DAroundX(position, rotation.x);
    position = rotate3DAroundY(position, rotation.y);
    position = rotate3DAroundZ(position, rotation.z);
    return position;
}