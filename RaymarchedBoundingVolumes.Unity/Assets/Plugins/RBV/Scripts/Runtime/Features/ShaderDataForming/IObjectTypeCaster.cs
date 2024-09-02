using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData.ObjectType;

namespace RBV.Features.ShaderDataForming
{
    public interface IObjectTypeCaster
    {
        public Array CastToShaderDataTypeArray(KeyValuePair<RaymarchedObjectType, List<RaymarchedObject>> source) =>
            CastToShaderDataTypeArray(source.Key, source.Value.Select(obj => obj.TypeShaderData));

        Array CastToShaderDataTypeArray(RaymarchedObjectType type, IEnumerable<IObjectTypeShaderData> source);

        Type GetShaderDataType(RaymarchedObjectType type);
    }
}