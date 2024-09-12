#pragma once

struct HypercubeData
{
    float4 halfDimensions;
};

struct HypersphereData
{
    float radius;
};

struct HyperellipsoidData
{
    float4 radii;
};

struct HypercapsuleData
{
    float halfHeight;
    float radius;
};

struct EllipsoidalHypercapsuleData
{
    float  halfHeight;
    float4 radii;
};