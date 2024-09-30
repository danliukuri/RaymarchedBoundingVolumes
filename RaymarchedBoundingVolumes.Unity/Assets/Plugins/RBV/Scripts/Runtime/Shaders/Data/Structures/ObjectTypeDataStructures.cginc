#pragma once

#include "WrappingDataStructures.cginc"

struct CubeData
{
    float3 halfDimensions;
};

struct SphereData
{
    float radius;
};

struct EllipsoidData
{
    float3 radii;
};

struct CapsuleData
{
    float      halfHeight;
    SphereData base;
};

struct EllipticCapsuleData
{
    float         halfHeight;
    EllipsoidData ellipsoid;
};

struct CylinderData
{
    float      height;
    SphereData base;
};

struct EllipticCylinderData
{
    float3 dimensions;
};

struct PlaneData
{
    float3 halfDimensions;
};

struct CappedConeData
{
    float halfHeight;
    float topBaseRadius;
    float bottomBaseRadius;
};

struct TorusData
{
    float majorRadius;
    float minorRadius;
};

struct CappedTorusData
{
    float     capAngle;
    TorusData torus;
};

struct RegularPrismData
{
    int   verticesCount;
    float circumradius;
    float halfLength;
};

struct RegularPolyhedronData
{
    float    inscribedRadius;
    RangeInt activeBoundPlaneRange;
};