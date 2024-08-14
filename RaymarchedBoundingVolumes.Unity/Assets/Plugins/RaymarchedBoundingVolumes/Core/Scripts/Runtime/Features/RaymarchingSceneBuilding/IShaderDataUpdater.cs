using RaymarchedBoundingVolumes.Data.Dynamic;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public interface IShaderDataUpdater
    {
        ShaderDataUpdater Initialize(ShaderBuffers shaderBuffers, RaymarchingData raymarchingData);

        void UpdateOperationData(RaymarchingOperation operation);
        void UpdateObjectData(RaymarchedObject obj);
        void Update();
    }
}