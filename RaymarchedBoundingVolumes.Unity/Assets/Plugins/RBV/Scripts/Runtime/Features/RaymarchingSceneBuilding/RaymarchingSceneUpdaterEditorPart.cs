#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RBV.Features.RaymarchingSceneBuilding
{
    [ExecuteInEditMode]
    public partial class RaymarchingSceneUpdater
    {
        private bool IsValid => this != default && gameObject.scene.isLoaded;

        private void OnEnable()
        {
            Construct();
            EditorApplication.delayCall += EditorInitialize;
        }

        private void OnDisable() => EditorDeinitialize();

        private void EditorInitialize()
        {
            EditorApplication.delayCall -= EditorInitialize;

            if (IsValid)
            {
                _sceneBuilder.SceneBuilt += MarkFeaturesAsChanged;
                _sceneBuilder.BuildLastScene();
                EditorApplication.QueuePlayerLoopUpdate();
            }
        }

        private void EditorDeinitialize()
        {
            if (IsValid)
            {
                _sceneBuilder.SceneBuilt -= MarkFeaturesAsChanged;
                Deinitialize();
                UnsubscribeFromFeatureEvents();
            }
        }

        private void MarkFeaturesAsChanged(Scene scene) => EditorUtility.SetDirty(this);
    }
}
#endif