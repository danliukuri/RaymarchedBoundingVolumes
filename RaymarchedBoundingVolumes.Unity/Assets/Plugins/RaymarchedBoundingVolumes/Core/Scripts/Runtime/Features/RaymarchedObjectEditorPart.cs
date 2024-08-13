#if UNITY_EDITOR
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features
{
    [ExecuteInEditMode]
    public partial class RaymarchedObject
    {
        private bool IsInitialized => _parent != default;

        private void OnEnable()
        {
            if (!IsInitialized)
                Initialize();

            SubscribeToChanges();
        }
    }
}
#endif