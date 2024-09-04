namespace RBV.Data.Dynamic
{
    public struct OperationMetaData
    {
        public int Index;
        public int ParentIndex;
        public int Layer;

        public int DirectChildObjectsCount;
        public int DirectChildOperationsCount;
    }
}