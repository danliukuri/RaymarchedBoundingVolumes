using System;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedTorusShaderData : IObjectTypeShaderData
    {
        public float MajorDiameter;
        public float MinorDiameter;

        public static RaymarchedTorusShaderData Default { get; } = new() { MajorDiameter = 1f, MinorDiameter = 0.5f };
    }
}