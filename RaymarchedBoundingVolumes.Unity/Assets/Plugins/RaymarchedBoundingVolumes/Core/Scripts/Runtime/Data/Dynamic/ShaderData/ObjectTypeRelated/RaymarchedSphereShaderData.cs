using System;

namespace RaymarchedBoundingVolumes.Data.Dynamic.ShaderData.ObjectTypeRelated
{
    [Serializable]
    public struct RaymarchedSphereShaderData
    {
        public float Diameter;

        public static RaymarchedSphereShaderData Default { get; } = new() { Diameter = 1f };
    }
}