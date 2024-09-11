using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct HypercapsuleShaderData : IObjectTypeShaderData
    {
        public float Height;
        public float Diameter;

        public static HypercapsuleShaderData Default { get; } = new() { Height = 1f, Diameter = 1f };
    }
}