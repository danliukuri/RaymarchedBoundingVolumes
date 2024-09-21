using RBV.Editor.Utilities.Extensions;
using RBV.Utilities.Attributes;
using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Project.Utilities.Attributes
{
    [CustomPropertyDrawer(typeof(ChildTooltipAttribute))]
    public class ChildTooltipAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
            property.DrawFoldoutAndEachChildren(label, child =>
                EditorGUILayout.PropertyField(child, GetTooltip(child), true));

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            -EditorGUIUtility.standardVerticalSpacing;

        private GUIContent GetTooltip(SerializedProperty child) =>
            attribute is ChildTooltipAttribute { FieldName: { } tooltipChildName } tooltipAttribute &&
            (child.name == tooltipChildName || child.name == tooltipChildName.ToBackingFieldFormat())
                ? new GUIContent(child.displayName, tooltipAttribute.Tooltip)
                : default;
    }
}