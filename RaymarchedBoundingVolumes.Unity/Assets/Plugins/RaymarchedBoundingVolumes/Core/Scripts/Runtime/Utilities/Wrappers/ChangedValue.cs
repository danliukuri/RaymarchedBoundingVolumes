namespace RaymarchedBoundingVolumes.Utilities.Wrappers
{
    public struct ChangedValue<T> : IChangedValue<T>
    {
        public T Old { get; }
        public T New { get; }

        public ChangedValue(T oldValue, T newValue)
        {
            Old = oldValue;
            New = newValue;
        }
    }
}