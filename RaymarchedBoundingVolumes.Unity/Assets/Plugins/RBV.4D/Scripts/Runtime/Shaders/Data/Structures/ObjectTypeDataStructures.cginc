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

struct CubicalCylinderData
{
    float radius;
    float halfHeight;
    /** @related _TrengthDefinition */
    float halfTrength;
};

struct SphericalCylinderData
{
    float radius;
    /** @related _TrengthDefinition */
    float halfTrength;
};

struct EllipsoidalCylinderData
{
    float3 radii;
    /** @related _TrengthDefinition */
    float halfTrength;
};

struct ConicalCylinderData
{
    float radius;
    float halfHeight;
    /** @related _TrengthDefinition */
    float halfTrength;
};

struct DoubleCylinderData
{
    float2 radii;
};

struct DoubleEllipticCylinderData
{
    float4 radii;
};

struct PrismicCylinderData
{
    int   verticesCount;
    float circumradius;
    float halfLength;
};

struct SphericalConeData
{
    float radius;
    /** @related _TrengthDefinition */
    float halfTrength;
};

struct CylindricalConeData
{
    float radius;
    /** @related _TrengthDefinition */
    float halfTrength;
};

struct DoubleTorusData
{
    float majorMajorRadius;
    float majorMinorRadius;
    float minorMinorRadius;
};

struct TigerData
{
    float2 majorRadii;
    float  minorRadii;
};