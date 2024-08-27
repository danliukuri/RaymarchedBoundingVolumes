using System;
using UnityEditor;

namespace RaymarchedBoundingVolumes.Editor.Utilities.Wrappers
{
    public class FoldoutIndentionInPropertyDrawerFixer : IDisposable
    {
        public FoldoutIndentionInPropertyDrawerFixer()
        {
            if (!EditorGUIUtility.hierarchyMode)
                EditorGUI.indentLevel--;
        }

        public void Dispose()
        {
            if (!EditorGUIUtility.hierarchyMode)
                EditorGUI.indentLevel++;
        }
    }
}