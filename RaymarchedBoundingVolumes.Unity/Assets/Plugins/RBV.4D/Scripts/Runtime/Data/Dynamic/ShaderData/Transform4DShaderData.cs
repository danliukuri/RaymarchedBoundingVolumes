using RBV.Data.Dynamic.ShaderData;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData
{
    public struct Transform4DShaderData : ITransformShaderData
    {
        public Vector4 Position;
        public Vector3 Rotation;
        public Vector3 Rotation4D;
        public Vector4 Scale;
    }
}