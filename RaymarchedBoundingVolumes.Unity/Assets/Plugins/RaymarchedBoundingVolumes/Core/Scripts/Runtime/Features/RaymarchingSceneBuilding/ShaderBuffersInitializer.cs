using System.Collections.Generic;
using System.Runtime.InteropServices;
using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Data.Dynamic.ShaderData;
using RaymarchedBoundingVolumes.Data.Static.Enumerations;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public class ShaderBuffersInitializer : IShaderBuffersInitializer
    {
        private ShaderBuffers _shaderBuffers;

        public ShaderBuffers InitializeBuffers(int operationsBufferSize, int objectsBufferSize,
                                               Dictionary<RaymarchedObjectType, List<RaymarchedObject>>
                                                   objectsByTypeBufferSizes)
        {
            _shaderBuffers = new ShaderBuffers
            {
                OperationNodes =
                    new ComputeBuffer(operationsBufferSize, Marshal.SizeOf<RaymarchingOperationNodeShaderData>()),
                Operations =
                    new ComputeBuffer(operationsBufferSize, Marshal.SizeOf<RaymarchingOperationShaderData>()),
                Objects =
                    new ComputeBuffer(objectsBufferSize, Marshal.SizeOf<RaymarchedObjectShaderData>()),
                ObjectTypeRelatedData =
                    new Dictionary<RaymarchedObjectType, ComputeBuffer>(objectsByTypeBufferSizes.Count)
            };

            foreach (RaymarchedObjectType type in objectsByTypeBufferSizes.Keys)
                _shaderBuffers.ObjectTypeRelatedData[type] =
                    new ComputeBuffer(objectsByTypeBufferSizes[type].Count, Marshal.SizeOf(type.GetShaderDataType()));

            return _shaderBuffers;
        }

        public ShaderBuffers ReleaseBuffers()
        {
            if (_shaderBuffers != default)
            {
                _shaderBuffers.OperationNodes?.Release();
                _shaderBuffers.Operations?.Release();
                _shaderBuffers.Objects?.Release();

                foreach (ComputeBuffer computeBuffer in _shaderBuffers.ObjectTypeRelatedData.Values)
                    computeBuffer.Release();
            }

            return _shaderBuffers;
        }
    }
}