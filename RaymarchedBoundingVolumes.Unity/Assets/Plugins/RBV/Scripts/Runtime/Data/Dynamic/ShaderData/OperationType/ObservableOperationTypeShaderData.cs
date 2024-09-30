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
                SmoothUnion.Changed      += value.CastCached<RadiusDefinedOperationShaderData>();
                SmoothSubtract.Changed   += value.CastCached<RadiusDefinedOperationShaderData>();
                SmoothIntersect.Changed  += value.CastCached<RadiusDefinedOperationShaderData>();
                SmoothXor.Changed        += value.CastCached<RadiusDefinedXorOperationShaderData>();
                ChamferUnion.Changed     += value.CastCached<RadiusDefinedOperationShaderData>();
                ChamferSubtract.Changed  += value.CastCached<RadiusDefinedOperationShaderData>();
                ChamferIntersect.Changed += value.CastCached<RadiusDefinedOperationShaderData>();
                ChamferXor.Changed       += value.CastCached<RadiusDefinedXorOperationShaderData>();
                StairsUnion.Changed      += value.CastCached<ColumnsOperationShaderData>();
                StairsSubtract.Changed   += value.CastCached<ColumnsOperationShaderData>();
                StairsIntersect.Changed  += value.CastCached<ColumnsOperationShaderData>();
                StairsXor.Changed        += value.CastCached<ColumnsXorOperationShaderData>();
                Morph.Changed            += value.CastCached<RatioDefinedOperationShaderData>();
            }
            remove
            {
                SmoothUnion.Changed      -= value.CastCached<RadiusDefinedOperationShaderData>();
                SmoothSubtract.Changed   -= value.CastCached<RadiusDefinedOperationShaderData>();
                SmoothIntersect.Changed  -= value.CastCached<RadiusDefinedOperationShaderData>();
                SmoothXor.Changed        -= value.CastCached<RadiusDefinedXorOperationShaderData>();
                ChamferUnion.Changed     -= value.CastCached<RadiusDefinedOperationShaderData>();
                ChamferSubtract.Changed  -= value.CastCached<RadiusDefinedOperationShaderData>();
                ChamferIntersect.Changed -= value.CastCached<RadiusDefinedOperationShaderData>();
                ChamferXor.Changed       -= value.CastCached<RadiusDefinedXorOperationShaderData>();
                StairsUnion.Changed      -= value.CastCached<ColumnsOperationShaderData>();
                StairsSubtract.Changed   -= value.CastCached<ColumnsOperationShaderData>();
                StairsIntersect.Changed  -= value.CastCached<ColumnsOperationShaderData>();
                StairsXor.Changed        -= value.CastCached<ColumnsXorOperationShaderData>();
                Morph.Changed            -= value.CastCached<RatioDefinedOperationShaderData>();
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

        [field: SerializeField] public ObservableValue<ColumnsOperationShaderData> StairsUnion { get; set; } =
            new(ColumnsOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<ColumnsOperationShaderData> StairsSubtract { get; set; } =
            new(ColumnsOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<ColumnsOperationShaderData> StairsIntersect { get; set; } =
            new(ColumnsOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<ColumnsXorOperationShaderData> StairsXor { get; set; } =
            new(ColumnsXorOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<RatioDefinedOperationShaderData> Morph { get; set; } =
            new(RatioDefinedOperationShaderData.Default);

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
            RaymarchingOperationType.StairsUnion      => StairsUnion.Value,
            RaymarchingOperationType.StairsSubtract   => StairsSubtract.Value,
            RaymarchingOperationType.StairsIntersect  => StairsIntersect.Value,
            RaymarchingOperationType.StairsXor        => StairsXor.Value,
            RaymarchingOperationType.Morph            => Morph.Value,

            _ => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}