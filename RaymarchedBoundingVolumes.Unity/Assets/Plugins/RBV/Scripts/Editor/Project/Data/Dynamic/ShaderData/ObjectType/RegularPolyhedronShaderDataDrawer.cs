using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Data.Static.Enumerations;
using RBV.Editor.Utilities.Extensions;
using RBV.Utilities.Wrappers;
using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Project.Data.Dynamic.ShaderData.ObjectType
{
    [CustomPropertyDrawer(typeof(RaymarchedRegularPolyhedronShaderData))]
    public class RegularPolyhedronShaderDataDrawer : PropertyDrawer
    {
        private SerializedProperty _circumdiameterProperty;
        private SerializedProperty _activeBoundPlaneRange;
        private SerializedProperty _beginProperty;
        private SerializedProperty _endProperty;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) =>
            property.DrawFoldoutAndChildren(label, DrawChildrenProperties);
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            -EditorGUIUtility.standardVerticalSpacing;

        private void InitializeChildrenProperties(SerializedProperty property)
        {
            _circumdiameterProperty ??= property
                .FindPropertyRelative(nameof(RaymarchedRegularPolyhedronShaderData.InscribedDiameter));
            _activeBoundPlaneRange ??= property
                .FindPropertyRelative(nameof(RaymarchedRegularPolyhedronShaderData.ActiveBoundPlanesRange));
            _beginProperty ??= _activeBoundPlaneRange
                .FindPropertyRelative(nameof(Range<int>.Start).ToBackingFieldFormat());
            _endProperty ??= _activeBoundPlaneRange
                .FindPropertyRelative(nameof(Range<int>.End).ToBackingFieldFormat());
        }

        private void DrawChildrenProperties(SerializedProperty property)
        {
            InitializeChildrenProperties(property);

            EditorGUILayout.PropertyField(_circumdiameterProperty, true);

            RegularPolyhedronType selectedBoundType =
                RegularPolyhedronTypeExtensions.GetType(_beginProperty.intValue, _endProperty.intValue);
            var newBoundType =
                (RegularPolyhedronType)EditorGUILayout.EnumPopup(new GUIContent("Bound Type"), selectedBoundType);

            if (selectedBoundType != newBoundType)
                (_beginProperty.intValue, _endProperty.intValue) =
                    RegularPolyhedronTypeExtensions.GetActiveBoundPlanesRange(newBoundType);

            _activeBoundPlaneRange.DrawProperty(new GUIContent(_activeBoundPlaneRange.displayName));
        }
    }
}