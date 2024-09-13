﻿using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct ConicalCylinderShaderData : IObjectTypeShaderData
    {
        public float Diameter;

        public float Height;

        [Tooltip(FourDimensionGlossaryConstants.Trength)] public float Trength;

        public static ConicalCylinderShaderData Default { get; } = new() { Diameter = 1f, Height = 1f, Trength = 1f };
    }
}