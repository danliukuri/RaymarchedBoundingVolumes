using UnityEngine;

namespace RBV.Infrastructure
{
    public class OptionalFeaturesShaderKeywordsEnabler
    {
#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
#endif
        public static void Enable()
        {
#if RBV_4D_ON
            Shader.EnableKeyword("RBV_4D_ON");
#endif
        }
    }
}