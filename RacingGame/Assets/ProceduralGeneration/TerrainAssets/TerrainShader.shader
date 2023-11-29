Shader "Custom/TerrainShader"
{
    Properties
    {
        //_Colors ("Color", Color[]) = (1,1,1,1)
        //_MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {

        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 200


        HLSLPROGRAM



        ENDHLSL
        /*
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/"

        //sampler2D _MainTex;

        struct Input
        {
            float3 worldPosition;
        };

        const static int maxColorCount = 8;

        float4 _baseColors[maxColorCount];
        float4 _baseHeight[maxColorCount];

        
        float _minHeight;
        float _maxHeight;

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        float inverseLerp(float a, float b, float value)
        {
            return saturate((value-a)/(b-a));
        }


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        //UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        //UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            //float heightPercent = inverseLerp(_minHeight, _maxHeight, IN.worldPosition[1]);
            o.Albedo = float3(0,1,1);
            // Metallic and smoothness come from slider variables
            //o.Metallic = _Metallic;
            //o.Smoothness = _Glossiness;
            //o.Alpha = c.a;
        }
        ENDCG
        */
        
            

    }
    FallBack "Diffuse"
}
