using System;
using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Utilities.Attributes;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine;
using Type = RaymarchedBoundingVolumes.Data.Static.RaymarchingOperationType;

namespace RaymarchedBoundingVolumes.Features
{
    [ExecuteInEditMode]
    public class RaymarchingOperation : MonoBehaviour
    {
        public event Action<RaymarchingOperation> Changed;

        [field: SerializeField, Unwrapped] public ObservableValue<Type>  OperationType { get; private set; }
        [field: SerializeField, Unwrapped] public ObservableValue<float> BlendStrength { get; private set; }

        public RaymarchingOperationShaderData ShaderData => new()
            {
                _operationType = (int) OperationType.Value,
                _childCount = transform.childCount,
                _blendStrength = BlendStrength.Value,
            };

        private void OnEnable()
        {
            OperationType.Changed += RaiseChangedEvent;
            BlendStrength.Changed += RaiseChangedEvent;
            RaiseChangedEvent();
        }
        
        private void OnDisable()
        {
            OperationType.Changed -= RaiseChangedEvent;
            BlendStrength.Changed -= RaiseChangedEvent;
            RaiseChangedEvent();
        }

        private void Update()
        {
            if(transform.hasChanged)
                RaiseChangedEvent();
        }

        private void RaiseChangedEvent() => Changed?.Invoke(this);
        private void RaiseChangedEvent(float data) => RaiseChangedEvent();
        private void RaiseChangedEvent(Type data) => RaiseChangedEvent();
    }
}