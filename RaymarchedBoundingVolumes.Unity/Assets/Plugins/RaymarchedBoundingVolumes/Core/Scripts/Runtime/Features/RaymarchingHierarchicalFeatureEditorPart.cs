using UnityEngine;

namespace RaymarchedBoundingVolumes.Features
{
    [ExecuteInEditMode]
    public abstract partial class RaymarchingHierarchicalFeature<T>
    {
        private bool IsValid => this != default && gameObject.scene.isLoaded;

        protected virtual void OnEnable()
        {
            if (IsValid)
            {
                Construct();
                SubscribeToEvents();
            }
        }
    }
}