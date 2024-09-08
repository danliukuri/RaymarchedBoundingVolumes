using System;
using System.Collections.Generic;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Data.Static.Enumerations;
using RBV.Utilities.Extensions;
using static RBV.Data.Static.Enumerations.RaymarchedObject3DType;

namespace RBV.Features.ShaderDataForming
{
    public class ObjectTypeCaster : IObjectTypeCaster
    {
        public Array CastToShaderDataTypeArray(RaymarchedObjectType type, IEnumerable<IObjectTypeShaderData> source) =>
            source.CastToArray(GetShaderDataType(type));

        public virtual Type GetShaderDataType(RaymarchedObjectType type) =>
            GetShaderDataType((RaymarchedObject3DType)(int)type);

        protected Type GetShaderDataType(RaymarchedObject3DType type) => type switch
        {
            Cube                => typeof(RaymarchedCubeShaderData),
            Sphere              => typeof(RaymarchedSphereShaderData),
            Ellipsoid           => typeof(RaymarchedEllipsoidShaderData),
            Capsule             => typeof(RaymarchedCapsuleShaderData),
            EllipsoidalCapsule  => typeof(RaymarchedEllipsoidalCapsuleShaderData),
            Cylinder            => typeof(RaymarchedCylinderShaderData),
            EllipsoidalCylinder => typeof(RaymarchedEllipsoidalCylinderShaderData),
            Plane               => typeof(RaymarchedPlaneShaderData),
            Cone                => typeof(RaymarchedConeShaderData),
            CappedCone          => typeof(RaymarchedCappedConeShaderData),
            Torus               => typeof(RaymarchedTorusShaderData),
            CappedTorus         => typeof(RaymarchedCappedTorusShaderData),
            _                   => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}