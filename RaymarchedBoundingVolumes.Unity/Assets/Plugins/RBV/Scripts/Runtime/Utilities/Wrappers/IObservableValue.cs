using System;

namespace RBV.Utilities.Wrappers
{
    public interface IObservableValue<T>
    {
        T Value { get; set; }

        event Action<ChangedValue<T>> Changed;
    }
}