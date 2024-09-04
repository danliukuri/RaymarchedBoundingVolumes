using RBV.Data.Dynamic;
using RBV.Infrastructure;
using UnityEngine;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public partial class RaymarchingSceneUpdater : MonoBehaviour,
        IRaymarchingSceneDataProvider, IRaymarchingFeatureEventsSubscriber
    {
        [field: SerializeField] public RaymarchingData Data { get; set; } = new();

        private IShaderBuffersInitializer _shaderBuffersInitializer;
        private IShaderDataUpdater        _shaderDataUpdater;
        private IRaymarchingSceneBuilder  _sceneBuilder;

        private void Awake() => Construct();

        private void Construct() => Construct(
            IServiceContainer.Global.Resolve<IShaderBuffersInitializer>(),
            IServiceContainer.Scoped(gameObject.scene).Resolve<IShaderDataUpdater>(),
            IServiceContainer.Scoped(gameObject.scene).Resolve<IRaymarchingSceneBuilder>());

        public void Construct(IShaderBuffersInitializer shaderBuffersInitializer,
                              IShaderDataUpdater        shaderDataUpdater,
                              IRaymarchingSceneBuilder  sceneBuilder)
        {
            _sceneBuilder             = sceneBuilder;
            _shaderBuffersInitializer = shaderBuffersInitializer;
            _shaderDataUpdater        = shaderDataUpdater;
        }

#if !UNITY_EDITOR
        private void Start()     => _sceneBuilder.BuildLastScene();
        private void OnEnable()  => SubscribeToFeatureEvents();
        private void OnDisable() => UnsubscribeFromFeatureEvents();
        private void OnDestroy() => Deinitialize();

#endif
        private void Update()
        {
            _sceneBuilder.Update(gameObject.scene);
            _shaderDataUpdater.Update();
        }

        private void Deinitialize() => _shaderBuffersInitializer.ReleaseBuffers();

        public IRaymarchingFeatureEventsSubscriber SubscribeToFeatureEvents()
        {
            _sceneBuilder.SubscribeToFeatureEvents();
            _shaderDataUpdater.SubscribeToFeatureEvents();
            return this;
        }

        public IRaymarchingFeatureEventsSubscriber UnsubscribeFromFeatureEvents()
        {
            _sceneBuilder.UnsubscribeFromFeatureEvents();
            _shaderDataUpdater.UnsubscribeToFeatureEvents();
            return this;
        }
    }
}