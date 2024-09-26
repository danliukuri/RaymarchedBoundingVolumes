using System;

namespace RBV.Data.Dynamic.ShaderData.OperationType
{
    [Serializable]
    public struct RadiusDefinedXorOperationShaderData : IOperationTypeShaderData
    {
        public float OuterRadius;
        public float InnerRadius;

        public static RadiusDefinedXorOperationShaderData Default { get; } =
            new() { OuterRadius = 0.5f, InnerRadius = 0.5f };
    }
}