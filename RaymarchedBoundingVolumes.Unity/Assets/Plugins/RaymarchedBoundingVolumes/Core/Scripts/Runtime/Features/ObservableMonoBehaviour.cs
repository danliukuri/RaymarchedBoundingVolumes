using System;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features
{
    public abstract class ObservableMonoBehaviour<T> : MonoBehaviour where T : ObservableMonoBehaviour<T>
    {
        public event Action<T>                           Changed;
        public event Action<T, IChangedValue<Transform>> ParentChanged;

        private readonly ObservableTransform<Vector3> _gameObjectTransform = new();
        private          ObservableValue<Transform>   _parent;

        protected virtual void Awake() => Initialize();

        protected virtual void OnEnable()
        {
            SubscribeToChanges();
            RaiseChangedEvent();
        }

        protected virtual void OnDisable()
        {
            UnsubscribeToChanges();
            RaiseChangedEvent();
        }

        protected virtual void Update()
        {
            if (transform.hasChanged)
                UpdateTransform();
        }

        protected virtual void Initialize() => _parent = new ObservableValue<Transform>(transform.parent);

        private void UpdateTransform()
        {
            _gameObjectTransform.SetValuesFrom(transform);
            _parent.Value        = transform.parent;
            transform.hasChanged = false;
        }

        protected virtual void SubscribeToChanges()
        {
            _gameObjectTransform.Changed += RaiseChangedEvent;
            _parent             .Changed += RaiseParentChangedEvent;
        }

        protected virtual void UnsubscribeToChanges()
        {
            _gameObjectTransform.Changed -= RaiseChangedEvent;
            _parent             .Changed -= RaiseParentChangedEvent;
        }

        protected void RaiseChangedEvent()                           => Changed?.Invoke(this as T);
        protected void RaiseChangedEvent(ChangedValue<Vector3> data) => RaiseChangedEvent();

        private void RaiseParentChangedEvent(ChangedValue<Transform> parent) =>
            ParentChanged?.Invoke(this as T, parent);
    }
}