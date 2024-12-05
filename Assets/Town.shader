Shader "Custom/Town"
{
    Properties
    {
        _LineColor ("Line Color", Color) = (1, 0, 0, 1) // ���� ����� �� ��������� - �������
        _GridColor ("Grid Color", Color) = (0, 1, 0, 1) // ���� ����� �� ��������� - �������
        _GridSize ("Grid Size", Float) = 1.0 // ������ �����
        _GlowIntensity ("Glow Intensity", Float) = 1.0 // ������������� ��������
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _LineColor; // ���� �����
            fixed4 _GridColor; // ���� �����
            float _GridSize; // ������ �����
            float _GlowIntensity; // ������������� ��������

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // ��������� ���������� �����
                float grid = step(0.5, fmod(i.uv.x * _GridSize, 1.0)) + step(0.5, fmod(i.uv.y * _GridSize, 1.0));
                
                // ���� ��������� �� ����� �����, ���������� ���� �����
                if (grid > 1.0)
                {
                    return _LineColor * _GlowIntensity; // �������� �� ������������� ��������
                }
                
                // ���� ��������� ����� �������, ���������� ���� �����
                return _GridColor * _GlowIntensity; // �������� �� ������������� ��������
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
