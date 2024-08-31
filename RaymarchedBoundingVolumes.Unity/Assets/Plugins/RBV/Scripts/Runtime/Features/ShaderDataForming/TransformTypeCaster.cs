﻿using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic.ShaderData;
using RBV.Data.Static.Enumerations;

namespace RBV.Features.ShaderDataForming
{
    public class TransformTypeCaster : ITransformTypeCaster
    {
        public Array CastToShaderDataTypeArray(KeyValuePair<TransformType, List<RaymarchedObject>> source) =>
            CastToShaderDataTypeArray(source.Key, source.Value.Select(obj => obj.TransformShaderData));

        public Array CastToShaderDataTypeArray(TransformType type, IEnumerable<ITransformShaderData> source) => type switch
        {
            TransformType.ThreeDimensional => source.Cast<Transform3DShaderData>().ToArray(),
            TransformType.FourDimensional  => throw TransformCastingException(),
            _                              => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };

        public Type GetShaderDataType(TransformType type) => type switch
        {
            TransformType.ThreeDimensional => typeof(Transform3DShaderData),
            TransformType.FourDimensional  => throw TransformCastingException(),
            _                              => throw new ArgumentOutOfRangeException(nameof(type), type, default)
        };

        private static Exception TransformCastingException()
        {
#if RBV_4D_ON
            return new InvalidOperationException("Cannot cast 4D transform shader data."                 +
                                                 $" Wrong {typeof(ITransformTypeCaster)} implementation" +
                                                 " is used for casting.");
#else
            return new InvalidOperationException("Cannot cast 4D transform shader data."+
                                                "The plugin com.danliukuri.rbv.4d is not found.");
#endif
        }
    }
}