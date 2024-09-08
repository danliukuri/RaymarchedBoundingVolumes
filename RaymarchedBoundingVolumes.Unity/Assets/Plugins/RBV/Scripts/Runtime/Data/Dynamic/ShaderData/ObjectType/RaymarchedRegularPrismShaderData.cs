using System;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedRegularPrismShaderData : IObjectTypeShaderData
    {
        public int   VerticesCount;
        public float Circumdiameter;
        public float Length;

        public static RaymarchedRegularPrismShaderData Default { get; } =
            new() { VerticesCount = 3, Circumdiameter = 1f, Length = 1f };
    }
}