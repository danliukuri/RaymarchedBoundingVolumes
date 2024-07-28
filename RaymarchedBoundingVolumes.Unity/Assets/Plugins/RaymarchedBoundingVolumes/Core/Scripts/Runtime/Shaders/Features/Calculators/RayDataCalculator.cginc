#pragma once
#include <UnityShaderVariables.cginc>

float3 calculateRayDirection(const float3 hitPosition)
{
    const float3 rayOrigin = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos, 1));
    const float3 rayDirection = normalize(hitPosition - rayOrigin);
    return rayDirection;
}
