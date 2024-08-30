using System;
using RBV.Data.Static.Enumerations;
using RBV.Utilities.Wrappers;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectTypeRelated
{
    [Serializable]
    public class ObservableRaymarchedObjectTypeRelatedShaderData : IObservableRaymarchedObjectTypeRelatedShaderData
    {
        public event Action<ChangedValue<object>> Changed
        {
            add
            {
                SphereShaderData.Changed += value.CastCached<object, RaymarchedSphereShaderData>();
                CubeShaderData.Changed   += value.CastCached<object, RaymarchedCubeShaderData>();
            }
            remove
            {
                SphereShaderData.Changed -= value.CastCached<object, RaymarchedSphereShaderData>();
                CubeShaderData.Changed   -= value.CastCached<object, RaymarchedCubeShaderData>();
            }
        }

        [field: SerializeField] public ObservableValue<RaymarchedSphereShaderData> SphereShaderData { get; set; } =
            new(RaymarchedSphereShaderData.Default);

        [field: SerializeField] public ObservableValue<RaymarchedCubeShaderData> CubeShaderData { get; set; } =
            new(RaymarchedCubeShaderData.Default);

        public object GetShaderData(int type) => GetShaderData((RaymarchedObjectType)type);

        public object GetShaderData(RaymarchedObjectType type)
        {
            const float fullToHalfScaleMultiplier =
                IObservableRaymarchedObjectTypeRelatedShaderData.FullToHalfScaleMultiplier;

            switch (type)
            {
                case RaymarchedObjectType.Sphere:
                    RaymarchedSphereShaderData sphereShaderData = SphereShaderData.Value;
                    sphereShaderData.Diameter *= fullToHalfScaleMultiplier;
                    return sphereShaderData;
                case RaymarchedObjectType.Cube:
                    RaymarchedCubeShaderData cubeShaderData = CubeShaderData.Value;
                    cubeShaderData.Dimensions *= fullToHalfScaleMultiplier;
                    return cubeShaderData;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, default);
            }
        }
    }
}