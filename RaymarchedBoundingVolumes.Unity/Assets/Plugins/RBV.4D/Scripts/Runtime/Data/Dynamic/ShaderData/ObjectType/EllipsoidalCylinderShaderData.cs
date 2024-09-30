using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static;
using RBV.Utilities.Attributes;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct EllipsoidalCylinderShaderData : IObjectTypeShaderData
    {
        [Unwrapped] public HyperellipsoidShaderData Base;

        [Tooltip(FourDimensionGlossaryConstants.Trength)] public float Trength;

        public static EllipsoidalCylinderShaderData Default { get; } =
            new() { Base = HyperellipsoidShaderData.Default, Trength = 1f };
    }
}