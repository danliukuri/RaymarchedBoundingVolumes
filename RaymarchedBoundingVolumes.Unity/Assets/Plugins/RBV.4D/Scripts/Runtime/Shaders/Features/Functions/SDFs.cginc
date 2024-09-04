float calculateHypersphereSDF(const float4 position, const float radius)
{
    return length(position) - radius;
}

float calculateHypercubeSDF(const float4 position, const float4 halfDimensions)
{
    float4 distance        = abs(position) - halfDimensions;
    float  outsideDistance = length(max(distance, 0.0));
    float  insideDistance  = min(max(distance.x, max(distance.y, max(distance.z, distance.w))), 0.0);
    return outsideDistance + insideDistance;
}