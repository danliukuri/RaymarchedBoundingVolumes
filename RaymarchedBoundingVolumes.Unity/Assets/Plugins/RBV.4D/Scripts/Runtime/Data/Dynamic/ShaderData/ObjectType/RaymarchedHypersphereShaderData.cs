using System;
using RBV.Data.Dynamic.ShaderData.ObjectType;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedHypersphereShaderData : IObjectTypeShaderData
    {
        public float Diameter;

        public static RaymarchedHypersphereShaderData Default { get; } = new() { Diameter = 1f };
    }
}