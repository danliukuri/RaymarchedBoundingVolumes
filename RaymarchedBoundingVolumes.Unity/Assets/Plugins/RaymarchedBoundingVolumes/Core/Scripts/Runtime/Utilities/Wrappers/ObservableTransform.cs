using System;
using RaymarchedBoundingVolumes.Utilities.Attributes;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Utilities.Wrappers
{
    [Serializable]
    public class ObservableTransform<T>
    {
        public event Action<ChangedValue<T>> Changed
        {
            add
            {
                Position.Changed += value;
                Rotation.Changed += value;
                Scale.Changed    += value;
            }
            remove
            {
                Position.Changed -= value;
                Rotation.Changed -= value;
                Scale.Changed    -= value;
            }
        }

        [field: SerializeField, Unwrapped] public ObservableValue<T> Position { get; set; } = new();
        [field: SerializeField, Unwrapped] public ObservableValue<T> Rotation { get; set; } = new();
        [field: SerializeField, Unwrapped] public ObservableValue<T> Scale    { get; set; } = new();
    }
}