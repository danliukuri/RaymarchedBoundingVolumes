using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct SphericalCylinderShaderData : IObjectTypeShaderData
    {
        public float Diameter;

        [Tooltip(FourDimensionGlossaryConstants.Trength)] public float Trength;

        public static SphericalCylinderShaderData Default { get; } = new() { Diameter = 1f, Trength = 1f };
    }
}