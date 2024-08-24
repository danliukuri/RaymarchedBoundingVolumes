using System;
using RaymarchedBoundingVolumes.Data.Dynamic.HierarchicalStates;
using RaymarchedBoundingVolumes.Data.Dynamic.ShaderData;
using RaymarchedBoundingVolumes.Data.Static.Enumerations;
using RaymarchedBoundingVolumes.Utilities.Attributes;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine;
using ObservableTypeRelatedShaderData = RaymarchedBoundingVolumes.Data.Dynamic.ShaderData.ObjectTypeRelated.ObservableRaymarchedObjectTypeRelatedShaderData;

namespace RaymarchedBoundingVolumes.Features
{
    public class RaymarchedObject : RaymarchingHierarchicalFeature<RaymarchedObject>
    {
        public event Action<RaymarchedObject> Changed;
        public event Action<RaymarchedObject> TypeChanged;
        public event Action<RaymarchedObject> TypeRelatedDataChanged;

        [field: SerializeField, Unwrapped] public ObservableValue<RaymarchedObjectType> Type { get; private set; }
        [field: SerializeField]            public ObservableTypeRelatedShaderData TypeRelatedData { get; private set; }
        [field: SerializeField]            public ObservableTransform<Vector3> Transform { get; private set; }

        private readonly ObservableTransform<Vector3> _gameObjectTransform = new();

        public object TypeRelatedShaderData => TypeRelatedData.GetShaderData(Type.Value);
        public int    TypeRelatedDataIndex  { get; set; }

        public override IRaymarchingFeatureHierarchicalState HierarchicalState =>
            new RaymarchedObjectHierarchicalState { BaseState = base.HierarchicalState, Type = Type.Value };

        public RaymarchedObjectShaderData ShaderData => new()
        {
            Type = (int)Type.Value,
            TypeRelatedDataIndex = TypeRelatedDataIndex,
            IsActive = Convert.ToInt32(enabled && gameObject is { activeSelf: true, activeInHierarchy: true }),
            Position = transform.position + Transform.Position.Value
        };

        protected override void OnEnable()
        {
            base.OnEnable();
            RaiseChangedEvent();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            RaiseChangedEvent();
        }

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            Type.Changed                 += RaiseChangedEvent;
            Type.Changed                 += RaiseTypeChangedEvent;
            Transform.Changed            += RaiseChangedEvent;
            TypeRelatedData.Changed      += RaiseTypeRelatedDataChangedEvent;
            _gameObjectTransform.Changed += RaiseChangedEvent;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            Type.Changed                 -= RaiseChangedEvent;
            Type.Changed                 -= RaiseTypeChangedEvent;
            Transform.Changed            -= RaiseChangedEvent;
            TypeRelatedData.Changed      -= RaiseTypeRelatedDataChangedEvent;
            _gameObjectTransform.Changed -= RaiseChangedEvent;
        }

        protected override void UpdateTransform()
        {
            base.UpdateTransform();
            _gameObjectTransform.SetValuesFrom(transform);
        }


        private void RaiseChangedEvent()                                        => Changed?.Invoke(this);
        private void RaiseChangedEvent(ChangedValue<Vector3>              data) => RaiseChangedEvent();
        private void RaiseChangedEvent(ChangedValue<RaymarchedObjectType> type) => RaiseChangedEvent();

        private void RaiseTypeChangedEvent(ChangedValue<RaymarchedObjectType> obj) => TypeChanged?.Invoke(this);

        private void RaiseTypeRelatedDataChangedEvent(ChangedValue<object> typeData) =>
            TypeRelatedDataChanged?.Invoke(this);
    }
}