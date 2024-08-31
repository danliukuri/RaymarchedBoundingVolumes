using System;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.HierarchicalStates;
using RBV.Data.Dynamic.ShaderData;
using RBV.Data.Dynamic.ShaderData.ObjectTypeRelated;
using RBV.Data.Static.Enumerations;
using RBV.Utilities.Wrappers;
using UnityEngine;
using IObservableTypeRelatedShaderData =
    RBV.Data.Dynamic.ShaderData.ObjectTypeRelated.IObservableRaymarchedObjectTypeRelatedShaderData;

namespace RBV.Features
{
    public abstract class RaymarchedObject : RaymarchingHierarchicalFeature<RaymarchedObject>
    {
        public event Action<RaymarchedObject> Changed;
        public event Action<RaymarchedObject> TransformChanged;
        public event Action<RaymarchedObject> TypeChanged;
        public event Action<RaymarchedObject> TypeRelatedDataChanged;

        private readonly ObservableTransform<Vector3> _gameObjectTransform = new();

        public override IRaymarchingFeatureHierarchicalState HierarchicalState =>
            new RaymarchedObjectHierarchicalState { BaseState = base.HierarchicalState, Type = Type.Value };

        public abstract ObservableValue<RaymarchedObjectType> Type            { get; protected set; }
        public abstract IObservableTypeRelatedShaderData      TypeRelatedData { get; }
        public abstract IObservableTransform                  Transform       { get; }
        public abstract TransformType                         TransformType   { get; }

        public abstract object TransformShaderData { get; }

        public IObjectTypeRelatedShaderData TypeRelatedShaderData => TypeRelatedData.GetShaderData(Type.Value);

        public RaymarchedObjectShaderData ShaderData => new()
        {
            Type = (int)Type.Value,
            TypeRelatedDataIndex = TypeRelatedDataIndex,
            IsActive = Convert.ToInt32(enabled && gameObject is { activeSelf: true, activeInHierarchy: true }),
            TransformType = (int)TransformType
        };

        public int TypeRelatedDataIndex { get; set; }

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
            Transform.Changed            += RaiseTransformChangedEvent;
            TypeRelatedData.Changed      += RaiseTypeRelatedDataChangedEvent;
            _gameObjectTransform.Changed += RaiseTransformChangedEvent;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            Type.Changed                 -= RaiseChangedEvent;
            Type.Changed                 -= RaiseTypeChangedEvent;
            Transform.Changed            -= RaiseTransformChangedEvent;
            TypeRelatedData.Changed      -= RaiseTypeRelatedDataChangedEvent;
            _gameObjectTransform.Changed -= RaiseTransformChangedEvent;
        }

        protected override void UpdateTransform()
        {
            base.UpdateTransform();
            _gameObjectTransform.SetValuesFrom(transform);
        }


        private void RaiseChangedEvent()                                            => Changed?.Invoke(this);
        private void RaiseChangedEvent(ChangedValue<RaymarchedObjectType> type)     => RaiseChangedEvent();
        private void RaiseTransformChangedEvent()                                   => TransformChanged?.Invoke(this);
        private void RaiseTransformChangedEvent(ChangedValue<Vector3>         data) => RaiseTransformChangedEvent();
        private void RaiseTypeChangedEvent(ChangedValue<RaymarchedObjectType> type) => TypeChanged?.Invoke(this);

        private void RaiseTypeRelatedDataChangedEvent(ChangedValue<IObjectTypeRelatedShaderData> typeData) =>
            TypeRelatedDataChanged?.Invoke(this);
    }
}