﻿using System;
using System.Collections.Generic;
using RBV.Data.Dynamic.ShaderData.OperationType;
using RBV.Data.Static.Enumerations;
using RBV.Utilities.Extensions;
using static RBV.Data.Static.Enumerations.RaymarchingOperationType;

namespace RBV.Features.ShaderDataForming
{
    public class OperationTypeCaster : IOperationTypeCaster
    {
        private const string NoShaderData = "{0} operation type doesn't have corresponding shader data to pass.";

        public Array CastToShaderDataTypeArray(RaymarchingOperationType              type,
                                               IEnumerable<IOperationTypeShaderData> source) =>
            source.CastToArray(GetShaderDataType(type));

        public bool HasCorrespondingShaderData(RaymarchingOperationType type) =>
            type is not (Union or Subtract or Intersect or Xor);

        public Type GetShaderDataType(RaymarchingOperationType type) => type switch
        {
            Union or Subtract or Intersect or Xor =>
                throw new InvalidOperationException(string.Format(NoShaderData, type.ToString())),

            SmoothUnion      => typeof(RadiusDefinedOperationShaderData),
            SmoothSubtract   => typeof(RadiusDefinedOperationShaderData),
            SmoothIntersect  => typeof(RadiusDefinedOperationShaderData),
            SmoothXor        => typeof(RadiusDefinedXorOperationShaderData),
            ChamferUnion     => typeof(RadiusDefinedOperationShaderData),
            ChamferSubtract  => typeof(RadiusDefinedOperationShaderData),
            ChamferIntersect => typeof(RadiusDefinedOperationShaderData),
            ChamferXor       => typeof(RadiusDefinedXorOperationShaderData),
            StairsUnion      => typeof(ColumnsOperationShaderData),
            StairsSubtract   => typeof(ColumnsOperationShaderData),
            StairsIntersect  => typeof(ColumnsOperationShaderData),
            StairsXor        => typeof(ColumnsXorOperationShaderData),
            Morph            => typeof(RatioDefinedOperationShaderData),

            _ => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}