using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Data.Static.Enumerations;
using static RBV.Data.Static.Enumerations.RaymarchedObject3DType;

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
            Cube               => source.Cast<RaymarchedCubeShaderData>().ToArray(),
            Sphere             => source.Cast<RaymarchedSphereShaderData>().ToArray(),
            Ellipsoid          => source.Cast<RaymarchedEllipsoidShaderData>().ToArray(),
            Capsule            => source.Cast<RaymarchedCapsuleShaderData>().ToArray(),
            EllipsoidalCapsule => source.Cast<RaymarchedEllipsoidalCapsuleShaderData>().ToArray(),
            Cylinder           => source.Cast<RaymarchedCylinderShaderData>().ToArray(),
            _                  => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };

        protected Type GetShaderDataType(RaymarchedObject3DType type) => type switch
        {
            Cube               => typeof(RaymarchedCubeShaderData),
            Sphere             => typeof(RaymarchedSphereShaderData),
            Ellipsoid          => typeof(RaymarchedEllipsoidShaderData),
            Capsule            => typeof(RaymarchedCapsuleShaderData),
            EllipsoidalCapsule => typeof(RaymarchedEllipsoidalCapsuleShaderData),
            Cylinder           => typeof(RaymarchedCylinderShaderData),
            _                  => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}