#pragma once

/**
 * The Golden Ratio (φ) is a mathematical constant approximately equal to 1.618034.<br/>
 * For more information, visit:<br/> <a href="https://en.wikipedia.org/wiki/Golden_ratio">Golden Ratio - Wikipedia</a>
 */
#define GOLDEN_RATIO ((sqrt(5) + 1) * 0.5)

/**
 * The set of plane normals that allows constructing a large variety of geometric primitives.<br/>
 * For more information, visit:<br/>
 * <a href="https://people.tamu.edu/~ergun/research/implicitmodeling/papers/sm99.pdf">
 * Generalized Distance Functions - Akleman and Chen</a>
 * 
 * Some of those are slow whenever a driver decides to not unroll the loop,
 * which seems to happen for Icosahedron (3, 12) and Truncated Icosahedron (3, 18) on Nvidia 350.12 at least.
 * Specialized implementations can well be faster in all cases.
 */
static float3 _BoundPlaneNormals[19] =
{
    normalize(float3(1, 0, 0)),
    normalize(float3(0, 1, 0)),
    normalize(float3(0, 0, 1)),

    normalize(float3(1, 1, 1)),
    normalize(float3(-1, 1, 1)),
    normalize(float3(1, -1, 1)),
    normalize(float3(1, 1, -1)),

    normalize(float3(0, 1, GOLDEN_RATIO + 1)),
    normalize(float3(0, -1, GOLDEN_RATIO + 1)),
    normalize(float3(GOLDEN_RATIO + 1, 0, 1)),
    normalize(float3(-GOLDEN_RATIO - 1, 0, 1)),
    normalize(float3(1, GOLDEN_RATIO + 1, 0)),
    normalize(float3(-1, GOLDEN_RATIO + 1, 0)),

    normalize(float3(0, GOLDEN_RATIO, 1)),
    normalize(float3(0, -GOLDEN_RATIO, 1)),
    normalize(float3(1, 0, GOLDEN_RATIO)),
    normalize(float3(-1, 0, GOLDEN_RATIO)),
    normalize(float3(GOLDEN_RATIO, 1, 0)),
    normalize(float3(-GOLDEN_RATIO, 1, 0))
};