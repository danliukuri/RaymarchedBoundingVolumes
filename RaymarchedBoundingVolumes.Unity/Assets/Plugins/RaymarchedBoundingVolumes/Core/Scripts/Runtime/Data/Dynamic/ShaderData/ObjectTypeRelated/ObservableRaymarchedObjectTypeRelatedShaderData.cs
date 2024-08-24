using System;
using RaymarchedBoundingVolumes.Data.Static.Enumerations;
using RaymarchedBoundingVolumes.Utilities.Attributes;
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

        [field: SerializeField, Unwrapped]
        public ObservableValue<RaymarchedSphereShaderData> SphereShaderData { get; set; } =
            new(RaymarchedSphereShaderData.Default);

        [field: SerializeField, Unwrapped]
        public ObservableValue<RaymarchedCubeShaderData> CubeShaderData { get; set; } =
            new(RaymarchedCubeShaderData.Default);

        public object GetShaderData(RaymarchedObjectType type) => type switch
        {
            RaymarchedObjectType.Sphere => SphereShaderData.Value,
            RaymarchedObjectType.Cube   => CubeShaderData.Value,
            _                           => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}