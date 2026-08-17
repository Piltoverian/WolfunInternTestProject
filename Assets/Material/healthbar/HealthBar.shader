
Shader "Custom/TextureBasedHealthBar"
{
    Properties
    {
       [MainTexture] _ColorMap("Base Map", 2D) = "white" {}
       [CurrentHealthPercentage] _CurrentHealth("Current Health", Range(0, 1)) = 1
       [BaseColor] _BaseColor("Base Color", Color) = (1, 1, 1, 1)
       [AnimSpeed] _AnimSpeed("Animation Speed", Range(0,10)) = 1
       [BorderWidth] _BorderWidth("Border Width", Range(0, 0.5)) = 0.05
       [BorderColor] _BorderColor("Border Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue"="Transparent" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
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

            TEXTURE2D(_ColorMap);
            SAMPLER(sampler_ColorMap);

            CBUFFER_START(UnityPerMaterial)
                float _CurrentHealth;
                float4 _BaseColor;
                float _AnimSpeed;
                float _BorderWidth;
                float4 _BorderColor;
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
                half4 color = SAMPLE_TEXTURE2D(_ColorMap, sampler_ColorMap,float2(_CurrentHealth,IN.uv.y));
                float pureDistance = distance(coord,pivot);
                if (pureDistance> 0.5-_BorderWidth)
                {
                    color = _BorderColor;
                }
                if (IN.uv.x >= _CurrentHealth)
                {
                    return _BaseColor;
                }
                if (_CurrentHealth<=0.2)
                {
                    float TimeFactor = sin(_Time.y * _AnimSpeed) * 0.5 + 0.5;
                     if (1-TimeFactor <= 0.6)
                    {
                        TimeFactor=0.4;
                    }
                    color=color*(1-TimeFactor);
                   
                }
                return color;
            }
            ENDHLSL
        }
    }
}
