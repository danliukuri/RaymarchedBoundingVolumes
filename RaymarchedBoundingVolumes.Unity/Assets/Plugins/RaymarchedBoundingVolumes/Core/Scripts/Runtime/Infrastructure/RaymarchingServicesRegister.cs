using System.Linq;
using RaymarchedBoundingVolumes.Features;
using RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RaymarchedBoundingVolumes.Infrastructure
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
            container.RegisterAsSingle<IShaderDataUpdater>(new ShaderDataUpdater());
            container.RegisterAsSingle<IRaymarchingChildrenCalculator>(new RaymarchingChildrenCalculator());
        }

        private static void RegisterSceneServices(Scene scene)
        {
            IServiceContainer sceneContainer = IServiceContainer.Scoped(scene);

            sceneContainer
                .RegisterAsSingle<IRaymarchingSceneBuilder>(FindObjectsOfType<RaymarchingSceneBuilder>().Single());

            sceneContainer.RegisterAsSingle<IRaymarchingFeaturesRegister>(new RaymarchingFeaturesRegister(
                sceneContainer.Resolve<IRaymarchingSceneBuilder>()
            ));
        }
    }
}