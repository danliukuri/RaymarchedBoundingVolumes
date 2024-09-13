#pragma once

#include "../Variables/FourDimensionGlossaryConstants.cginc"

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

struct CubicalCylinderData
{
    float radius;
    float halfHeight;
    /** @relates _TrengthDefinition */
    float halfTrength;
};

struct SphericalCylinderData
{
    float radius;
    /** @relates _TrengthDefinition */
    float halfTrength;
};

struct EllipsoidalCylinderData
{
    float3 radii;
    /** @relates _TrengthDefinition */
    float halfTrength;
};

struct ConicalCylinderData
{
    float radius;
    float height;
    /** @relates _TrengthDefinition */
    float halfTrength;
};