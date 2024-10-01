using System.IO;
using RBV.Heatmapping.Editor.Utilities.Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace RBV.Heatmapping.Editor.Features
{
#if !UNITY_2021_2_OR_NEWER
    [InitializeOnLoad]
    internal sealed class HeatmappingSettings
#else
    internal sealed class HeatmappingSettings : AssetPostprocessor
#endif
    {
        private const string HeatmappingEnabledKey      = "RaymarchedBoundingVolumes/Heatmapping/Enabled";
        private const string HeatmappingTexturePathKey  = "RaymarchedBoundingVolumes/Heatmapping/TexturePath";
        private const string DefaultTexturePathRelative = "Art/Sprites/Heatmaps/HitmapMagma.png";
        private const string PreferencePathRelative     = "Raymarched Bounding Volumes/Heatmapping";
        private const string PreferencePath             = "Preferences/" + PreferencePathRelative;

        private static readonly string _defaultTexturePath =
            Path.Combine(PathExtensions.RootPathRelativeToProject(), DefaultTexturePathRelative);

        private static readonly GlobalKeyword _heatmappingEnabledShaderKeywordID =
            GlobalKeyword.Create("RBV_HEATMAPPING_ON");

        private static readonly int _heatmapTexturePropertyID = Shader.PropertyToID("_RbvHeatmapTexture");

        private static Texture2D _heatmapTexture = InitializeHeatmapTexture();

        private static Texture2D HeatmapTexture
        {
            get => _heatmapTexture;
            set
            {
                if (value != default)
                {
                    _heatmapTexture    = value;
                    value.hideFlags    = HideFlags.DontSaveInEditor;
                    HeatmapTexturePath = AssetDatabase.GetAssetPath(value);
                    Shader.SetGlobalTexture(_heatmapTexturePropertyID, value);
                }
            }
        }

        private static string HeatmapTexturePath
        {
            get => EditorPrefs.GetString(HeatmappingTexturePathKey, _defaultTexturePath);
            set => EditorPrefs.SetString(HeatmappingTexturePathKey, value);
        }

        private static bool IsHeatmappingEnabled
        {
            get => EditorPrefs.GetBool(HeatmappingEnabledKey, default);
            set
            {
                EditorPrefs.SetBool(HeatmappingEnabledKey, value);
                Shader.SetKeyword(_heatmappingEnabledShaderKeywordID, value);
            }
        }

        private static Texture2D InitializeHeatmapTexture()
        {
            var heatmapTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(HeatmapTexturePath);
            heatmapTexture.hideFlags = HideFlags.DontSaveInEditor;
            return heatmapTexture;
        }

#if UNITY_2019_1_OR_NEWER
        [SettingsProvider]
        private static SettingsProvider CreateProjectSettingsProvider() => new(PreferencePath, SettingsScope.User)
        {
            guiHandler = searchContext => OnGUI()
        };
#else
        [PreferenceItem(PreferencePathRelative)]
#endif
        private static void OnGUI()
        {
            const float width   = 500f;
            const float padding = 3f;

            DrawEnabledToggle();
            DrawTexture(width, padding);
        }

        private static void DrawTexture(float width, float padding)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            EditorGUI.LabelField(rect, new GUIContent("Texture"));
            float localPadding = padding * Mathf.Sqrt(0.5f);
            rect.xMin  += localPadding + EditorGUIUtility.labelWidth;
            rect.width =  width        - EditorGUIUtility.labelWidth - padding - (padding - localPadding);

            EditorGUI.BeginChangeCheck();
            var newTexture = (Texture2D)EditorGUI.ObjectField(rect, HeatmapTexture, typeof(Texture2D), false);
            if (EditorGUI.EndChangeCheck())
                HeatmapTexture = newTexture;

            DrawTexturePreview(width, padding);
        }

        private static void DrawTexturePreview(float width, float padding)
        {
            float aspectRatio = (float)HeatmapTexture.height / HeatmapTexture.width;
            float height      = width                        * aspectRatio;

            GUILayout.BeginScrollView(Vector2.zero, false, false,
                GUILayout.Width(width + padding), GUILayout.Height(height));

            Rect textureRect = EditorGUILayout.GetControlRect();
            textureRect.width  = width;
            textureRect.height = height;

            GUILayout.BeginArea(textureRect);
            GUI.DrawTexture(textureRect, HeatmapTexture, ScaleMode.ScaleToFit);
            GUILayout.EndArea();

            GUILayout.EndScrollView();
        }

        private static void DrawEnabledToggle()
        {
            bool newHeatmappingEnabledState = EditorGUILayout.Toggle(new GUIContent("Enabled"), IsHeatmappingEnabled);
            if (EditorGUI.EndChangeCheck())
                IsHeatmappingEnabled = newHeatmappingEnabledState;
        }
    }
}