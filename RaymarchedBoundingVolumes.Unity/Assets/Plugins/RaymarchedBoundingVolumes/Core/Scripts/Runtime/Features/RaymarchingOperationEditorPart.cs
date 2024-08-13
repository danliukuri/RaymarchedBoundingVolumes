#if UNITY_EDITOR
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features
{
    [ExecuteInEditMode]
    public partial class RaymarchingOperation
    {
        private bool IsInitialized => DirectChildObjectsCount <= default(int);

        private void OnEnable()
        {
            if (!IsInitialized)
                Initialize();

            SubscribeToChanges();
        }
    }
}
#endif