using System;
using RaymarchedBoundingVolumes.Utilities.Attributes;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Utilities.Wrappers
{
    [Serializable]
    public class ObservableTransform<T>
    {
        [field: SerializeField, Unwrapped] public ObservableValue<T> Position { get; private set; } = new();
        [field: SerializeField, Unwrapped] public ObservableValue<T> Rotation { get; private set; } = new();
        [field: SerializeField, Unwrapped] public ObservableValue<T> Scale    { get; private set; } = new();

        public event Action<ChangedValue<T>> Changed
        {
            add
            {
                Position.Changed += value;
                Rotation.Changed += value;
                Scale   .Changed += value;
            }
            remove
            {
                Position.Changed -= value;
                Rotation.Changed -= value;
                Scale   .Changed -= value;
            }
        }
    }
}