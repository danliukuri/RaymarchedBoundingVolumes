using RaymarchedBoundingVolumes.Data.Dynamic;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public interface IShaderDataUpdater
    {
        IShaderDataUpdater Initialize(ShaderBuffers shaderBuffers);
        IShaderDataUpdater SubscribeToFeatureEvents();
        IShaderDataUpdater UnsubscribeToFeatureEvents();
        IShaderDataUpdater Update();
    }
}