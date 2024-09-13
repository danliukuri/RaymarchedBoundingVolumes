using System;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct EllipticCylinderShaderData : IObjectTypeShaderData
    {
        public Vector3 Dimensions;

        public static EllipticCylinderShaderData Default { get; } = new() { Dimensions = Vector3.one };
    }
}