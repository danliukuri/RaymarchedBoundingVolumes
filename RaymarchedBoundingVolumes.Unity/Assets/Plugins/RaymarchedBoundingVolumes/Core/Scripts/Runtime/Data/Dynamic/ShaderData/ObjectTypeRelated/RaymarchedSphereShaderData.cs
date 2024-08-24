using System;

namespace RaymarchedBoundingVolumes.Data.Dynamic.ShaderData.ObjectTypeRelated
{
    [Serializable]
    public struct RaymarchedSphereShaderData
    {
        public float Radius;

        public static RaymarchedSphereShaderData Default { get; } = new() { Radius = 1f };
    }
}