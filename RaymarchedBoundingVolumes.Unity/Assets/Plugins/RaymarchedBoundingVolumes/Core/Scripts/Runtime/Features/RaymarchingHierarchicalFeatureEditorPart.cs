﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features
{
    [ExecuteInEditMode]
    public abstract partial class RaymarchingHierarchicalFeature<T>
    {
        private bool IsValid => this != default && gameObject.scene.isLoaded;

        protected virtual void OnEnable()
        {
            Construct();
            EditorApplication.delayCall += EditorInitialize;
        }

        private void EditorInitialize()
        {
            EditorApplication.delayCall -= EditorInitialize;

            if (IsValid)
                SubscribeToEvents();
        }
    }
}
#endif