using System.Runtime.InteropServices;
using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Data.Dynamic.ShaderData;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public class ShaderBuffersInitializer
    {
        public ShaderBuffers InitializeBuffers(int operationsBufferSize, int objectsBufferSize)
        {
            _shaderBuffers?.OperationNodes?.Release();
            _shaderBuffers?.Operations    ?.Release();
            _shaderBuffers?.Objects       ?.Release();
            return _shaderBuffers = new ShaderBuffers
            {
                OperationNodes =
                    new ComputeBuffer(operationsBufferSize, Marshal.SizeOf<RaymarchingOperationNodeShaderData>()),
                Operations =
                    new ComputeBuffer(operationsBufferSize, Marshal.SizeOf<RaymarchingOperationShaderData>()),
                Objects =
                    new ComputeBuffer(objectsBufferSize, Marshal.SizeOf<RaymarchedObjectShaderData>())
            };
        }

        private ShaderBuffers _shaderBuffers;

        public void ReleaseBuffers()
        {
            if (_shaderBuffers != default)
            {
                _shaderBuffers.OperationNodes?.Release();
                _shaderBuffers.Operations    ?.Release();
                _shaderBuffers.Objects       ?.Release();
            }
        }
    }
}