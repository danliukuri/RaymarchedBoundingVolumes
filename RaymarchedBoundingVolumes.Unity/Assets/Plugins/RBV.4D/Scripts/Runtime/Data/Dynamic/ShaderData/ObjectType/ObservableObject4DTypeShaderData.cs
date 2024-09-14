using System;
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
                CubicalCylinder.Changed     += value.CastCached<IObjectTypeShaderData, CubicalCylinderShaderData>();
                SphericalCylinder.Changed   += value.CastCached<IObjectTypeShaderData, SphericalCylinderShaderData>();
                EllipsoidalCylinder.Changed += value.CastCached<IObjectTypeShaderData, EllipsoidalCylinderShaderData>();
                ConicalCylinder.Changed     += value.CastCached<IObjectTypeShaderData, ConicalCylinderShaderData>();
                DoubleCylinder.Changed      += value.CastCached<IObjectTypeShaderData, DoubleCylinderShaderData>();
                DoubleEllipticCylinder.Changed +=
                    value.CastCached<IObjectTypeShaderData, DoubleEllipticCylinderShaderData>();
                PrismicCylinder.Changed += value.CastCached<IObjectTypeShaderData, PrismicCylinderShaderData>();
            }
            remove
            {
                Hypercube.Changed      -= value.CastCached<IObjectTypeShaderData, HypercubeShaderData>();
                Hypersphere.Changed    -= value.CastCached<IObjectTypeShaderData, HypersphereShaderData>();
                Hyperellipsoid.Changed -= value.CastCached<IObjectTypeShaderData, HyperellipsoidShaderData>();
                Hypercapsule.Changed   -= value.CastCached<IObjectTypeShaderData, HypercapsuleShaderData>();
                EllipsoidalHypercapsule.Changed -=
                    value.CastCached<IObjectTypeShaderData, EllipsoidalHypercapsuleShaderData>();
                CubicalCylinder.Changed     -= value.CastCached<IObjectTypeShaderData, CubicalCylinderShaderData>();
                SphericalCylinder.Changed   -= value.CastCached<IObjectTypeShaderData, SphericalCylinderShaderData>();
                EllipsoidalCylinder.Changed -= value.CastCached<IObjectTypeShaderData, EllipsoidalCylinderShaderData>();
                ConicalCylinder.Changed     -= value.CastCached<IObjectTypeShaderData, ConicalCylinderShaderData>();
                DoubleCylinder.Changed      -= value.CastCached<IObjectTypeShaderData, DoubleCylinderShaderData>();
                DoubleEllipticCylinder.Changed -=
                    value.CastCached<IObjectTypeShaderData, DoubleEllipticCylinderShaderData>();
                PrismicCylinder.Changed -= value.CastCached<IObjectTypeShaderData, PrismicCylinderShaderData>();
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

        [field: SerializeField] public ObservableValue<CubicalCylinderShaderData> CubicalCylinder { get; set; } =
            new(CubicalCylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<SphericalCylinderShaderData> SphericalCylinder { get; set; } =
            new(SphericalCylinderShaderData.Default);

        [field: SerializeField]
        public ObservableValue<EllipsoidalCylinderShaderData> EllipsoidalCylinder { get; set; } =
            new(EllipsoidalCylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<ConicalCylinderShaderData> ConicalCylinder { get; set; } =
            new(ConicalCylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<DoubleCylinderShaderData> DoubleCylinder { get; set; } =
            new(DoubleCylinderShaderData.Default);

        [field: SerializeField]
        public ObservableValue<DoubleEllipticCylinderShaderData> DoubleEllipticCylinder { get; set; } =
            new(DoubleEllipticCylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<PrismicCylinderShaderData> PrismicCylinder { get; set; } =
            new(PrismicCylinderShaderData.Default);

        public IObjectTypeShaderData GetShaderData(RaymarchedObjectType type) =>
            GetShaderData((RaymarchedObject4DType)(int)type);

        public IObjectTypeShaderData GetShaderData(RaymarchedObject4DType type)
        {
            const float fullToHalfScaleMultiplier = IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;

            switch (type)
            {
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, default);
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
                case RaymarchedObject4DType.CubicalCylinder:
                    CubicalCylinderShaderData cubicalCylinder = CubicalCylinder.Value;
                    cubicalCylinder.Diameter *= fullToHalfScaleMultiplier;
                    cubicalCylinder.Height   *= fullToHalfScaleMultiplier;
                    cubicalCylinder.Trength  *= fullToHalfScaleMultiplier;
                    return cubicalCylinder;
                case RaymarchedObject4DType.SphericalCylinder:
                    SphericalCylinderShaderData sphericalCylinder = SphericalCylinder.Value;
                    sphericalCylinder.Diameter *= fullToHalfScaleMultiplier;
                    sphericalCylinder.Trength  *= fullToHalfScaleMultiplier;
                    return sphericalCylinder;
                case RaymarchedObject4DType.EllipsoidalCylinder:
                    EllipsoidalCylinderShaderData ellipsoidalCylinder = EllipsoidalCylinder.Value;
                    ellipsoidalCylinder.Diameters *= fullToHalfScaleMultiplier;
                    ellipsoidalCylinder.Trength   *= fullToHalfScaleMultiplier;
                    return ellipsoidalCylinder;
                case RaymarchedObject4DType.ConicalCylinder:
                    ConicalCylinderShaderData conicalCylinder = ConicalCylinder.Value;
                    conicalCylinder.Diameter *= fullToHalfScaleMultiplier;
                    conicalCylinder.Trength  *= fullToHalfScaleMultiplier;
                    return conicalCylinder;
                case RaymarchedObject4DType.DoubleCylinder:
                    DoubleCylinderShaderData doubleCylinder = DoubleCylinder.Value;
                    doubleCylinder.Diameters *= fullToHalfScaleMultiplier;
                    return doubleCylinder;
                case RaymarchedObject4DType.DoubleEllipticCylinder:
                    DoubleEllipticCylinderShaderData doubleEllipticCylinder = DoubleEllipticCylinder.Value;
                    doubleEllipticCylinder.Diameters *= fullToHalfScaleMultiplier;
                    return doubleEllipticCylinder;
                case RaymarchedObject4DType.PrismicCylinder:
                    PrismicCylinderShaderData prismicCylinder = PrismicCylinder.Value;
                    prismicCylinder.Circumdiameter *= fullToHalfScaleMultiplier;
                    prismicCylinder.Length         *= fullToHalfScaleMultiplier;
                    return prismicCylinder;
            }
        }
    }
}