using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData.ObjectTypeRelated;
using RBV.Data.Static.Enumerations;

namespace RBV.Features.ShaderDataForming
{
    public class ObjectTypeCaster : IObjectTypeCaster
    {
        public Array CastToShaderDataTypeArray(KeyValuePair<RaymarchedObjectType, List<RaymarchedObject>> source) =>
            CastToShaderDataTypeArray(source.Key, source.Value.Select(obj => obj.TypeRelatedShaderData));

        public Array CastToShaderDataTypeArray(RaymarchedObjectType type, IEnumerable<object> source) =>
            CastToShaderDataTypeArray((RaymarchedObject3DType)(int)type, source);

        public Type GetShaderDataType(RaymarchedObjectType type) =>
            GetShaderDataType((RaymarchedObject3DType)(int)type);

        private Array CastToShaderDataTypeArray(RaymarchedObject3DType type, IEnumerable<object> source) => type switch
        {
            RaymarchedObject3DType.Sphere => source.Cast<RaymarchedSphereShaderData>().ToArray(),
            RaymarchedObject3DType.Cube   => source.Cast<RaymarchedCubeShaderData>().ToArray(),
            _                             => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };

        private Type GetShaderDataType(RaymarchedObject3DType type) => type switch
        {
            RaymarchedObject3DType.Sphere => typeof(RaymarchedSphereShaderData),
            RaymarchedObject3DType.Cube   => typeof(RaymarchedCubeShaderData),
            _                             => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}