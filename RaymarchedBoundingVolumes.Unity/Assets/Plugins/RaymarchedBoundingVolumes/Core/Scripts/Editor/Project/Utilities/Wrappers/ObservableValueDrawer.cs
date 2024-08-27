using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEditor;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Editor.Project.Utilities.Wrappers
{
    [CustomPropertyDrawer(typeof(ObservableValue<>))]
    public class ObservableValueDrawer : PropertyDrawer
    {
        private SerializedProperty _valueProperty;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
            EditorGUI.PropertyField(position, GetValueProperty(property), label, true);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(GetValueProperty(property), label, true);

        private SerializedProperty GetValueProperty(SerializedProperty property) =>
            _valueProperty ??= property.FindPropertyRelative(nameof(ObservableValue<object>.Value).ToLower());
    }
}