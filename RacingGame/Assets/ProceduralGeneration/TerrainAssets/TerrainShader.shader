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

        Tags { "RenderType"="Opaque"}
        LOD 200

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        float4 _flatColor = float4(1,0,1,0);
        float4 _steepColor = float4(1,1,0,1);

        float _maxHeight;
        float _minHeight;

        struct Input
        {
            float3 worldPos;
            float3 worldNormal;
        };

        float lerp(float a, float b, float t)
        {
            return a+(b-a)*t;
        }

        float3 lerp3(float3 a, float3 b, float t)
        {
            return float3(a.x+(b.x-a.x)*t, a.y+(b.y-a.y)*t, a.z+(b.z-a.z)*t);
        }
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //1: steep;   0: flat
            float steepness = 1 - (dot(float3(0,1,0), IN.worldNormal));
            //o.Albedo = lerp3(_flatColor.xyz, _steepColor.xyz, steepness);
            //color = 
            o.Albedo = float3(0,steepness,0);
        }


        ENDCG
        
        
            

    }
    FallBack "Diffuse"
}
