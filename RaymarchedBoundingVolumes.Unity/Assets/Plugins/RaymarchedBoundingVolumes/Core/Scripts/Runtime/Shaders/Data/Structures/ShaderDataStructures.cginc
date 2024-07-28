#pragma once

#include <UnityInstancing.cginc>

struct AppData
{
    float4 vertex : POSITION;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct FragmentData
{
    float4 vertex : SV_POSITION;
    float3 hitPosition : TEXCOORD1;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct PixelData
{
    fixed4 color : SV_Target;
    float depth : SV_Depth;
};