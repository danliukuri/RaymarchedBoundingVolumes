using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic.ShaderData.OperationType;
using RBV.Data.Static.Enumerations;

namespace RBV.Features.ShaderDataForming
{
    public interface IOperationTypeCaster
    {
        Array CastToShaderDataTypeArray(KeyValuePair<RaymarchingOperationType, List<RaymarchingOperation>> source) =>
            CastToShaderDataTypeArray(source.Key, source.Value.Select(operation => operation.TypeShaderData));

        Array CastToShaderDataTypeArray(RaymarchingOperationType type, IEnumerable<IOperationTypeShaderData> source);

        bool HasCorrespondingShaderData(RaymarchingOperationType type);

        Type GetShaderDataType(RaymarchingOperationType type);
    }
}