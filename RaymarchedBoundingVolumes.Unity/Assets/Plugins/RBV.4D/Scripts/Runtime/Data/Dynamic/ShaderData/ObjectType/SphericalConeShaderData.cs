using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct SphericalConeShaderData : IObjectTypeShaderData
    {
        public float Diameter;
        public float Trength;

        public static SphericalConeShaderData Default { get; } = new() { Diameter = 1f, Trength = 1f };
    }
}