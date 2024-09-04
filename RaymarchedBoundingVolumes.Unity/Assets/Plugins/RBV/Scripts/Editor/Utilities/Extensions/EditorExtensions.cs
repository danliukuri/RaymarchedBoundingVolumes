using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Utilities.Extensions
{
    public static class EditorExtensions
    {
        public static UnityEditor.Editor DrawScriptField<TTarget>(this UnityEditor.Editor editor)
            where TTarget : MonoBehaviour
        {
            var customClass = (TTarget)editor.target;
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour(customClass), typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();

            return editor;
        }

        public static UnityEditor.Editor DrawEditorField(this UnityEditor.Editor editor)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Editor", MonoScript.FromScriptableObject(editor), typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();

            return editor;
        }
    }
}