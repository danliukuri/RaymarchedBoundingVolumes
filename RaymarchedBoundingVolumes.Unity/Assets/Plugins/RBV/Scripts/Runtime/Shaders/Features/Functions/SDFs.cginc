#pragma once

#include "1DSDFs.cginc"
#include "2DSDFs.cginc"
#include "ModificationOperations.cginc"
#include "../../Data/Variables/CommonBoundingPlanesVariables.cginc"

float calculateCubeSDF(const float3 position, const float3 halfDimensions)
{
    return extrudeOrigin(position, halfDimensions);
}

float calculateSphereSDF(const float3 position, const float radius)
{
    return length(position) - radius;
}

/**
 * Calculate the signed distance of a point to an ellipsoid.
 *
 * For more information on ellipsoid SDF calculations, visit:
 * <a href="https://iquilezles.org/articles/ellipsoids">Inigo Quilez - Ellipsoid SDF</a>
 *
 * @param position The 3D position vector to calculate the distance from.
 * @param radii The radii of the ellipsoid in the X, Y, and Z axes.
 * @return The signed distance of the point to the ellipsoid.
 */
float calculateEllipsoidSDF(const float3 position, const float3 radii)
{
    float3 normalizedPosition = position / radii;
    float  normalizedDistance = length(normalizedPosition);
    float  outsideDistance    = normalizedDistance - 1.0;
    float  gradientLength     = length(normalizedPosition / radii);
    return normalizedDistance * outsideDistance / gradientLength;
}

float calculateCapsuleSDF(const float3 position, const float halfHeight, const float radius)
{
    return calculateSphereSDF(elongateY(position, halfHeight), radius);
}

float calculateEllipticCapsuleSDF(const float3 position, const float halfHeight, const float3 radii)
{
    return calculateEllipsoidSDF(elongateY(position, halfHeight), radii);
}

float calculateCylinderSDF(const float3 position, const float height, const float radius)
{
    return extrude(calculateCircleSDF(position.xz, radius), position.y, height);
}

float calculateEllipticCylinderSDF(const float3 position, const float3 dimensions)
{
    return extrude(calculateEllipseSDF(position.xz, dimensions.xz), position.y, dimensions.y);
}

float calculatePlaneSDF(const float3 position, const float3 halfDimensions)
{
    return calculateCubeSDF(position, halfDimensions);
}

float calculateConeSDF(const float3 position, const float height, const float radius)
{
    float2 baseToHeight = float2(radius / height, -1.0);

    float2 projectedPosition = revolutionizeY(position, 0.0, height * 0.5);
    float2 vectorToBase      = height * baseToHeight;

    float clampedProjectionFactor =
        clamp(dot(projectedPosition, vectorToBase) / dot(vectorToBase, vectorToBase), 0.0, 1.0);
    float2 clampedBaseIntersection =
        float2(clamp(projectedPosition.x / vectorToBase.x, 0.0, 1.0), 1.0);

    float2 distanceToSurfaceA = projectedPosition - vectorToBase * clampedProjectionFactor;
    float2 distanceToSurfaceB = projectedPosition - vectorToBase * clampedBaseIntersection;
    float  distanceSquared    =
        min(dot(distanceToSurfaceA, distanceToSurfaceA), dot(distanceToSurfaceB, distanceToSurfaceB));

    float signCorrection   = sign(vectorToBase.y);
    float sideDistance     = projectedPosition.x * vectorToBase.y - projectedPosition.y * vectorToBase.x;
    float heightDifference = projectedPosition.y - vectorToBase.y;
    float surfaceSign      = max(signCorrection * sideDistance, signCorrection * heightDifference);

    return sqrt(distanceSquared) * sign(surfaceSign);
}

float calculateCappedConeSDF(const float3 position,
                             const float  height, const float topBaseRadius, const float bottomBaseRadius)
{
    float2 projectedPosition = revolutionizeY(position);

    float2 baseParams   = float2(topBaseRadius, height);
    float2 radiusParams = float2(topBaseRadius - bottomBaseRadius, 2.0 * height);

    float  minBaseRadius      = projectedPosition.y < 0.0 ? bottomBaseRadius : topBaseRadius;
    float  horizontalDistance = projectedPosition.x - min(projectedPosition.x, minBaseRadius);
    float  verticalDistance   = calculateLineSDF(projectedPosition.y, height);
    float2 distanceToBase     = float2(horizontalDistance, verticalDistance);

    float2 deltaPosition   = baseParams - projectedPosition;
    float  slantFactor     = clamp(dot(deltaPosition, radiusParams) / dot(radiusParams, radiusParams), 0.0, 1.0);
    float2 distanceToSlant = projectedPosition - baseParams + radiusParams * slantFactor;

    float sign = distanceToSlant.x < 0.0 && distanceToBase.y < 0.0 ? -1.0 : 1.0;
    return sign * sqrt(min(dot(distanceToBase, distanceToBase), dot(distanceToSlant, distanceToSlant)));
}

float calculateTorusSDF(const float3 position, const float majorRadius, const float minorRadius)
{
    return calculateCircleSDF(revolutionizeY(position, majorRadius), minorRadius);
}

float calculateCappedTorusSDF(const float3 position,
                              const float  capAngle, const float majorRadius, const float minorRadius)
{
    float  capAngleSin    = sin(capAngle);
    float  capAngleSinAbs = abs(capAngleSin);
    float2 capDirection   = float2(capAngleSinAbs, cos(capAngle) * (capAngleSinAbs / capAngleSin));

    float3 absPosition = float3(abs(position.x), position.y, position.z);

    float distanceToEdge = capDirection.y * absPosition.x > capDirection.x * absPosition.z
                               ? dot(absPosition.xz, capDirection)
                               : length(absPosition.xz);

    float distanceToTorus =
        sqrt(dot(position, position) + majorRadius * majorRadius - 2.0 * majorRadius * distanceToEdge);
    return distanceToTorus - minorRadius;
}

float calculateRegularPrismSDF(const float3 position,
                               const float  verticesCount, const float circumradius, const float length)
{
    float3 rotatedPosition = position.yzx;
    float  polyhedronSDF   = calculateRegularPolygonSDF(rotatedPosition.zx, verticesCount, circumradius);
    return extrude(polyhedronSDF, rotatedPosition.y, abs(length));
}

float calculateRegularPolyhedronSDF(const float3 position,
                                    const float  inscribedRadius,
                                    const int    activeBoundPlaneStartIndex,
                                    const int    activeBoundPlaneEndIndex)
{
    float distance = 0.0;
    for (int i   = activeBoundPlaneStartIndex; i <= activeBoundPlaneEndIndex; ++i)
        distance = max(distance, abs(dot(position, _BoundPlaneNormals[i])));
    return distance - inscribedRadius;
}