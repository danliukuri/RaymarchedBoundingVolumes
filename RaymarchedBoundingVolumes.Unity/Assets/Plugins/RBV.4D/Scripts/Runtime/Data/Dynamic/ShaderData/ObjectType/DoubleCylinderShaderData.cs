using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct DoubleCylinderShaderData : IObjectTypeShaderData
    {
        public Vector2 Diameters;

        public static DoubleCylinderShaderData Default { get; } = new() { Diameters = Vector2.one };
    }
}