using System;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct SphereShaderData : IObjectTypeShaderData
    {
        public float Diameter;

        public static SphereShaderData Default { get; } = new() { Diameter = 1f };
    }
}