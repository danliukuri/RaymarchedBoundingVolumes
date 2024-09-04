using RBV.Editor.Utilities.Extensions;
using RBV.Editor.Utilities.Wrappers;
using RBV.Utilities.Attributes;
using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Project.Utilities.Attributes
{
    [CustomPropertyDrawer(typeof(ChildTooltipAttribute))]
    public class ChildTooltipAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new FoldoutIndentionInPropertyDrawerFixer())
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);

            if (property.isExpanded && attribute is ChildTooltipAttribute tooltipAttribute)
                using (new EditorGUI.IndentLevelScope())
                    foreach (SerializedProperty child in property.GetDirectChildren())
                    {
                        GUIContent childLabel = child.name == tooltipAttribute.FieldName ||
                                                child.name == tooltipAttribute.FieldName.ToBackingFieldFormat()
                            ? new GUIContent(child.displayName, tooltipAttribute.Tooltip)
                            : default;

                        EditorGUILayout.PropertyField(child, childLabel, true);
                    }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property, false);
    }
}