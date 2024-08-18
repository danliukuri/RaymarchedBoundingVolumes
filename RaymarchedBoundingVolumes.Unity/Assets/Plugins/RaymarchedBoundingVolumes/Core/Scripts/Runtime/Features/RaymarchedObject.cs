using System;
using RaymarchedBoundingVolumes.Data.Dynamic.ShaderData;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features
{
    [ExecuteInEditMode]
    public partial class RaymarchedObject : RaymarchingFeature
    {
        public event Action<RaymarchedObject> Changed;

        private readonly ObservableTransform<Vector3> _gameObjectTransform = new();

        [field: SerializeField] public ObservableTransform<Vector3> Transform { get; private set; }

        public RaymarchedObjectShaderData ShaderData => new()
        {
            IsActive = Convert.ToInt32(gameObject is { activeSelf: true, activeInHierarchy: true }),
            Position = transform.position + Transform.Position.Value
        };

        private void OnEnable()  => SubscribeToChanges();
        private void OnDisable() => UnsubscribeToChanges();

        private void Update()
        {
            if (transform.hasChanged)
                UpdateTransform();
        }

        private void SubscribeToChanges()
        {
            Transform.Changed            += RaiseChangedEvent;
            _gameObjectTransform.Changed += RaiseChangedEvent;
        }

        private void UnsubscribeToChanges()
        {
            Transform.Changed            -= RaiseChangedEvent;
            _gameObjectTransform.Changed -= RaiseChangedEvent;
        }

        private void UpdateTransform()
        {
            _gameObjectTransform.SetValuesFrom(transform);
            transform.hasChanged = false;
        }

        private void RaiseChangedEvent()                           => Changed?.Invoke(this);

        private void RaiseChangedEvent(ChangedValue<Vector3> data) => RaiseChangedEvent();
    }
}