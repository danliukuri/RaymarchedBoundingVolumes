using RBV.Editor.Utilities.Extensions;
using RBV.Features;
using UnityEditor;

namespace RBV.Editor.Project.Features
{
    [CustomEditor(typeof(RaymarchingOperation)), CanEditMultipleObjects]
    public class RaymarchingOperationEditor : UnityEditor.Editor
    {
        private SerializedProperty _typeProperty;
        private SerializedProperty _typeDataProperty;

        private void OnEnable() => Initialize();

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProperties();
            serializedObject.ApplyModifiedProperties();
        }

        private void Initialize()
        {
            _typeProperty =
                serializedObject.FindProperty(nameof(RaymarchingOperation.Type).ToBackingFieldFormat());
            _typeDataProperty =
                serializedObject.FindProperty(nameof(RaymarchingOperation.TypeData).ToBackingFieldFormat());
        }

        private void DrawProperties()
        {
            this.DrawScriptField<RaymarchingOperation>();
            this.DrawEditorField();

            EditorGUILayout.PropertyField(_typeProperty);
            EditorGUILayout.PropertyField(_typeDataProperty);
        }
    }
}