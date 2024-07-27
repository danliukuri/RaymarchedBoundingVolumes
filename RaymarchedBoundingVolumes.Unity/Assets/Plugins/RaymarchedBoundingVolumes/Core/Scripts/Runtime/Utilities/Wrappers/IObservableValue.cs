using System;

namespace RaymarchedBoundingVolumes.Utilities.Wrappers
{
    public interface IObservableValue<T>
    {
        T Value { get; set; }
        event Action<T> Changed;
    }
}