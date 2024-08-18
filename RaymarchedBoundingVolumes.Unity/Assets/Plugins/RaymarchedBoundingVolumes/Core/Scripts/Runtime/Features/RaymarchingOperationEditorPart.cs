#if UNITY_EDITOR
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features
{
    [ExecuteInEditMode]
    public partial class RaymarchingOperation
    {
        private bool IsValid => this != default && gameObject.scene.isLoaded;

        private void OnEnable()
        {
            if (IsValid)
            {
                Construct();
                SubscribeToChanges();
            }
        }
    }
}
#endif