using RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding;
using UnityEditor;

namespace RaymarchedBoundingVolumes.Infrastructure
{
    public static class RaymarchingServicesRegister
    {
        [InitializeOnLoadMethod]
        private static void RegisterServices()
        {
            IServiceContainer container = ServiceContainer.Global;

            container.RegisterAsSingle<IRaymarchingSceneTreeTraverser>(new RaymarchingSceneTreePostorderDFSTraverser());
            container.RegisterAsSingle<IRaymarchingDataInitializer>(new RaymarchingDataInitializer(
                container.Resolve<IRaymarchingSceneTreeTraverser>()
            ));
            container.RegisterAsSingle<IShaderBuffersInitializer>(new ShaderBuffersInitializer());
            container.RegisterAsSingle<IShaderDataUpdater>(new ShaderDataUpdater());
        }
    }
}