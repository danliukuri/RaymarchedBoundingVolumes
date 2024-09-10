using System;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct CappedTorusShaderData : IObjectTypeShaderData
    {
        public float CapAngle;
        public float MajorDiameter;
        public float MinorDiameter;

        public static CappedTorusShaderData Default { get; } =
            new() { CapAngle = 270f, MajorDiameter = 1f, MinorDiameter = 0.5f };
    }
}