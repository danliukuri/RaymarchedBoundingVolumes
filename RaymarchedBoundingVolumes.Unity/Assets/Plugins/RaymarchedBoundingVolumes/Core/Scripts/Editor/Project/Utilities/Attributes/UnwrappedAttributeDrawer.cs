using RaymarchedBoundingVolumes.Utilities.Attributes;
using UnityEditor;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Editor.Project.Utilities.Attributes
{
    [CustomPropertyDrawer(typeof(UnwrappedAttribute))]
    public class UnwrappedAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
            EditorGUI.PropertyField(position, FindChildPropertyOrDefault(property), label, true);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(FindChildPropertyOrDefault(property), label, true);

        private SerializedProperty FindChildPropertyOrDefault(SerializedProperty property) =>
            attribute is UnwrappedAttribute unwrappedAttribute
                ? property.FindPropertyRelative(unwrappedAttribute.FieldName)
                : property;
    }
}