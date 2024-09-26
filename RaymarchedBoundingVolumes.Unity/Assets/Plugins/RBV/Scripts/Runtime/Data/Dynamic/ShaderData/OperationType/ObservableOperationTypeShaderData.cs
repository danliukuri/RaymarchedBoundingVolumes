﻿using System;
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
                SmoothXor.Changed    += value.CastCached<IOperationTypeShaderData, SmoothXorOperationShaderData>();
                ChamferUnion.Changed += value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                ChamferSubtract.Changed +=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
            }
            remove
            {
                SmoothUnion.Changed -= value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                SmoothSubtract.Changed -=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                SmoothIntersect.Changed -=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                SmoothXor.Changed    -= value.CastCached<IOperationTypeShaderData, SmoothXorOperationShaderData>();
                ChamferUnion.Changed -= value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
                ChamferSubtract.Changed -=
                    value.CastCached<IOperationTypeShaderData, RadiusDefinedOperationShaderData>();
            }
        }

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> SmoothUnion { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> SmoothSubtract { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> SmoothIntersect { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<SmoothXorOperationShaderData> SmoothXor { get; set; } =
            new(SmoothXorOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> ChamferUnion { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        [field: SerializeField] public ObservableValue<RadiusDefinedOperationShaderData> ChamferSubtract { get; set; } =
            new(RadiusDefinedOperationShaderData.Default);

        public IOperationTypeShaderData GetShaderData(RaymarchingOperationType type) => type switch
        {
            Union or Subtract or Intersect or Xor    => default,
            RaymarchingOperationType.SmoothUnion     => SmoothUnion.Value,
            RaymarchingOperationType.SmoothSubtract  => SmoothSubtract.Value,
            RaymarchingOperationType.SmoothIntersect => SmoothIntersect.Value,
            RaymarchingOperationType.SmoothXor       => SmoothXor.Value,
            RaymarchingOperationType.ChamferUnion    => ChamferUnion.Value,
            RaymarchingOperationType.ChamferSubtract => ChamferSubtract.Value,

            _ => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}