float calculateSphereSDF(const float3 position, const float radius)
{
    return length(position) - radius;
}

float calculateCubeSDF(const float3 position, const float3 halfDimensions)
{
    float3 distance        = abs(position) - halfDimensions;
    float  outsideDistance = length(max(distance, 0.0));
    float  insideDistance  = min(max(distance.x, max(distance.y, distance.z)), 0.0);
    return outsideDistance + insideDistance;
}