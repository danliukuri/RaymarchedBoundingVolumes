﻿namespace RBV.Data.Static.Enumerations
{
    public enum RaymarchingOperationType
    {
        Union     = 0,
        Subtract  = 1,
        Intersect = 2,
        Xor       = 3,

        SmoothUnion     = 4,
        SmoothSubtract  = 5,
        SmoothIntersect = 6,
        SmoothXor       = 7,

        ChamferUnion     = 8,
        ChamferSubtract  = 9,
        ChamferIntersect = 10,
        ChamferXor       = 11,

        StairsUnion     = 12,
        StairsSubtract  = 13,
        StairsIntersect = 14,
        StairsXor       = 15,

        Morph = 16
    }
}