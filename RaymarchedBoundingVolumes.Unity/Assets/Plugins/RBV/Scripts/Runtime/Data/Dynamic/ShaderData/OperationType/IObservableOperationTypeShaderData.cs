using System;
using RBV.Data.Static.Enumerations;
using RBV.Utilities.Wrappers;

namespace RBV.Data.Dynamic.ShaderData.OperationType
{
    public interface IObservableOperationTypeShaderData
    {
        event Action<ChangedValue<IOperationTypeShaderData>> Changed;

        IOperationTypeShaderData GetShaderData(RaymarchingOperationType type);
    }
}