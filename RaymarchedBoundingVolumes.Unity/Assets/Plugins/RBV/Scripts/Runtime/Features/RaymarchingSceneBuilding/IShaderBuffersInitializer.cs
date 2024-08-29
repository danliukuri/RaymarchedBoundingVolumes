using System.Collections.Generic;
using RBV.Data.Dynamic;
using RBV.Data.Static.Enumerations;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public interface IShaderBuffersInitializer
    {
        public ShaderBuffers InitializeBuffers(int operationsBufferSize, int objectsBufferSize,
                                               Dictionary<RaymarchedObjectType, List<RaymarchedObject>>
                                                   objectsByTypeBufferSizes);

        ShaderBuffers ReleaseBuffers();
    }
}