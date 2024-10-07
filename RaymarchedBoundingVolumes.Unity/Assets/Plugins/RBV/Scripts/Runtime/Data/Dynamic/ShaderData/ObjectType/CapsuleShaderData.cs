using System;
using RBV.Utilities.Attributes;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct CapsuleShaderData : IObjectTypeShaderData
    {
        public float Height;

        [Unwrapped] public SphereShaderData Base;

        public static CapsuleShaderData Default { get; } = new() { Height = 1f, Base = SphereShaderData.Default };
    }
}