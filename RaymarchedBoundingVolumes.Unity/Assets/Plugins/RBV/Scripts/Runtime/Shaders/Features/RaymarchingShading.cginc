#include "Calculators/SDFCalculator.cginc"

float3 calculateNormal(const float3 position)
{
    const float2 epsilon = float2(0.001, 0);
    const float3 normal  = calculateSDF(position).distance - float3(calculateSDF(position - epsilon.xyy).distance,
                                                                   calculateSDF(position - epsilon.yxy).distance,
                                                                   calculateSDF(position - epsilon.yyx).distance);
    return normalize(normal);
}

float calculateShadows(float3 rayOrigin, float3 rayDirection)
{
    static float noShadows   = 1.0f;
    static float fullShadows = 0.0f;

#ifdef SHADOWS_TYPE_NONE
    return noShadows;
#endif

    float penumbrae      = noShadows;
    float travelDistance = _ShadowsMinDistance;

    UNITY_LOOP
    for (int i = 0; i < _ShadowsMaxDetectionIterations && travelDistance < _ShadowsMaxDistance; i++)
    {
        float surfaceDistance = calculateSDF(rayOrigin + rayDirection * travelDistance).distance;

        UNITY_BRANCH
        if (surfaceDistance < _ShadowsMaxDetectionOffset)
            return lerp(noShadows, fullShadows, _ShadowsIntensity);

#ifdef SHADOWS_TYPE_SOFT
        float currentPenumbra = _ShadowsPenumbraSize * surfaceDistance / travelDistance;
        penumbrae             = unionSDF(penumbrae, lerp(noShadows, currentPenumbra, _ShadowsIntensity));
#endif
        travelDistance += surfaceDistance;
    }

    return penumbrae;
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