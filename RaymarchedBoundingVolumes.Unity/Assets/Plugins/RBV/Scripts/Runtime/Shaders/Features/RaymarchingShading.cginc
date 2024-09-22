#include "Calculators/SDFCalculator.cginc"
#include "Calculators/ShadowsCalculator.cginc"

float3 calculateNormal(const float3 position)
{
    const float2 epsilon = float2(0.001, 0);
    const float3 normal  = calculateSDF(position).distance - float3(calculateSDF(position - epsilon.xyy).distance,
                                                                   calculateSDF(position - epsilon.yxy).distance,
                                                                   calculateSDF(position - epsilon.yyx).distance);
    return normalize(normal);
}

float3 applyShading(const float3 position)
{
    const float3 normal = calculateNormal(position);
    // Directional light // Lambertian shading // Diffuse
    const float3 directLight = _LightColor0 * max(dot(_WorldSpaceLightPos0.xyz, normal), 0);
    // Ambient light // Environmental lighting
    const float3 ambientLight = ShadeSH9(float4(normal, 1));
    // Shadows
    const float shadow = calculateShadows(position, _WorldSpaceLightPos0.xyz);

    // // Ambient occlusion
    // float ambientOcclusion = applyAmbientOcclusion(position, normal);

    return directLight * shadow + ambientLight; // * ambientOcclusion;
}

fixed4 calculateShadedPixelColor(const RaymarchingData data)
{
    return fixed4(data.color * applyShading(data.position), 1);
}