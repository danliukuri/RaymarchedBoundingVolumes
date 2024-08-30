using System;
using UnityEngine;

namespace RBV.Utilities.Wrappers
{
    [Serializable]
    public class ObservableTransform<T> : IObservableTransform
    {
        event Action IObservableTransform.Changed
        {
            add    => Changed += value.CastCached<T>();
            remove => Changed -= value.CastCached<T>();
        }

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

        [field: SerializeField] public ObservableValue<T> Position { get; set; } = new();
        [field: SerializeField] public ObservableValue<T> Rotation { get; set; } = new();
        [field: SerializeField] public ObservableValue<T> Scale    { get; set; } = new();
    }
}