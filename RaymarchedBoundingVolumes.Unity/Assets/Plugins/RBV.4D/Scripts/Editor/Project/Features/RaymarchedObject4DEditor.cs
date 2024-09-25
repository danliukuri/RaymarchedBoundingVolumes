using RBV.Editor.Utilities.Extensions;
using RBV.FourDimensional.Features;
using UnityEditor;

namespace RBV.FourDimensional.Editor.Project.Features
{
    [CustomEditor(typeof(RaymarchedObject4D)), CanEditMultipleObjects]
    public class RaymarchedObject4DEditor : UnityEditor.Editor
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
            _typeProperty              = serializedObject.FindProperty(RaymarchedObject4D.FieldNames.Type);
            _typeDataProperty          = serializedObject.FindProperty(RaymarchedObject4D.FieldNames.TypeData);
            _transformProperty         = serializedObject.FindProperty(RaymarchedObject4D.FieldNames.Transform);
            _renderingSettingsProperty = serializedObject.FindProperty(RaymarchedObject4D.FieldNames.RenderingSettings);
        }

        private void DrawProperties()
        {
            this.DrawScriptField<RaymarchedObject4D>();
            this.DrawEditorField();

            EditorGUILayout.PropertyField(_typeProperty);
            EditorGUILayout.PropertyField(_typeDataProperty);
            EditorGUILayout.PropertyField(_transformProperty);
            EditorGUILayout.PropertyField(_renderingSettingsProperty);
        }
    }
}