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
            }
            remove
            {
                Cube.Changed      -= value.CastCached<IObjectTypeShaderData, RaymarchedCubeShaderData>();
                Sphere.Changed    -= value.CastCached<IObjectTypeShaderData, RaymarchedSphereShaderData>();
                Ellipsoid.Changed -= value.CastCached<IObjectTypeShaderData, RaymarchedEllipsoidShaderData>();
            }
        }

        [field: SerializeField] public ObservableValue<RaymarchedCubeShaderData> Cube { get; set; } =
            new(RaymarchedCubeShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedSphereShaderData> Sphere { get; set; } =
            new(RaymarchedSphereShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedEllipsoidShaderData> Ellipsoid { get; set; } =
            new(RaymarchedEllipsoidShaderData.Default);

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
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, default);
            }
        }
    }
}