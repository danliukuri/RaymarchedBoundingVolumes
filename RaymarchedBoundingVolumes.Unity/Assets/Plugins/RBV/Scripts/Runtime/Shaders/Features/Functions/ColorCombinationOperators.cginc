#include <HLSLSupport.cginc>

#include "../../Data/Structures/RaymarchingDataStructures.cginc"

fixed3 smoothUnionColor(const SDFData sdf1, const SDFData sdf2, const float radius)
{
    const float weight = clamp(0.5 + 0.5 * (sdf2.distance - sdf1.distance) / radius, 0.0, 1.0);
    return lerp(sdf2.color, sdf1.color, weight - radius * weight * (1.0 - weight));
}