using System;
using System.Collections.Generic;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Utilities.Wrappers
{
    [Serializable]
    public class ObservableValue<T> : IObservableValue<T>, ISerializationCallbackReceiver
    {
        [SerializeField] private T value;
        private T _nonSerializedValue;

        public ObservableValue(T value = default) => this.value = value;

        public T Value { get => value; set => SetValue(this.value,this.value = value); }

        private void SetValue(T previousValue, T newValue)
        {
            if (!EqualityComparer<T>.Default.Equals(previousValue, newValue))
                Changed?.Invoke(new ChangedValue<T>(previousValue, newValue));
        }

        public event Action<ChangedValue<T>> Changed;

        public void OnBeforeSerialize() => _nonSerializedValue = value;

        public void OnAfterDeserialize() => SetValue(_nonSerializedValue, value);
    }
}