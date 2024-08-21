﻿namespace RaymarchedBoundingVolumes.Utilities.Wrappers
{
    public interface IChangedValue<out T>
    {
        T Old { get; }
        T New { get; }
    }
}