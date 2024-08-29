using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData
{
    public struct RaymarchedObjectShaderData
    {
        public int Type;
        public int TypeRelatedDataIndex;

        public int     IsActive;
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
    }
}