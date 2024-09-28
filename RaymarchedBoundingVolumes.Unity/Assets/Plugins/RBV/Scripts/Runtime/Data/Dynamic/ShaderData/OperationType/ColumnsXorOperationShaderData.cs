using System;

namespace RBV.Data.Dynamic.ShaderData.OperationType
{
    [Serializable]
    public struct ColumnsXorOperationShaderData : IOperationTypeShaderData
    {
        public ColumnsOperationShaderData Outer;
        public ColumnsOperationShaderData Inner;

        public static ColumnsXorOperationShaderData Default { get; } =
            new() { Outer = ColumnsOperationShaderData.Default, Inner = ColumnsOperationShaderData.Default };
    }
}