#include "../../../../../Core/Scripts/Runtime/Shaders/Data/Structures/RaymarchingDataStructures.cginc"
#include "../../../../../Core/Scripts/Runtime/Shaders/Data/Variables/RaymarchingGlobalVariables.cginc"
#include "../../../../../Core/Scripts/Runtime/Shaders/Features/Calculators/SDFCalculator.cginc"

sampler2D _HeatmapTexture;

RaymarchingData raymarch(const float3 rayOrigin, const float3 rayDirection)
{
    RaymarchingData result;
    result.objectPosition = rayOrigin;
    float traveledDistance = 0;
    int iterationIndex = _MaxDetectionIterations;

    for (int i = 0; i < _MaxDetectionIterations; i++)
    {
        const float3 currentPosition = rayOrigin + rayDirection * traveledDistance;
        const SDFData sdf = calculateSDF(currentPosition);

        if (sdf.distanceToObject < _MaxDetectionOffset ||
            (traveledDistance += sdf.distanceToObject) >= _FarClippingPlane)
        {
            iterationIndex = i + 1;
            break;
        }
    }
    result.pixelColor = tex2D(_HeatmapTexture, half2((half)iterationIndex / _MaxDetectionIterations, 1));
    return result;
}
