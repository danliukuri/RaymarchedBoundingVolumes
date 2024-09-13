﻿#pragma once

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
    /**
     * Trength - the measurement of an object's extension along the fourth dimensional axis (W),
     * the 4D analog of length, width, and height.
     * 
     * For more information, visit:\n
     * <a href="http://hi.gher.space/classic/glossary.htm">Garrett Jones - Multi-dimensional Glossary</a>\n
     * <a href="https://everything2.com/user/Tem42/writeups/Spissitude">Tem42 - Spissitude</a>
     */
    float halfTrength;
};

struct SphericalCylinderData
{
    float radius;
    /**
     * Trength - the measurement of an object's extension along the fourth dimensional axis (W),
     * the 4D analog of length, width, and height.
     * 
     * For more information, visit:\n
     * <a href="http://hi.gher.space/classic/glossary.htm">Garrett Jones - Multi-dimensional Glossary</a>\n
     * <a href="https://everything2.com/user/Tem42/writeups/Spissitude">Tem42 - Spissitude</a>
     */
    float halfTrength;
};