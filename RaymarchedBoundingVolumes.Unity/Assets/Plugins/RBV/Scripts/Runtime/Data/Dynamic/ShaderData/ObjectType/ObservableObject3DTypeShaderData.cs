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
                Cube.Changed      += value.CastCached<IObjectTypeShaderData, RaymarchedCubeShaderData>();
                Sphere.Changed    += value.CastCached<IObjectTypeShaderData, RaymarchedSphereShaderData>();
                Ellipsoid.Changed += value.CastCached<IObjectTypeShaderData, RaymarchedEllipsoidShaderData>();
                Capsule.Changed   += value.CastCached<IObjectTypeShaderData, RaymarchedCapsuleShaderData>();
                EllipsoidalCapsule.Changed +=
                    value.CastCached<IObjectTypeShaderData, RaymarchedEllipsoidalCapsuleShaderData>();
                Cylinder.Changed += value.CastCached<IObjectTypeShaderData, RaymarchedCylinderShaderData>();
                EllipsoidalCylinder.Changed +=
                    value.CastCached<IObjectTypeShaderData, RaymarchedEllipsoidalCylinderShaderData>();
                Plane.Changed        += value.CastCached<IObjectTypeShaderData, RaymarchedPlaneShaderData>();
                Cone.Changed         += value.CastCached<IObjectTypeShaderData, RaymarchedConeShaderData>();
                CappedCone.Changed   += value.CastCached<IObjectTypeShaderData, RaymarchedCappedConeShaderData>();
                Torus.Changed        += value.CastCached<IObjectTypeShaderData, RaymarchedTorusShaderData>();
                CappedTorus.Changed  += value.CastCached<IObjectTypeShaderData, RaymarchedCappedTorusShaderData>();
                RegularPrism.Changed += value.CastCached<IObjectTypeShaderData, RaymarchedRegularPrismShaderData>();
                RegularPolyhedron.Changed +=
                    value.CastCached<IObjectTypeShaderData, RaymarchedRegularPolyhedronShaderData>();
            }
            remove
            {
                Cube.Changed      -= value.CastCached<IObjectTypeShaderData, RaymarchedCubeShaderData>();
                Sphere.Changed    -= value.CastCached<IObjectTypeShaderData, RaymarchedSphereShaderData>();
                Ellipsoid.Changed -= value.CastCached<IObjectTypeShaderData, RaymarchedEllipsoidShaderData>();
                Capsule.Changed   -= value.CastCached<IObjectTypeShaderData, RaymarchedCapsuleShaderData>();
                EllipsoidalCapsule.Changed -=
                    value.CastCached<IObjectTypeShaderData, RaymarchedEllipsoidalCapsuleShaderData>();
                Cylinder.Changed -= value.CastCached<IObjectTypeShaderData, RaymarchedCylinderShaderData>();
                EllipsoidalCylinder.Changed -=
                    value.CastCached<IObjectTypeShaderData, RaymarchedEllipsoidalCylinderShaderData>();
                Plane.Changed        -= value.CastCached<IObjectTypeShaderData, RaymarchedPlaneShaderData>();
                Cone.Changed         -= value.CastCached<IObjectTypeShaderData, RaymarchedConeShaderData>();
                CappedCone.Changed   -= value.CastCached<IObjectTypeShaderData, RaymarchedCappedConeShaderData>();
                Torus.Changed        -= value.CastCached<IObjectTypeShaderData, RaymarchedTorusShaderData>();
                CappedTorus.Changed  -= value.CastCached<IObjectTypeShaderData, RaymarchedCappedTorusShaderData>();
                RegularPrism.Changed -= value.CastCached<IObjectTypeShaderData, RaymarchedRegularPrismShaderData>();
                RegularPolyhedron.Changed -=
                    value.CastCached<IObjectTypeShaderData, RaymarchedRegularPolyhedronShaderData>();
            }
        }

        [field: SerializeField] public ObservableValue<RaymarchedCubeShaderData> Cube { get; set; } =
            new(RaymarchedCubeShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedSphereShaderData> Sphere { get; set; } =
            new(RaymarchedSphereShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedEllipsoidShaderData> Ellipsoid { get; set; } =
            new(RaymarchedEllipsoidShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedCapsuleShaderData> Capsule { get; set; } =
            new(RaymarchedCapsuleShaderData.Default);

        [field: SerializeField]
        public ObservableValue<RaymarchedEllipsoidalCapsuleShaderData> EllipsoidalCapsule { get; set; } =
            new(RaymarchedEllipsoidalCapsuleShaderData.Default);

        [field: SerializeField]
        public ObservableValue<RaymarchedCylinderShaderData> Cylinder { get; set; } =
            new(RaymarchedCylinderShaderData.Default);

        [field: SerializeField]
        public ObservableValue<RaymarchedEllipsoidalCylinderShaderData> EllipsoidalCylinder { get; set; } =
            new(RaymarchedEllipsoidalCylinderShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedPlaneShaderData> Plane { get; set; } =
            new(RaymarchedPlaneShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedConeShaderData> Cone { get; set; } =
            new(RaymarchedConeShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedCappedConeShaderData> CappedCone { get; set; } =
            new(RaymarchedCappedConeShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedTorusShaderData> Torus { get; set; } =
            new(RaymarchedTorusShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedCappedTorusShaderData> CappedTorus { get; set; } =
            new(RaymarchedCappedTorusShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedRegularPrismShaderData> RegularPrism { get; set; } =
            new(RaymarchedRegularPrismShaderData.Default);
        
        [field: SerializeField] public ObservableValue<RaymarchedRegularPolyhedronShaderData> RegularPolyhedron { get; set; } =
            new(RaymarchedRegularPolyhedronShaderData.Default);

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
                    RaymarchedSphereShaderData sphere = Sphere.Value;
                    sphere.Diameter *= fullToHalfScaleMultiplier;
                    return sphere;
                case RaymarchedObject3DType.Ellipsoid:
                    RaymarchedEllipsoidShaderData ellipsoid = Ellipsoid.Value;
                    ellipsoid.Diameters *= fullToHalfScaleMultiplier;
                    return ellipsoid;
                case RaymarchedObject3DType.Capsule:
                    RaymarchedCapsuleShaderData capsule = Capsule.Value;
                    capsule.Height   *= fullToHalfScaleMultiplier;
                    capsule.Diameter *= fullToHalfScaleMultiplier;
                    return capsule;
                case RaymarchedObject3DType.EllipsoidalCapsule:
                    RaymarchedEllipsoidalCapsuleShaderData ellipsoidalCapsule = EllipsoidalCapsule.Value;
                    ellipsoidalCapsule.Height    *= fullToHalfScaleMultiplier;
                    ellipsoidalCapsule.Diameters *= fullToHalfScaleMultiplier;
                    return ellipsoidalCapsule;
                case RaymarchedObject3DType.Cylinder:
                    RaymarchedCylinderShaderData cylinder = Cylinder.Value;
                    cylinder.Diameter *= fullToHalfScaleMultiplier;
                    return cylinder;
                case RaymarchedObject3DType.EllipsoidalCylinder:
                    RaymarchedEllipsoidalCylinderShaderData ellipsoidalCylinder = EllipsoidalCylinder.Value;
                    ellipsoidalCylinder.Dimensions =
                        new Vector3(ellipsoidalCylinder.Dimensions.x * fullToHalfScaleMultiplier,
                            ellipsoidalCylinder.Dimensions.y,
                            ellipsoidalCylinder.Dimensions.z * fullToHalfScaleMultiplier);
                    return ellipsoidalCylinder;
                case RaymarchedObject3DType.Plane:
                    RaymarchedPlaneShaderData plane = Plane.Value;
                    plane.Dimensions   *= RaymarchedPlaneShaderData.ScaleMultiplier;
                    plane.Dimensions.y =  RaymarchedPlaneShaderData.Thickness;
                    plane.Dimensions   *= fullToHalfScaleMultiplier;
                    return plane;
                case RaymarchedObject3DType.Cone:
                    RaymarchedConeShaderData cone = Cone.Value;
                    cone.Diameter *= fullToHalfScaleMultiplier;
                    return cone;
                case RaymarchedObject3DType.CappedCone:
                    RaymarchedCappedConeShaderData cappedCone = CappedCone.Value;
                    cappedCone.Height             *= fullToHalfScaleMultiplier;
                    cappedCone.TopBaseDiameter    *= fullToHalfScaleMultiplier;
                    cappedCone.BottomBaseDiameter *= fullToHalfScaleMultiplier;
                    return cappedCone;
                case RaymarchedObject3DType.Torus:
                    RaymarchedTorusShaderData torus = Torus.Value;
                    torus.MajorDiameter *= fullToHalfScaleMultiplier;
                    torus.MinorDiameter *= fullToHalfScaleMultiplier;
                    return torus;
                case RaymarchedObject3DType.CappedTorus:
                    RaymarchedCappedTorusShaderData cappedTorus = CappedTorus.Value;
                    cappedTorus.CapAngle      *= Mathf.Deg2Rad * fullToHalfScaleMultiplier;
                    cappedTorus.MajorDiameter *= fullToHalfScaleMultiplier;
                    cappedTorus.MinorDiameter *= fullToHalfScaleMultiplier;
                    return cappedTorus;
                case RaymarchedObject3DType.RegularPrism:
                    RaymarchedRegularPrismShaderData regularPrism = RegularPrism.Value;
                    regularPrism.Circumdiameter *= fullToHalfScaleMultiplier;
                    regularPrism.Length         *= fullToHalfScaleMultiplier;
                    return regularPrism;
                case RaymarchedObject3DType.RegularPolyhedron:
                    RaymarchedRegularPolyhedronShaderData regularPolyhedron = RegularPolyhedron.Value;
                    regularPolyhedron.InscribedDiameter *= fullToHalfScaleMultiplier;
                    return regularPolyhedron;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, default);
            }
        }
    }
}