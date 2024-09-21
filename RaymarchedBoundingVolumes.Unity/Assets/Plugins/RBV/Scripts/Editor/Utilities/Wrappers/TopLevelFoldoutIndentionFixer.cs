using System;
using UnityEditor;

namespace RBV.Editor.Utilities.Wrappers
{
    public class TopLevelFoldoutIndentionFixer : IDisposable
    {
        private readonly int _depth;

        private bool IsTopLevelFoldout => _depth == default && !EditorGUIUtility.hierarchyMode;

        public TopLevelFoldoutIndentionFixer(int depth)
        {
            _depth = depth;
            if (IsTopLevelFoldout)
                EditorGUI.indentLevel--;
        }

        public void Dispose()
        {
            if (IsTopLevelFoldout)
                EditorGUI.indentLevel++;
        }
    }
}