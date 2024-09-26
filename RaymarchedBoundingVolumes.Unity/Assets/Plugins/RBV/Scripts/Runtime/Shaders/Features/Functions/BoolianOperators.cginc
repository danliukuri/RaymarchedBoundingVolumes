float unionSDF(const float distance1, const float distance2)
{
    return min(distance1, distance2);
}

float subtractSDF(const float distance1, const float distance2)
{
    return -unionSDF(distance1, -distance2);
}

float intersectSDF(const float distance1, const float distance2)
{
    return -unionSDF(-distance1, -distance2);
}

float xorSDF(const float distance1, const float distance2)
{
    return subtractSDF(intersectSDF(distance1, distance2), unionSDF(distance1, distance2));
}


float smoothUnionSDF(const float distance1, const float distance2, const float radius)
{
    const float weight = clamp(0.5 + 0.5 * (distance2 - distance1) / radius, 0.0, 1.0);
    return lerp(distance2, distance1, weight) - radius * weight * (1.0 - weight);
}

float smoothSubtractSDF(const float distance1, const float distance2, const float radius)
{
    return -smoothUnionSDF(distance1, -distance2, radius);
}

float smoothIntersectSDF(const float distance1, const float distance2, const float radius)
{
    return -smoothUnionSDF(-distance1, -distance2, radius);
}

float smoothXorSDF(const float distance1, const float distance2, const float outerRadius, const float innerRadius)
{
    float smoothUnion     = smoothUnionSDF(distance1, distance2, outerRadius);
    float smoothIntersect = smoothIntersectSDF(distance1, distance2, innerRadius);
    return subtractSDF(smoothIntersect, smoothUnion);
}


float chamferUnionSDF(float distance1, float distance2, float radius)
{
    return unionSDF(unionSDF(distance1, distance2), (distance1 + distance2 - radius) * sqrt(0.5));
}