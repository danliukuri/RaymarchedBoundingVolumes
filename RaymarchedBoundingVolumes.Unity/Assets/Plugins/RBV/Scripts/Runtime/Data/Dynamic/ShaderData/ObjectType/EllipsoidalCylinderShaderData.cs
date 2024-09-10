using System;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct EllipsoidalCylinderShaderData : IObjectTypeShaderData
    {
        public Vector3 Dimensions;

        public static EllipsoidalCylinderShaderData Default { get; } = new() { Dimensions = Vector3.one };
    }
}