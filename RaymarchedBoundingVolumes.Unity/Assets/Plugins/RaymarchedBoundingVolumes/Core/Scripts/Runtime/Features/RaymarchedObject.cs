using System;
using RaymarchedBoundingVolumes.Data.Dynamic.ShaderData;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features
{
    public partial class RaymarchedObject : RaymarchingHierarchicalFeature<RaymarchedObject>
    {
        public event Action<RaymarchedObject> Changed;

        private readonly ObservableTransform<Vector3> _gameObjectTransform = new();

        [field: SerializeField] public ObservableTransform<Vector3> Transform { get; private set; }

        public RaymarchedObjectShaderData ShaderData => new()
        {
            IsActive = Convert.ToInt32(gameObject is { activeSelf: true, activeInHierarchy: true }),
            Position = transform.position + Transform.Position.Value
        };

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            Transform.Changed            += RaiseChangedEvent;
            _gameObjectTransform.Changed += RaiseChangedEvent;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            Transform.Changed            -= RaiseChangedEvent;
            _gameObjectTransform.Changed -= RaiseChangedEvent;
        }

        protected override void UpdateTransform()
        {
            base.UpdateTransform();
            _gameObjectTransform.SetValuesFrom(transform);
        }

        private void RaiseChangedEvent()                           => Changed?.Invoke(this);

        private void RaiseChangedEvent(ChangedValue<Vector3> data) => RaiseChangedEvent();
    }
}