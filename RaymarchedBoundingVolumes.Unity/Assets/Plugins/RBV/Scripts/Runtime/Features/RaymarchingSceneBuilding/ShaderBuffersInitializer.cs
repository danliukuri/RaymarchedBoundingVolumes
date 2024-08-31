using System.Collections.Generic;
using System.Runtime.InteropServices;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData;
using RBV.Data.Static.Enumerations;
using RBV.Features.ShaderDataForming;
using UnityEngine;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public class ShaderBuffersInitializer : IShaderBuffersInitializer
    {
        private readonly IObjectTypeCaster    _objectTypeCaster;
        private readonly ITransformTypeCaster _transformTypeCaster;

        private ShaderBuffers _shaderBuffers;

        public ShaderBuffersInitializer(IObjectTypeCaster objectTypeCaster, ITransformTypeCaster transformTypeCaster)
        {
            _objectTypeCaster    = objectTypeCaster;
            _transformTypeCaster = transformTypeCaster;
        }

        public ShaderBuffers InitializeBuffers(int operationsBufferSize, int objectsBufferSize,
                                               Dictionary<TransformType, List<RaymarchedObject>>
                                                   objectsByTransformsType,
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
                ObjectTransformData =
                    new Dictionary<TransformType, ComputeBuffer>(objectsByTransformsType.Count),
                ObjectTypeRelatedData =
                    new Dictionary<RaymarchedObjectType, ComputeBuffer>(objectsByTypeBufferSizes.Count)
            };

            foreach (TransformType type in objectsByTransformsType.Keys)
            {
                int size = Marshal.SizeOf(_transformTypeCaster.GetShaderDataType(type));
                _shaderBuffers.ObjectTransformData[type] = new ComputeBuffer(objectsByTransformsType[type].Count, size);
            }

            foreach (RaymarchedObjectType type in objectsByTypeBufferSizes.Keys)
            {
                int size = Marshal.SizeOf(_objectTypeCaster.GetShaderDataType(type));
                _shaderBuffers.ObjectTypeRelatedData[type] =
                    new ComputeBuffer(objectsByTypeBufferSizes[type].Count, size);
            }

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