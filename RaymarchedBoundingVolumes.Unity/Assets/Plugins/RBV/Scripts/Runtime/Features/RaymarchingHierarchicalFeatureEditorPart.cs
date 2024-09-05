#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace RBV.Features
{
    [ExecuteInEditMode]
    public abstract partial class RaymarchingHierarchicalFeature<T>
    {
        private bool IsValid => this != default && gameObject.scene.isLoaded;

        protected virtual void OnEnable()
        {
            Construct();
            Initialize();
            EditorApplication.delayCall += EditorInitialize;
        }

        private void EditorInitialize()
        {
            EditorApplication.delayCall -= EditorInitialize;

            if (IsValid)
            {
                SubscribeToEvents();
                RaiseActiveStateChangedEvent();
            }
        }
    }
}
#endif