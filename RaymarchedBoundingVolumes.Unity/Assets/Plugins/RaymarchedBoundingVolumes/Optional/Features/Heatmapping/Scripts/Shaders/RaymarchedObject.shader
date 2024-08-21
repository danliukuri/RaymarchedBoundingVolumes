Shader "RaymarchedBoundingVolumes/Heatmapping/RaymarchedObject"
{
    
    Properties
    {
        _MaxDetectionIterations ("Max Detection Iterations", Range(0  , 512     )) = 32
        _MaxDetectionOffset     ("Max Detection Offset"    , Range(0.1, 0.000001)) = 0.005
        _FarClippingPlane       ("Far Clipping Plane"      , Range(0  , 1024    )) = 32

        [NoScaleOffset] _HeatmapTexture ("Heatmap Texture", 2D) = "white" {}

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            Tags { "LightMode"="ForwardBase" }

            CGPROGRAM
            #pragma vertex processVertex
            #pragma fragment processFragment
            #pragma multi_compile_instancing

            #include "../../../../../Core/Scripts/Runtime/Shaders/Data/Structures/RaymarchingDataStructures.cginc"
            #include "../../../../../Core/Scripts/Runtime/Shaders/Data/Structures/ShaderDataStructures.cginc"
            #include "../../../../../Core/Scripts/Runtime/Shaders/Data/Variables/RaymarchingGlobalVariables.cginc"
            #include "../../../../../Core/Scripts/Runtime/Shaders/Features/Calculators/RayDataCalculator.cginc"
            #include "../../../../../Core/Scripts/Runtime/Shaders/Features/Calculators/PixelDepthCalculator.cginc"
            #include "Features/Raymarcher.cginc"
            
            FragmentData processVertex(const AppData input)
            {
                FragmentData output;
                UNITY_SETUP_INSTANCE_ID(input);
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.hitPosition = input.vertex;
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                return output;
            }

            PixelData processFragment(const FragmentData input)
            {
                if (_RaymarchingOperationsCount == 0)
                    discard;

                UNITY_SETUP_INSTANCE_ID(input)
                const float3 rayDirection = calculateRayDirection(input.hitPosition);
                const RaymarchingData raymarchingData = raymarch(input.hitPosition, rayDirection);
                const PixelData pixel = {fixed4(raymarchingData.color, 1), calculateDepth(raymarchingData.position)};
                return pixel;
            }
            ENDCG
        }
    }
}