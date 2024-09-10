using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct HypersphereShaderData : IObjectTypeShaderData
    {
        public float Diameter;

        public static HypersphereShaderData Default { get; } = new() { Diameter = 1f };
    }
}