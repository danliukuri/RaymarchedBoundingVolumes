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
                Hypercube.Changed      += value.CastCached<IObjectTypeShaderData, HypercubeShaderData>();
                Hypersphere.Changed    += value.CastCached<IObjectTypeShaderData, HypersphereShaderData>();
                Hyperellipsoid.Changed += value.CastCached<IObjectTypeShaderData, HyperellipsoidShaderData>();
                Hypercapsule.Changed   += value.CastCached<IObjectTypeShaderData, HypercapsuleShaderData>();
                EllipsoidalHypercapsule.Changed +=
                    value.CastCached<IObjectTypeShaderData, EllipsoidalHypercapsuleShaderData>();
            }
            remove
            {
                Hypercube.Changed      -= value.CastCached<IObjectTypeShaderData, HypercubeShaderData>();
                Hypersphere.Changed    -= value.CastCached<IObjectTypeShaderData, HypersphereShaderData>();
                Hyperellipsoid.Changed -= value.CastCached<IObjectTypeShaderData, HyperellipsoidShaderData>();
                Hypercapsule.Changed   -= value.CastCached<IObjectTypeShaderData, HypercapsuleShaderData>();
                EllipsoidalHypercapsule.Changed -=
                    value.CastCached<IObjectTypeShaderData, EllipsoidalHypercapsuleShaderData>();
            }
        }

        [field: SerializeField] public ObservableValue<HypercubeShaderData> Hypercube { get; set; } =
            new(HypercubeShaderData.Default);

        [field: SerializeField] public ObservableValue<HypersphereShaderData> Hypersphere { get; set; } =
            new(HypersphereShaderData.Default);

        [field: SerializeField] public ObservableValue<HyperellipsoidShaderData> Hyperellipsoid { get; set; } =
            new(HyperellipsoidShaderData.Default);

        [field: SerializeField] public ObservableValue<HypercapsuleShaderData> Hypercapsule { get; set; } =
            new(HypercapsuleShaderData.Default);

        [field: SerializeField]
        public ObservableValue<EllipsoidalHypercapsuleShaderData> EllipsoidalHypercapsule { get; set; } =
            new(EllipsoidalHypercapsuleShaderData.Default);

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
                case RaymarchedObject4DType.Hyperellipsoid:
                    HyperellipsoidShaderData hyperellipsoid = Hyperellipsoid.Value;
                    hyperellipsoid.Diameters *= fullToHalfScaleMultiplier;
                    return hyperellipsoid;
                case RaymarchedObject4DType.Hypercapsule:
                    HypercapsuleShaderData hypercapsule = Hypercapsule.Value;
                    hypercapsule.Height   *= fullToHalfScaleMultiplier;
                    hypercapsule.Diameter *= fullToHalfScaleMultiplier;
                    return hypercapsule;
                case RaymarchedObject4DType.EllipsoidalHypercapsule:
                    EllipsoidalHypercapsuleShaderData ellipsoidalHypercapsule = EllipsoidalHypercapsule.Value;
                    ellipsoidalHypercapsule.Height    *= fullToHalfScaleMultiplier;
                    ellipsoidalHypercapsule.Diameters *= fullToHalfScaleMultiplier;
                    return ellipsoidalHypercapsule;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, default);
            }
        }
    }
}