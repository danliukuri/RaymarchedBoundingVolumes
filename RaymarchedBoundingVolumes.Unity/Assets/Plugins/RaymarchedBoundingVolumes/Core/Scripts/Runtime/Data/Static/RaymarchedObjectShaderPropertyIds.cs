using System;
using System.Collections.Generic;
using RaymarchedBoundingVolumes.Data.Static.Enumerations;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Data.Static
{
    public class RaymarchedObjectShaderPropertyIds
    {
        private const string Prefix                        = "_";
        private const string ObjectTypeRelatedDataIdFormat = "Raymarched{0}Data";

        public static readonly int
            RaymarchingOperationsCount = PropertyToID(nameof(RaymarchingOperationsCount)),
            RaymarchingOperationNodes  = PropertyToID(nameof(RaymarchingOperationNodes)),
            RaymarchingOperations      = PropertyToID(nameof(RaymarchingOperations)),
            RaymarchedObjects          = PropertyToID(nameof(RaymarchedObjects));

        public static Dictionary<RaymarchedObjectType, int> ObjectTypeRelatedDataIds { get; } = new();

        static RaymarchedObjectShaderPropertyIds()
        {
            foreach (object type in Enum.GetValues(typeof(RaymarchedObjectType)))
                ObjectTypeRelatedDataIds.Add((RaymarchedObjectType)type,
                    PropertyToID(string.Format(ObjectTypeRelatedDataIdFormat, type)));
        }

        private static int PropertyToID(string name) => Shader.PropertyToID(Prefix + name);
    }
}