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
                SphereShaderData.Changed +=
                    value.CastCached<IObjectTypeShaderData, RaymarchedSphereShaderData>();
                CubeShaderData.Changed +=
                    value.CastCached<IObjectTypeShaderData, RaymarchedCubeShaderData>();
            }
            remove
            {
                SphereShaderData.Changed -=
                    value.CastCached<IObjectTypeShaderData, RaymarchedSphereShaderData>();
                CubeShaderData.Changed -=
                    value.CastCached<IObjectTypeShaderData, RaymarchedCubeShaderData>();
            }
        }

        [field: SerializeField] public ObservableValue<RaymarchedSphereShaderData> SphereShaderData { get; set; } =
            new(RaymarchedSphereShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedCubeShaderData> CubeShaderData { get; set; } =
            new(RaymarchedCubeShaderData.Default);

        public IObjectTypeShaderData GetShaderData(RaymarchedObjectType type) =>
            GetShaderData((RaymarchedObject3DType)(int)type);

        public IObjectTypeShaderData GetShaderData(RaymarchedObject3DType type)
        {
            const float fullToHalfScaleMultiplier =
                IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;

            switch (type)
            {
                case RaymarchedObject3DType.Sphere:
                    RaymarchedSphereShaderData sphereShaderData = SphereShaderData.Value;
                    sphereShaderData.Diameter *= fullToHalfScaleMultiplier;
                    return sphereShaderData;
                case RaymarchedObject3DType.Cube:
                    RaymarchedCubeShaderData cubeShaderData = CubeShaderData.Value;
                    cubeShaderData.Dimensions *= fullToHalfScaleMultiplier;
                    return cubeShaderData;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, default);
            }
        }
    }
}