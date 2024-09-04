#pragma once

#include "../Structures/RaymarchingDataStructures.cginc"

// Automatically set from shader properties

uniform int   _MaxDetectionIterations;
uniform float _MaxDetectionOffset;
uniform float _FarClippingPlane;

uniform fixed4 _ObjectColor;

// Automatically set by Unity

uniform float4 _LightColor0;

// Manually set by scripts

uniform int                                 _RaymarchingOperationsCount;
uniform StructuredBuffer<OperationNodeData> _RaymarchingOperationNodes;
uniform StructuredBuffer<OperationData>     _RaymarchingOperations;
uniform StructuredBuffer<ObjectData>        _RaymarchedObjects;
uniform StructuredBuffer<ObjectTransform3D> _RaymarchedObjectsThreeDimensionalTransforms;

// Default values shortcuts

static SDFData _DefaultSDFData = {_ObjectColor.rgb, _FarClippingPlane};

// Constants definitions

#define MAX_INHERITANCE_LEVEL 10