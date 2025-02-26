﻿using System;
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
            Cube              => typeof(CubeShaderData),
            Sphere            => typeof(SphereShaderData),
            Ellipsoid         => typeof(EllipsoidShaderData),
            Capsule           => typeof(CapsuleShaderData),
            EllipticCapsule   => typeof(EllipticCapsuleShaderData),
            Cylinder          => typeof(CylinderShaderData),
            EllipticCylinder  => typeof(EllipticCylinderShaderData),
            Plane             => typeof(PlaneShaderData),
            Cone              => typeof(CapsuleShaderData),
            CappedCone        => typeof(CappedConeShaderData),
            Torus             => typeof(TorusShaderData),
            CappedTorus       => typeof(CappedTorusShaderData),
            RegularPrism      => typeof(RegularPrismShaderData),
            RegularPolyhedron => typeof(RegularPolyhedronShaderData),
            _                 => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}