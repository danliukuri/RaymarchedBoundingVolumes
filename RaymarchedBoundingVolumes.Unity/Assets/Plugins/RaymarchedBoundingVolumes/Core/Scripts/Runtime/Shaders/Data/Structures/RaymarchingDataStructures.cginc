#pragma once

#include <HLSLSupport.cginc>

struct SDFData
{
    fixed3 pixelColor;
    float  distanceToObject;
};

struct RaymarchingData
{
    fixed3 pixelColor;
    float3 objectPosition;
};

struct ObjectData
{
    bool   isActive;
    float3 position;
};

struct OperationData
{
    int   type;
    int   childCount;
    float blendStrength;
};