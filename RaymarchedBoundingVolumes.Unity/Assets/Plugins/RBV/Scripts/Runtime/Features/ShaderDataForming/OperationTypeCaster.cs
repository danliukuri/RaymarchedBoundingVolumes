using System;
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
            type is SmoothUnion or SmoothSubtract or SmoothIntersect or SmoothXor;

        public Type GetShaderDataType(RaymarchingOperationType type) => type switch
        {
            SmoothUnion     => typeof(RadiusDefinedOperationShaderData),
            SmoothSubtract  => typeof(RadiusDefinedOperationShaderData),
            SmoothIntersect => typeof(RadiusDefinedOperationShaderData),
            SmoothXor       => typeof(SmoothXorOperationShaderData),
            Union or Subtract or Intersect or Xor =>
                throw new InvalidOperationException(string.Format(NoShaderData, type.ToString())),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}