﻿using RaymarchedBoundingVolumes.Editor.Utilities.Extensions;
using RaymarchedBoundingVolumes.Utilities.Attributes;
using UnityEditor;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Editor.Project.Utilities.Attributes
{
    [CustomPropertyDrawer(typeof(UnwrappedAttribute))]
    public class UnwrappedAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            foreach (SerializedProperty child in property.GetDirectChildren())
                EditorGUILayout.PropertyField(child, new GUIContent(child.displayName), true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            EditorGUI.GetPropertyHeight(property, true) - EditorGUI.GetPropertyHeight(property, false);
    }
}