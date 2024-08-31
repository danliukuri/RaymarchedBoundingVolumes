using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData
{
    public struct Transform3DShaderData : ITransformShaderData
    {
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
    }
}