using UnityEditor;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Editor
{
    public class DrawIfOnDrawer : MaterialPropertyDrawer
    {
        private readonly string _shaderKeyword;

        public DrawIfOnDrawer(string keyword) => _shaderKeyword = keyword;

        public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
        {
            if (editor.target is Material material && material.IsKeywordEnabled(_shaderKeyword))
                editor.DefaultShaderProperty(position, prop, label.text);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor) =>
            editor.target is Material material && material.IsKeywordEnabled(_shaderKeyword)
                ? MaterialEditor.GetDefaultPropertyHeight(prop)
                : default;
    }
}