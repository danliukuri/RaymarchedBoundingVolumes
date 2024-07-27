using UnityEditor;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Utilities.Attributes
{
    public class UnwrappedAttribute : PropertyAttribute { }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(UnwrappedAttribute))]
    public class ObservableValueDrawer : PropertyDrawer
    {
        private const string WrappedValueName = "value";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
            EditorGUI.PropertyField(position, property.FindPropertyRelative(WrappedValueName), label, true);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property.FindPropertyRelative(WrappedValueName), label, true);
    }
#endif
}