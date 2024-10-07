#if UNITY_EDITOR
using RBV.Utilities.Extensions;
using UnityEngine;

namespace RBV.Infrastructure
{
    [ExecuteInEditMode]
    public partial class RaymarchingServicesRegister
    {
        private bool _isInitialized;

        private void Awake()     => EditorInitializeIfNotAlreadyDid();
        private void OnEnable()  => EditorInitializeIfNotAlreadyDid();
        private void OnDisable() => EditorDeinitialize();

        private void EditorInitializeIfNotAlreadyDid() => _isInitialized.IfNotInvoke(Initialize).IfNotSet(true);

        private void EditorDeinitialize()
        {
            Deinitialize();
            _isInitialized = false;
        }
    }
}
#endif