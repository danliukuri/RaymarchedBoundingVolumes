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

float modifyPositionAxis(inout float axisPosition, const float axisSize)
{
    const float halfSize = axisSize * 0.5;
    const float cellIndex = floor((axisPosition + halfSize) / axisSize);
    axisPosition = fmod(axisPosition + halfSize, axisSize) - halfSize;
    axisPosition = fmod(-axisPosition + halfSize, axisSize) - halfSize;
    return cellIndex;
}