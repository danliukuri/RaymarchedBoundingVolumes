#pragma once

struct RadiusDefinedOperationData
{
    float radius;
};

struct RadiusDefinedXorOperationData
{
    float outerRadius;
    float innerRadius;
};

struct ColumnsOperationData
{
    float radius;
    int   count;
};

struct ColumnsXorOperationData
{
    ColumnsOperationData outer;
    ColumnsOperationData inner;
};

struct RatioDefinedOperationData
{
    float ratio;
};