using RBV.Data.Dynamic;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public interface IShaderDataUpdater
    {
        IShaderDataUpdater Initialize(ShaderBuffers shaderBuffers);
        IShaderDataUpdater SubscribeToFeatureEvents();
        IShaderDataUpdater UnsubscribeToFeatureEvents();
        IShaderDataUpdater Update();
    }
}