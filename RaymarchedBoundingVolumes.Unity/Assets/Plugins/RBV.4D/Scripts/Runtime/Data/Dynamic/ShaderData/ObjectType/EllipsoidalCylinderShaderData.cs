using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct EllipsoidalCylinderShaderData : IObjectTypeShaderData
    {
        public Vector3 Diameters;

        [Tooltip(FourDimensionGlossaryConstants.Trength)] public float Trength;

        public static EllipsoidalCylinderShaderData Default { get; } = new() { Diameters = Vector3.one, Trength = 1f };
    }
}