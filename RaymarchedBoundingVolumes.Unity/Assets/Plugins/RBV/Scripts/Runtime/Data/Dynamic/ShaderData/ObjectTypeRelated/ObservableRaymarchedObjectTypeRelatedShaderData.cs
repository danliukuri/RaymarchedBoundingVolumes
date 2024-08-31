using System;
using RBV.Data.Static.Enumerations;
using RBV.Utilities.Wrappers;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectTypeRelated
{
    [Serializable]
    public class ObservableRaymarchedObjectTypeRelatedShaderData : IObservableRaymarchedObjectTypeRelatedShaderData
    {
        public event Action<ChangedValue<IObjectTypeRelatedShaderData>> Changed
        {
            add
            {
                SphereShaderData.Changed +=
                    value.CastCached<IObjectTypeRelatedShaderData, RaymarchedSphereShaderData>();
                CubeShaderData.Changed +=
                    value.CastCached<IObjectTypeRelatedShaderData, RaymarchedCubeShaderData>();
            }
            remove
            {
                SphereShaderData.Changed -=
                    value.CastCached<IObjectTypeRelatedShaderData, RaymarchedSphereShaderData>();
                CubeShaderData.Changed -=
                    value.CastCached<IObjectTypeRelatedShaderData, RaymarchedCubeShaderData>();
            }
        }

        [field: SerializeField] public ObservableValue<RaymarchedSphereShaderData> SphereShaderData { get; set; } =
            new(RaymarchedSphereShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedCubeShaderData> CubeShaderData { get; set; } =
            new(RaymarchedCubeShaderData.Default);

        public IObjectTypeRelatedShaderData GetShaderData(RaymarchedObjectType type) =>
            GetShaderData((RaymarchedObject3DType)(int)type);

        public IObjectTypeRelatedShaderData GetShaderData(RaymarchedObject3DType type)
        {
            const float fullToHalfScaleMultiplier =
                IObservableRaymarchedObjectTypeRelatedShaderData.FullToHalfScaleMultiplier;

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