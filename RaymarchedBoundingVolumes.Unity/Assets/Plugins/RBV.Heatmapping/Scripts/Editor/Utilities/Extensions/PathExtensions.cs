using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace RBV.Heatmapping.Editor.Utilities.Extensions
{
    public static class PathExtensions
    {
        private const string AsmdefRootRelativePath = "../../";
        private const string AsmdefFilter           = "t:AssemblyDefinitionAsset {0}";

        public static string AsmdefPath()
        {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string asmdef       = AssetDatabase.FindAssets(string.Format(AsmdefFilter, assemblyName)).First();
            string asmdefPath   = Path.GetDirectoryName(AssetDatabase.GUIDToAssetPath(asmdef));
            return asmdefPath;
        }

        public static string RootPathRelativeToProject()
        {
            string asmdefPath  = AsmdefPath();
            string projectName = new DirectoryInfo(Application.dataPath).Parent?.Name;
            return asmdefPath != default && projectName != default
                ? Path.GetRelativePath(projectName, Path.Combine(asmdefPath, AsmdefRootRelativePath))
                : default;
        }
    }
}