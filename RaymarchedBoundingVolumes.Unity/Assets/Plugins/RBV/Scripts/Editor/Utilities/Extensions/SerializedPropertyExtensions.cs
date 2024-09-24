using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RBV.Editor.Utilities.Wrappers;
using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Utilities.Extensions
{
    public static class SerializedPropertyExtensions
    {
        public static IEnumerable<SerializedProperty> GetDirectChildren(this SerializedProperty property) =>
            property.GetChildren(false);

        public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property) =>
            property.GetChildren(true);

        private static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property, bool isRecursive)
        {
            if (property is not { hasChildren: true })
                yield break;

            SerializedProperty iterator    = property.Copy();
            SerializedProperty endProperty = iterator.GetEndProperty(true);

            iterator.NextVisible(true);

            while (!SerializedProperty.EqualContents(iterator, endProperty))
            {
                yield return iterator.Copy();
                iterator.NextVisible(isRecursive);
            }
        }

        public static bool HasPropertyDrawer(this object source) => source.GetType().HasPropertyDrawer();

        public static bool HasPropertyDrawer(this Type source) =>
            TypeCache.GetTypesDerivedFrom<PropertyDrawer>().Any(drawerType =>
                drawerType.CustomAttributes.FirstOrDefault()?.ConstructorArguments.FirstOrDefault()
                    .Value is Type type && type == source);

        public static bool HasAttributeWithPropertyDrawer(this SerializedProperty property) =>
            property.GetCustomAttributes().Any(HasPropertyDrawer);

        public static void DrawProperty(this SerializedProperty property, GUIContent label) =>
            property.DrawProperty(label, property.depth);

        public static void DrawProperty(this SerializedProperty property, GUIContent label, int depth)
        {
            if (depth == default                                  && property.hasChildren &&
                !property.GetUnderlyingType().HasPropertyDrawer() && !property.HasAttributeWithPropertyDrawer())
                property.DrawFoldoutAndEachChildren(label, depth);
            else
                EditorGUILayout.PropertyField(property, label);
        }

        public static void DrawFoldoutAndEachChildren(this SerializedProperty    property, GUIContent label,
                                                      Action<SerializedProperty> drawChild = default) =>
            property.DrawFoldoutAndEachChildren(label, property.depth, drawChild);

        public static void DrawFoldoutAndEachChildren(this SerializedProperty property, GUIContent label,
                                                      int depth, Action<SerializedProperty> drawChild = default) =>
            property.DrawFoldoutAndChildren(label, parentProperty =>
            {
                foreach (SerializedProperty child in parentProperty.GetDirectChildren())
                    if (drawChild != default)
                        drawChild.Invoke(child);
                    else
                        EditorGUILayout.PropertyField(child, true);
            }, depth);

        public static void DrawFoldoutAndChildren(this SerializedProperty    property, GUIContent label,
                                                  Action<SerializedProperty> drawChildren) =>
            property.DrawFoldoutAndChildren(label, drawChildren, property.depth);

        public static void DrawFoldoutAndChildren(this SerializedProperty    property,     GUIContent label,
                                                  Action<SerializedProperty> drawChildren, int        depth)
        {
            using (new TopLevelFoldoutIndentionFixer(depth))
                property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, label, true);
            if (property.isExpanded)
                using (new EditorGUI.IndentLevelScope())
                    drawChildren?.Invoke(property);
        }

        public static IEnumerable<PropertyAttribute> GetCustomAttributes(this SerializedProperty property) =>
            property.GetCustomAttributes<PropertyAttribute>();

        public static IEnumerable<T> GetCustomAttributes<T>(this SerializedProperty property) =>
            property.GetFieldInfo()?.GetCustomAttributes(typeof(T), true).Cast<T>() ?? Enumerable.Empty<T>();

        public static Type GetUnderlyingType(this SerializedProperty property) => property.GetFieldInfo().FieldType;

        public static FieldInfo GetFieldInfo(this SerializedProperty property)
        {
            Type               targetType   = property.serializedObject.targetObject.GetType();
            string[]           pathParts    = property.propertyPath.Split('.');
            FieldInfo          fieldInfo    = default;
            const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

            foreach (string pathPart in pathParts)
            {
                fieldInfo = targetType.GetField(pathPart, bindingFlags);
                if (fieldInfo == default)
                    break;
                targetType = fieldInfo.FieldType;
            }

            return fieldInfo;
        }
    }
}