using System;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedEllipsoidalCylinderShaderData : IObjectTypeShaderData
    {
        public Vector3 Dimensions;

        public static RaymarchedEllipsoidalCylinderShaderData Default { get; } = new() { Dimensions = Vector3.one };
    }
}