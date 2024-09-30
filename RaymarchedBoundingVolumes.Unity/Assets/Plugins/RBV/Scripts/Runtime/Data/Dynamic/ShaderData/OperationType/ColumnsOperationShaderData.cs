using System;

namespace RBV.Data.Dynamic.ShaderData.OperationType
{
    [Serializable]
    public struct ColumnsOperationShaderData : IOperationTypeShaderData
    {
        public float Radius;
        public int   Count;

        public static ColumnsOperationShaderData Default { get; } = new() { Radius = 0.5f, Count = 3 };
    }
}