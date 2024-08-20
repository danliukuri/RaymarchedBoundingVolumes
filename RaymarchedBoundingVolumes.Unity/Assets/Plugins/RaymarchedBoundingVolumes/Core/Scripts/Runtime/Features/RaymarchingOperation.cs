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
    public partial class RaymarchingOperation : RaymarchingHierarchicalFeature<RaymarchingOperation>
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

        protected override void Awake()
        {
            base.Awake();
            Construct();
        }

        protected override void Construct()
        {
            base.Construct();
            Construct(IServiceContainer.Global.Resolve<IRaymarchingChildrenCalculator>());
        }

        public void Construct(IRaymarchingChildrenCalculator raymarchingChildrenCalculator) =>
            _raymarchingChildrenCalculator = raymarchingChildrenCalculator;

        public OperationChildrenData CalculateChildrenCount() =>
            Children = _raymarchingChildrenCalculator.CalculateChildrenCount(this);

        protected override void SubscribeToEvents()
        {
            base.SubscribeToEvents();
            OperationType.Changed += RaiseChangedEvent;
            BlendStrength.Changed += RaiseChangedEvent;
        }

        protected override void UnsubscribeFromEvents()
        {
            base.UnsubscribeFromEvents();
            OperationType.Changed -= RaiseChangedEvent;
            BlendStrength.Changed -= RaiseChangedEvent;
        }

        private void RaiseChangedEvent()                                  => Changed?.Invoke(this);
        private void RaiseChangedEvent(ChangedValue<float> blendStrength) => RaiseChangedEvent();
        private void RaiseChangedEvent(ChangedValue<Type> type)           => RaiseChangedEvent();
    }
}