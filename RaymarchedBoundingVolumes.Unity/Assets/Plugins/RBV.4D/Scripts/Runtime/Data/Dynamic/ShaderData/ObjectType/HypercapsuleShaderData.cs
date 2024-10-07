using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Utilities.Attributes;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct HypercapsuleShaderData : IObjectTypeShaderData
    {
        public float Height;

        [Unwrapped] public HypersphereShaderData Base;

        public static HypercapsuleShaderData Default { get; } =
            new() { Height = 1f, Base = HypersphereShaderData.Default };
    }
}