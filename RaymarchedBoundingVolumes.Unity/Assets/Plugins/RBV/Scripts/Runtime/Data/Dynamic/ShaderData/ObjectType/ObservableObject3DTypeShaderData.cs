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
                Cube.Changed              += value.CastCached<IObjectTypeShaderData, RaymarchedCubeShaderData>();
                Sphere.Changed            += value.CastCached<IObjectTypeShaderData, SphereShaderData>();
                Ellipsoid.Changed         += value.CastCached<IObjectTypeShaderData, EllipsoidShaderData>();
                Capsule.Changed           += value.CastCached<IObjectTypeShaderData, RaymarchedCapsuleShaderData>();
                EllipticCapsule.Changed   += value.CastCached<IObjectTypeShaderData, EllipticCapsuleShaderData>();
                Cylinder.Changed          += value.CastCached<IObjectTypeShaderData, RaymarchedCylinderShaderData>();
                EllipticCylinder.Changed  += value.CastCached<IObjectTypeShaderData, EllipticCylinderShaderData>();
                Plane.Changed             += value.CastCached<IObjectTypeShaderData, PlaneShaderData>();
                Cone.Changed              += value.CastCached<IObjectTypeShaderData, RaymarchedConeShaderData>();
                CappedCone.Changed        += value.CastCached<IObjectTypeShaderData, CappedConeShaderData>();
                Torus.Changed             += value.CastCached<IObjectTypeShaderData, TorusShaderData>();
                CappedTorus.Changed       += value.CastCached<IObjectTypeShaderData, CappedTorusShaderData>();
                RegularPrism.Changed      += value.CastCached<IObjectTypeShaderData, RegularPrismShaderData>();
                RegularPolyhedron.Changed += value.CastCached<IObjectTypeShaderData, RegularPolyhedronShaderData>();
            }
            remove
            {
                Cube.Changed              -= value.CastCached<IObjectTypeShaderData, RaymarchedCubeShaderData>();
                Sphere.Changed            -= value.CastCached<IObjectTypeShaderData, SphereShaderData>();
                Ellipsoid.Changed         -= value.CastCached<IObjectTypeShaderData, EllipsoidShaderData>();
                Capsule.Changed           -= value.CastCached<IObjectTypeShaderData, RaymarchedCapsuleShaderData>();
                EllipticCapsule.Changed   -= value.CastCached<IObjectTypeShaderData, EllipticCapsuleShaderData>();
                Cylinder.Changed          -= value.CastCached<IObjectTypeShaderData, RaymarchedCylinderShaderData>();
                EllipticCylinder.Changed  -= value.CastCached<IObjectTypeShaderData, EllipticCylinderShaderData>();
                Plane.Changed             -= value.CastCached<IObjectTypeShaderData, PlaneShaderData>();
                Cone.Changed              -= value.CastCached<IObjectTypeShaderData, RaymarchedConeShaderData>();
                CappedCone.Changed        -= value.CastCached<IObjectTypeShaderData, CappedConeShaderData>();
                Torus.Changed             -= value.CastCached<IObjectTypeShaderData, TorusShaderData>();
                CappedTorus.Changed       -= value.CastCached<IObjectTypeShaderData, CappedTorusShaderData>();
                RegularPrism.Changed      -= value.CastCached<IObjectTypeShaderData, RegularPrismShaderData>();
                RegularPolyhedron.Changed -= value.CastCached<IObjectTypeShaderData, RegularPolyhedronShaderData>();
            }
        }

        [field: SerializeField] public ObservableValue<RaymarchedCubeShaderData> Cube { get; set; } =
            new(RaymarchedCubeShaderData.Default);

        [field: SerializeField] public ObservableValue<SphereShaderData> Sphere { get; set; } =
            new(SphereShaderData.Default);

        [field: SerializeField] public ObservableValue<EllipsoidShaderData> Ellipsoid { get; set; } =
            new(EllipsoidShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedCapsuleShaderData> Capsule { get; set; } =
            new(RaymarchedCapsuleShaderData.Default);

        [field: SerializeField] public ObservableValue<EllipticCapsuleShaderData> EllipticCapsule { get; set; } =
            new(EllipticCapsuleShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedCylinderShaderData> Cylinder { get; set; } =
            new(RaymarchedCylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<EllipticCylinderShaderData> EllipticCylinder { get; set; } =
            new(EllipticCylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<PlaneShaderData> Plane { get; set; } =
            new(PlaneShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedConeShaderData> Cone { get; set; } =
            new(RaymarchedConeShaderData.Default);

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
                    RaymarchedCubeShaderData cube = Cube.Value;
                    cube.Dimensions *= fullToHalfScaleMultiplier;
                    return cube;
                case RaymarchedObject3DType.Sphere:
                    SphereShaderData sphere = Sphere.Value;
                    sphere.Diameter *= fullToHalfScaleMultiplier;
                    return sphere;
                case RaymarchedObject3DType.Ellipsoid:
                    EllipsoidShaderData ellipsoid = Ellipsoid.Value;
                    ellipsoid.Diameters *= fullToHalfScaleMultiplier;
                    return ellipsoid;
                case RaymarchedObject3DType.Capsule:
                    RaymarchedCapsuleShaderData capsule = Capsule.Value;
                    capsule.Height   *= fullToHalfScaleMultiplier;
                    capsule.Diameter *= fullToHalfScaleMultiplier;
                    return capsule;
                case RaymarchedObject3DType.EllipticCapsule:
                    EllipticCapsuleShaderData ellipticCapsule = EllipticCapsule.Value;
                    ellipticCapsule.Height    *= fullToHalfScaleMultiplier;
                    ellipticCapsule.Diameters *= fullToHalfScaleMultiplier;
                    return ellipticCapsule;
                case RaymarchedObject3DType.Cylinder:
                    RaymarchedCylinderShaderData cylinder = Cylinder.Value;
                    cylinder.Diameter *= fullToHalfScaleMultiplier;
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
                    RaymarchedConeShaderData cone = Cone.Value;
                    cone.Height   *= fullToHalfScaleMultiplier;
                    cone.Diameter *= fullToHalfScaleMultiplier;
                    return cone;
                case RaymarchedObject3DType.CappedCone:
                    CappedConeShaderData cappedCone = CappedCone.Value;
                    cappedCone.Height             *= fullToHalfScaleMultiplier;
                    cappedCone.TopBaseDiameter    *= fullToHalfScaleMultiplier;
                    cappedCone.BottomBaseDiameter *= fullToHalfScaleMultiplier;
                    return cappedCone;
                case RaymarchedObject3DType.Torus:
                    TorusShaderData torus = Torus.Value;
                    torus.MajorDiameter *= fullToHalfScaleMultiplier;
                    torus.MinorDiameter *= fullToHalfScaleMultiplier;
                    return torus;
                case RaymarchedObject3DType.CappedTorus:
                    CappedTorusShaderData cappedTorus = CappedTorus.Value;
                    cappedTorus.CapAngle      *= Mathf.Deg2Rad * fullToHalfScaleMultiplier;
                    cappedTorus.MajorDiameter *= fullToHalfScaleMultiplier;
                    cappedTorus.MinorDiameter *= fullToHalfScaleMultiplier;
                    return cappedTorus;
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
    }
}