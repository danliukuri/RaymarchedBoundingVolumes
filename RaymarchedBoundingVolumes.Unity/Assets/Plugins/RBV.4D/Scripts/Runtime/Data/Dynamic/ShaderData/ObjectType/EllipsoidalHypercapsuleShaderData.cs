using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Utilities.Attributes;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct EllipsoidalHypercapsuleShaderData : IObjectTypeShaderData
    {
        public float Height;

        [Unwrapped] public HyperellipsoidShaderData Base;

        public static EllipsoidalHypercapsuleShaderData Default { get; } =
            new() { Height = 1f, Base = HyperellipsoidShaderData.Default };
    }
}