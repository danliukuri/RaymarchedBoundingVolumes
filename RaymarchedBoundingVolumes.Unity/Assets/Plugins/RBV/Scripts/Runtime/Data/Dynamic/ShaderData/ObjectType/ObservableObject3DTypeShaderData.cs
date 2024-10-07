using System;
using RBV.Data.Static.Enumerations;
using RBV.Utilities.Wrappers;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public class ObservableObject3DTypeShaderData : IObservableObjectTypeShaderData
    {
        public event Action<ChangedValue<IObjectTypeShaderData>> Changed
        {
            add
            {
                Cube.Changed              += value.CastCached<CubeShaderData>();
                Sphere.Changed            += value.CastCached<SphereShaderData>();
                Ellipsoid.Changed         += value.CastCached<EllipsoidShaderData>();
                Capsule.Changed           += value.CastCached<CapsuleShaderData>();
                EllipticCapsule.Changed   += value.CastCached<EllipticCapsuleShaderData>();
                Cylinder.Changed          += value.CastCached<CylinderShaderData>();
                EllipticCylinder.Changed  += value.CastCached<EllipticCylinderShaderData>();
                Plane.Changed             += value.CastCached<PlaneShaderData>();
                Cone.Changed              += value.CastCached<CapsuleShaderData>();
                CappedCone.Changed        += value.CastCached<CappedConeShaderData>();
                Torus.Changed             += value.CastCached<TorusShaderData>();
                CappedTorus.Changed       += value.CastCached<CappedTorusShaderData>();
                RegularPrism.Changed      += value.CastCached<RegularPrismShaderData>();
                RegularPolyhedron.Changed += value.CastCached<RegularPolyhedronShaderData>();
            }
            remove
            {
                Cube.Changed              -= value.CastCached<CubeShaderData>();
                Sphere.Changed            -= value.CastCached<SphereShaderData>();
                Ellipsoid.Changed         -= value.CastCached<EllipsoidShaderData>();
                Capsule.Changed           -= value.CastCached<CapsuleShaderData>();
                EllipticCapsule.Changed   -= value.CastCached<EllipticCapsuleShaderData>();
                Cylinder.Changed          -= value.CastCached<CylinderShaderData>();
                EllipticCylinder.Changed  -= value.CastCached<EllipticCylinderShaderData>();
                Plane.Changed             -= value.CastCached<PlaneShaderData>();
                Cone.Changed              -= value.CastCached<CapsuleShaderData>();
                CappedCone.Changed        -= value.CastCached<CappedConeShaderData>();
                Torus.Changed             -= value.CastCached<TorusShaderData>();
                CappedTorus.Changed       -= value.CastCached<CappedTorusShaderData>();
                RegularPrism.Changed      -= value.CastCached<RegularPrismShaderData>();
                RegularPolyhedron.Changed -= value.CastCached<RegularPolyhedronShaderData>();
            }
        }

        [field: SerializeField] public ObservableValue<CubeShaderData> Cube { get; set; } =
            new(CubeShaderData.Default);

        [field: SerializeField] public ObservableValue<SphereShaderData> Sphere { get; set; } =
            new(SphereShaderData.Default);

        [field: SerializeField] public ObservableValue<EllipsoidShaderData> Ellipsoid { get; set; } =
            new(EllipsoidShaderData.Default);

        [field: SerializeField] public ObservableValue<CapsuleShaderData> Capsule { get; set; } =
            new(CapsuleShaderData.Default);

        [field: SerializeField] public ObservableValue<EllipticCapsuleShaderData> EllipticCapsule { get; set; } =
            new(EllipticCapsuleShaderData.Default);

        [field: SerializeField] public ObservableValue<CylinderShaderData> Cylinder { get; set; } =
            new(CylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<EllipticCylinderShaderData> EllipticCylinder { get; set; } =
            new(EllipticCylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<PlaneShaderData> Plane { get; set; } =
            new(PlaneShaderData.Default);

        [field: SerializeField] public ObservableValue<CapsuleShaderData> Cone { get; set; } =
            new(CapsuleShaderData.Default);

        [field: SerializeField] public ObservableValue<CappedConeShaderData> CappedCone { get; set; } =
            new(CappedConeShaderData.Default);

        [field: SerializeField] public ObservableValue<TorusShaderData> Torus { get; set; } =
            new(TorusShaderData.Default);

        [field: SerializeField] public ObservableValue<CappedTorusShaderData> CappedTorus { get; set; } =
            new(CappedTorusShaderData.Default);

        [field: SerializeField] public ObservableValue<RegularPrismShaderData> RegularPrism { get; set; } =
            new(RegularPrismShaderData.Default);

        [field: SerializeField] public ObservableValue<RegularPolyhedronShaderData> RegularPolyhedron { get; set; } =
            new(RegularPolyhedronShaderData.Default);

        public IObjectTypeShaderData GetShaderData(RaymarchedObjectType type) =>
            GetShaderData((RaymarchedObject3DType)(int)type);

        public IObjectTypeShaderData GetShaderData(RaymarchedObject3DType type) => type switch
        {
            RaymarchedObject3DType.Cube              => Scale(Cube.Value),
            RaymarchedObject3DType.Sphere            => Scale(Sphere.Value),
            RaymarchedObject3DType.Ellipsoid         => Scale(Ellipsoid.Value),
            RaymarchedObject3DType.Capsule           => Scale(Capsule.Value),
            RaymarchedObject3DType.EllipticCapsule   => Scale(EllipticCapsule.Value),
            RaymarchedObject3DType.Cylinder          => Scale(Cylinder.Value),
            RaymarchedObject3DType.EllipticCylinder  => Scale(EllipticCylinder.Value),
            RaymarchedObject3DType.Plane             => Scale(Plane.Value),
            RaymarchedObject3DType.Cone              => Scale(Cone.Value),
            RaymarchedObject3DType.CappedCone        => Scale(CappedCone.Value),
            RaymarchedObject3DType.Torus             => Scale(Torus.Value),
            RaymarchedObject3DType.CappedTorus       => Scale(CappedTorus.Value),
            RaymarchedObject3DType.RegularPrism      => Scale(RegularPrism.Value),
            RaymarchedObject3DType.RegularPolyhedron => Scale(RegularPolyhedron.Value),

            _ => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };

        private static CubeShaderData Scale(CubeShaderData cube)
        {
            cube.Dimensions *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return cube;
        }

        private static SphereShaderData Scale(SphereShaderData sphere)
        {
            sphere.Diameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return sphere;
        }

        private static EllipsoidShaderData Scale(EllipsoidShaderData ellipsoid)
        {
            ellipsoid.Diameters *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return ellipsoid;
        }

        private static CapsuleShaderData Scale(CapsuleShaderData capsule)
        {
            capsule.Height = ScaleCapsuleHeightData(capsule.Height);
            capsule.Base   = Scale(capsule.Base);
            return capsule;
        }

        private static float ScaleCapsuleHeightData(float height) =>
            height * IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;

        private static EllipticCapsuleShaderData Scale(EllipticCapsuleShaderData ellipticCapsule)
        {
            ellipticCapsule.Height    = ScaleCapsuleHeightData(ellipticCapsule.Height);
            ellipticCapsule.Ellipsoid = Scale(ellipticCapsule.Ellipsoid);
            return ellipticCapsule;
        }

        private static CylinderShaderData Scale(CylinderShaderData cylinder)
        {
            cylinder.Base = Scale(cylinder.Base);
            return cylinder;
        }

        private static EllipticCylinderShaderData Scale(EllipticCylinderShaderData ellipticCylinder)
        {
            ellipticCylinder.Dimensions = new Vector3(
                ellipticCylinder.Dimensions.x * IObservableObjectTypeShaderData.FullToHalfScaleMultiplier,
                ellipticCylinder.Dimensions.y,
                ellipticCylinder.Dimensions.z * IObservableObjectTypeShaderData.FullToHalfScaleMultiplier
            );
            return ellipticCylinder;
        }

        private static PlaneShaderData Scale(PlaneShaderData plane)
        {
            plane.Dimensions   *= PlaneShaderData.ScaleMultiplier;
            plane.Dimensions.y =  PlaneShaderData.Thickness;
            plane.Dimensions   *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return plane;
        }

        private static CappedConeShaderData Scale(CappedConeShaderData cappedCone)
        {
            cappedCone.Height             =  ScaleCapsuleHeightData(cappedCone.Height);
            cappedCone.TopBaseDiameter    *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            cappedCone.BottomBaseDiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return cappedCone;
        }

        private static TorusShaderData Scale(TorusShaderData torus)
        {
            torus.MajorDiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            torus.MinorDiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return torus;
        }

        private static CappedTorusShaderData Scale(CappedTorusShaderData cappedTorus)
        {
            cappedTorus.CapAngle *= Mathf.Deg2Rad * IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            cappedTorus.Torus    =  Scale(cappedTorus.Torus);
            return cappedTorus;
        }

        private static RegularPrismShaderData Scale(RegularPrismShaderData regularPrism)
        {
            regularPrism.Circumdiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            regularPrism.Length         *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return regularPrism;
        }

        private static RegularPolyhedronShaderData Scale(RegularPolyhedronShaderData regularPolyhedron)
        {
            regularPolyhedron.InscribedDiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return regularPolyhedron;
        }
    }
}