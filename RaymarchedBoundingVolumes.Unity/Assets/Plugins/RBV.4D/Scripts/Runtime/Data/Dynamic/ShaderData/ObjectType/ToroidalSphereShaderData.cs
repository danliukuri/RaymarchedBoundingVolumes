using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct ToroidalSphereShaderData : IObjectTypeShaderData
    {
        public float MajorDiameter;
        public float MinorDiameter;

        public static ToroidalSphereShaderData Default { get; } = new() { MajorDiameter = 1f, MinorDiameter = 0.5f,};
    }
}