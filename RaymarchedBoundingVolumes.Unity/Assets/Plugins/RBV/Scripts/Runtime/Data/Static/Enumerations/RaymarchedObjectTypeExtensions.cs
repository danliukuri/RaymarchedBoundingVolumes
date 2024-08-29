using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic.ShaderData.ObjectTypeRelated;

namespace RBV.Data.Static.Enumerations
{
    public static class RaymarchedObjectTypeExtensions
    {
        public static Type GetShaderDataType(this RaymarchedObjectType type) =>
            type switch
            {
                RaymarchedObjectType.Sphere => typeof(RaymarchedSphereShaderData),
                RaymarchedObjectType.Cube   => typeof(RaymarchedCubeShaderData),
                _                           => throw new ArgumentOutOfRangeException(nameof(type), type, default)
            };

        public static Array CastToShaderDataTypeArray(this RaymarchedObjectType type, IEnumerable<object> source) =>
            type switch
            {
                RaymarchedObjectType.Sphere => source.Cast<RaymarchedSphereShaderData>().ToArray(),
                RaymarchedObjectType.Cube   => source.Cast<RaymarchedCubeShaderData>().ToArray(),
                _                           => throw new ArgumentOutOfRangeException(nameof(type), type, default)
            };
    }
}