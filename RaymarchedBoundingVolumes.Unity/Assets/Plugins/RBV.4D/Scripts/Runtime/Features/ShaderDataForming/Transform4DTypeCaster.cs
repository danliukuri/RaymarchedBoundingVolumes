using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic.ShaderData;
using RBV.Features.ShaderDataForming;
using RBV.FourDimensional.Data.Dynamic.ShaderData;

namespace RBV.FourDimensional.Features.ShaderDataForming
{
    public class Transform4DTypeCaster : TransformTypeCaster
    {
        protected override Array CastToFourDimensionalTransformShaderDataArray(IEnumerable<ITransformShaderData> source)
            => source.Cast<Transform4DShaderData>().ToArray();

        protected override Type GetFourDimensionalTransformShaderDataType() => typeof(Transform4DShaderData);
    }
}