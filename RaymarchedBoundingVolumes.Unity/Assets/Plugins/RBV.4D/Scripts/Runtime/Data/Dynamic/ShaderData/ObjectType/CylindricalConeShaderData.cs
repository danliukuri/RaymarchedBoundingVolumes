using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct CylindricalConeShaderData : IObjectTypeShaderData
    {
        public float Diameter;
        public float Trength;

        public static CylindricalConeShaderData Default { get; } = new() { Diameter = 1f, Trength = 1f };
    }
}