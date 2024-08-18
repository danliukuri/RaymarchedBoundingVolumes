using System;
using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Data.Dynamic.ShaderData;
using RaymarchedBoundingVolumes.Infrastructure;
using RaymarchedBoundingVolumes.Utilities.Attributes;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine;
using Type = RaymarchedBoundingVolumes.Data.Static.RaymarchingOperationType;

namespace RaymarchedBoundingVolumes.Features
{
    public partial class RaymarchingOperation : RaymarchingFeature
    {
        public event Action<RaymarchingOperation> Changed;

        [field: SerializeField, Unwrapped] public ObservableValue<Type>  OperationType { get; private set; }
        [field: SerializeField, Unwrapped] public ObservableValue<float> BlendStrength { get; private set; }

        private IRaymarchingChildrenCalculator _raymarchingChildrenCalculator;

        public OperationChildrenData Children { get; private set; }

        public RaymarchingOperationShaderData ShaderData => new()
        {
            OperationType = (int)OperationType.Value,
            BlendStrength = BlendStrength.Value
        };

        private void Awake() => Construct();

        private void Construct() => Construct(IServiceContainer.Global.Resolve<IRaymarchingChildrenCalculator>());

        public void Construct(IRaymarchingChildrenCalculator raymarchingChildrenCalculator) =>
            _raymarchingChildrenCalculator = raymarchingChildrenCalculator;

#if !UNITY_EDITOR
        private void OnEnable()  => SubscribeToChanges();
#endif
        private void OnDisable() => UnsubscribeToChanges();

        public OperationChildrenData CalculateChildrenCount() =>
            Children = _raymarchingChildrenCalculator.CalculateChildrenCount(this);

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

        private void RaiseChangedEvent()                                  => Changed?.Invoke(this);
        private void RaiseChangedEvent(ChangedValue<float> blendStrength) => RaiseChangedEvent();
        private void RaiseChangedEvent(ChangedValue<Type> type)           => RaiseChangedEvent();
    }
}