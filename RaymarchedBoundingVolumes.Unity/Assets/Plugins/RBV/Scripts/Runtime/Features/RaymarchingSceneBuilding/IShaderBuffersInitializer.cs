using RBV.Data.Dynamic;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public interface IShaderBuffersInitializer
    {
        public ShaderBuffers InitializeBuffers(RaymarchingData raymarchingData);

        ShaderBuffers ReleaseBuffers();
    }
}