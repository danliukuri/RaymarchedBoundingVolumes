#pragma once

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
    float halfHeight;
    float radius;
};

struct EllipsoidalCapsuleData
{
    float halfHeight;
    float3 radii;
};

struct CylinderData
{
    float height;
    float radius;
};

struct EllipsoidalCylinderData
{
    float3 dimensions;
};

struct PlaneData
{
    float3 halfDimensions;
};

struct ConeData
{
    float height;
    float radius;
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