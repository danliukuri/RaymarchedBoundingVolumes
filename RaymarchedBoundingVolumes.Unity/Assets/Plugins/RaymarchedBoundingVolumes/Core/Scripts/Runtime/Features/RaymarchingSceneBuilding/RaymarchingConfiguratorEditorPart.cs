#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    [ExecuteInEditMode]
    public partial class RaymarchingConfigurator
    {
        private bool IsInitialized => data.OperationMetaData != default;

        private void Start()
        {
            if (data.Features.Any() && !IsInitialized)
                Initialize();
        }

        private void OnEnable()
        {
            EditorApplication.delayCall += EditorInitialize;
            SubscribeToChanges();
        }

        private void OnDisable()
        {
            Deinitialize();
            UnsubscribeFromChanges();
        }

        private void EditorInitialize()
        {
            EditorApplication.delayCall -= EditorInitialize;

            if (!data.Features.Any())
            {
                RegisterAllRaymarchingFeatures();
                RegisterFeatures();
            }
            if (!IsInitialized)
                Initialize();
        }

        private void RegisterAllRaymarchingFeatures()
        {
            data.Features = FindAllRaymarchingFeatures(gameObject.scene);
            EditorUtility.SetDirty(this);
        }

        private static List<RaymarchingFeature> FindAllRaymarchingFeatures(Scene scene) => scene.GetRootGameObjects()
            .SelectMany(rootGameObject => rootGameObject.GetComponentsInChildren<RaymarchingFeature>()).ToList();
    }
}
#endif