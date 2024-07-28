#include "../../Data/Structures/RaymarchingDataStructures.cginc"

SDFData blendSDF(const SDFData object1, const SDFData object2, const float blendFactor)
{
    const float blendWeight =
        clamp(0.5 + 0.5 * (object2.distanceToObject - object1.distanceToObject) / blendFactor, 0, 1);
    SDFData sdf;
    sdf.pixelColor = lerp(object2.pixelColor, object1.pixelColor, blendWeight);
    sdf.distanceToObject = lerp(object2.distanceToObject, object1.distanceToObject, blendWeight) -
        blendFactor * blendWeight * (1 - blendWeight);
    return sdf;
}

float smoothSubtractSDF(const float distance1, const float distance2, const float blendFactor)
{
    const float weight = clamp(0.5 - 0.5 * (distance2 + distance1) / blendFactor, 0.0, 1.0);
    return lerp(distance2, -distance1, weight) + blendFactor * weight * (1.0 - weight);
}

float smoothIntersectSDF(const float distance1, const float distance2, const float blendFactor)
{
    const float weight = clamp(0.5 - 0.5 * (distance2 - distance1) / blendFactor, 0.0, 1.0);
    return lerp(distance2, distance1, weight) + blendFactor * weight * (1.0 - weight);
}