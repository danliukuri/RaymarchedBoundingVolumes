using System;
using System.Collections.Generic;
using RBV.Data.Dynamic;
using RBV.Data.Static.Enumerations;
using UnityEngine;

namespace RBV.Data.Static
{
    public static class RaymarchedObjectShaderPropertyIds
    {
        private const string Prefix                        = "_";
        private const string ObjectTypeRelatedDataIdFormat = "Raymarched{0}Data";
        private const string ObjectTransformDataIdFormat   = "RaymarchedObjects{0}Transforms";

        public static readonly int
            RaymarchingOperationsCount = PropertyToID(nameof(RaymarchingOperationsCount)),
            RaymarchingOperationNodes  = PropertyToID(nameof(RaymarchingOperationNodes)),
            RaymarchingOperations      = PropertyToID(nameof(RaymarchingOperations)),
            RaymarchedObjects          = PropertyToID(nameof(RaymarchedObjects));

        public static Dictionary<TransformType, int>        ObjectTransformDataIds   { get; } = new();
        public static Dictionary<RaymarchedObjectType, int> ObjectTypeRelatedDataIds { get; } = new();

        static RaymarchedObjectShaderPropertyIds()
        {
            foreach (TransformType type in Enum.GetValues(typeof(TransformType)))
                ObjectTransformDataIds.Add(type,
                    PropertyToID(string.Format(ObjectTransformDataIdFormat, type)));

            foreach (RaymarchedObject3DType type in Enum.GetValues(typeof(RaymarchedObject3DType)))
                ObjectTypeRelatedDataIds.Add((RaymarchedObjectType)(int)type,
                    PropertyToID(string.Format(ObjectTypeRelatedDataIdFormat, type)));
        }

        private static int PropertyToID(string name) => Shader.PropertyToID(Prefix + name);
    }
}