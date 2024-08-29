namespace RBV.Data.Dynamic.ShaderData
{
    public struct RaymarchingOperationNodeShaderData
    {
        public int ChildOperationsCount;
        public int ChildObjectsCount;

        public int ParentIndex;
        public int Layer;

        public const int RootLayer     = -1;
        public const int RootNodeIndex = -1;

        public static int NextLayer(int layer) => ++layer;
    }
}