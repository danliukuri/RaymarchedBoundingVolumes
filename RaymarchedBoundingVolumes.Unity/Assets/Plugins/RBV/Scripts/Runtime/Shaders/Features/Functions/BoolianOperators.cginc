float unionSDF(const float distance1, const float distance2)
{
    return min(distance1, distance2);
}

float subtractSDF(const float distance1, const float distance2)
{
    return max(-distance1, distance2);
}

float intersectSDF(const float distance1, const float distance2)
{
    return max(distance1, distance2);
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
    const float weight = clamp(0.5 - 0.5 * (distance2 + distance1) / radius, 0.0, 1.0);
    return lerp(distance2, -distance1, weight) + radius * weight * (1.0 - weight);
}

float smoothIntersectSDF(const float distance1, const float distance2, const float radius)
{
    const float weight = clamp(0.5 - 0.5 * (distance2 - distance1) / radius, 0.0, 1.0);
    return lerp(distance2, distance1, weight) + radius * weight * (1.0 - weight);
}