Shader "Custom/RandomVertexDisplacement"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Amplitude ("Displacement Amplitude", Float) = 1.0
        _Speed ("Noise Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard vertex:vert

        sampler2D _MainTex;
        float _Amplitude;
        float _Speed;

        // Declaration of unity_Time for time-based effects
        float4 unity_Time;

        struct Input
        {
            float2 uv_MainTex;
        };

        // Generate Perlin-like noise based on position and time for randomness
        float noise(float3 position)
        {
            return frac(sin(dot(position, float3(12.9898, 78.233, 37.719))) * 43758.5453);
        }

        // Vertex function to randomly displace vertices over time using noise
        void vert (inout appdata_full v)
        {
            // Generate time-based seed using unity_Time.x for continuous effect
            float timeFactor = _Time.x * _Speed;
            float noiseValue = noise(v.vertex.xyz + timeFactor);

            // Select vertices to displace based on noise threshold
            if (noiseValue > 0.5)  // Adjust threshold to control displacement percentage
            {
                // Displace vertices randomly along x-axis within range [-1, 1]
                float displacement = (noiseValue - 0.5) * 2.0 * _Amplitude; 
                v.vertex.x += displacement;
            }
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Sample and set the texture as Albedo color
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}