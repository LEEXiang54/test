Shader "Zgame/PostProcessing/CameraWideRangeEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BgTex("BgTex",2D)="white"{}
        _Tilling("Tilling",Range(0,5))=1
        _OffsetX("OffsetX",Float)=0
        _OffsetY("OffsetY",Float)=0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" "LightMode"="ForwardBase"}
        LOD 100
        ZWrite Off 
        ZTest Always
        Cull  Off 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            sampler2D _BgTex;
            float _Tilling;
            float _OffsetX;
            float _OffsetY;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half2 dir=normalize(half2(0.5,0.5)-i.uv);
                half2 bgUv=i.uv*_Tilling+half2(_OffsetX,_OffsetY);


                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 bg=tex2D(_BgTex,bgUv);

                if(bgUv.x<0||bgUv.x>1||bgUv.y<0||bgUv.y>1)
                    bg.xyz=0;
                col.xyz=lerp(bg.xyz,col.xyz,col.w);
                return col;
            }
            ENDCG
        }
    }
}
