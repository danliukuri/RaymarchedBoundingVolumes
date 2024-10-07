using RBV.Utilities.Extensions;
using UnityEditor;
using UnityEngine;
using static RBV.Data.Static.PackageRelatedShaderKeywords;
using static RBV.Data.Static.PathConstants;

namespace RBV.Infrastructure
{
    public class OptionalFeaturesShaderKeywordsEnabler
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
#else
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
        public static void SetPackagesRelatedShaderKeywords()
        {
            Shader.SetKeyword(RbvIsPackage,            AssetDatabase.IsValidFolder(RbvPackageRoot));
            Shader.SetKeyword(Rbv4dIsPackage,          AssetDatabase.IsValidFolder(Rbv4dPackageRoot));
            Shader.SetKeyword(RbvHeatmappingIsPackage, AssetDatabase.IsValidFolder(RbvHeatmappingPackageRoot));

            Shader.SetKeyword(Rbv4dOn, PathExtensions.FirstOrDefaultAsmdef(Rbv4dRuntimeAssemblyName) != default);

            if (PathExtensions.FirstOrDefaultAsmdef(RbvHeatmappingEditorAssemblyName) == default)
                Shader.DisableKeyword(RbvHeatmappingOn);
        }
    }
}