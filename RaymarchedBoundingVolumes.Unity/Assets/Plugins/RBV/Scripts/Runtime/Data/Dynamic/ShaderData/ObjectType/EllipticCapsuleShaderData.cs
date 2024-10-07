using System;
using RBV.Utilities.Attributes;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct EllipticCapsuleShaderData : IObjectTypeShaderData
    {
        public float Height;

        [Unwrapped] public EllipsoidShaderData Ellipsoid;

        public static EllipticCapsuleShaderData Default { get; } =
            new() { Height = 1f, Ellipsoid = EllipsoidShaderData.Default };
    }
}