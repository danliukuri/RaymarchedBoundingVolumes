#pragma once

#include "ModificationOperations.cginc"

float calculateHypercubeSDF(const float4 position, const float4 halfDimensions)
{
    return extrudeOrigin(position, halfDimensions);
}

float calculateHypersphereSDF(const float4 position, const float radius)
{
    return length(position) - radius;
}

float calculateHyperellipsoidSDF(const float4 position, const float4 radii)
{
    float4 normalizedPosition = position / radii;
    float  normalizedDistance = length(normalizedPosition);
    float  outsideDistance    = normalizedDistance - 1.0;
    float  gradientLength     = length(normalizedPosition / radii);
    return normalizedDistance * outsideDistance / gradientLength;
}

float calculateHypercapsuleSDF(const float4 position, const float halfHeight, const float radius)
{
    return calculateHypersphereSDF(elongateY(position, halfHeight), radius);
}

float calculateEllipsoidalHypercapsuleSDF(const float4 position, const float halfHeight, const float4 radii)
{
    return calculateHyperellipsoidSDF(elongateY(position, halfHeight), radii);
}

float calculateCubicalCylinderSDF(const float4 position,
                                  const float  radius, const float halfHeight, const float halfTrength)
{
    return extrude(calculateCircleSDF(position.xz, radius), position.yw, float2(halfHeight, halfTrength));
}

float calculateSphericalCylinderSDF(const float4 position, const float radius, const float halfTrength)
{
    return extrude(calculateSphereSDF(position.xyz, radius), position.w, halfTrength);
}

float calculateEllipsoidalCylinderSDF(const float4 position, const float3 radii, const float halfTrength)
{
    return extrude(calculateEllipsoidSDF(position.xyz, radii), position.w, halfTrength);
}

float calculateConicalCylinderSDF(const float4 position,
                                  const float  radius, const float halfHeight, const float halfTrength)
{
    return extrude(calculateConeSDF(position.xyz, halfHeight, radius), position.w, halfTrength);
}

float calculateDoubleCylinderSDF(const float4 position, const float2 radii)
{
    return cartesianProduct(calculateCircleSDF(position.xz, radii.x), calculateCircleSDF(position.yw, radii.y));
}

float calculateDoubleEllipticCylinderSDF(const float4 position, const float4 radii)
{
    return cartesianProduct(calculateEllipseSDF(position.xz, radii.xz), calculateEllipseSDF(position.yw, radii.yw));
}

float calculatePrismicCylinderSDF(const float4 position,
                                  const int    verticesCount, const float circumradius, const float halfLength)
{
    float regularPolygonSDF = calculateRegularPolygonSDF(position.xz, verticesCount, circumradius);
    float circleSDF         = calculateCircleSDF(position.yw, halfLength);
    return cartesianProduct(regularPolygonSDF, circleSDF);
}

float calculateSphericalConeSDF(const float4 position, const float radius, const float halfTrength)
{
    return calculateIsoscelesTriangleSDF(revolutionizeW(position), radius, halfTrength);
}

float calculateCylindricalConeSDF(const float4 position, const float radius, const float halfTrength)
{
    float  cylinder       = calculateCylinderSDF(position, halfTrength, abs(radius));
    float2 revolvedAlongW = float2(cylinder + 1.0, position.w - halfTrength);
    return calculateIsoscelesTriangleSDF(revolvedAlongW, 1.0, halfTrength * 2.0);
}

float calculateToroidalSphereSDF(const float4 position, const float majorRadius, const float minorRadius)
{
    return calculateCircleSDF(revolutionizeW(position, majorRadius), minorRadius);
}

float calculateSphericalTorusSDF(const float4 position, const float majorRadius, const float minorRadius)
{
    float2 revolvedAlongY = float2(calculateCircleSDF(position.xz, majorRadius), position.y);
    float3 revolvedAlongW = float3(revolvedAlongY, position.w);
    return calculateSphereSDF(revolvedAlongW, minorRadius);
}

float calculateDoubleTorusSDF(const float4 position, const float majorMajorRadius, const float majorMinorRadius,
                              const float  minorMinorRadius)
{
    float2 revolvedAlongW = float2(calculateTorusSDF(position, majorMajorRadius, majorMinorRadius), position.w);
    return calculateCircleSDF(revolvedAlongW, minorMinorRadius);
}