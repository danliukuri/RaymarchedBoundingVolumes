using System;
using System.Collections.Generic;
using RBV.Data.Dynamic.ShaderData;
using RBV.Data.Static.Enumerations;

namespace RBV.Features.ShaderDataForming
{
    public interface ITransformTypeCaster
    {
        Array CastToShaderDataTypeArray(KeyValuePair<TransformType, List<RaymarchedObject>> source);

        Array CastToShaderDataTypeArray(TransformType type, IEnumerable<ITransformShaderData> source);

        Type GetShaderDataType(TransformType type);
    }
}