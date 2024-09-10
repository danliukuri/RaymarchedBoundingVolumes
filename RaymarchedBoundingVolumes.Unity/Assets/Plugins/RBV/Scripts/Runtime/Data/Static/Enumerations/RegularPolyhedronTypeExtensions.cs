using System.Collections.Generic;
using System.Linq;
using RBV.Utilities.Wrappers;

namespace RBV.Data.Static.Enumerations
{
    public static class RegularPolyhedronTypeExtensions
    {
        public const float MinBoundPlaneIndex = 0f, MaxBoundPlaneIndex = 18;

        private static readonly Dictionary<RegularPolyhedronType, Range<int>> _activeBoundPlanesRanges = new()
        {
            [RegularPolyhedronType.Custom]               = new Range<int>(0,  3),
            [RegularPolyhedronType.Cube]                 = new Range<int>(0,  2),
            [RegularPolyhedronType.Dodecahedron]         = new Range<int>(13, 18),
            [RegularPolyhedronType.Icosahedron]          = new Range<int>(3,  12),
            [RegularPolyhedronType.TruncatedIcosahedron] = new Range<int>(3,  18),
            [RegularPolyhedronType.Octahedron]           = new Range<int>(3,  6),
            [RegularPolyhedronType.TruncatedOctahedron]  = new Range<int>(0,  6)
        };

        public static RegularPolyhedronType GetType(int activeBoundPlaneStartIndex, int activeBoundPlaneEndIndex) =>
            _activeBoundPlanesRanges.FirstOrDefault(pair =>
                pair.Value.Start == activeBoundPlaneStartIndex && pair.Value.End == activeBoundPlaneEndIndex).Key;

        public static RegularPolyhedronType GetType(Range<int> range) =>
            _activeBoundPlanesRanges.FirstOrDefault(pair => pair.Value == range).Key;

        public static Range<int> GetActiveBoundPlanesRange(RegularPolyhedronType type) =>
            _activeBoundPlanesRanges.TryGetValue(type, out Range<int> activeBoundPlaneRange)
                ? activeBoundPlaneRange
                : _activeBoundPlanesRanges[RegularPolyhedronType.Custom];
    }
}