#pragma once

struct RaymarchingData
{
    fixed3 color;
    float3 position;
};

struct SDFData
{
    fixed3 color;
    float  distance;
};

struct OperationNodeData
{
    int childOperationsCount;
    int childObjectsCount;

    int parentIndex;
    int layer;
};

struct OperationData
{
    int   type;
    float blendStrength;
};

struct ObjectData
{
    int type;
    int typeRelatedDataIndex;

    bool isActive;
    int  transformType;
};

struct ObjectTransform3D
{
    float3 position;
    float3 rotation;
    float3 scale;
};