﻿#pragma once

#include "../Structures/OperationTypeDataStructures.cginc"

// Manually set by scripts

uniform StructuredBuffer<RadiusDefinedOperationData>    _RaymarchingSmoothUnionOperationData;
uniform StructuredBuffer<RadiusDefinedOperationData>    _RaymarchingSmoothSubtractOperationData;
uniform StructuredBuffer<RadiusDefinedOperationData>    _RaymarchingSmoothIntersectOperationData;
uniform StructuredBuffer<RadiusDefinedXorOperationData> _RaymarchingSmoothXorOperationData;

uniform StructuredBuffer<RadiusDefinedOperationData>    _RaymarchingChamferUnionOperationData;
uniform StructuredBuffer<RadiusDefinedOperationData>    _RaymarchingChamferSubtractOperationData;
uniform StructuredBuffer<RadiusDefinedOperationData>    _RaymarchingChamferIntersectOperationData;
uniform StructuredBuffer<RadiusDefinedXorOperationData> _RaymarchingChamferXorOperationData;

uniform StructuredBuffer<ColumnsOperationData> _RaymarchingStairsUnionOperationData;