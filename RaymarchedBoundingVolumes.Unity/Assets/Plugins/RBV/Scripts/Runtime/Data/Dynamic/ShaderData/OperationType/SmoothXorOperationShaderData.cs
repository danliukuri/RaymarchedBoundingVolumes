using System;

namespace RBV.Data.Dynamic.ShaderData.OperationType
{
    [Serializable]
    public struct SmoothXorOperationShaderData : IOperationTypeShaderData
    {
        public float OuterRadius;
        public float InnerRadius;

        public static SmoothXorOperationShaderData Default { get; } = new() { OuterRadius = 0.5f, InnerRadius = 0.5f };
    }
}