Shader "RaymarchedBoundingVolumes/Core/RaymarchedObject"
{
    Properties
    {
        _MaxDetectionIterations ("Max Detection Iterations", Range(0  , 512     )) = 80
        _MaxDetectionOffset     ("Max Detection Offset"    , Range(0.1, 0.000001)) = 0.005
        _FarClippingPlane       ("Far Clipping Plane"      , Range(0  , 1024    )) = 20

        _ObjectColor ("Object Color", Color) = (1, 1, 1, 1)

        [Toggle(HEATMAP_VISUALIZATION_ON)] _HeatmapVisualizationEnabled ("Heatmap Visualization Enabled", Float) = 0
        [DrawIfOn(HEATMAP_VISUALIZATION_ON)] [NoScaleOffset] _HeatmapTexture ("Heatmap Texture", 2D) = "white" {}
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
            #pragma multi_compile __ HEATMAP_VISUALIZATION_ON

            #include "UnityCG.cginc"
            #include "RaymarchingGlobalVariables.cginc"
            #include "SDFCalculators.cginc"
            #include "RaymarchingShading.cginc"
            #include "Raymarcher.cginc"

            struct AppData
            {
                float4 vertex : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct FragmentData
            {
                float4 vertex : SV_POSITION;
                float3 hitPosition : TEXCOORD1;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct PixelData
            {
                fixed4 color : SV_Target;
                float depth : SV_Depth;
            };

            FragmentData processVertex(const AppData input)
            {
                FragmentData output;
                UNITY_SETUP_INSTANCE_ID(input);
                output.vertex = UnityObjectToClipPos(input.vertex);
                output.hitPosition = input.vertex;
                UNITY_TRANSFER_INSTANCE_ID(input, output);
                return output;
            }

            float calculateDepth(const float3 position)
            {
                float4 clipPos = UnityObjectToClipPos(float4(position, 1));
                return clipPos.z / clipPos.w;
            }

            PixelData processFragment(const FragmentData input)
            {
                if (_RaymarchingOperationsCount == 0)
                    discard;

                UNITY_SETUP_INSTANCE_ID(input)
                const float3 rayOrigin = mul(unity_WorldToObject, float4(_WorldSpaceCameraPos, 1));
                const float3 rayDirection = normalize(input.hitPosition - rayOrigin);

                PixelData pixel;
                #ifdef HEATMAP_VISUALIZATION_ON
                const RaymarchingData raymarching = raymarchHeatmap(input.hitPosition, rayDirection);
                pixel.color =  fixed4(raymarching.pixelColor, 1);
                #else
                const RaymarchingData raymarching = raymarch(input.hitPosition, rayDirection);
                pixel.color = fixed4(raymarching.pixelColor * shade(raymarching.objectPosition), 1);
                #endif
                pixel.depth = calculateDepth(raymarching.objectPosition);
                return pixel;
            }
            ENDCG
        }
    }
}