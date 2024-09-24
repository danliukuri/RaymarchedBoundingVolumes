#pragma once

#include <HLSLSupport.cginc>

#include "../../Data/Variables/RaymarchingGlobalVariables.cginc"

#ifdef SHADOWS_TYPE_HARD
float calculateHardShadows(float3 rayOrigin, float3 rayDirection)
{
    float travelDistance = _ShadowsMinDistance;

    UNITY_LOOP
    for (int i = 0; i < _ShadowsMaxDetectionIterations && travelDistance < _ShadowsMaxDistance; i++)
    {
        float surfaceDistance = calculateSDF(rayOrigin + rayDirection * travelDistance).distance;

        UNITY_BRANCH
        if (surfaceDistance < _ShadowsMaxDetectionOffset)
            return _FullShading;

        travelDistance += surfaceDistance;
    }

    return _NoShading;
}
#elif SHADOWS_TYPE_SOFT_A
/**
 * This type of soft shadows is fastest but may generate artifacts.
 * Sharp corners in the objects casting the shadow often lead to missing penumbras.
 */
float calculateSoftShadowsA(float3 rayOrigin, float3 rayDirection)
{
    float shadows        = _NoShading;
    float travelDistance = _ShadowsMinDistance;

    UNITY_LOOP
    for (int i = 0; i < _ShadowsMaxDetectionIterations && travelDistance < _ShadowsMaxDistance; i++)
    {
        float surfaceDistance = calculateSDF(rayOrigin + rayDirection * travelDistance).distance;

        UNITY_BRANCH
        if (surfaceDistance < _ShadowsMaxDetectionOffset)
            return _FullShading;

        float currentShadows = surfaceDistance / (_ShadowsPenumbraSize * travelDistance);
        shadows              = unionSDF(shadows, currentShadows);

        travelDistance += surfaceDistance;
    }

    return shadows;
}
#elif SHADOWS_TYPE_SOFT_B
/**
 * This type of soft shadows is slower than type A but creates better results in challenging situations.
 * It effectively handles sharp corners on shadow casters.
 */
float calculateSoftShadowsB(float3 rayOrigin, float3 rayDirection)
{
    static float previousDistance = 1e20;
    
    float shadows        = _NoShading;
    float travelDistance = _ShadowsMinDistance;

    UNITY_LOOP
    for (int i = 0; i < _ShadowsMaxDetectionIterations && travelDistance < _ShadowsMaxDistance; i++)
    {
        float surfaceDistance = calculateSDF(rayOrigin + rayDirection * travelDistance).distance;

        UNITY_BRANCH
        if (surfaceDistance < _ShadowsMaxDetectionOffset)
            return _FullShading;

        float distanceAlongRay      = surfaceDistance * surfaceDistance / (2.0 * previousDistance);
        previousDistance            = surfaceDistance;
        float perpendicularDistance = sqrt(surfaceDistance * surfaceDistance - distanceAlongRay * distanceAlongRay);
        float distanceFactor        = max(0.0, travelDistance - distanceAlongRay);
        float currentShadows        = perpendicularDistance / (_ShadowsPenumbraSize * distanceFactor);
        shadows                     = unionSDF(shadows, currentShadows);

        travelDistance += surfaceDistance;
    }

    return shadows;
}
#elif SHADOWS_TYPE_SOFT_C
/**
 * This type of soft shadows is the slowest but delivers results closest to physically accurate shadows.
 * Like type B, it also excels in challenging cases with sharp corners on shadow casters.
 */
float calculateSoftShadowsC(float3 rayOrigin, float3 rayDirection)
{
    static float absoluteShadows = -1.0f;

    float shadows        = _NoShading;
    float travelDistance = _ShadowsMinDistance;

    UNITY_LOOP
    for (int i = 0; i < _ShadowsMaxDetectionIterations && travelDistance < _ShadowsMaxDistance; i++)
    {
        float surfaceDistance = calculateSDF(rayOrigin + rayDirection * travelDistance).distance;

        shadows = unionSDF(shadows, surfaceDistance / (_ShadowsPenumbraSize * travelDistance));
        travelDistance += clamp(surfaceDistance, _ShadowsMaxDetectionOffset, _NoShading);

        UNITY_BRANCH
        if (shadows < absoluteShadows)
            break;
    }

    return smoothstep(absoluteShadows, _NoShading, shadows);
}
#endif

/**
 * For more information, visit:\n
 * <a href="https://iquilezles.org/articles/rmshadows">Inigo Quilez - Soft Shadows in Raymarched SDFs</a>
 */
float calculateShadows(float3 rayOrigin, float3 rayDirection)
{
    float shadows = _NoShading;
#ifdef SHADOWS_TYPE_NONE
    return _NoShading;
#elif SHADOWS_TYPE_HARD
    shadows = calculateHardShadows(rayOrigin, rayDirection); 
#elif SHADOWS_TYPE_SOFT_A
    shadows = calculateSoftShadowsA(rayOrigin, rayDirection);
#elif SHADOWS_TYPE_SOFT_B
    shadows = calculateSoftShadowsB(rayOrigin, rayDirection);
#elif SHADOWS_TYPE_SOFT_C
    shadows = calculateSoftShadowsC(rayOrigin, rayDirection);
#endif
    return lerp(_NoShading, shadows, _ShadowsIntensity);
}