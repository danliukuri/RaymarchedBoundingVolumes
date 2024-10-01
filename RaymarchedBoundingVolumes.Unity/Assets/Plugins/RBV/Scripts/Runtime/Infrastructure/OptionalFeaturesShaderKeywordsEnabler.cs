using UnityEditor;
using UnityEngine;

namespace RBV.Infrastructure
{
    public class OptionalFeaturesShaderKeywordsEnabler
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
#else
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
        public static void Toggle()
        {
            ToggleHeatmapping();
            Toggle4D();
        }

        private static void ToggleHeatmapping()
        {
#if !RBV_HEATMAPPING_ON
            Shader.DisableKeyword("RBV_HEATMAPPING_ON");
#endif
        }

        private static void Toggle4D()
        {
#if RBV_4D_ON
            Shader.EnableKeyword("RBV_4D_ON");
#else
            Shader.DisableKeyword("RBV_4D_ON");
#endif
        }
    }
}