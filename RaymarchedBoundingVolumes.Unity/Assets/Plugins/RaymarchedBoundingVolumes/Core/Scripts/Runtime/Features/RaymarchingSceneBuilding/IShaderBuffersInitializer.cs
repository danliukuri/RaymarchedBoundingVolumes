using RaymarchedBoundingVolumes.Data.Dynamic;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public interface IShaderBuffersInitializer
    {
        ShaderBuffers InitializeBuffers(int operationsBufferSize, int objectsBufferSize);
        ShaderBuffers ReleaseBuffers();
    }
}