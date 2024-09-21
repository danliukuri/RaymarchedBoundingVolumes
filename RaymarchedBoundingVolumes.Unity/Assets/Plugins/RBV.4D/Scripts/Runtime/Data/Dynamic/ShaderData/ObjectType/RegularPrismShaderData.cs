using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RegularDoublePrismShaderData : IObjectTypeShaderData
    {
        public Vector2Int VerticesCount;
        public Vector2    Circumdiameter;

        public static RegularDoublePrismShaderData Default { get; } =
            new() { VerticesCount = 3 * Vector2Int.one, Circumdiameter = Vector2Int.one };
    }
}