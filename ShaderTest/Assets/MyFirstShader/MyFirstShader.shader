// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "MyFirstShader"
{

    SubShader
    {

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // a : application
            // v : vertex shader
            struct a2v
            {
                float4 vertex : POSITION;   // 模型空间顶点坐标
                float3 normal : NORMAL;     // 模型空间的法线方向
                float4 textureCoord : TEXCOORD0;    // 模型的第一套纹理坐标
            };

            struct v2f
            {
                float4 pos : SV_POSITION;   // 裁剪空间中的顶点坐标
                fixed3 color : COLOR0;      // 存储颜色信息
            };

            v2f vert (a2v v)   // 裁剪空间中的顶点坐标
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.normal * 0.5 + fixed3(0.50, 0.5, 0.5);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target  // 输出颜色存储到一个渲染目标
            {
                return fixed4(i.color.x, 0, 0, 1.0);
            }
            ENDCG
        }
    }
}
