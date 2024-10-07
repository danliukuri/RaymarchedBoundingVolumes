using System.Linq;
using UnityEditor;
using static RBV.Data.Static.PathConstants;

namespace RBV.Utilities.Extensions
{
    public class PathExtensions
    {
        public static string FirstOrDefaultAsmdef(string assemblyName) =>
            AssetDatabase.FindAssets(string.Format(AsmdefFilter, assemblyName)).FirstOrDefault();
    }
}