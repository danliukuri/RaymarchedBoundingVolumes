using RBV.Editor.Utilities.Extensions;
using RBV.Utilities.Wrappers;
using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Project.Utilities.Wrappers
{
    [CustomPropertyDrawer(typeof(ObservableValue<>))]
    public class ObservableValueDrawer : PropertyDrawer
    {
        private SerializedProperty _valueProperty;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
            GetValueProperty(property).DrawProperty(new GUIContent(property.displayName), property.depth);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            -EditorGUIUtility.standardVerticalSpacing;

        private SerializedProperty GetValueProperty(SerializedProperty property) =>
            _valueProperty ??= property.FindPropertyRelative(nameof(ObservableValue<object>.Value).ToLower());
    }
}