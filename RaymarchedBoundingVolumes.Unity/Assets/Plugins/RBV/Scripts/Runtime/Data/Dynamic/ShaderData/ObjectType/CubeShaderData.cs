using System;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct CubeShaderData : IObjectTypeShaderData
    {
        public Vector3 Dimensions;

        public static CubeShaderData Default { get; } = new() { Dimensions = Vector3.one };
    }
}