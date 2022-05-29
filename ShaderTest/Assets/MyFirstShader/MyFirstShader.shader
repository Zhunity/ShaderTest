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


            float4 vert (a2v v) : SV_POSITION   // 裁剪空间中的顶点坐标
            {
                return UnityObjectToClipPos(v.vertex);
            }

            fixed4 frag () : SV_Target  // 输出颜色存储到一个渲染目标
            {
                return fixed4(1.0, 0.0, 0.0, 1.0);
            }
            ENDCG
        }
    }
}
