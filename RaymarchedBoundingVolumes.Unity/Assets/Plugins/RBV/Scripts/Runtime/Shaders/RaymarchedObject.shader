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
        [Info(SHADOWS_TYPE_SOFT_A, This type of shadows is fastest but may generate artifacts. Sharp corners in the objects casting the shadow often lead to missing penumbras.)]
        [Info(SHADOWS_TYPE_SOFT_B, This type of shadows is slower than type A but creates better results in challenging situations. It effectively handles sharp corners on shadow casters.)]
        [Info(SHADOWS_TYPE_SOFT_C, This type of shadows is the slowest but delivers results closest to physically accurate shadows. Like type B it also excels in challenging cases with sharp corners on shadow casters.)]
        [KeywordEnum(None, Hard, Soft A, Soft B, Soft C)]
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
        [DrawIfAny (SHADOWS_TYPE_SOFT_A, SHADOWS_TYPE_SOFT_B, SHADOWS_TYPE_SOFT_C)]
        _ShadowsPenumbraSize           ("Penumbra Size"           , Range(0.025 , 0.8    )) = 0.2
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
            #pragma multi_compile SHADOWS_TYPE_NONE SHADOWS_TYPE_HARD SHADOWS_TYPE_SOFT_A SHADOWS_TYPE_SOFT_B SHADOWS_TYPE_SOFT_C

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