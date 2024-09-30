using System;
using RBV.Utilities.Attributes;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct CappedTorusShaderData : IObjectTypeShaderData
    {
        public float CapAngle;

        [Unwrapped] public TorusShaderData Torus;

        public static CappedTorusShaderData Default { get; } =
            new() { CapAngle = 270f, Torus = TorusShaderData.Default };
    }
}