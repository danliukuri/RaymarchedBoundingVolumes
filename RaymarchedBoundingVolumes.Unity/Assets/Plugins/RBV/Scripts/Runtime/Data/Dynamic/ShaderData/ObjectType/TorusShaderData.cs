using System;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct TorusShaderData : IObjectTypeShaderData
    {
        public float MajorDiameter;
        public float MinorDiameter;

        public static TorusShaderData Default { get; } = new() { MajorDiameter = 1f, MinorDiameter = 0.5f };
    }
}