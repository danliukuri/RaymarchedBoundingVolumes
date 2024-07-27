#include "RaymarchingGlobalVariables.cginc"
#include "SDFCalculators.cginc"

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

#ifdef HEATMAP_VISUALIZATION_ON
sampler2D _HeatmapTexture;
RaymarchingData raymarchHeatmap(const float3 rayOrigin, const float3 rayDirection)
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
    result.pixelColor = tex2D(_HeatmapTexture, half2((half) iterationIndex/_MaxDetectionIterations, 1));
    return result;
}
#endif