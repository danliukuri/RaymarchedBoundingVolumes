using System;
using System.Collections.Generic;
using RBV.Data.Static.Enumerations;
using UnityEngine;

namespace RBV.Data.Static
{
    public class RaymarchedObjectShaderPropertyIds
    {
        private const string Prefix                        = "_";
        private const string ObjectTypeRelatedDataIdFormat = "Raymarched{0}Data";
        private const string ObjectTransformDataIdFormat   = "RaymarchedObjects{0}Transforms";

        public static readonly int
            RaymarchingOperationsCount = PropertyToID(nameof(RaymarchingOperationsCount)),
            RaymarchingOperationNodes  = PropertyToID(nameof(RaymarchingOperationNodes)),
            RaymarchingOperations      = PropertyToID(nameof(RaymarchingOperations)),
            RaymarchedObjects          = PropertyToID(nameof(RaymarchedObjects));

        public static Dictionary<TransformType, int> ObjectTransformDataIds   { get; } = new();
        public static Dictionary<int, int>           ObjectTypeRelatedDataIds { get; } = new();

        static RaymarchedObjectShaderPropertyIds()
        {
            foreach (object type in Enum.GetValues(typeof(TransformType)))
                ObjectTransformDataIds.Add((TransformType)type,
                    PropertyToID(string.Format(ObjectTransformDataIdFormat, type)));

            foreach (object type in Enum.GetValues(typeof(RaymarchedObjectType)))
                ObjectTypeRelatedDataIds.Add((int)type,
                    PropertyToID(string.Format(ObjectTypeRelatedDataIdFormat, type)));
        }

        private static int PropertyToID(string name) => Shader.PropertyToID(Prefix + name);
    }
}