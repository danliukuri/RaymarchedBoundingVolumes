#pragma once

#include "../Structures/ObjectTypeDataStructures.cginc"

// Manually set by scripts

uniform StructuredBuffer<CubeData>               _RaymarchedCubeData;
uniform StructuredBuffer<SphereData>             _RaymarchedSphereData;
uniform StructuredBuffer<EllipsoidData>          _RaymarchedEllipsoidData;
uniform StructuredBuffer<CapsuleData>            _RaymarchedCapsuleData;
uniform StructuredBuffer<EllipsoidalCapsuleData> _RaymarchedEllipsoidalCapsuleData;