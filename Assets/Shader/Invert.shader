Shader "Custom/Invert"
{
    Properties
    {
        [HideInInspector]_MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }
        GrabPass {}

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _GrabTexture_ST;
            sampler2D _GrabTexture;
            float4 _MainTex_ST;
            sampler2D _MainTex;

            struct a2v
            {
                float4 vertex : POSITION;
                float4 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 screen_pos : TEXCOORD1;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                // o.uv = v.uv;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screen_pos = ComputeGrabScreenPos(o.pos);
                o.screen_pos /= o.screen_pos.w;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // fixed4 colorMain = tex2D(_MainTex, i.uv);
                fixed3 color = tex2D(_GrabTexture, i.screen_pos).rgb;
                fixed3 white = fixed3(1, 1, 1);
                return fixed4(white - color, 1);
            }
            ENDCG
        }
    }
    Fallback Off
}