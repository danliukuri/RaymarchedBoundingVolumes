Shader "RBV/RaymarchedObject"
{
    Properties
    {
        [Header(Raymarching)] [Space]
        [IntRange]
        _MaxDetectionIterations        ("Max Detection Iterations", Range(0    , 512     )) = 64
        _MaxDetectionOffset            ("Max Detection Offset"    , Range(0.1  , 0.000001)) = 0.005
        _FarClippingPlane              ("Far Clipping Plane"      , Range(0    , 1024    )) = 32

        [Header(Shadows    )] [Space]
        [KeywordEnum(None, Hard, Soft)]
        Shadows_Type                   ("Type"                    , Int                   ) = 0
        [DrawIfOff(SHADOWS_TYPE_NONE)]
        _ShadowsMaxDetectionIterations ("Max Detection Iterations", Range(0    , 512     )) = 32
        [DrawIfOff(SHADOWS_TYPE_NONE)]
        _ShadowsMaxDetectionOffset     ("Max Detection Offset"    , Range(0.035, 0.000001)) = 0.005
        [DrawIfOff(SHADOWS_TYPE_NONE)]
        _ShadowsMinDistance            ("Min Distance"            , Range(0    , 2       )) = 0.05
        [DrawIfOff(SHADOWS_TYPE_NONE)]
        _ShadowsMaxDistance            ("Max Distance"            , Range(0    , 1024    )) = 5
        [DrawIfOff(SHADOWS_TYPE_NONE)]
        _ShadowsIntensity              ("Intensity"               , Range(0    , 1       )) = 1
        [DrawIfOn (SHADOWS_TYPE_SOFT)]
        _ShadowsPenumbraSize           ("Penumbra Size"           , Range(1.25 , 32      )) = 3
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 100

        Pass
        {
            Tags
            {
                "LightMode"="ForwardBase"
            }

            CGPROGRAM
            #pragma vertex processVertex
            #pragma fragment processFragment
            #pragma multi_compile_instancing
            #pragma multi_compile RBV_4D_OFF RBV_4D_ON
            #pragma multi_compile SHADOWS_TYPE_NONE SHADOWS_TYPE_HARD SHADOWS_TYPE_SOFT

            #include "Data/Structures/RaymarchingDataStructures.cginc"
            #include "Data/Structures/ShaderDataStructures.cginc"
            #include "Data/Variables/RaymarchingGlobalVariables.cginc"
            #include "Features/Calculators/RayDataCalculator.cginc"
            #include "Features/Calculators/PixelDepthCalculator.cginc"
            #include "Features/RaymarchingShading.cginc"
            #include "Features/Raymarcher.cginc"

            FragmentData processVertex(const AppData input)
            {
                FragmentData output;
                UNITY_SETUP_INSTANCE_ID(input);
                output.vertex      = UnityObjectToClipPos(input.vertex);
                output.hitPosition = input.vertex;
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                return output;
            }

            PixelData processFragment(const FragmentData input)
            {
                if (_RaymarchingOperationsCount == 0)
                    discard;

                UNITY_SETUP_INSTANCE_ID(input)
                const float3          rayDirection    = calculateRayDirection(input.hitPosition);
                const RaymarchingData raymarchingData = raymarch(input.hitPosition, rayDirection);
                const PixelData       pixel           =
                    {calculateShadedPixelColor(raymarchingData), calculateDepth(raymarchingData.position)};

                return pixel;
            }
            ENDCG
        }
    }
}