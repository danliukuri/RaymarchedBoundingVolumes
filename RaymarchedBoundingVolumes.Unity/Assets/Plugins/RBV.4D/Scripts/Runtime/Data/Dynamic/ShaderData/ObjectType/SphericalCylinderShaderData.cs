using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static;
using RBV.Utilities.Attributes;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct SphericalCylinderShaderData : IObjectTypeShaderData
    {
        [Unwrapped] public HypersphereShaderData Base;

        [Tooltip(FourDimensionGlossaryConstants.Trength)] public float Trength;

        public static SphericalCylinderShaderData Default { get; } =
            new() { Base = HypersphereShaderData.Default, Trength = 1f };
    }
}