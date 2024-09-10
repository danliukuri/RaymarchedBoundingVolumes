using System;
using RBV.Data.Static.Enumerations;
using RBV.Utilities.Attributes;
using RBV.Utilities.Wrappers;
using static RBV.Data.Static.Enumerations.RegularPolyhedronTypeExtensions;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RegularPolyhedronShaderData : IObjectTypeShaderData
    {
        public float InscribedDiameter;

        [ChildRange(MinBoundPlaneIndex, MaxBoundPlaneIndex, nameof(Range<int>.Start), nameof(Range<int>.End))]
        public Range<int> ActiveBoundPlanesRange;

        public static RegularPolyhedronShaderData Default { get; } = new()
        {
            InscribedDiameter      = 1f,
            ActiveBoundPlanesRange = GetActiveBoundPlanesRange(RegularPolyhedronType.TruncatedIcosahedron)
        };
    }
}