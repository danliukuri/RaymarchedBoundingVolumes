using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Utilities.Attributes;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct CubicalCylinderShaderData : IObjectTypeShaderData
    {
        public float Height;

        [Unwrapped] public SphericalCylinderShaderData SphericalCylinder;

        public static CubicalCylinderShaderData Default { get; } =
            new() { Height = 1f, SphericalCylinder = SphericalCylinderShaderData.Default };
    }
}