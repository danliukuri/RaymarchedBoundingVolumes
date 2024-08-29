#if UNITY_EDITOR
using UnityEngine;

namespace RBV.Infrastructure
{
    [ExecuteInEditMode]
    public partial class RaymarchingServicesRegister
    {
        private bool _isInitialized;

        private void Awake()
        {
            RegisterSceneServices(gameObject.scene);
            _isInitialized = true;
        }

        private void OnEnable()
        {
            if (!_isInitialized)
                RegisterSceneServices(gameObject.scene);
        }

        private void OnDisable()
        {
            ServiceContainer.Scoped(gameObject.scene).Dispose();
            _isInitialized = false;
        }
    }
}
#endif