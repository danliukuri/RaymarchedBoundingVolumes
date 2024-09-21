using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct PrismicCylinderShaderData : IObjectTypeShaderData
    {
        public int   VerticesCount;
        public float Circumdiameter;
        public float Length;

        public static PrismicCylinderShaderData Default { get; } = 
            new() { VerticesCount = 3, Circumdiameter = 1f, Length = 1f };
    }
}