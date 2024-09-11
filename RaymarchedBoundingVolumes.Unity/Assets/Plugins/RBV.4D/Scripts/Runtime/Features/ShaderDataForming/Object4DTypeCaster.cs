using System;
using RBV.Data.Dynamic;
using RBV.Data.Static.Enumerations;
using RBV.Features.ShaderDataForming;
using RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static;
using RBV.FourDimensional.Data.Static.Enumerations;

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
            RaymarchedObject4DType.Hypercube   => typeof(HypercubeShaderData),
            RaymarchedObject4DType.Hypersphere => typeof(HypersphereShaderData),
            _                                  => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}