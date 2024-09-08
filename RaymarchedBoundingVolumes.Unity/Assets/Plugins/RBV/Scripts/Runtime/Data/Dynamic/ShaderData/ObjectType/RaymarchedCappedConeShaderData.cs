using System;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedCappedConeShaderData : IObjectTypeShaderData
    {
        public float Height;
        public float TopBaseDiameter;
        public float BottomBaseDiameter;

        public static RaymarchedCappedConeShaderData Default { get; } =
            new() { Height = 1f, TopBaseDiameter = 0.5f, BottomBaseDiameter = 1f };
    }
}