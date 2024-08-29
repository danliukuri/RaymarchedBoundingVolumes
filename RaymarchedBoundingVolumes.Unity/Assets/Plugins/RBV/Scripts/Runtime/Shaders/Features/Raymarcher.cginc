#include "../Data/Variables/RaymarchingGlobalVariables.cginc"
#include "Calculators/SDFCalculator.cginc"

RaymarchingData raymarch(const float3 rayOrigin, const float3 rayDirection)
{
    RaymarchingData result = {half3(0, 0, 0), half3(0, 0, 0)};
    float traveledDistance = 0;

    UNITY_LOOP
    for (int i = 0; i < _MaxDetectionIterations; i++)
    {
        const float3 currentPosition = rayOrigin + rayDirection * traveledDistance;
        const SDFData sdf = calculateSDF(currentPosition);

        if (sdf.distance < _MaxDetectionOffset)
        {
            result.color = sdf.color;
            result.position = currentPosition;
            break;
        }

        if ((traveledDistance += sdf.distance) >= _FarClippingPlane || i == _MaxDetectionIterations - 1)
            discard;
    }
    return result;
}