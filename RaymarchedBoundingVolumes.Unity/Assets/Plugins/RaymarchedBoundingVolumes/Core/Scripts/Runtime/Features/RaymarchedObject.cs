using System;
using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features
{
    [ExecuteInEditMode]
    public class RaymarchedObject : MonoBehaviour
    {
        public event Action<RaymarchedObject> Changed;

        [field: SerializeField] public ObservableTransform<Vector3> Transform { get; private set; }

        public RaymarchedObjectShaderData ShaderData => new()
            {
                _isActive = Convert.ToInt32(gameObject is { activeSelf: true, activeInHierarchy: true }),
                _position = transform.position + Transform.Position.Value,
            };

        private void OnEnable()
        {
            Transform.Position.Changed += RaiseChangedEvent;
            Transform.Rotation.Changed += RaiseChangedEvent;
            Transform.Scale   .Changed += RaiseChangedEvent;
            RaiseChangedEvent();
        }

        private void OnDisable()
        {
            Transform.Position.Changed -= RaiseChangedEvent;
            Transform.Rotation.Changed -= RaiseChangedEvent;
            Transform.Scale   .Changed -= RaiseChangedEvent;
            RaiseChangedEvent();
        }

        private void Update()
        {
            if(transform.hasChanged)
                RaiseChangedEvent();
        }

        private void RaiseChangedEvent()              => Changed?.Invoke(this);
        private void RaiseChangedEvent(Vector3 data)  => RaiseChangedEvent();
    }
}