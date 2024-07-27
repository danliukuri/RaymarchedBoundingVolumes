using System;
using RaymarchedBoundingVolumes.Utilities.Attributes;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Utilities.Wrappers
{
    [Serializable]
    public class ObservableTransform<T>
    {
        [field: SerializeField, Unwrapped] public ObservableValue<T> Position { get; private set; }
        [field: SerializeField, Unwrapped] public ObservableValue<T> Rotation { get; private set; }
        [field: SerializeField, Unwrapped] public ObservableValue<T> Scale    { get; private set; }
    }
}