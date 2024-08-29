#pragma once
#include <UnityShaderUtilities.cginc>

float calculateDepth(const float3 position)
{
    float4 clipPos = UnityObjectToClipPos(float4(position, 1));
    return clipPos.z / clipPos.w;
}
