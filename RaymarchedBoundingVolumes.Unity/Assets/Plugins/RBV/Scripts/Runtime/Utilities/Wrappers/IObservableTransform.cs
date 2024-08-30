using System;

namespace RBV.Utilities.Wrappers
{
    public interface IObservableTransform
    {
        event Action Changed;
    }
}