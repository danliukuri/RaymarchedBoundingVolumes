using System;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedCappedTorusShaderData : IObjectTypeShaderData
    {
        public float CapAngle;
        public float MajorDiameter;
        public float MinorDiameter;

        public static RaymarchedCappedTorusShaderData Default { get; } =
            new() { CapAngle = 270f, MajorDiameter = 1f, MinorDiameter = 0.5f };
    }
}