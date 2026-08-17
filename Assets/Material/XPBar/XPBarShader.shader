Shader "Custom/XPBarShader"
{
    Properties
    {
        [MainColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        [BorderWidth] _BorderWidth("Border Width", Range(0, 0.5)) = 0.05
        [BorderColor] _BorderColor("Border Color", Color) = (1, 1, 1, 1)
        [XPPercentage] _XPPercentage("XP Percentage", Range(0, 1)) = 0
        [XPColor] _XPColor("XP Color", Color) = (1, 1, 0, 1)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                float _BorderWidth;
                half4 _BorderColor;
                float _XPPercentage;
                half4 _XPColor;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float2 coord=IN.uv;
                coord.x*=16;
                float2 pivot= float2(clamp(coord.x,0.5,15.5),0.5);
                float sdf = distance(coord,pivot)*2 - 1;
                clip(-sdf);
                half4 color = _XPColor;
                float pureDistance = distance(coord,pivot);
                if (pureDistance> 0.5-_BorderWidth)
                {
                    color = _BorderColor;
                }
                if (IN.uv.x >= _XPPercentage)
                {
                    return _BaseColor;
                }
                return color;
            }
            ENDHLSL
        }
    }
}
