#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    [ExecuteInEditMode]
    public partial class RaymarchingSceneBuilder
    {
        private bool IsValid => this != default && gameObject.scene.isLoaded;

        private void OnEnable() => EditorApplication.delayCall += EditorInitialize;

        private void OnDisable()
        {
            if (IsValid)
            {
                Deinitialize();
                UnsubscribeFromEvents();
            }
        }

        private void EditorInitialize()
        {
            EditorApplication.delayCall -= EditorInitialize;

            if (IsValid)
            {
                Construct();
                _raymarchingFeaturesRegister.RegisterFeatures();
                foreach (RaymarchingOperation operation in Data.Operations)
                    operation.CalculateChildrenCount();
                SubscribeToEvents();
                BuildSceneInternal();
                EditorApplication.QueuePlayerLoopUpdate();
            }
        }
    }
}
#endif