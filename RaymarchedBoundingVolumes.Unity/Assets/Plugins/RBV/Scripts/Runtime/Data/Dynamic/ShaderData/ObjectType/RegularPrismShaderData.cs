using System;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RegularPrismShaderData : IObjectTypeShaderData
    {
        public int   VerticesCount;
        public float Circumdiameter;
        public float Length;

        public static RegularPrismShaderData Default { get; } =
            new() { VerticesCount = 3, Circumdiameter = 1f, Length = 1f };
    }
}