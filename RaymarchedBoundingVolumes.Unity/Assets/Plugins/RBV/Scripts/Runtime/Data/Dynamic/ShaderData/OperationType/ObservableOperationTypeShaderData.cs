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
            add
            {
                SmoothUnion.Changed += value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                SmoothSubtract.Changed +=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                SmoothIntersect.Changed +=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
            }
            remove
            {
                SmoothUnion.Changed -= value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                SmoothSubtract.Changed -=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                SmoothIntersect.Changed -=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
            }
        }

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> SmoothUnion { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> SmoothSubtract { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> SmoothIntersect { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        public IOperationTypeShaderData GetShaderData(RaymarchingOperationType type) => type switch
        {
            Union or Subtract or Intersect or Xor => default(IOperationTypeShaderData),
            RaymarchingOperationType.SmoothUnion => SmoothUnion.Value,
            RaymarchingOperationType.SmoothSubtract => SmoothSubtract.Value,
            RaymarchingOperationType.SmoothIntersect => SmoothIntersect.Value,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}