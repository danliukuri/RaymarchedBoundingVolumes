using System;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.OperationType
{
    [Serializable]
    public struct RatioDefinedOperationShaderData : IOperationTypeShaderData
    {
        [Range(0f, 1f)] public float Ratio;

        public static RatioDefinedOperationShaderData Default { get; } = new() { Ratio = default };
    }
}