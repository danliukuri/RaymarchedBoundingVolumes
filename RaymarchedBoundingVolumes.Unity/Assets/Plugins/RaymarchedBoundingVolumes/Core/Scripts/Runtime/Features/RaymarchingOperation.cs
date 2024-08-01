using System.Linq;
using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Utilities.Attributes;
using RaymarchedBoundingVolumes.Utilities.Extensions;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine;
using Type = RaymarchedBoundingVolumes.Data.Static.RaymarchingOperationType;

namespace RaymarchedBoundingVolumes.Features
{
    [ExecuteInEditMode]
    public class RaymarchingOperation : ObservableMonoBehaviour<RaymarchingOperation>
    {
        [field: SerializeField, Unwrapped] public ObservableValue<Type>  OperationType { get; private set; }
        [field: SerializeField, Unwrapped] public ObservableValue<float> BlendStrength { get; private set; }

        public int RaymarchedChildCount { get; private set; }

        public RaymarchingOperationShaderData ShaderData => new()
        {
            _operationType = (int)OperationType.Value,
            _childCount    = RaymarchedChildCount,
            _blendStrength = BlendStrength.Value
        };

        private void OnTransformChildrenChanged() => RaymarchedChildCount = CalculateRaymarchedChildCount();

        protected override void Initialize()
        {
            base.Initialize();
            RaymarchedChildCount = CalculateRaymarchedChildCount();
        }

        protected override void SubscribeToChanges()
        {
            base.SubscribeToChanges();
            OperationType.Changed += RaiseChangedEvent;
            BlendStrength.Changed += RaiseChangedEvent;
        }

        protected override void UnsubscribeToChanges()
        {
            base.UnsubscribeToChanges();
            OperationType.Changed -= RaiseChangedEvent;
            BlendStrength.Changed -= RaiseChangedEvent;
        }

        private int CalculateRaymarchedChildCount() =>
            transform.GetChildren().Count(child => child.HasComponent<RaymarchedObject>());

        private void RaiseChangedEvent(ChangedValue<float> blendStrength) => RaiseChangedEvent();
        private void RaiseChangedEvent(ChangedValue<Type> type)           => RaiseChangedEvent();
    }
}