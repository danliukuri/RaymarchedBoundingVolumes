using RBV.Editor.Utilities.Extensions;
using RBV.Features;
using UnityEditor;

namespace RBV.Editor.Project.Features
{
    [CustomEditor(typeof(RaymarchedObject3D)), CanEditMultipleObjects]
    public class RaymarchedObject3DEditor : UnityEditor.Editor
    {
        private SerializedProperty _typeProperty;
        private SerializedProperty _typeDataProperty;
        private SerializedProperty _transformProperty;
        private SerializedProperty _renderingSettingsProperty;

        private void OnEnable() => Initialize();

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProperties();
            serializedObject.ApplyModifiedProperties();
        }

        private void Initialize()
        {
            _typeProperty              = serializedObject.FindProperty(RaymarchedObject3D.FieldNames.Type);
            _typeDataProperty          = serializedObject.FindProperty(RaymarchedObject3D.FieldNames.TypeData);
            _transformProperty         = serializedObject.FindProperty(RaymarchedObject3D.FieldNames.Transform);
            _renderingSettingsProperty = serializedObject.FindProperty(RaymarchedObject3D.FieldNames.RenderingSettings);
        }

        private void DrawProperties()
        {
            this.DrawScriptField<RaymarchedObject3D>();
            this.DrawEditorField();

            EditorGUILayout.PropertyField(_typeProperty);
            EditorGUILayout.PropertyField(_typeDataProperty);
            EditorGUILayout.PropertyField(_transformProperty);
            EditorGUILayout.PropertyField(_renderingSettingsProperty);
        }
    }
}