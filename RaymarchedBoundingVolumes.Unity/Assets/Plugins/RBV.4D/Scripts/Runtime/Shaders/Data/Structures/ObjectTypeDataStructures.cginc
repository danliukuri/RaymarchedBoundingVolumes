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
    float           halfHeight;
    HypersphereData base;
};

struct EllipsoidalHypercapsuleData
{
    float              halfHeight;
    HyperellipsoidData base;
};

struct SphericalCylinderData
{
    HypersphereData base;
    /** @related _TrengthDefinition */
    float halfTrength;
};

struct CubicalCylinderData
{
    float                 halfHeight;
    SphericalCylinderData sphericalCylinder;
};

struct EllipsoidalCylinderData
{
    HyperellipsoidData base;
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

struct RegularDoublePrismData
{
    int2   verticesCount;
    float2 circumradius;
    float2 halfLength;
};