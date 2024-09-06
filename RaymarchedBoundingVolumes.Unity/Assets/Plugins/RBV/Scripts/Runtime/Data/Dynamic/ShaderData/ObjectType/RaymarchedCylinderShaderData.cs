using System;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedCylinderShaderData : IObjectTypeShaderData
    {
        public float Height;
        public float Diameter;

        public static RaymarchedCylinderShaderData Default { get; } = new() { Diameter = 1f, Height = 1f };
    }
}