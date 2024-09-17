using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct TigerShaderData : IObjectTypeShaderData
    {
        public Vector2 MajorDiameters;
        public float   MinorDiameter;

        public static TigerShaderData Default { get; } = new() { MajorDiameters = Vector2.one, MinorDiameter = 0.5f };
    }
}