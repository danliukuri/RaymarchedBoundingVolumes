using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct HyperellipsoidShaderData : IObjectTypeShaderData
    {
        public Vector4 Diameters;

        public static HyperellipsoidShaderData Default { get; } = new() { Diameters = Vector4.one };
    }
}