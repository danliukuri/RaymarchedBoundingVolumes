#include "../Data/Variables/RaymarchingGlobalVariables.cginc"
#include "Calculators/SDFCalculator.cginc"

RaymarchingData raymarch(const float3 rayOrigin, const float3 rayDirection)
{
    RaymarchingData result = {half3(0, 0, 0), half3(0, 0, 0)};
    float traveledDistance = 0;

    for (int i = 0; i < _MaxDetectionIterations; i++)
    {
        const float3 currentPosition = rayOrigin + rayDirection * traveledDistance;
        const SDFData sdf = calculateSDF(currentPosition);

        if (sdf.distanceToObject < _MaxDetectionOffset)
        {
            result.pixelColor = sdf.pixelColor;
            result.objectPosition = currentPosition;
            break;
        }

        if ((traveledDistance += sdf.distanceToObject) >= _FarClippingPlane || i == _MaxDetectionIterations - 1)
            discard;
    }
    return result;
}