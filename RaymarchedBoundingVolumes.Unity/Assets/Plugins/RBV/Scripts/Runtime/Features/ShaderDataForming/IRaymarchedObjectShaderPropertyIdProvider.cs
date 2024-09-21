using System.Collections.Generic;
using RBV.Data.Dynamic;
using RBV.Data.Static.Enumerations;

namespace RBV.Features.ShaderDataForming
{
    public interface IRaymarchedObjectShaderPropertyIdProvider
    {
        Dictionary<TransformType, int>        ObjectTransformDataIds   { get; }
        Dictionary<RaymarchedObjectType, int> ObjectTypeRelatedDataIds { get; }

        int RaymarchingOperationsCount         { get; }
        int RaymarchingOperationNodes          { get; }
        int RaymarchingOperations              { get; }
        int RaymarchedObjects                  { get; }
        int RaymarchedObjectsRenderingSettings { get; }
    }
}