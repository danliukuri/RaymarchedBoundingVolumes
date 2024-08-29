using System.Linq;
using RBV.Features;
using RBV.Features.RaymarchingSceneBuilding;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RBV.Infrastructure
{
    public partial class RaymarchingServicesRegister : MonoBehaviour
    {
#if !UNITY_EDITOR
        private void Awake() => RegisterSceneServices(gameObject.scene);

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
#else
        [InitializeOnLoadMethod]
#endif
        private static void RegisterGlobalServices()
        {
            IServiceContainer container = ServiceContainer.Initialize();

            container.RegisterAsSingle<IRaymarchingSceneTreeTraverser>(new RaymarchingSceneTreePostorderDFSTraverser());
            container.RegisterAsSingle<IRaymarchingDataInitializer>(new RaymarchingDataInitializer(
                container.Resolve<IRaymarchingSceneTreeTraverser>()
            ));
            container.RegisterAsSingle<IShaderBuffersInitializer>(new ShaderBuffersInitializer());
            container.RegisterAsSingle<IRaymarchingChildrenCalculator>(new RaymarchingChildrenCalculator());
        }

        private static void RegisterSceneServices(Scene scene)
        {
            IServiceContainer sceneContainer = IServiceContainer.Scoped(scene);

            RaymarchingSceneUpdater sceneUpdater = FindObjectsOfType<RaymarchingSceneUpdater>().Single();
            sceneContainer.RegisterAsSingle<IRaymarchingSceneDataProvider>(sceneUpdater);
            sceneContainer.RegisterAsSingle<IRaymarchingFeatureEventsSubscriber>(sceneUpdater);

            sceneContainer.RegisterAsSingle<IRaymarchingFeaturesRegister>(new RaymarchingFeaturesRegister(
                sceneContainer.Resolve<IRaymarchingSceneDataProvider>()
            ));

            sceneContainer.RegisterAsSingle<IShaderDataUpdater>(new ShaderDataUpdater(
                sceneContainer.Resolve<IRaymarchingSceneDataProvider>()
            ));

            sceneContainer.RegisterAsSingle<IRaymarchingSceneBuilder>(new RaymarchingSceneBuilder(
                sceneContainer.Resolve<IRaymarchingDataInitializer>(),
                sceneContainer.Resolve<IRaymarchingSceneDataProvider>(),
                sceneContainer.Resolve<IRaymarchingFeaturesRegister>(),
                sceneContainer.Resolve<IShaderBuffersInitializer>(),
                sceneContainer.Resolve<IShaderDataUpdater>(),
                sceneContainer.Resolve<IRaymarchingFeatureEventsSubscriber>()
            ));
        }
    }
}