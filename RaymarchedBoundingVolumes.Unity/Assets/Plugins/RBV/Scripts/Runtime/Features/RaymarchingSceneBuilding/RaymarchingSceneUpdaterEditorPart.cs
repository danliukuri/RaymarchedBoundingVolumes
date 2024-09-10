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
            _sceneBuilder.NewSceneBuilt += MarkFeaturesAsChanged;
            EditorApplication.delayCall += EditorInitialize;
        }

        private void OnDisable() => EditorDeinitialize();

        private void EditorInitialize()
        {
            EditorApplication.delayCall -= EditorInitialize;

            if (IsValid)
            {
                _sceneBuilder.BuildLastScene();
                EditorApplication.QueuePlayerLoopUpdate();
            }
        }

        private void EditorDeinitialize()
        {
            if (IsValid)
            {
                _sceneBuilder.NewSceneBuilt -= MarkFeaturesAsChanged;
                Deinitialize();
                UnsubscribeFromFeatureEvents();
            }
        }

        private void MarkFeaturesAsChanged(Scene scene) => EditorUtility.SetDirty(this);
    }
}
#endif