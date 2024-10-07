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
                Hypercube.Changed               += value.CastCached<HypercubeShaderData>();
                Hypersphere.Changed             += value.CastCached<HypersphereShaderData>();
                Hyperellipsoid.Changed          += value.CastCached<HyperellipsoidShaderData>();
                Hypercapsule.Changed            += value.CastCached<HypercapsuleShaderData>();
                EllipsoidalHypercapsule.Changed += value.CastCached<EllipsoidalHypercapsuleShaderData>();
                CubicalCylinder.Changed         += value.CastCached<CubicalCylinderShaderData>();
                SphericalCylinder.Changed       += value.CastCached<SphericalCylinderShaderData>();
                EllipsoidalCylinder.Changed     += value.CastCached<EllipsoidalCylinderShaderData>();
                ConicalCylinder.Changed         += value.CastCached<CubicalCylinderShaderData>();
                DoubleCylinder.Changed          += value.CastCached<DoubleCylinderShaderData>();
                DoubleEllipticCylinder.Changed  += value.CastCached<DoubleEllipticCylinderShaderData>();
                PrismicCylinder.Changed         += value.CastCached<PrismicCylinderShaderData>();
                SphericalCone.Changed           += value.CastCached<SphericalCylinderShaderData>();
                CylindricalCone.Changed         += value.CastCached<SphericalCylinderShaderData>();
                ToroidalSphere.Changed          += value.CastCached<TorusShaderData>();
                SphericalTorus.Changed          += value.CastCached<TorusShaderData>();
                DoubleTorus.Changed             += value.CastCached<DoubleTorusShaderData>();
                Tiger.Changed                   += value.CastCached<TigerShaderData>();
                RegularDoublePrism.Changed      += value.CastCached<RegularDoublePrismShaderData>();
            }
            remove
            {
                Hypercube.Changed               -= value.CastCached<HypercubeShaderData>();
                Hypersphere.Changed             -= value.CastCached<HypersphereShaderData>();
                Hyperellipsoid.Changed          -= value.CastCached<HyperellipsoidShaderData>();
                Hypercapsule.Changed            -= value.CastCached<HypercapsuleShaderData>();
                EllipsoidalHypercapsule.Changed -= value.CastCached<EllipsoidalHypercapsuleShaderData>();
                CubicalCylinder.Changed         -= value.CastCached<CubicalCylinderShaderData>();
                SphericalCylinder.Changed       -= value.CastCached<SphericalCylinderShaderData>();
                EllipsoidalCylinder.Changed     -= value.CastCached<EllipsoidalCylinderShaderData>();
                ConicalCylinder.Changed         -= value.CastCached<CubicalCylinderShaderData>();
                DoubleCylinder.Changed          -= value.CastCached<DoubleCylinderShaderData>();
                DoubleEllipticCylinder.Changed  -= value.CastCached<DoubleEllipticCylinderShaderData>();
                PrismicCylinder.Changed         -= value.CastCached<PrismicCylinderShaderData>();
                SphericalCone.Changed           -= value.CastCached<SphericalCylinderShaderData>();
                CylindricalCone.Changed         -= value.CastCached<SphericalCylinderShaderData>();
                ToroidalSphere.Changed          -= value.CastCached<TorusShaderData>();
                SphericalTorus.Changed          -= value.CastCached<TorusShaderData>();
                DoubleTorus.Changed             -= value.CastCached<DoubleTorusShaderData>();
                Tiger.Changed                   -= value.CastCached<TigerShaderData>();
                RegularDoublePrism.Changed      -= value.CastCached<RegularDoublePrismShaderData>();
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

        public IObjectTypeShaderData GetShaderData(RaymarchedObject4DType type) => type switch
        {
            RaymarchedObject4DType.Hypercube               => Scale(Hypercube.Value),
            RaymarchedObject4DType.Hypersphere             => Scale(Hypersphere.Value),
            RaymarchedObject4DType.Hyperellipsoid          => Scale(Hyperellipsoid.Value),
            RaymarchedObject4DType.Hypercapsule            => Scale(Hypercapsule.Value),
            RaymarchedObject4DType.EllipsoidalHypercapsule => Scale(EllipsoidalHypercapsule.Value),
            RaymarchedObject4DType.CubicalCylinder         => Scale(CubicalCylinder.Value),
            RaymarchedObject4DType.SphericalCylinder       => Scale(SphericalCylinder.Value),
            RaymarchedObject4DType.EllipsoidalCylinder     => Scale(EllipsoidalCylinder.Value),
            RaymarchedObject4DType.ConicalCylinder         => Scale(ConicalCylinder.Value),
            RaymarchedObject4DType.DoubleCylinder          => Scale(DoubleCylinder.Value),
            RaymarchedObject4DType.DoubleEllipticCylinder  => Scale(DoubleEllipticCylinder.Value),
            RaymarchedObject4DType.PrismicCylinder         => Scale(PrismicCylinder.Value),
            RaymarchedObject4DType.SphericalCone           => Scale(SphericalCone.Value),
            RaymarchedObject4DType.CylindricalCone         => Scale(CylindricalCone.Value),
            RaymarchedObject4DType.ToroidalSphere          => Scale(ToroidalSphere.Value),
            RaymarchedObject4DType.SphericalTorus          => Scale(SphericalTorus.Value),
            RaymarchedObject4DType.DoubleTorus             => Scale(DoubleTorus.Value),
            RaymarchedObject4DType.Tiger                   => Scale(Tiger.Value),
            RaymarchedObject4DType.RegularDoublePrism      => Scale(RegularDoublePrism.Value),

            _ => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };

        private static HypercubeShaderData Scale(HypercubeShaderData hypercube)
        {
            hypercube.Dimensions *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return hypercube;
        }

        private static HypersphereShaderData Scale(HypersphereShaderData hypersphere)
        {
            hypersphere.Diameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return hypersphere;
        }

        private static HyperellipsoidShaderData Scale(HyperellipsoidShaderData hyperellipsoid)
        {
            hyperellipsoid.Diameters *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return hyperellipsoid;
        }

        private static HypercapsuleShaderData Scale(HypercapsuleShaderData hypercapsule)
        {
            hypercapsule.Height *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            hypercapsule.Base   =  Scale(hypercapsule.Base);
            return hypercapsule;
        }

        private static EllipsoidalHypercapsuleShaderData Scale(
            EllipsoidalHypercapsuleShaderData ellipsoidalHypercapsule)
        {
            ellipsoidalHypercapsule.Height *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            ellipsoidalHypercapsule.Base   =  Scale(ellipsoidalHypercapsule.Base);
            return ellipsoidalHypercapsule;
        }

        private static CubicalCylinderShaderData Scale(CubicalCylinderShaderData cubicalCylinder)
        {
            cubicalCylinder.Height            *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            cubicalCylinder.SphericalCylinder =  Scale(cubicalCylinder.SphericalCylinder);
            return cubicalCylinder;
        }

        private static SphericalCylinderShaderData Scale(SphericalCylinderShaderData sphericalCylinder)
        {
            sphericalCylinder.Base    =  Scale(sphericalCylinder.Base);
            sphericalCylinder.Trength *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return sphericalCylinder;
        }

        private static EllipsoidalCylinderShaderData Scale(EllipsoidalCylinderShaderData ellipsoidalCylinder)
        {
            ellipsoidalCylinder.Base    =  Scale(ellipsoidalCylinder.Base);
            ellipsoidalCylinder.Trength *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return ellipsoidalCylinder;
        }

        private static DoubleCylinderShaderData Scale(DoubleCylinderShaderData doubleCylinder)
        {
            doubleCylinder.Diameters *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return doubleCylinder;
        }

        private static DoubleEllipticCylinderShaderData Scale(DoubleEllipticCylinderShaderData doubleEllipticCylinder)
        {
            doubleEllipticCylinder.Diameters *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return doubleEllipticCylinder;
        }

        private static PrismicCylinderShaderData Scale(PrismicCylinderShaderData prismicCylinder)
        {
            prismicCylinder.Circumdiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            prismicCylinder.Length         *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return prismicCylinder;
        }

        private static TorusShaderData Scale(TorusShaderData sphericalTorus)
        {
            sphericalTorus.MajorDiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            sphericalTorus.MinorDiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return sphericalTorus;
        }

        private static DoubleTorusShaderData Scale(DoubleTorusShaderData doubleTorus)
        {
            doubleTorus.MajorMajorDiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            doubleTorus.MajorMinorDiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            doubleTorus.MinorMinorDiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return doubleTorus;
        }

        private static TigerShaderData Scale(TigerShaderData tiger)
        {
            tiger.MajorDiameters *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            tiger.MinorDiameter  *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return tiger;
        }

        private static RegularDoublePrismShaderData Scale(RegularDoublePrismShaderData regularDoublePrism)
        {
            regularDoublePrism.Circumdiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return regularDoublePrism;
        }
    }
}