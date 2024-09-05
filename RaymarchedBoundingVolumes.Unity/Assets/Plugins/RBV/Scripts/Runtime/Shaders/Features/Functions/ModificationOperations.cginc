#pragma once

float calculateElongationDisplacement(float3 position, const float strength)
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

float3 elongateSDF(float3 position, const float3 strength)
{
    position.x = elongateX(position, strength.x);
    position.y = elongateY(position, strength.y);
    position.z = elongateZ(position, strength.z);
    return position;
}