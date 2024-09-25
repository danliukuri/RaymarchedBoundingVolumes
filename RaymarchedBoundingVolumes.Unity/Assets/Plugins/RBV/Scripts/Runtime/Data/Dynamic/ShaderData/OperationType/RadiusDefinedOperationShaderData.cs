using System;

namespace RBV.Data.Dynamic.ShaderData.OperationType
{
    [Serializable]
    public struct RadiusDefinedOperationShaderData : IOperationTypeShaderData
    {
        public float Radius;

        public static RadiusDefinedOperationShaderData Default { get; } = new() { Radius = 0.5f};
    }
}