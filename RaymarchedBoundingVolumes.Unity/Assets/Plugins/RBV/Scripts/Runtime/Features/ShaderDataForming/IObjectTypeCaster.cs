using System;
using System.Collections.Generic;
using RBV.Data.Dynamic;

namespace RBV.Features.ShaderDataForming
{
    public interface IObjectTypeCaster
    {
        Array CastToShaderDataTypeArray(KeyValuePair<RaymarchedObjectType, List<RaymarchedObject>> source);

        Array CastToShaderDataTypeArray(RaymarchedObjectType type, IEnumerable<object> source);

        Type GetShaderDataType(RaymarchedObjectType type);
    }
}