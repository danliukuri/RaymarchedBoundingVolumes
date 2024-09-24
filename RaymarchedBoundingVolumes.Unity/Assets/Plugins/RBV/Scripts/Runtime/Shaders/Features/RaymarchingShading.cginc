#pragma once

#include "../Data/Variables/RaymarchingGlobalVariables.cginc"
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

float calculateAmbientOcclusion(float3 pos, float3 normal)
{
#ifdef AMBIENT_OCCLUSION_ON
    float ambientOcclusion = _FullShading;

    UNITY_LOOP
    for (int i = 1; i <= _AOMaxDetectionIterations; i++)
    {
        float dist = _AOStepSize * i;
        ambientOcclusion += max(0.0, (dist - calculateSDF(pos + normal * dist).distance) / dist);
    }
    return lerp(_NoShading, _FullShading, saturate(ambientOcclusion) * _AOIntensity);
#else
    return _NoShading;
#endif
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
    // Ambient occlusion
    float ambientOcclusion = calculateAmbientOcclusion(position, normal);

    return directLight * shadow * ambientOcclusion + ambientLight;
}

fixed4 calculateShadedPixelColor(const RaymarchingData data)
{
    return fixed4(data.color * applyShading(data.position), 1);
}