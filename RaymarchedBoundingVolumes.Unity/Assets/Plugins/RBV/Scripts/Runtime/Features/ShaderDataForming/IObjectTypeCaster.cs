using System;
using System.Collections.Generic;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData.ObjectTypeRelated;

namespace RBV.Features.ShaderDataForming
{
    public interface IObjectTypeCaster
    {
        Array CastToShaderDataTypeArray(KeyValuePair<RaymarchedObjectType, List<RaymarchedObject>> source);

        Array CastToShaderDataTypeArray(RaymarchedObjectType type, IEnumerable<IObjectTypeRelatedShaderData> source);

        Type GetShaderDataType(RaymarchedObjectType type);
    }
}