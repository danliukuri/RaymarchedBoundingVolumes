using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic.ShaderData.ObjectTypeRelated;
using RBV.Data.Static.Enumerations;

namespace RBV.Features.ShaderDataForming
{
    public class ObjectTypeCaster : IObjectTypeCaster
    {
        public Array CastToShaderDataTypeArray(KeyValuePair<int, List<RaymarchedObject>> source) =>
            CastToShaderDataTypeArray(source.Key, source.Value.Select(obj => obj.TypeRelatedShaderData));

        public Array CastToShaderDataTypeArray(int type, IEnumerable<object> source) =>
            CastToShaderDataTypeArray((RaymarchedObjectType)type, source);

        public Type GetShaderDataType(int type) => GetShaderDataType((RaymarchedObjectType)type);

        private Array CastToShaderDataTypeArray(RaymarchedObjectType type, IEnumerable<object> source) => type switch
        {
            RaymarchedObjectType.Sphere => source.Cast<RaymarchedSphereShaderData>().ToArray(),
            RaymarchedObjectType.Cube   => source.Cast<RaymarchedCubeShaderData>().ToArray(),
            _                           => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };

        private Type GetShaderDataType(RaymarchedObjectType type) => type switch
        {
            RaymarchedObjectType.Sphere => typeof(RaymarchedSphereShaderData),
            RaymarchedObjectType.Cube   => typeof(RaymarchedCubeShaderData),
            _                           => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}