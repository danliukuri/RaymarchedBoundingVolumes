using System;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedCappedConeShaderData : IObjectTypeShaderData
    {
        public float Height;
        public float TopBaseRadius;
        public float BottomBaseRadius;

        public static RaymarchedCappedConeShaderData Default { get; } =
            new() { Height = 1f, TopBaseRadius = 0.5f, BottomBaseRadius = 1f };
    }
}