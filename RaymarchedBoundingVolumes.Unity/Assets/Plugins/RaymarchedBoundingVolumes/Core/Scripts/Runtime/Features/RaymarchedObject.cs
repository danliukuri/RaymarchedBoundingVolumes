using System;
using RaymarchedBoundingVolumes.Data.Dynamic.ShaderData;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features
{
    public partial class RaymarchedObject : RaymarchingFeature
    {
        public event Action<RaymarchedObject>                           Changed;
        public event Action<RaymarchedObject, IChangedValue<Transform>> ParentChanged;

        private readonly ObservableTransform<Vector3> _gameObjectTransform = new();
        private          ObservableValue<Transform>   _parent;

        [field: SerializeField] public ObservableTransform<Vector3> Transform { get; private set; }

        public RaymarchedObjectShaderData ShaderData => new()
        {
            IsActive = Convert.ToInt32(gameObject is { activeSelf: true, activeInHierarchy: true }),
            Position = transform.position + Transform.Position.Value,
        };

        private void Awake() => Initialize();

#if !UNITY_EDITOR
        private void OnEnable()  => SubscribeToChanges();
#endif
        private void OnDisable() => UnsubscribeToChanges();

        private void Update()
        {
            if (transform.hasChanged)
                UpdateTransform();
        }

        private void Initialize() => _parent = new ObservableValue<Transform>(transform.parent);

        private void SubscribeToChanges()
        {
            Transform.Changed            += RaiseChangedEvent;
            _gameObjectTransform.Changed += RaiseChangedEvent;
            _parent.Changed              += RaiseParentChangedEvent;
        }

        private void UnsubscribeToChanges()
        {
            Transform.Changed            -= RaiseChangedEvent;
            _gameObjectTransform.Changed -= RaiseChangedEvent;
            _parent.Changed              -= RaiseParentChangedEvent;
        }

        private void UpdateTransform()
        {
            _gameObjectTransform.SetValuesFrom(transform);
            _parent.Value        = transform.parent;
            transform.hasChanged = false;
        }


        private void RaiseChangedEvent()                                     => Changed?.Invoke(this);
        private void RaiseChangedEvent(ChangedValue<Vector3> data)           => RaiseChangedEvent();
        private void RaiseParentChangedEvent(ChangedValue<Transform> parent) => ParentChanged?.Invoke(this, parent);
    }
}