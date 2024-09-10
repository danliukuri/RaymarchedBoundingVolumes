using System;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct PlaneShaderData : IObjectTypeShaderData
    {
        public const float ScaleMultiplier = 10f;
        public const float Thickness       = 0.00165F;

        public Vector3 Dimensions;

        public static PlaneShaderData Default { get; } = new() { Dimensions = Vector3.one };
    }
}