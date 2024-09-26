#pragma once

#include "../Structures/OperationTypeDataStructures.cginc"

// Manually set by scripts

uniform StructuredBuffer<RadiusDefinedOperationData> _RaymarchingSmoothUnionOperationData;
uniform StructuredBuffer<RadiusDefinedOperationData> _RaymarchingSmoothSubtractOperationData;
uniform StructuredBuffer<RadiusDefinedOperationData> _RaymarchingSmoothIntersectOperationData;
uniform StructuredBuffer<SmoothXorOperationData>     _RaymarchingSmoothXorOperationData;