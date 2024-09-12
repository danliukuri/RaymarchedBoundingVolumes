using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct EllipsoidalHypercapsuleShaderData : IObjectTypeShaderData
    {
        public float   Height;
        public Vector4 Diameters;

        public static EllipsoidalHypercapsuleShaderData Default { get; } =
            new() { Diameters = Vector4.one, Height = 1f };
    }
}