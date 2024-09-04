using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedHypercubeShaderData : IObjectTypeShaderData
    {
        public Vector4 Dimensions;

        public static RaymarchedHypercubeShaderData Default { get; } = new() { Dimensions = Vector4.one };
    }
}