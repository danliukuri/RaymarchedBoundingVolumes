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
                ConicalCylinder.Changed     += value.CastCached<IObjectTypeShaderData, CubicalCylinderShaderData>();
                DoubleCylinder.Changed      += value.CastCached<IObjectTypeShaderData, DoubleCylinderShaderData>();
                DoubleEllipticCylinder.Changed +=
                    value.CastCached<IObjectTypeShaderData, DoubleEllipticCylinderShaderData>();
                PrismicCylinder.Changed    += value.CastCached<IObjectTypeShaderData, PrismicCylinderShaderData>();
                SphericalCone.Changed      += value.CastCached<IObjectTypeShaderData, SphericalCylinderShaderData>();
                CylindricalCone.Changed    += value.CastCached<IObjectTypeShaderData, SphericalCylinderShaderData>();
                ToroidalSphere.Changed     += value.CastCached<IObjectTypeShaderData, TorusShaderData>();
                SphericalTorus.Changed     += value.CastCached<IObjectTypeShaderData, TorusShaderData>();
                DoubleTorus.Changed        += value.CastCached<IObjectTypeShaderData, DoubleTorusShaderData>();
                Tiger.Changed              += value.CastCached<IObjectTypeShaderData, TigerShaderData>();
                RegularDoublePrism.Changed += value.CastCached<IObjectTypeShaderData, RegularDoublePrismShaderData>();
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
                ConicalCylinder.Changed     -= value.CastCached<IObjectTypeShaderData, CubicalCylinderShaderData>();
                DoubleCylinder.Changed      -= value.CastCached<IObjectTypeShaderData, DoubleCylinderShaderData>();
                DoubleEllipticCylinder.Changed -=
                    value.CastCached<IObjectTypeShaderData, DoubleEllipticCylinderShaderData>();
                PrismicCylinder.Changed    -= value.CastCached<IObjectTypeShaderData, PrismicCylinderShaderData>();
                SphericalCone.Changed      -= value.CastCached<IObjectTypeShaderData, SphericalCylinderShaderData>();
                CylindricalCone.Changed    -= value.CastCached<IObjectTypeShaderData, SphericalCylinderShaderData>();
                ToroidalSphere.Changed     -= value.CastCached<IObjectTypeShaderData, TorusShaderData>();
                SphericalTorus.Changed     -= value.CastCached<IObjectTypeShaderData, TorusShaderData>();
                DoubleTorus.Changed        -= value.CastCached<IObjectTypeShaderData, DoubleTorusShaderData>();
                Tiger.Changed              -= value.CastCached<IObjectTypeShaderData, TigerShaderData>();
                RegularDoublePrism.Changed -= value.CastCached<IObjectTypeShaderData, RegularDoublePrismShaderData>();
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

        [field: SerializeField] public ObservableValue<CubicalCylinderShaderData> ConicalCylinder { get; set; } =
            new(CubicalCylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<DoubleCylinderShaderData> DoubleCylinder { get; set; } =
            new(DoubleCylinderShaderData.Default);

        [field: SerializeField]
        public ObservableValue<DoubleEllipticCylinderShaderData> DoubleEllipticCylinder { get; set; } =
            new(DoubleEllipticCylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<PrismicCylinderShaderData> PrismicCylinder { get; set; } =
            new(PrismicCylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<SphericalCylinderShaderData> SphericalCone { get; set; } =
            new(SphericalCylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<SphericalCylinderShaderData> CylindricalCone { get; set; } =
            new(SphericalCylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<TorusShaderData> ToroidalSphere { get; set; } =
            new(TorusShaderData.Default);

        [field: SerializeField] public ObservableValue<TorusShaderData> SphericalTorus { get; set; } =
            new(TorusShaderData.Default);

        [field: SerializeField] public ObservableValue<DoubleTorusShaderData> DoubleTorus { get; set; } =
            new(DoubleTorusShaderData.Default);

        [field: SerializeField] public ObservableValue<TigerShaderData> Tiger { get; set; } =
            new(TigerShaderData.Default);

        [field: SerializeField] public ObservableValue<RegularDoublePrismShaderData> RegularDoublePrism { get; set; } =
            new(RegularDoublePrismShaderData.Default);

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
                    return ScaleHypersphereData(Hypersphere.Value);
                case RaymarchedObject4DType.Hyperellipsoid:
                    return ScaleHyperellipsoidData(Hyperellipsoid.Value);
                case RaymarchedObject4DType.Hypercapsule:
                    HypercapsuleShaderData hypercapsule = Hypercapsule.Value;
                    hypercapsule.Height *= fullToHalfScaleMultiplier;
                    hypercapsule.Base   =  ScaleHypersphereData(hypercapsule.Base);
                    return hypercapsule;
                case RaymarchedObject4DType.EllipsoidalHypercapsule:
                    EllipsoidalHypercapsuleShaderData ellipsoidalHypercapsule = EllipsoidalHypercapsule.Value;
                    ellipsoidalHypercapsule.Height *= fullToHalfScaleMultiplier;
                    ellipsoidalHypercapsule.Base   =  ScaleHyperellipsoidData(ellipsoidalHypercapsule.Base);
                    return ellipsoidalHypercapsule;
                case RaymarchedObject4DType.CubicalCylinder:
                    return ScaleCubicalCylinderData(CubicalCylinder.Value);
                case RaymarchedObject4DType.SphericalCylinder:
                    return ScaleSphericalCylinderData(SphericalCylinder.Value);
                case RaymarchedObject4DType.EllipsoidalCylinder:
                    EllipsoidalCylinderShaderData ellipsoidalCylinder = EllipsoidalCylinder.Value;
                    ellipsoidalCylinder.Base    =  ScaleHyperellipsoidData(ellipsoidalCylinder.Base);
                    ellipsoidalCylinder.Trength *= fullToHalfScaleMultiplier;
                    return ellipsoidalCylinder;
                case RaymarchedObject4DType.ConicalCylinder:
                    return ScaleCubicalCylinderData(ConicalCylinder.Value);
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
                case RaymarchedObject4DType.SphericalCone:
                    return ScaleSphericalCylinderData(SphericalCone.Value);
                case RaymarchedObject4DType.CylindricalCone:
                    return ScaleSphericalCylinderData(CylindricalCone.Value);
                    ;
                case RaymarchedObject4DType.ToroidalSphere:
                    TorusShaderData toroidalSphere = ToroidalSphere.Value;
                    toroidalSphere.MajorDiameter *= fullToHalfScaleMultiplier;
                    toroidalSphere.MinorDiameter *= fullToHalfScaleMultiplier;
                    return toroidalSphere;
                case RaymarchedObject4DType.SphericalTorus:
                    TorusShaderData sphericalTorus = SphericalTorus.Value;
                    sphericalTorus.MajorDiameter *= fullToHalfScaleMultiplier;
                    sphericalTorus.MinorDiameter *= fullToHalfScaleMultiplier;
                    return sphericalTorus;
                case RaymarchedObject4DType.DoubleTorus:
                    DoubleTorusShaderData doubleTorus = DoubleTorus.Value;
                    doubleTorus.MajorMajorDiameter *= fullToHalfScaleMultiplier;
                    doubleTorus.MajorMinorDiameter *= fullToHalfScaleMultiplier;
                    doubleTorus.MinorMinorDiameter *= fullToHalfScaleMultiplier;
                    return doubleTorus;
                case RaymarchedObject4DType.Tiger:
                    TigerShaderData tiger = Tiger.Value;
                    tiger.MajorDiameters *= fullToHalfScaleMultiplier;
                    tiger.MinorDiameter  *= fullToHalfScaleMultiplier;
                    return tiger;
                case RaymarchedObject4DType.RegularDoublePrism:
                    RegularDoublePrismShaderData regularDoublePrism = RegularDoublePrism.Value;
                    regularDoublePrism.Circumdiameter *= fullToHalfScaleMultiplier;
                    return regularDoublePrism;
            }
        }

        private static CubicalCylinderShaderData ScaleCubicalCylinderData(CubicalCylinderShaderData cubicalCylinder)
        {
            cubicalCylinder.Height            *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            cubicalCylinder.SphericalCylinder =  ScaleSphericalCylinderData(cubicalCylinder.SphericalCylinder);
            return cubicalCylinder;
        }

        private static SphericalCylinderShaderData ScaleSphericalCylinderData(
            SphericalCylinderShaderData sphericalCylinder)
        {
            sphericalCylinder.Base    =  ScaleHypersphereData(sphericalCylinder.Base);
            sphericalCylinder.Trength *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return sphericalCylinder;
        }

        private static HyperellipsoidShaderData ScaleHyperellipsoidData(HyperellipsoidShaderData hyperellipsoid)
        {
            hyperellipsoid.Diameters *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return hyperellipsoid;
        }

        private static HypersphereShaderData ScaleHypersphereData(HypersphereShaderData hypersphere)
        {
            hypersphere.Diameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return hypersphere;
        }
    }
}