using RBV.Editor.Utilities.Extensions;
using RBV.Utilities.Attributes;
using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Project.Utilities.Attributes
{
    [CustomPropertyDrawer(typeof(UnwrappedAttribute))]
    public class UnwrappedAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            property.DrawChildren(property.depth);
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            -EditorGUIUtility.standardVerticalSpacing;
    }
}