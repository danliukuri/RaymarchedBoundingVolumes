using System;
using RBV.Utilities.Attributes;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct CylinderShaderData : IObjectTypeShaderData
    {
        public float Height;

        [Unwrapped] public SphereShaderData Base;

        public static CylinderShaderData Default { get; } = new() { Height = 1f, Base = SphereShaderData.Default };
    }
}