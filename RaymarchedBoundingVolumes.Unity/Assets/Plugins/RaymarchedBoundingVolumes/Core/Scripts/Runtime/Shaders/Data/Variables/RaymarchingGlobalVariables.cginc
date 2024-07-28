#pragma once

#include <HLSLSupport.cginc>
#include "../Structures/RaymarchingDataStructures.cginc"

// Automatically set from shader properties

int   _MaxDetectionIterations;
float _MaxDetectionOffset;
float _FarClippingPlane;

fixed4 _ObjectColor;
fixed4 _OutOfIterationsColor;

// Automatically set by Unity

float4 _LightColor0; 

// Manually set by scripts

int                             _RaymarchingOperationsCount;
StructuredBuffer<OperationData> _RaymarchingOperations;
StructuredBuffer<ObjectData>    _RaymarchedObjects;