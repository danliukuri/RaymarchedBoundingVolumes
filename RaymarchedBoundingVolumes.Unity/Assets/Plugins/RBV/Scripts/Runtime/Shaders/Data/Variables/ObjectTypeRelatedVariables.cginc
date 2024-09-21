#pragma once

#include "../Structures/ObjectTypeDataStructures.cginc"

// Manually set by scripts

uniform StructuredBuffer<CubeData>              _RaymarchedCubeData;
uniform StructuredBuffer<SphereData>            _RaymarchedSphereData;
uniform StructuredBuffer<EllipsoidData>         _RaymarchedEllipsoidData;
uniform StructuredBuffer<CapsuleData>           _RaymarchedCapsuleData;
uniform StructuredBuffer<EllipticCapsuleData>   _RaymarchedEllipticCapsuleData;
uniform StructuredBuffer<CylinderData>          _RaymarchedCylinderData;
uniform StructuredBuffer<EllipticCylinderData>  _RaymarchedEllipticCylinderData;
uniform StructuredBuffer<PlaneData>             _RaymarchedPlaneData;
uniform StructuredBuffer<ConeData>              _RaymarchedConeData;
uniform StructuredBuffer<CappedConeData>        _RaymarchedCappedConeData;
uniform StructuredBuffer<TorusData>             _RaymarchedTorusData;
uniform StructuredBuffer<CappedTorusData>       _RaymarchedCappedTorusData;
uniform StructuredBuffer<RegularPrismData>      _RaymarchedRegularPrismData;
uniform StructuredBuffer<RegularPolyhedronData> _RaymarchedRegularPolyhedronData;