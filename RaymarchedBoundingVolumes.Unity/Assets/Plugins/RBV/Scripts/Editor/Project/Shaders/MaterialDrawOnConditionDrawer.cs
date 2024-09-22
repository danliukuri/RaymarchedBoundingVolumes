using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Project.Shaders
{
    public abstract class MaterialDrawOnConditionDrawer : MaterialPropertyDrawer
    {
        protected abstract bool IsConditionMet(Material material);

        public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
        {
            if (editor.target is Material material && IsConditionMet(material))
                editor.DefaultShaderProperty(position, prop, label.text);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor) =>
            editor.target is Material material && IsConditionMet(material)
                ? MaterialEditor.GetDefaultPropertyHeight(prop)
                : default;
    }

    public class MaterialDrawIfOffDrawer : MaterialDrawOnConditionDrawer
    {
        private readonly string _shaderKeyword;

        public MaterialDrawIfOffDrawer(string keyword) => _shaderKeyword = keyword;

        protected override bool IsConditionMet(Material material) => !material.IsKeywordEnabled(_shaderKeyword);
    }

    public class MaterialDrawIfAnyDrawer : MaterialDrawOnConditionDrawer
    {
        private readonly string[] _shaderKeywords;

        public MaterialDrawIfAnyDrawer(params string[] keywords) => _shaderKeywords = keywords;

        protected override bool IsConditionMet(Material material) => _shaderKeywords.Any(material.IsKeywordEnabled);
    }
}