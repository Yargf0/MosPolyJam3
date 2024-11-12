Shader "Custom/RandomVertexOffset"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Amplitude ("Offset Amplitude", Float) = 0.1
        _Speed ("Speed", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard vertex:vert

        sampler2D _MainTex;
        fixed3 _Color;
        float _Amplitude;
        half _Speed;

        struct Input
        {
            float2 uv_MainTex;
        };

        float random (float2 seed)
        {
            return frac(sin(dot(seed, float2(12.9898, 78.233))) * 43758.5453);
        }

        float remap(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            return toMin + (value.x - fromMin) * (toMax - toMin) / (fromMax - fromMin);
        }

        float2 remap(float2 value, float fromMin, float fromMax, float toMin, float toMax)
        {
            return float2(
                toMin + (value.x - fromMin) * (toMax - toMin) / (fromMax - fromMin),
                toMin + (value.y - fromMin) * (toMax - toMin) / (fromMax - fromMin));
        }

        void vert (inout appdata_full v)
        {
            float rand = remap(random(_Time.xy / 100), 0, 1, -1, 1);

            if (rand > 0.5)
            {
                //float3 viewDir = normalize(ObjSpaceViewDir(v.vertex));
                //float3 perpendicularDir = normalize(cross(viewDir, float3(0, 1, 0)));
                // v.vertex.xyz += perpendicularDir * _Amplitude * (rand - 0.5);
            }

            v.vertex.x += _Amplitude * rand;
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
        }

        ENDCG
    }
    FallBack "Diffuse"
}