using System;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedCubeShaderData : IObjectTypeShaderData
    {
        public Vector3 Dimensions;

        public static RaymarchedCubeShaderData Default { get; } = new() { Dimensions = Vector3.one };
    }
}