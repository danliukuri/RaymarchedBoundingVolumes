using System;
using RBV.Data.Static.Enumerations;
using RBV.Utilities.Wrappers;
using UnityEngine;
using static RBV.Data.Static.Enumerations.RaymarchingOperationType;

namespace RBV.Data.Dynamic.ShaderData.OperationType
{
    [Serializable]
    public class ObservableOperationTypeShaderData : IObservableOperationTypeShaderData
    {
        public event Action<ChangedValue<IOperationTypeShaderData>> Changed
        {
            add =>
                Blend.Changed += value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
            remove =>
                Blend.Changed -= value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
        }

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> Blend { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        public IOperationTypeShaderData GetShaderData(RaymarchingOperationType type) => type switch
        {
            Union or Subtract or Intersect => default(IOperationTypeShaderData),
            RaymarchingOperationType.Blend => Blend.Value,
            _                              => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}