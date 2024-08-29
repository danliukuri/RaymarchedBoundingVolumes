float calculateSphereSDF(const float3 position, const float radius)
{
    return length(position) - radius;
}

float calculateCubeSDF(const float3 position, const float3 halfDimensions)
{
    float3 offsetVector = abs(position) - halfDimensions;
    return min(max(offsetVector.x, max(offsetVector.y, offsetVector.z)), 0.0) + length(max(offsetVector, 0.0));
}