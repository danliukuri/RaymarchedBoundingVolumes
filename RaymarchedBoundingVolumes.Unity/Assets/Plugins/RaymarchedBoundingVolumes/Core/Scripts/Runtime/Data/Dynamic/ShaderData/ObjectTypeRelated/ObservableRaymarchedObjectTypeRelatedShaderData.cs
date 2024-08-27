using System;
using RaymarchedBoundingVolumes.Data.Static.Enumerations;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Data.Dynamic.ShaderData.ObjectTypeRelated
{
    [Serializable]
    public class ObservableRaymarchedObjectTypeRelatedShaderData
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

        public object GetShaderData(RaymarchedObjectType type)
        {
            const float fullToHalfScaleMultiplier = 0.5f;
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