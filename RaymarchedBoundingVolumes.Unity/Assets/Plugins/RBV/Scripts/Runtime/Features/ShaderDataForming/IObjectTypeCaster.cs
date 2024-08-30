using System;
using System.Collections.Generic;

namespace RBV.Features.ShaderDataForming
{
    public interface IObjectTypeCaster
    {
        Array CastToShaderDataTypeArray(KeyValuePair<int, List<RaymarchedObject>> source);

        Array CastToShaderDataTypeArray(int type, IEnumerable<object> source);

        Type GetShaderDataType(int type);
    }
}