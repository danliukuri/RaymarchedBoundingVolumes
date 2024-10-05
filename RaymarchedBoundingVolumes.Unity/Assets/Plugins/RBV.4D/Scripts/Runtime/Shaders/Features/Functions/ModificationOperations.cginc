#pragma once

#ifdef RBV_4D_ON_PROJECT
#include "../../../../../../RBV/Scripts/Runtime/Shaders/Features/Functions/ModificationOperations.cginc"
#else
#include "Packages/com.danliukuri.rbv/Scripts/Runtime/Shaders/Features/Functions/ModificationOperations.cginc"
#endif

float4 elongateX(float4 position, const float strength)
{
    position.x -= calculateElongationDisplacement(position.x, strength);
    return position;
}

float4 elongateY(float4 position, const float strength)
{
    position.y -= calculateElongationDisplacement(position.y, strength);
    return position;
}

float4 elongateZ(float4 position, const float strength)
{
    position.z -= calculateElongationDisplacement(position.z, strength);
    return position;
}

float4 elongateW(float4 position, const float strength)
{
    position.w -= calculateElongationDisplacement(position.w, strength);
    return position;
}


float extrudeOrigin(const float4 positionOnAxes, const float4 distanceAlongAxes)
{
    return cartesianProduct(calculateFourLinesSDF(positionOnAxes, distanceAlongAxes));
}

float extrude(const float sdf, const float4 positionOnAxes, const float4 distanceAlongAxes)
{
    return cartesianProduct(sdf, calculateFourLinesSDF(positionOnAxes, distanceAlongAxes));
}


float2 revolveInSphere(const float3 position, const float positionOnAxis,
                       const float  radius      = 0.0,
                       const float  axialOffset = 0.0)
{
    return revolve(calculateSphereSDF(position, radius), positionOnAxis, axialOffset);
}

float2 revolveInSphereByW(const float4 position, const float radius = 0.0, const float axialOffset = 0.0)
{
    return revolveInSphere(position.xyz, position.w, radius, axialOffset);
}