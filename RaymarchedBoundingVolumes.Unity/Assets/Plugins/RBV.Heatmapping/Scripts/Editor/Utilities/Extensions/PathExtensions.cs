using System.IO;
using UnityEditor;
using UnityEngine;
using static RBV.Data.Static.PathConstants;
using static RBV.Utilities.Extensions.PathExtensions;

namespace RBV.Heatmapping.Editor.Utilities.Extensions
{
    public static class PathExtensions
    {
        private const string HeatmappingEditorAsmdefRootRelativePath = "../../";

        public static string GetAsmdefPath(string asmdefGuid) =>
            Path.GetDirectoryName(AssetDatabase.GUIDToAssetPath(asmdefGuid));

        public static string AbsoluteHeatmappingPackageRootPathOrDefault()
        {
            string asmdef = FirstOrDefaultAsmdef(RbvHeatmappingEditorAssemblyName);
            return asmdef != default
                ? Path.GetFullPath(Path.Combine(GetAsmdefPath(asmdef), HeatmappingEditorAsmdefRootRelativePath))
                : default;
        }

        private static string RelativeToProjectPathOrDefault(string absolutePath)
        {
            string projectName = new DirectoryInfo(Application.dataPath).Parent?.FullName;
            return projectName != default && absolutePath != default
                ? Path.GetRelativePath(projectName, absolutePath)
                : default;
        }

        public static string HeatmappingPackageRootPathRelativeToProjectOrDefault() =>
            AssetDatabase.IsValidFolder(RbvHeatmappingPackageRoot)
                ? RbvHeatmappingPackageRoot
                : RelativeToProjectPathOrDefault(AbsoluteHeatmappingPackageRootPathOrDefault());
    }
}