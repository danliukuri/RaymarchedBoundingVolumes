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
                Cube.Changed              += value.CastCached<IObjectTypeShaderData, CubeShaderData>();
                Sphere.Changed            += value.CastCached<IObjectTypeShaderData, SphereShaderData>();
                Ellipsoid.Changed         += value.CastCached<IObjectTypeShaderData, EllipsoidShaderData>();
                Capsule.Changed           += value.CastCached<IObjectTypeShaderData, CapsuleShaderData>();
                EllipticCapsule.Changed   += value.CastCached<IObjectTypeShaderData, EllipticCapsuleShaderData>();
                Cylinder.Changed          += value.CastCached<IObjectTypeShaderData, CylinderShaderData>();
                EllipticCylinder.Changed  += value.CastCached<IObjectTypeShaderData, EllipticCylinderShaderData>();
                Plane.Changed             += value.CastCached<IObjectTypeShaderData, PlaneShaderData>();
                Cone.Changed              += value.CastCached<IObjectTypeShaderData, CapsuleShaderData>();
                CappedCone.Changed        += value.CastCached<IObjectTypeShaderData, CappedConeShaderData>();
                Torus.Changed             += value.CastCached<IObjectTypeShaderData, TorusShaderData>();
                CappedTorus.Changed       += value.CastCached<IObjectTypeShaderData, CappedTorusShaderData>();
                RegularPrism.Changed      += value.CastCached<IObjectTypeShaderData, RegularPrismShaderData>();
                RegularPolyhedron.Changed += value.CastCached<IObjectTypeShaderData, RegularPolyhedronShaderData>();
            }
            remove
            {
                Cube.Changed              -= value.CastCached<IObjectTypeShaderData, CubeShaderData>();
                Sphere.Changed            -= value.CastCached<IObjectTypeShaderData, SphereShaderData>();
                Ellipsoid.Changed         -= value.CastCached<IObjectTypeShaderData, EllipsoidShaderData>();
                Capsule.Changed           -= value.CastCached<IObjectTypeShaderData, CapsuleShaderData>();
                EllipticCapsule.Changed   -= value.CastCached<IObjectTypeShaderData, EllipticCapsuleShaderData>();
                Cylinder.Changed          -= value.CastCached<IObjectTypeShaderData, CylinderShaderData>();
                EllipticCylinder.Changed  -= value.CastCached<IObjectTypeShaderData, EllipticCylinderShaderData>();
                Plane.Changed             -= value.CastCached<IObjectTypeShaderData, PlaneShaderData>();
                Cone.Changed              -= value.CastCached<IObjectTypeShaderData, CapsuleShaderData>();
                CappedCone.Changed        -= value.CastCached<IObjectTypeShaderData, CappedConeShaderData>();
                Torus.Changed             -= value.CastCached<IObjectTypeShaderData, TorusShaderData>();
                CappedTorus.Changed       -= value.CastCached<IObjectTypeShaderData, CappedTorusShaderData>();
                RegularPrism.Changed      -= value.CastCached<IObjectTypeShaderData, RegularPrismShaderData>();
                RegularPolyhedron.Changed -= value.CastCached<IObjectTypeShaderData, RegularPolyhedronShaderData>();
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

        public IObjectTypeShaderData GetShaderData(RaymarchedObject3DType type)
        {
            const float fullToHalfScaleMultiplier =
                IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;

            switch (type)
            {
                case RaymarchedObject3DType.Cube:
                    CubeShaderData cube = Cube.Value;
                    cube.Dimensions *= fullToHalfScaleMultiplier;
                    return cube;
                case RaymarchedObject3DType.Sphere:
                    return ScaleSphereData(Sphere.Value);
                case RaymarchedObject3DType.Ellipsoid:
                    return ScaleEllipsoidData(Ellipsoid.Value);
                case RaymarchedObject3DType.Capsule:
                    return ScaleCapsuleData(Capsule.Value);
                case RaymarchedObject3DType.EllipticCapsule:
                    EllipticCapsuleShaderData ellipticCapsule = EllipticCapsule.Value;
                    ellipticCapsule.Height    = ScaleCapsuleHeightData(ellipticCapsule.Height);
                    ellipticCapsule.Ellipsoid = ScaleEllipsoidData(ellipticCapsule.Ellipsoid);
                    return ellipticCapsule;
                case RaymarchedObject3DType.Cylinder:
                    CylinderShaderData cylinder = Cylinder.Value;
                    cylinder.Base = ScaleSphereData(cylinder.Base);
                    return cylinder;
                case RaymarchedObject3DType.EllipticCylinder:
                    EllipticCylinderShaderData ellipticCylinder = EllipticCylinder.Value;
                    ellipticCylinder.Dimensions = new Vector3(
                        ellipticCylinder.Dimensions.x * fullToHalfScaleMultiplier,
                        ellipticCylinder.Dimensions.y,
                        ellipticCylinder.Dimensions.z * fullToHalfScaleMultiplier
                    );
                    return ellipticCylinder;
                case RaymarchedObject3DType.Plane:
                    PlaneShaderData plane = Plane.Value;
                    plane.Dimensions   *= PlaneShaderData.ScaleMultiplier;
                    plane.Dimensions.y =  PlaneShaderData.Thickness;
                    plane.Dimensions   *= fullToHalfScaleMultiplier;
                    return plane;
                case RaymarchedObject3DType.Cone:
                    return ScaleCapsuleData(Cone.Value);
                case RaymarchedObject3DType.CappedCone:
                    CappedConeShaderData cappedCone = CappedCone.Value;
                    cappedCone.Height             =  ScaleCapsuleHeightData(cappedCone.Height);
                    cappedCone.TopBaseDiameter    *= fullToHalfScaleMultiplier;
                    cappedCone.BottomBaseDiameter *= fullToHalfScaleMultiplier;
                    return cappedCone;
                case RaymarchedObject3DType.Torus:
                    return ScaleTorusData(Torus.Value);
                case RaymarchedObject3DType.CappedTorus:
                    CappedTorusShaderData cappedTorus = CappedTorus.Value;
                    cappedTorus.CapAngle *= Mathf.Deg2Rad * fullToHalfScaleMultiplier;
                    cappedTorus.Torus    =  ScaleTorusData(cappedTorus.Torus);
                    return cappedTorus;
                    ;
                case RaymarchedObject3DType.RegularPrism:
                    RegularPrismShaderData regularPrism = RegularPrism.Value;
                    regularPrism.Circumdiameter *= fullToHalfScaleMultiplier;
                    regularPrism.Length         *= fullToHalfScaleMultiplier;
                    return regularPrism;
                case RaymarchedObject3DType.RegularPolyhedron:
                    RegularPolyhedronShaderData regularPolyhedron = RegularPolyhedron.Value;
                    regularPolyhedron.InscribedDiameter *= fullToHalfScaleMultiplier;
                    return regularPolyhedron;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, default);
            }
        }

        private static SphereShaderData ScaleSphereData(SphereShaderData sphere)
        {
            sphere.Diameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return sphere;
        }

        private EllipsoidShaderData ScaleEllipsoidData(EllipsoidShaderData ellipsoid)
        {
            ellipsoid.Diameters *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return ellipsoid;
        }

        private CapsuleShaderData ScaleCapsuleData(CapsuleShaderData capsule)
        {
            capsule.Height = ScaleCapsuleHeightData(capsule.Height);
            capsule.Base   = ScaleSphereData(capsule.Base);
            return capsule;
        }

        private float ScaleCapsuleHeightData(float height) =>
            height * IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;

        private TorusShaderData ScaleTorusData(TorusShaderData torus)
        {
            torus.MajorDiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            torus.MinorDiameter *= IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;
            return torus;
        }
    }
}