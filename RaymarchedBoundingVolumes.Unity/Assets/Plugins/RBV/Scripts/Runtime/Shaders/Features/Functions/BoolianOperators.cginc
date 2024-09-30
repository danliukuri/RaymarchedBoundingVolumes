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


float chamferUnionSDF(const float distance1, const float distance2, const float radius)
{
    return unionSDF(unionSDF(distance1, distance2), (distance1 + distance2 - radius) * sqrt(0.5));
}

float chamferSubtractSDF(const float distance1, const float distance2, const float radius)
{
    return intersectSDF(subtractSDF(distance1, distance2), (-distance1 + distance2 + radius) * sqrt(0.5));
}

float chamferIntersectSDF(const float distance1, const float distance2, const float radius)
{
    return intersectSDF(intersectSDF(distance1, distance2), (distance1 + distance2 + radius) * 0.5);
}

float chamferXorSDF(const float distance1, const float distance2, const float outerRadius, const float innerRadius)
{
    float smoothUnion     = chamferUnionSDF(distance1, distance2, outerRadius);
    float smoothIntersect = chamferIntersectSDF(distance1, distance2, innerRadius);
    return subtractSDF(smoothIntersect, smoothUnion);
}


float stairsUnionSDF(const float distance1, const float distance2, const float radius, const float count)
{
    float stepSize           = radius / (count + 1);
    float roundedSdf2        = addRoundness(distance2, radius);
    float distanceDifference = roundedSdf2 - distance1 + stepSize;
    float modulatedValue     = fmod(abs(distanceDifference), 2 * stepSize);
    float stairsSdf          = 0.5 * (roundedSdf2 + distance1 + abs(modulatedValue - stepSize));

    return unionSDF(unionSDF(distance1, distance2), stairsSdf);
}

float stairsSubtractSDF(const float distance1, const float distance2, const float radius, const float count)
{
    return -stairsUnionSDF(distance1, -distance2, radius, count - 1);
}

float stairsIntersectSDF(const float distance1, const float distance2, const float radius, const float count)
{
    return -stairsUnionSDF(-distance1, -distance2, radius, count);
}

float stairsXorSDF(const float distance1, const float   distance2,
                   const float outerRadius, const float innerRadius,
                   const float outerCount, const float  innerCount)
{
    float smoothUnion     = stairsUnionSDF(distance1, distance2, outerRadius, outerCount);
    float smoothIntersect = stairsIntersectSDF(distance1, distance2, innerRadius, innerCount);
    return subtractSDF(smoothIntersect, smoothUnion);
}


float morphSDF(const float distance1, const float distance2, const float ratio)
{
    return lerp(distance2, distance1, ratio);
}