#include "../../Data/Structures/RaymarchingDataStructures.cginc"

SDFData blendSDF(const SDFData sdf1, const SDFData sdf2, const float blendFactor)
{
    const float blendWeight = clamp(0.5 + 0.5 * (sdf2.distance - sdf1.distance) / blendFactor, 0, 1);
    SDFData sdf;
    sdf.color    = lerp(sdf2.color, sdf1.color, blendWeight);
    sdf.distance = lerp(sdf2.distance, sdf1.distance, blendWeight) - blendFactor * blendWeight * (1 - blendWeight);
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