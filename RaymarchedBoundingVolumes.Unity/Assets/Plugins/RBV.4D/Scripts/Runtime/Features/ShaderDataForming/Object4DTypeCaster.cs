using System;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Data.Static.Enumerations;
using RBV.Features.ShaderDataForming;
using RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static;
using RBV.FourDimensional.Data.Static.Enumerations;
using static RBV.FourDimensional.Data.Static.Enumerations.RaymarchedObject4DType;

namespace RBV.FourDimensional.Features.ShaderDataForming
{
    public class Object4DTypeCaster : ObjectTypeCaster
    {
        public override Type GetShaderDataType(RaymarchedObjectType type) =>
            (int)type < EnumerationConstants.RaymarchedObject4DTypeOffset
                ? GetShaderDataType((RaymarchedObject3DType)(int)type)
                : GetShaderDataType((RaymarchedObject4DType)(int)type);

        private Type GetShaderDataType(RaymarchedObject4DType type) => type switch
        {
            Hypercube               => typeof(HypercubeShaderData),
            Hypersphere             => typeof(HypersphereShaderData),
            Hyperellipsoid          => typeof(HyperellipsoidShaderData),
            Hypercapsule            => typeof(HypercapsuleShaderData),
            EllipsoidalHypercapsule => typeof(EllipsoidalHypercapsuleShaderData),
            CubicalCylinder         => typeof(CubicalCylinderShaderData),
            SphericalCylinder       => typeof(SphericalCylinderShaderData),
            EllipsoidalCylinder     => typeof(EllipsoidalCylinderShaderData),
            ConicalCylinder         => typeof(ConicalCylinderShaderData),
            DoubleCylinder          => typeof(DoubleCylinderShaderData),
            DoubleEllipticCylinder  => typeof(DoubleEllipticCylinderShaderData),
            PrismicCylinder         => typeof(PrismicCylinderShaderData),
            SphericalCone           => typeof(SphericalConeShaderData),
            CylindricalCone         => typeof(CylindricalConeShaderData),
            ToroidalSphere          => typeof(TorusShaderData),
            SphericalTorus          => typeof(TorusShaderData),
            DoubleTorus             => typeof(DoubleTorusShaderData),
            Tiger                   => typeof(TigerShaderData),
            _                       => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}