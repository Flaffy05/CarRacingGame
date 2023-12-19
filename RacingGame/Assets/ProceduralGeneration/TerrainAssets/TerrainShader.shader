Shader "Custom/TerrainShader"
{
    Properties
    {
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

        static const int maxNumberOfColors = 8;
        int numberOfColors = 2;

        float3 flatColor = float3(1,0,1);
        float3 steepColor = float3(1,0,1);

        float3 colorArray[maxNumberOfColors];

        float maxHeight;
        float minHeight;

        struct Input
        {
            float3 worldPos;
            float3 worldNormal;
        };

        float lerp(float a, float b, float t)
        {
            return a+(b-a)*t;
        }

        float inverseLerp(float a, float b, float t)
        {
            return saturate((t-a)/(b-a));     
        }

        float3 lerp3(float3 a, float3 b, float t)
        {
            return saturate(float3(a.x+(b.x-a.x)*t, a.y+(b.y-a.y)*t, a.z+(b.z-a.z)*t));
        }
        
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //1: steep;   0: flat
            float steepness = 1 - (dot(float3(0,1,0), IN.worldNormal));
            
            o.Albedo = colorArray[(int)lerp(0, (float)numberOfColors, steepness)];
            
            //o.Albedo = float3(inverseLerp(minHeight, maxHeight, IN.worldPos.y),0,0.5);

        }


        ENDCG
        
        
            

    }
    FallBack "Diffuse"
}
