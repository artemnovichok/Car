Shader "Custom/BlueToCyanGradientShader"
{
    Properties
    {
        _TopColor("Top Color", Color) = (0, 1, 1, 1) // Cyan
        _BottomColor("Bottom Color", Color) = (0, 0, 1, 1) // Blue
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
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _TopColor;
            float4 _BottomColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.vertex.xy;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Вертикальный градиент от _BottomColor к _TopColor
                return lerp(_BottomColor, _TopColor, i.uv.y);
            }
            ENDCG
        }
    }
}
