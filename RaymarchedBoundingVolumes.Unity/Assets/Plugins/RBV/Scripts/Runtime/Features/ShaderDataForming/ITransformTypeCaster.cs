using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic.ShaderData;
using RBV.Data.Static.Enumerations;

namespace RBV.Features.ShaderDataForming
{
    public interface ITransformTypeCaster
    {
        public Array CastToShaderDataTypeArray(KeyValuePair<TransformType, List<RaymarchedObject>> source) =>
            CastToShaderDataTypeArray(source.Key, source.Value.Select(obj => obj.TransformShaderData));

        Array CastToShaderDataTypeArray(TransformType type, IEnumerable<ITransformShaderData> source);

        Type GetShaderDataType(TransformType type);
    }
}