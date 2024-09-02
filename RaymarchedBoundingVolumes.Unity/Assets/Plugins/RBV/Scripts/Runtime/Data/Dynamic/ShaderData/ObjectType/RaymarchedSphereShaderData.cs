using System;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedSphereShaderData : IObjectTypeShaderData
    {
        public float Diameter;

        public static RaymarchedSphereShaderData Default { get; } = new() { Diameter = 1f };
    }
}