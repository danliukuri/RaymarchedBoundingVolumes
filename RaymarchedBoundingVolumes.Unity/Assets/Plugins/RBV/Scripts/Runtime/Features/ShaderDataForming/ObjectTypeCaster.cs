using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Data.Static.Enumerations;

namespace RBV.Features.ShaderDataForming
{
    public class ObjectTypeCaster : IObjectTypeCaster
    {
        public virtual Array CastToShaderDataTypeArray(RaymarchedObjectType               type,
                                                       IEnumerable<IObjectTypeShaderData> source) =>
            CastToShaderDataTypeArray((RaymarchedObject3DType)(int)type, source);

        public virtual Type GetShaderDataType(RaymarchedObjectType type) =>
            GetShaderDataType((RaymarchedObject3DType)(int)type);

        protected Array CastToShaderDataTypeArray(RaymarchedObject3DType             type,
                                                  IEnumerable<IObjectTypeShaderData> source) => type switch
        {
            RaymarchedObject3DType.Cube      => source.Cast<RaymarchedCubeShaderData>().ToArray(),
            RaymarchedObject3DType.Sphere    => source.Cast<RaymarchedSphereShaderData>().ToArray(),
            RaymarchedObject3DType.Ellipsoid => source.Cast<RaymarchedEllipsoidShaderData>().ToArray(),
            _                                => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };

        protected Type GetShaderDataType(RaymarchedObject3DType type) => type switch
        {
            RaymarchedObject3DType.Cube      => typeof(RaymarchedCubeShaderData),
            RaymarchedObject3DType.Sphere    => typeof(RaymarchedSphereShaderData),
            RaymarchedObject3DType.Ellipsoid => typeof(RaymarchedEllipsoidShaderData),
            _                                => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}