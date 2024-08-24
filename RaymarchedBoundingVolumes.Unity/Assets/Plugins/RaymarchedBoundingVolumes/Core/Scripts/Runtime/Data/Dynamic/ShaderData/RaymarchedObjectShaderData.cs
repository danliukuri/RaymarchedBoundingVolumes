using UnityEngine;

namespace RaymarchedBoundingVolumes.Data.Dynamic.ShaderData
{
    public struct RaymarchedObjectShaderData
    {
        public int Type;
        public int TypeRelatedDataIndex;

        public int     IsActive;
        public Vector3 Position;
    }
}