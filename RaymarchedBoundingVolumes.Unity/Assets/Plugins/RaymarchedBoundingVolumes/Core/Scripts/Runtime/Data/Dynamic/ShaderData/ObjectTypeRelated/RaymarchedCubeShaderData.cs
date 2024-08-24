using System;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Data.Dynamic.ShaderData.ObjectTypeRelated
{
    [Serializable]
    public struct RaymarchedCubeShaderData
    {
        public Vector3 Dimensions;

        public static RaymarchedCubeShaderData Default { get; } = new() { Dimensions = Vector3.one };
    }
}