#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using RaymarchedBoundingVolumes.Data.Dynamic;
using UnityEditor;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    [ExecuteInEditMode]
    public partial class RaymarchingSceneBuilder
    {
        private bool IsValid => this != default && gameObject.scene.isLoaded;

        private void OnEnable()
        {
            EditorApplication.hierarchyChanged += BuildSceneIfChanged;
            EditorApplication.delayCall        += EditorInitialize;
        }

        private void OnDisable()
        {
            EditorApplication.hierarchyChanged -= BuildSceneIfChanged;

            if (IsValid)
            {
                Deinitialize();
                UnsubscribeFromChanges();
            }
        }

        private void OnDestroy()
        {
            if (IsValid)
                Deinitialize();
        }

        private void BuildSceneIfChanged()
        {
            List<RaymarchingFeature> oldFeatures = Data.Features;
            Dictionary<RaymarchingFeature, OperationChildrenData> oldOperationData = Data.Features.ToDictionary(
                feature => feature,
                feature => feature is RaymarchingOperation operation ? operation.Children : default
            );

            Data.Features = _raymarchingFeaturesRegister.FindAllRaymarchingFeatures(gameObject.scene);
            foreach (RaymarchingOperation operation in Data.Operations)
                operation.CalculateChildrenCount();

            if (IsSceneChanged(oldOperationData, oldFeatures, Data.Features))
            {
                UnsubscribeFromChanges();
                _raymarchingFeaturesRegister.RegisterFeatures();
                SubscribeToChanges();
                BuildScene();
                EditorUtility.SetDirty(this);
            }
        }
        
        private bool IsSceneChanged(Dictionary<RaymarchingFeature, OperationChildrenData> oldOperationData,
            List<RaymarchingFeature> oldFeatures, List<RaymarchingFeature> newFeatures)
        {
            if (oldFeatures.Count != newFeatures.Count)
                return true;

            for (var i = 0; i < oldFeatures.Count; i++)
            {
                if (oldFeatures[i] != newFeatures[i])
                    return true;

                if (newFeatures[i] is RaymarchingOperation newOperation &&
                    oldOperationData[oldFeatures[i]] != newOperation.Children)
                    return true;
            }

            return false;
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
                SubscribeToChanges();
                BuildScene();
                EditorApplication.QueuePlayerLoopUpdate();
            }
        }
    }
}
#endif