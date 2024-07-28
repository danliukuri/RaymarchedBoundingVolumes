float calculateSphereSDF(const float3 position, const float radius)
{
    return length(position) - radius;
}

// (Infinite) Plane
// normal.xyz: normal of the plane (normalized).
// normal.w: offset
float calculatePlaneSDF(const float3 position, float4 normal)
{
    return dot(position, normal.xyz) + normal.w;
}

float calculateBoxSDF(const float3 position, const float3 size)
{
    float3 offsetVector = abs(position) - size;
    return min(max(offsetVector.x, max(offsetVector.y, offsetVector.z)), 0.0) + length(max(offsetVector, 0.0));
}

float calculateRoundBoxSDF(const in float3 position, const in float3 boxDimensions, const in float radius)
{
    float3 offsetVector = abs(position) - boxDimensions;
    return min(max(offsetVector.x, max(offsetVector.y, offsetVector.z)), 0.0) + length(max(offsetVector, 0.0)) - radius;
}