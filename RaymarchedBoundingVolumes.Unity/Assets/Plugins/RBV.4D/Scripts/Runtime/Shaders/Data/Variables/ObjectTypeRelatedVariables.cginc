﻿#pragma once

#include "../Structures/ObjectTypeDataStructures.cginc"

// Manually set by scripts

uniform StructuredBuffer<HypercubeData>               _RaymarchedHypercubeData;
uniform StructuredBuffer<HypersphereData>             _RaymarchedHypersphereData;
uniform StructuredBuffer<HyperellipsoidData>          _RaymarchedHyperellipsoidData;
uniform StructuredBuffer<HypercapsuleData>            _RaymarchedHypercapsuleData;
uniform StructuredBuffer<EllipsoidalHypercapsuleData> _RaymarchedEllipsoidalHypercapsuleData;
uniform StructuredBuffer<CubicalCylinderData>         _RaymarchedCubicalCylinderData;
uniform StructuredBuffer<SphericalCylinderData>       _RaymarchedSphericalCylinderData;
uniform StructuredBuffer<EllipsoidalCylinderData>     _RaymarchedEllipsoidalCylinderData;
uniform StructuredBuffer<CubicalCylinderData>         _RaymarchedConicalCylinderData;
uniform StructuredBuffer<DoubleCylinderData>          _RaymarchedDoubleCylinderData;
uniform StructuredBuffer<DoubleEllipticCylinderData>  _RaymarchedDoubleEllipticCylinderData;
uniform StructuredBuffer<PrismicCylinderData>         _RaymarchedPrismicCylinderData;
uniform StructuredBuffer<SphericalCylinderData>       _RaymarchedSphericalConeData;
uniform StructuredBuffer<SphericalCylinderData>       _RaymarchedCylindricalConeData;
uniform StructuredBuffer<TorusData>                   _RaymarchedToroidalSphereData;
uniform StructuredBuffer<TorusData>                   _RaymarchedSphericalTorusData;
uniform StructuredBuffer<DoubleTorusData>             _RaymarchedDoubleTorusData;
uniform StructuredBuffer<TigerData>                   _RaymarchedTigerData;
uniform StructuredBuffer<RegularDoublePrismData>      _RaymarchedRegularDoublePrismData;