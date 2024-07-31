Shader "Custom/PurpleToTealGradientShader"
{
    Properties
    {
        _TopColor("Top Color", Color) = (0, 1, 1, 1) // Teal
        _BottomColor("Bottom Color", Color) = (0.5, 0, 0.5, 1) // Purple
        _NoiseTexture("Noise Texture", 2D) = "white" {} // Noise Texture
        _NoiseStrength("Noise Strength", Range(0, 1)) = 0.5 // Strength of noise effect
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };

                float4 _TopColor;
                float4 _BottomColor;
                sampler2D _NoiseTexture;
                float _NoiseStrength;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Вертикальный градиент от _BottomColor к _TopColor
                    fixed4 gradientColor = lerp(_BottomColor, _TopColor, i.uv.y);

                // Добавляем шумовую текстуру для создания пятен
                fixed4 noise = tex2D(_NoiseTexture, i.uv);
                gradientColor.rgb += noise.rgb * _NoiseStrength;

                return gradientColor;
            }
            ENDCG
        }
        }
}
