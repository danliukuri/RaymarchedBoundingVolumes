using UnityEngine.Rendering;

namespace RBV.Data.Static
{
    public static class PackageRelatedShaderKeywords
    {
        public static readonly GlobalKeyword RbvIsPackage   = GlobalKeyword.Create("RBV_IS_PACKAGE");
        public static readonly GlobalKeyword Rbv4dIsPackage = GlobalKeyword.Create("RBV_4D_IS_PACKAGE");

        public static readonly GlobalKeyword RbvHeatmappingIsPackage =
            GlobalKeyword.Create("RBV_HEATMAPPING_IS_PACKAGE");

        public static readonly GlobalKeyword Rbv4dOn          = GlobalKeyword.Create("RBV_4D_ON");
        public static readonly GlobalKeyword RbvHeatmappingOn = GlobalKeyword.Create("RBV_HEATMAPPING_ON");
    }
}