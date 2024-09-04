using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Data.Static.Enumerations;
using RBV.Features.ShaderDataForming;
using RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static;
using RBV.FourDimensional.Data.Static.Enumerations;

namespace RBV.FourDimensional.Features.ShaderDataForming
{
    public class Object4DTypeCaster : ObjectTypeCaster
    {
        public override Array CastToShaderDataTypeArray(RaymarchedObjectType               type,
                                                        IEnumerable<IObjectTypeShaderData> source) =>
            (int)type < EnumerationConstants.RaymarchedObject4DTypeOffset
                ? CastToShaderDataTypeArray((RaymarchedObject3DType)(int)type, source)
                : CastToShaderDataTypeArray((RaymarchedObject4DType)(int)type, source);

        public override Type GetShaderDataType(RaymarchedObjectType type) =>
            (int)type < EnumerationConstants.RaymarchedObject4DTypeOffset
                ? GetShaderDataType((RaymarchedObject3DType)(int)type)
                : GetShaderDataType((RaymarchedObject4DType)(int)type);

        private Array CastToShaderDataTypeArray(RaymarchedObject4DType             type,
                                                IEnumerable<IObjectTypeShaderData> source) => type switch
        {
            RaymarchedObject4DType.Hypersphere => source.Cast<RaymarchedHypersphereShaderData>().ToArray(),
            RaymarchedObject4DType.Hypercube   => source.Cast<RaymarchedHypercubeShaderData>().ToArray(),
            _                                  => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };

        private Type GetShaderDataType(RaymarchedObject4DType type) => type switch
        {
            RaymarchedObject4DType.Hypersphere => typeof(RaymarchedHypersphereShaderData),
            RaymarchedObject4DType.Hypercube   => typeof(RaymarchedHypercubeShaderData),
            _                                  => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };
    }
}