using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct DoubleEllipticCylinderShaderData : IObjectTypeShaderData
    {
        public Vector4 Diameters;

        public static DoubleEllipticCylinderShaderData Default { get; } = new() { Diameters = Vector4.one };
    }
}