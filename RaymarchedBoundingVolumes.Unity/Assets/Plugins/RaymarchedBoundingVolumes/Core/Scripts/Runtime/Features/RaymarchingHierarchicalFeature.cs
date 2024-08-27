using System;
using RaymarchedBoundingVolumes.Data.Dynamic.HierarchicalStates;
using RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding;
using RaymarchedBoundingVolumes.Infrastructure;
using RaymarchedBoundingVolumes.Utilities.Wrappers;

namespace RaymarchedBoundingVolumes.Features
{
    public abstract partial class RaymarchingHierarchicalFeature<T> : RaymarchingFeature,
                                                                      IRaymarchingHierarchicalFeature
        where T : RaymarchingHierarchicalFeature<T>
    {
        public event Action<T>                    ParentChanged;
        public event Action<T, ChangedValue<int>> Reordered;

        private IRaymarchingSceneBuilder _sceneBuilder;
        private ObservableValue<int>     _siblingIndex;

        public virtual IRaymarchingFeatureHierarchicalState HierarchicalState =>
            new RaymarchingFeatureHierarchicalState { SiblingIndex = _siblingIndex.Value };

        protected virtual void Construct() =>
            Construct(IServiceContainer.Scoped(gameObject.scene).Resolve<IRaymarchingSceneBuilder>());

        public void Construct(IRaymarchingSceneBuilder sceneBuilder) => _sceneBuilder = sceneBuilder;

        protected virtual void Awake()
        {
            Construct();
            _siblingIndex = new ObservableValue<int>(transform.GetSiblingIndex());
        }

        protected void Start() => _sceneBuilder.BuildNewScene();

        protected virtual void Update()
        {
            if (transform.hasChanged)
                UpdateTransform();
            _siblingIndex.Value = transform.GetSiblingIndex();
        }

#if !UNITY_EDITOR
        protected virtual void OnEnable()  => SubscribeToEvents();
#endif
        protected virtual void OnDisable() => UnsubscribeFromEvents();

        protected void OnDestroy() => _sceneBuilder?.BuildNewScene();

        protected virtual void OnTransformParentChanged() => RaiseParentChangedEvent();

        protected virtual void UpdateTransform() => transform.hasChanged = false;

        protected virtual void SubscribeToEvents()     => _siblingIndex.Changed += RaiseReorderedEvent;
        protected virtual void UnsubscribeFromEvents() => _siblingIndex.Changed -= RaiseReorderedEvent;

        private void RaiseReorderedEvent(ChangedValue<int> siblingIndex) => Reordered?.Invoke(this as T, siblingIndex);
        private void RaiseParentChangedEvent()                           => ParentChanged?.Invoke(this as T);
    }
}