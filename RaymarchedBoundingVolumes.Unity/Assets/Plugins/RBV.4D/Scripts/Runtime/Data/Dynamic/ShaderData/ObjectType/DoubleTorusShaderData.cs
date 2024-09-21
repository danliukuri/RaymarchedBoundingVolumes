using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct DoubleTorusShaderData : IObjectTypeShaderData
    {
        public float MajorMajorDiameter;
        public float MajorMinorDiameter;
        public float MinorMinorDiameter;

        public static DoubleTorusShaderData Default { get; } =
            new() { MajorMajorDiameter = 1f, MajorMinorDiameter = 0.4f, MinorMinorDiameter = 0.1f };
    }
}