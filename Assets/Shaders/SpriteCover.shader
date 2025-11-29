Shader "PokemonGame/SpriteCover"
{
    Properties
    {
        _MainTex("Base Sprite", 2D) = "white" {}
        _CoverTex("Cover Sprite", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        _CoverOffsetX("Cover Offset X", Float) = 0
        _CoverOffsetY("Cover Offset Y", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uvMain : TEXCOORD0;
                float2 uvCover : TEXCOORD1;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _CoverTex;
            float4 _CoverTex_ST;
            fixed4 _Color;
            float _CoverOffsetX;
            float _CoverOffsetY;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uvMain = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.uvCover = TRANSFORM_TEX(v.texcoord, _CoverTex) + float2(_CoverOffsetX, _CoverOffsetY);
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Base sprite always visible
                fixed4 baseCol = tex2D(_MainTex, i.uvMain);

                // Cover color, using its alpha to overlay
                fixed4 coverCol = tex2D(_CoverTex, i.uvCover) * _Color;

                // Blend cover over base using cover alpha
                fixed4 finalCol;
                finalCol.rgb = lerp(baseCol.rgb, coverCol.rgb, coverCol.a);
                finalCol.a = baseCol.a; // keep base sprite alpha

                return finalCol;
            }
            ENDCG
        }
    }
}
