﻿using System;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.HierarchicalStates;
using RBV.Data.Dynamic.ShaderData;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Data.Static.Enumerations;
using RBV.Utilities.Wrappers;
using UnityEngine;

namespace RBV.Features
{
    public abstract class RaymarchedObject : RaymarchingHierarchicalFeature<RaymarchedObject>
    {
        public event Action<RaymarchedObject> Changed;
        public event Action<RaymarchedObject> TransformChanged;
        public event Action<RaymarchedObject> TypeChanged;
        public event Action<RaymarchedObject> TypeDataChanged;
        public event Action<RaymarchedObject> RenderingSettingsChanged;

        private readonly ObservableTransform<Vector3> _gameObjectTransform = new();

        public override IRaymarchingFeatureHierarchicalState HierarchicalState =>
            new RaymarchedObjectHierarchicalState { BaseState = base.HierarchicalState, Type = Type.Value };

        public abstract ObservableValue<RaymarchedObjectType> Type          { get; protected set; }
        public abstract IObservableObjectTypeShaderData       TypeData      { get; }
        public abstract IObservableTransform                  Transform     { get; }
        public abstract TransformType                         TransformType { get; }

        public abstract ObservableValue<RaymarchedObjectRenderingSettingsShaderData> RenderingSettings { get; }

        public abstract ITransformShaderData TransformShaderData { get; }

        public IObjectTypeShaderData TypeShaderData => TypeData.GetShaderData(Type.Value);

        public RaymarchedObjectShaderData ShaderData => new()
        {
            Type               = (int)Type.Value,
            TypeDataIndex      = TypeDataIndex,
            TransformType      = (int)TransformType,
            TransformDataIndex = TransformDataIndex
        };

        public int TypeDataIndex      { get; set; }
        public int TransformDataIndex { get; set; }

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            Type.Changed                 += RaiseChangedEvent;
            Type.Changed                 += RaiseTypeChangedEvent;
            Transform.Changed            += RaiseTransformChangedEvent;
            TypeData.Changed             += RaiseTypeDataChangedEvent;
            RenderingSettings.Changed    += RaiseRenderingSettingsChangedEvent;
            _gameObjectTransform.Changed += RaiseTransformChangedEvent;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            Type.Changed                 -= RaiseChangedEvent;
            Type.Changed                 -= RaiseTypeChangedEvent;
            Transform.Changed            -= RaiseTransformChangedEvent;
            TypeData.Changed             -= RaiseTypeDataChangedEvent;
            RenderingSettings.Changed    -= RaiseRenderingSettingsChangedEvent;
            _gameObjectTransform.Changed -= RaiseTransformChangedEvent;
        }

        protected override void UpdateTransform()
        {
            base.UpdateTransform();
            _gameObjectTransform.SetValuesFrom(transform);
        }

        private void RaiseChangedEvent()                                            => Changed?.Invoke(this);
        private void RaiseChangedEvent(ChangedValue<RaymarchedObjectType>     type) => RaiseChangedEvent();
        private void RaiseTypeChangedEvent(ChangedValue<RaymarchedObjectType> type) => TypeChanged?.Invoke(this);

        private void RaiseTypeDataChangedEvent(ChangedValue<IObjectTypeShaderData> typeData) =>
            TypeDataChanged?.Invoke(this);

        private void RaiseTransformChangedEvent()                           => TransformChanged?.Invoke(this);
        private void RaiseTransformChangedEvent(ChangedValue<Vector3> data) => RaiseTransformChangedEvent();

        private void RaiseRenderingSettingsChangedEvent(ChangedValue<RaymarchedObjectRenderingSettingsShaderData> data)
            => RenderingSettingsChanged?.Invoke(this);
    }
}