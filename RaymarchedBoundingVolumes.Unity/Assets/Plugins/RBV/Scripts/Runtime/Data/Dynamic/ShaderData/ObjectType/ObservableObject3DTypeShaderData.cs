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
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, default);
            }
        }
    }
}