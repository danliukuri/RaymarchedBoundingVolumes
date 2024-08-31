using System;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectTypeRelated
{
    [Serializable]
    public struct RaymarchedCubeShaderData : IObjectTypeRelatedShaderData
    {
        public Vector3 Dimensions;

        public static RaymarchedCubeShaderData Default { get; } = new() { Dimensions = Vector3.one };
    }
}