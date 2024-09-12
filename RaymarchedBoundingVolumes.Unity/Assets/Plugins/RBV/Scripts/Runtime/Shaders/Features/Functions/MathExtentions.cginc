#pragma once

float minAxisOf2(float2 value)
{
    return min(value.x, value.y);
}

float maxAxisOf2(float2 value)
{
    return max(value.x, value.y);
}

float minAxisOf3(float3 value)
{
    return min(min(value.x, value.y), value.z);
}

float maxAxisOf3(float3 value)
{
    return max(max(value.x, value.y), value.z);
}

float minAxisOf4(float4 value)
{
    return min(min(value.x, value.y), min(value.z, value.w));
}

float maxAxisOf4(float4 value)
{
    return max(max(value.x, value.y), max(value.z, value.w));
}