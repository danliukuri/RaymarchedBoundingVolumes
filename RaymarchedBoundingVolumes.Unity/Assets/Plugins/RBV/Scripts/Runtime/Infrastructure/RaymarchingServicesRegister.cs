using System.Linq;
using RBV.Features;
using RBV.Features.RaymarchingSceneBuilding;
using RBV.Features.ShaderDataForming;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RBV.Infrastructure
{
    public partial class RaymarchingServicesRegister : MonoBehaviour
    {
#if !UNITY_EDITOR
        private void Awake()     => Initialize();
        private void OnDestroy() => Deinitialize();
#endif
        private void Initialize()
        {
            RegisterGlobalServices();
            RegisterSceneServices(gameObject.scene);
        }

        private void Deinitialize() => ServiceContainer.Global.Dispose();

        protected virtual void RegisterGlobalServices()
        {
            IServiceContainer container = InitializeServiceContainer();

            RegisterRaymarchingSceneTreeTraverser(container);
            RegisterObjectTypeCaster(container);
            RegisterTransformTypeCaster(container);
            RegisterRaymarchingDataInitializer(container);
            RegisterShaderBuffersInitializer(container);
            RegisterRaymarchingChildrenCalculator(container);
            RegisterRaymarchedObjectShaderPropertyIdProvider(container);
        }

        protected virtual IServiceContainer InitializeServiceContainer() => ServiceContainer.Initialize();

        protected virtual void RegisterRaymarchingSceneTreeTraverser(IServiceContainer container) =>
            container.RegisterAsSingle<IRaymarchingSceneTreeTraverser>(new RaymarchingSceneTreePostorderDFSTraverser());

        protected virtual void RegisterObjectTypeCaster(IServiceContainer container) =>
            container.RegisterAsSingle<IObjectTypeCaster>(new ObjectTypeCaster());

        protected virtual void RegisterTransformTypeCaster(IServiceContainer container) =>
            container.RegisterAsSingle<ITransformTypeCaster>(new TransformTypeCaster());

        protected virtual void RegisterRaymarchingDataInitializer(IServiceContainer container) =>
            container.RegisterAsSingle<IRaymarchingDataInitializer>(new RaymarchingDataInitializer(
                container.Resolve<IRaymarchingSceneTreeTraverser>(),
                container.Resolve<IObjectTypeCaster>(),
                container.Resolve<ITransformTypeCaster>()
            ));

        protected virtual void RegisterShaderBuffersInitializer(IServiceContainer container) =>
            container.RegisterAsSingle<IShaderBuffersInitializer>(new ShaderBuffersInitializer(
                container.Resolve<IObjectTypeCaster>(),
                container.Resolve<ITransformTypeCaster>()
            ));

        protected virtual void RegisterRaymarchingChildrenCalculator(IServiceContainer container) =>
            container.RegisterAsSingle<IRaymarchingChildrenCalculator>(new RaymarchingChildrenCalculator());

        protected virtual void RegisterRaymarchedObjectShaderPropertyIdProvider(IServiceContainer container) =>
            container.RegisterAsSingle<IRaymarchedObjectShaderPropertyIdProvider>(
                new RaymarchedObjectShaderPropertyIdProvider()
            );

        protected virtual void RegisterSceneServices(Scene scene)
        {
            IServiceContainer sceneContainer = IServiceContainer.Scoped(scene);

            RegisterRaymarchingSceneUpdater(sceneContainer);
            RegisterRaymarchingFeaturesRegister(sceneContainer);
            RegisterShaderDataUpdater(sceneContainer);
            RegisterRaymarchingSceneBuilder(sceneContainer);
        }

        protected virtual void RegisterRaymarchingSceneUpdater(IServiceContainer container)
        {
            RaymarchingSceneUpdater sceneUpdater = FindObjectsOfType<RaymarchingSceneUpdater>().Single();
            container.RegisterAsSingle<IRaymarchingSceneDataProvider>(sceneUpdater);
            container.RegisterAsSingle<IRaymarchingFeatureEventsSubscriber>(sceneUpdater);
        }

        protected virtual void RegisterRaymarchingFeaturesRegister(IServiceContainer container) =>
            container.RegisterAsSingle<IRaymarchingFeaturesRegister>(new RaymarchingFeaturesRegister(
                container.Resolve<IRaymarchingSceneDataProvider>()
            ));

        protected virtual void RegisterShaderDataUpdater(IServiceContainer container) =>
            container.RegisterAsSingle<IShaderDataUpdater>(new ShaderDataUpdater(
                container.Resolve<IRaymarchingSceneDataProvider>(),
                container.Resolve<IRaymarchedObjectShaderPropertyIdProvider>()
            ));

        protected virtual void RegisterRaymarchingSceneBuilder(IServiceContainer container) =>
            container.RegisterAsSingle<IRaymarchingSceneBuilder>(new RaymarchingSceneBuilder(
                container.Resolve<IRaymarchingDataInitializer>(),
                container.Resolve<IRaymarchingSceneDataProvider>(),
                container.Resolve<IRaymarchingFeaturesRegister>(),
                container.Resolve<IShaderBuffersInitializer>(),
                container.Resolve<IShaderDataUpdater>(),
                container.Resolve<IRaymarchingFeatureEventsSubscriber>()
            ));
    }
}