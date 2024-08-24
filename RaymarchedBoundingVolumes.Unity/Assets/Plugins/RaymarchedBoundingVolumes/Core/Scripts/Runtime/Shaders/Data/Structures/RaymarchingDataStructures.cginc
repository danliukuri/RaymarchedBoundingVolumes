#pragma once

struct SDFData
{
    fixed3 color;
    float  distance;
};

struct RaymarchingData
{
    fixed3 color;
    float3 position;
};

struct ObjectData
{
    int type;
    int typeRelatedDataIndex;

    bool   isActive;
    float3 position;
};

struct OperationData
{
    int   type;
    float blendStrength;
};

struct OperationNodeData
{
    int childOperationsCount;
    int childObjectsCount;

    int parentIndex;
    int layer;
};