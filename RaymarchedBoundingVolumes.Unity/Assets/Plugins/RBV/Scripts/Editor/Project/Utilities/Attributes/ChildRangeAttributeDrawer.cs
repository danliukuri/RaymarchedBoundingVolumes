using System.Linq;
using RBV.Editor.Utilities.Extensions;
using RBV.Utilities.Attributes;
using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Project.Utilities.Attributes
{
    [CustomPropertyDrawer(typeof(ChildRangeAttribute))]
    public class ChildRangeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var range = (ChildRangeAttribute)attribute;
            EditorGUI.BeginProperty(position, label, property);
            property.DrawFoldoutAndEachChildren(label, childProperty =>
            {
                if (range.PropertyNames.Any(propertyName => childProperty.name == propertyName ||
                                                            childProperty.name == propertyName.ToBackingFieldFormat()))
                    DrawRange(childProperty, label, range.Min, range.Max);
                else
                    EditorGUILayout.PropertyField(childProperty, true);
            });
            EditorGUI.EndProperty();
        }

        private static void DrawRange(SerializedProperty property, GUIContent label, float min, float max)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Float:
                    EditorGUILayout.Slider(property, min, max);
                    break;
                case SerializedPropertyType.Integer:
                    EditorGUILayout.IntSlider(property, (int)min, (int)max);
                    break;
                default:
                    EditorGUILayout.LabelField(label.text, $"Use {nameof(ChildRangeAttribute)} with float or int.");
                    break;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            -EditorGUIUtility.standardVerticalSpacing;
    }
}