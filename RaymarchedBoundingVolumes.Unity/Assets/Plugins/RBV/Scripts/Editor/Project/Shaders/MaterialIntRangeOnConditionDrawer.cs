using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Project.Shaders
{
    public abstract class MaterialIntRangeOnConditionDrawer : MaterialDrawOnConditionDrawer
    {
        protected override void DrawGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
        {
            if (prop.type != MaterialProperty.PropType.Range)
                EditorGUILayout.HelpBox("IntRange used on a non-range property: " + prop.name, MessageType.Warning);
            else
                DrawIntRangeProperty(position, prop, label);
        }

        /// <summary>
        ///     Pure copy of internal method <see cref="UnityEditor.MaterialEditor.DoIntRangeProperty"/> from
        ///     <see cref="UnityEditor.MaterialEditor"/>
        /// </summary>
        private static int DrawIntRangeProperty(Rect position, MaterialProperty prop, GUIContent label)
        {
            MaterialEditor.BeginProperty(position, prop);

            EditorGUI.BeginChangeCheck();

            // For range properties we want to show the slider so we adjust label width to use default width
            // (setting it to 0)
            // See SetDefaultGUIWidths where we set:
            // EditorGUIUtility.labelWidth = GUIClip.visibleRect.width - EditorGUIUtility.fieldWidth - 17;
            float oldLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 0f;

            int newValue = EditorGUI.IntSlider(position, label, (int)prop.floatValue,
                (int)prop.rangeLimits.x, (int)prop.rangeLimits.y);

            EditorGUIUtility.labelWidth = oldLabelWidth;

            if (EditorGUI.EndChangeCheck())
                prop.floatValue = newValue;

            MaterialEditor.EndProperty();

            return (int)prop.floatValue;
        }
    }

    public class MaterialIntRangeIfOnDrawer : MaterialIntRangeOnConditionDrawer
    {
        private readonly string _shaderKeyword;

        public MaterialIntRangeIfOnDrawer(string keyword) => _shaderKeyword = keyword;

        protected override bool IsConditionMet(Material material) => material.IsKeywordEnabled(_shaderKeyword);
    }

    public class MaterialIntRangeIfOffDrawer : MaterialIntRangeOnConditionDrawer
    {
        private readonly string _shaderKeyword;

        public MaterialIntRangeIfOffDrawer(string keyword) => _shaderKeyword = keyword;

        protected override bool IsConditionMet(Material material) => !material.IsKeywordEnabled(_shaderKeyword);
    }
}