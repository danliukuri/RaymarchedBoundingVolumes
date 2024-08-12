using System;
using System.Linq;
using RaymarchedBoundingVolumes.Data.Dynamic.ShaderData;
using RaymarchedBoundingVolumes.Utilities.Attributes;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine;
using Type = RaymarchedBoundingVolumes.Data.Static.RaymarchingOperationType;

namespace RaymarchedBoundingVolumes.Features
{
    public class RaymarchingOperation : RaymarchingFeature
    {
        public event Action<RaymarchingOperation> Changed;

        [field: SerializeField, Unwrapped] public ObservableValue<Type>  OperationType { get; private set; }
        [field: SerializeField, Unwrapped] public ObservableValue<float> BlendStrength { get; private set; }

        public int DirectChildObjectsCount    { get; private set; }
        public int ChildObjectsCount          { get; private set; }
        public int DirectChildOperationsCount { get; private set; }
        public int ChildOperationsCount       { get; private set; }

        public RaymarchingOperationShaderData ShaderData => new()
        {
            OperationType = (int)OperationType.Value,
            BlendStrength = BlendStrength.Value
        };

        private void Awake() => Initialize();

        private void OnEnable()  => SubscribeToChanges();
        private void OnDisable() => UnsubscribeToChanges();

        private void OnTransformChildrenChanged() => CalculateChildCount();

        public void Initialize() => CalculateChildCount();

        private void CalculateChildCount()
        {
            CalculateDirectChildObjectsCount();
            CalculateChildObjectsCount();
            CalculateDirectChildOperationsCount();
            CalculateChildOperationsCount();
        }

        private void SubscribeToChanges()
        {
            OperationType.Changed += RaiseChangedEvent;
            BlendStrength.Changed += RaiseChangedEvent;
        }

        private void UnsubscribeToChanges()
        {
            OperationType.Changed -= RaiseChangedEvent;
            BlendStrength.Changed -= RaiseChangedEvent;
        }

        private int CalculateDirectChildObjectsCount() => DirectChildObjectsCount =
            transform.GetComponentsInChildren<RaymarchedObject>()
                     .Count(child => child.GetComponentInParent<RaymarchingOperation>() == this);

        private int CalculateChildObjectsCount() => ChildObjectsCount =
            transform.GetComponentsInChildren<RaymarchedObject>().Length;

        private int CalculateDirectChildOperationsCount() => DirectChildOperationsCount = transform
            .GetComponentsInChildren<RaymarchingOperation>().Count(operation =>
                operation != this && operation.GetComponentsInParent<RaymarchingOperation>()
                    .SkipWhile(component => component == operation).First() == this);

        private int CalculateChildOperationsCount() => ChildOperationsCount =
            transform.GetComponentsInChildren<RaymarchingOperation>().Count(operation => operation != this);

        private void RaiseChangedEvent()                                  => Changed?.Invoke(this);
        private void RaiseChangedEvent(ChangedValue<float> blendStrength) => RaiseChangedEvent();
        private void RaiseChangedEvent(ChangedValue<Type> type)           => RaiseChangedEvent();
    }
}