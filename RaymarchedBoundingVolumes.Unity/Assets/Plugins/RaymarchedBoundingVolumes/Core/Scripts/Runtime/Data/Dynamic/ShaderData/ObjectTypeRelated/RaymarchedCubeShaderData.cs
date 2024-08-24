using System;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Data.Dynamic.ShaderData.ObjectTypeRelated
{
    [Serializable]
    public struct RaymarchedCubeShaderData
    {
        public Vector3 Size;

        public static RaymarchedCubeShaderData Default { get; } = new() { Size = Vector3.one };
    }
}