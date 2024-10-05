using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace RBV.Heatmapping.Editor.Utilities.Extensions
{
    public static class PathExtensions
    {
        private const string AsmdefRootRelativePath  = "../../";
        private const string AsmdefFilter            = "t:AssemblyDefinitionAsset {0}";
        private const string RelativePackageRootPath = "Packages/com.danliukuri.rbv.heatmapping";

        public static string AsmdefPath()
        {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string asmdef       = AssetDatabase.FindAssets(string.Format(AsmdefFilter, assemblyName)).First();
            string asmdefPath   = Path.GetDirectoryName(AssetDatabase.GUIDToAssetPath(asmdef));
            return asmdefPath;
        }

        public static string AbsolutePackageRootPath() =>
            Path.GetFullPath(Path.Combine(AsmdefPath(), AsmdefRootRelativePath));

        public static string PackageRootPathRelativeToProject()
        {
#if !RBV_HEATMAPPING_ON_PROJECT
            return RelativePackageRootPath;
#endif
            string projectName = new DirectoryInfo(Application.dataPath).Parent?.FullName;
            return projectName != default ? Path.GetRelativePath(projectName, AbsolutePackageRootPath()) : default;
        }
    }
}