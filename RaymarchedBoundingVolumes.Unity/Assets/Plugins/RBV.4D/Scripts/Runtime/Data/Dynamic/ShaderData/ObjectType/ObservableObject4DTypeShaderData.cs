﻿using System;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static.Enumerations;
using RBV.Utilities.Wrappers;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public class ObservableObject4DTypeShaderData : IObservableObjectTypeShaderData
    {
        public event Action<ChangedValue<IObjectTypeShaderData>> Changed
        {
            add
            {
                Hypercube.Changed   += value.CastCached<IObjectTypeShaderData, HypercubeShaderData>();
                Hypersphere.Changed += value.CastCached<IObjectTypeShaderData, HypersphereShaderData>();
            }
            remove
            {
                Hypercube.Changed   -= value.CastCached<IObjectTypeShaderData, HypercubeShaderData>();
                Hypersphere.Changed -= value.CastCached<IObjectTypeShaderData, HypersphereShaderData>();
            }
        }

        [field: SerializeField] public ObservableValue<HypercubeShaderData> Hypercube { get; set; } =
            new(HypercubeShaderData.Default);

        [field: SerializeField] public ObservableValue<HypersphereShaderData> Hypersphere { get; set; } =
            new(HypersphereShaderData.Default);

        public IObjectTypeShaderData GetShaderData(RaymarchedObjectType type) =>
            GetShaderData((RaymarchedObject4DType)(int)type);

        public IObjectTypeShaderData GetShaderData(RaymarchedObject4DType type)
        {
            const float fullToHalfScaleMultiplier = IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;

            switch (type)
            {
                case RaymarchedObject4DType.Hypercube:
                    HypercubeShaderData hypercube = Hypercube.Value;
                    hypercube.Dimensions *= fullToHalfScaleMultiplier;
                    return hypercube;
                case RaymarchedObject4DType.Hypersphere:
                    HypersphereShaderData hypersphere = Hypersphere.Value;
                    hypersphere.Diameter *= fullToHalfScaleMultiplier;
                    return hypersphere;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, default);
            }
        }
    }
}