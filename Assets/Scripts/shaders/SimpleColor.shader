Shader "Custom/SimpleColor"
{
    // 第一部分：属性面板（暴露给 Unity 外部修改的变量）
    Properties
    {
        // 变量名 ("面板显示的文字", 类型) = 默认值
        _MainColor ("My Color", Color) = (1, 0, 0, 1) // 默认红色，1代表不透明
    }

    // 第二部分：子着色器（真正的渲染逻辑）
    SubShader
    {
        Pass
        {
            CGPROGRAM
            // 告诉 GPU，我的顶点函数叫 vert，像素函数叫 frag
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // 把上面的 Properties 里的变量在这里重新声明一下，代码才能用
            float4 _MainColor;

            // 1. 顶点着色器：计算位置
            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                // 这句是死记硬背的公式：把物体的 3D 坐标转换成屏幕上的 2D 坐标
                return UnityObjectToClipPos(vertex);
            }

            // 2. 片元着色器：上色
            float4 frag () : SV_Target
            {
                // 极其简单粗暴，直接返回我们在面板里选的颜色
                return _MainColor; 
            }
            ENDCG
        }
    }
}