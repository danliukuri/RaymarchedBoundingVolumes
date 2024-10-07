namespace RBV.Data.Static
{
    public static class PathConstants
    {
        public const string Separator            = "/";
        public const string PackageNameSeparator = ".";
        public const string PackagesRoot         = "Packages";
        public const string AsmdefFilter         = "t:AssemblyDefinitionAsset {0}";

        public const string RbvPackageRoot            = PackagesRoot   + Separator            + "com.danliukuri.rbv";
        public const string Rbv4dPackageRoot          = RbvPackageRoot + PackageNameSeparator + "4d";
        public const string RbvHeatmappingPackageRoot = RbvPackageRoot + PackageNameSeparator + "heatmapping";

        public const string Rbv4dRuntimeAssemblyName         = "RBV.FourDimensional";
        public const string RbvHeatmappingEditorAssemblyName = "RBV.Heatmapping.Editor";
    }
}