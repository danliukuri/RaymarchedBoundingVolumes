using System;
using System.Collections.Generic;
using RBV.Data.Static.Enumerations;

namespace RBV.Features.ShaderDataForming
{
    public interface ITransformTypeCaster
    {
        Array CastToShaderDataTypeArray(KeyValuePair<TransformType, List<RaymarchedObject>> source);

        Array CastToShaderDataTypeArray(TransformType type, IEnumerable<object> source);

        Type GetShaderDataType(TransformType type);
    }
}