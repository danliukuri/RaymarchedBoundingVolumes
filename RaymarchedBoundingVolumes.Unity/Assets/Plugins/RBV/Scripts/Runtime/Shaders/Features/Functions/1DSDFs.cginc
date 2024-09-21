#pragma once

float calculateLineSDF(const float positionOnAxis, const float halfLength)
{
    return abs(positionOnAxis) - halfLength;
}


float2 calculateTwoLinesSDF(const float positionOnAxis, const float2 halfLengths)
{
    return abs(positionOnAxis) - halfLengths;
}

float2 calculateTwoLinesSDF(const float positionOnAxis, const float halfLength1, const float halfLength2)
{
    return calculateTwoLinesSDF(positionOnAxis, float2(halfLength1, halfLength2));
}


float3 calculateThreeLinesSDF(const float positionOnAxis, const float3 halfLengths)
{
    return abs(positionOnAxis) - halfLengths;
}

float3 calculateThreeLinesSDF(const float positionOnAxis,
                              const float halfLength1, const float halfLength2, const float halfLength3)
{
    return calculateThreeLinesSDF(positionOnAxis, float3(halfLength1, halfLength2, halfLength3));
}


float4 calculateFourLinesSDF(const float positionOnAxis, const float4 halfLengths)
{
    return abs(positionOnAxis) - halfLengths;
}

float4 calculateFourLinesSDF(const float positionOnAxis,
                             const float halfLength1, const float halfLength2,
                             const float halfLength3, const float halfLength4)
{
    return calculateFourLinesSDF(positionOnAxis, float4(halfLength1, halfLength2, halfLength3, halfLength4));
}


float2 calculateTwoLinesSDF(const float2 positionOnAxis, const float2 halfLengths)
{
    return float2(calculateLineSDF(positionOnAxis.x, halfLengths.x), calculateLineSDF(positionOnAxis.y, halfLengths.y));
}

float3 calculateThreeLinesSDF(const float3 positionOnAxis, const float3 halfLengths)
{
    return float3(calculateLineSDF(positionOnAxis.x, halfLengths.x), calculateLineSDF(positionOnAxis.y, halfLengths.y),
                  calculateLineSDF(positionOnAxis.z, halfLengths.z));
}

float4 calculateFourLinesSDF(const float4 positionOnAxis, const float4 halfLengths)
{
    return float4(calculateLineSDF(positionOnAxis.x, halfLengths.x), calculateLineSDF(positionOnAxis.y, halfLengths.y),
                  calculateLineSDF(positionOnAxis.z, halfLengths.z), calculateLineSDF(positionOnAxis.w, halfLengths.w));
}