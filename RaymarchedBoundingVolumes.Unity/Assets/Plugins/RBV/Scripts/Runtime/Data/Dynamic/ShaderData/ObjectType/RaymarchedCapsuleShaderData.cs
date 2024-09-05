using System;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedCapsuleShaderData : IObjectTypeShaderData
    {
        public float Height;
        public float Diameter;

        public static RaymarchedCapsuleShaderData Default { get; } = new() { Diameter = 1f, Height = 1f };
    }
}