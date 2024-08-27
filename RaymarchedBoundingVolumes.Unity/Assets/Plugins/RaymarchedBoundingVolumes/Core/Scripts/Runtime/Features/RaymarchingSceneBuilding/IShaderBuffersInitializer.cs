using System.Collections.Generic;
using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Data.Static.Enumerations;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public interface IShaderBuffersInitializer
    {
        public ShaderBuffers InitializeBuffers(int operationsBufferSize, int objectsBufferSize,
                                               Dictionary<RaymarchedObjectType, List<RaymarchedObject>>
                                                   objectsByTypeBufferSizes);

        ShaderBuffers ReleaseBuffers();
    }
}