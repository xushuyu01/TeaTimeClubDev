Shader "Unlit/SimpleStamp"
{
    Properties
    {
        _MainTex("Base (RT)", 2D) = "white" {}
        _StampTex("Stamp Texture", 2D) = "white" {}
        _StampPos("Stamp Position", Vector) = (0.5, 0.5, 0, 0)
        _StampSize("Stamp Size", Float) = 0.1
    }

        SubShader
        {
            Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
            LOD 100

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                sampler2D _StampTex;
                float4 _StampPos;
                float _StampSize;

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

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // 原始像素
                    float4 baseCol = tex2D(_MainTex, i.uv);

                    // 是否在 stamp 区域
                    float2 stampCenter = _StampPos.xy;
                    float2 dist = abs(i.uv - stampCenter);
                    float2 stampUV = dist / _StampSize;

                    if (stampUV.x > 1 || stampUV.y > 1)
                    {
                        return baseCol;
                    }

                    // 将 UV 转换到 0~1 的 stamp 图采样区
                    float2 localUV = (i.uv - (stampCenter - _StampSize)) / (_StampSize * 2);

                    float4 stampColor = tex2D(_StampTex, localUV);

                    // 叠加到原图
                    return lerp(baseCol, stampColor, stampColor.a);
                }
                ENDCG
            }
        }
}
