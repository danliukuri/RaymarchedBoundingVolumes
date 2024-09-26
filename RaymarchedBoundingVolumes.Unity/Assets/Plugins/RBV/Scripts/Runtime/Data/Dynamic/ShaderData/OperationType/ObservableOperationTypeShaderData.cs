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
                SmoothXor.Changed += value.CastCached<IOperationTypeShaderData, RadiusDefinedXorOperationShaderData>();
                ChamferUnion.Changed += value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                ChamferSubtract.Changed +=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                ChamferIntersect.Changed +=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                ChamferXor.Changed += value.CastCached<IOperationTypeShaderData, RadiusDefinedXorOperationShaderData>();
            }
            remove
            {
                SmoothUnion.Changed -= value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                SmoothSubtract.Changed -=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                SmoothIntersect.Changed -=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                SmoothXor.Changed -= value.CastCached<IOperationTypeShaderData, RadiusDefinedXorOperationShaderData>();
                ChamferUnion.Changed -= value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                ChamferSubtract.Changed -=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                ChamferIntersect.Changed -=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                ChamferXor.Changed -= value.CastCached<IOperationTypeShaderData, RadiusDefinedXorOperationShaderData>();
            }
        }

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> SmoothUnion { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> SmoothSubtract { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> SmoothIntersect { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<RadiusDefinedXorOperationShaderData> SmoothXor { get; set; } =
            new(RadiusDefinedXorOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> ChamferUnion { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> ChamferSubtract { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        [field: SerializeField]
        public ObservableValue<RadiusDefinedOperationShaderData> ChamferIntersect { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<RadiusDefinedXorOperationShaderData> ChamferXor { get; set; } =
            new(RadiusDefinedXorOperationShaderData.Default);

        public IOperationTypeShaderData GetShaderData(RaymarchingOperationType type) => type switch
        {
            Union or Subtract or Intersect or Xor     => default,
            RaymarchingOperationType.SmoothUnion      => SmoothUnion.Value,
            RaymarchingOperationType.SmoothSubtract   => SmoothSubtract.Value,
            RaymarchingOperationType.SmoothIntersect  => SmoothIntersect.Value,
            RaymarchingOperationType.SmoothXor        => SmoothXor.Value,
            RaymarchingOperationType.ChamferUnion     => ChamferUnion.Value,
            RaymarchingOperationType.ChamferSubtract  => ChamferSubtract.Value,
            RaymarchingOperationType.ChamferIntersect => ChamferIntersect.Value,
            RaymarchingOperationType.ChamferXor       => ChamferXor.Value,

            _ => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}