using System;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct EllipticCapsuleShaderData : IObjectTypeShaderData
    {
        public float   Height;
        public Vector3 Diameters;

        public static EllipticCapsuleShaderData Default { get; } = new() { Height = 1f, Diameters = Vector3.one };
    }
}