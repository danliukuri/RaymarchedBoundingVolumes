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
        private readonly IOperationTypeCaster _operationTypeCaster;
        private readonly IObjectTypeCaster    _objectTypeCaster;
        private readonly ITransformTypeCaster _transformTypeCaster;

        private ShaderBuffers _shaderBuffers;

        public ShaderBuffersInitializer(IOperationTypeCaster operationTypeCaster,
                                        IObjectTypeCaster    objectTypeCaster,
                                        ITransformTypeCaster transformTypeCaster)
        {
            _operationTypeCaster = operationTypeCaster;
            _objectTypeCaster    = objectTypeCaster;
            _transformTypeCaster = transformTypeCaster;
        }

        public ShaderBuffers InitializeBuffers(RaymarchingData data)
        {
            int operationsBufferSize = data.OperationsShaderData.Count;
            int objectsBufferSize    = data.ObjectsShaderData.Count;

            _shaderBuffers = new ShaderBuffers
            {
                OperationNodes =
                    new ComputeBuffer(operationsBufferSize, Marshal.SizeOf<RaymarchingOperationNodeShaderData>()),
                Operations =
                    new ComputeBuffer(operationsBufferSize, Marshal.SizeOf<RaymarchingOperationShaderData>()),
                Objects =
                    new ComputeBuffer(objectsBufferSize, Marshal.SizeOf<RaymarchedObjectShaderData>()),
                OperationTypeData =
                    new Dictionary<RaymarchingOperationType, ComputeBuffer>(data.OperationsShaderDataByType.Count),
                ObjectTransformData =
                    new Dictionary<TransformType, ComputeBuffer>(data.ObjectsByTransformsType.Count),
                ObjectTypeData =
                    new Dictionary<RaymarchedObjectType, ComputeBuffer>(data.ObjectsShaderDataByType.Count),
                ObjectsRenderingSettings =
                    new ComputeBuffer(objectsBufferSize, Marshal.SizeOf<RaymarchedObjectRenderingSettingsShaderData>())
            };

            foreach (RaymarchingOperationType type in data.OperationsShaderDataByType.Keys)
            {
                int size  = Marshal.SizeOf(_operationTypeCaster.GetShaderDataType(type));
                int count = data.OperationsShaderDataByType[type].Length;
                _shaderBuffers.OperationTypeData[type] = new ComputeBuffer(count, size);
            }

            foreach (TransformType type in data.ObjectsByTransformsType.Keys)
            {
                int size  = Marshal.SizeOf(_transformTypeCaster.GetShaderDataType(type));
                int count = data.ObjectsByTransformsType[type].Count;
                _shaderBuffers.ObjectTransformData[type] = new ComputeBuffer(count, size);
            }

            foreach (RaymarchedObjectType type in data.ObjectsShaderDataByType.Keys)
            {
                int size  = Marshal.SizeOf(_objectTypeCaster.GetShaderDataType(type));
                int count = data.ObjectsShaderDataByType[type].Length;
                _shaderBuffers.ObjectTypeData[type] = new ComputeBuffer(count, size);
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

                foreach (ComputeBuffer computeBuffer in _shaderBuffers.OperationTypeData.Values)
                    computeBuffer.Release();
                foreach (ComputeBuffer computeBuffer in _shaderBuffers.ObjectTransformData.Values)
                    computeBuffer.Release();
                foreach (ComputeBuffer computeBuffer in _shaderBuffers.ObjectTypeData.Values)
                    computeBuffer.Release();

                _shaderBuffers.ObjectsRenderingSettings?.Release();
            }

            return _shaderBuffers;
        }
    }
}