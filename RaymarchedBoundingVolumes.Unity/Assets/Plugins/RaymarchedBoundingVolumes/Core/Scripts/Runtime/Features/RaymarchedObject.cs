using System;
using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features
{
    [ExecuteInEditMode]
    public class RaymarchedObject : ObservableMonoBehaviour<RaymarchedObject>
    {
        [field: SerializeField] public ObservableTransform<Vector3> Transform { get; private set; }

        public RaymarchedObjectShaderData ShaderData => new()
        {
            _isActive = Convert.ToInt32(gameObject is { activeSelf: true, activeInHierarchy: true }),
            _position = transform.position + Transform.Position.Value,
        };

        protected override void SubscribeToChanges()
        {
            base.SubscribeToChanges();
            Transform.Changed += RaiseChangedEvent;
        }

        protected override void UnsubscribeToChanges()
        {
            base.UnsubscribeToChanges();
            Transform.Changed -= RaiseChangedEvent;
        }
    }
}