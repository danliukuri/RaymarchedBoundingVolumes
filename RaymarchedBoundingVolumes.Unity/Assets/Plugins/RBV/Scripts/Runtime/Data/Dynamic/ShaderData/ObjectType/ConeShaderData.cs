using System;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedConeShaderData : IObjectTypeShaderData
    {
        public float Height;
        public float Diameter;

        public static RaymarchedConeShaderData Default { get; } = new() { Diameter = 1f, Height = 1f };
    }
}