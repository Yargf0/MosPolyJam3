Shader "Custom/GlowingGridShader"
{
    Properties
    {
        _Color ("Glow Color", Color) = (0, 1, 0, 1) // ���� �������� �� ��������� - �������
        _GridSize ("Grid Size", Float) = 1.0 // ������ �����
        _GlowIntensity ("Glow Intensity", Float) = 1.0 // ������������� ��������
        _LineThickness ("Line Thickness", Float) = 0.05 // ������� �����
        _Speed ("Movement Speed", Float) = 1.0 // �������� �������� �����
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

            fixed4 _Color; // ���� ��������
            float _GridSize; // ������ �����
            float _GlowIntensity; // ������������� ��������
            float _LineThickness; // ������� �����
            float _Speed; // �������� �������� �����

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // ��������� �������� � ����������� UV ������ �� ��� X
                float time = _Time.y * _Speed; // ���������� ����� ��� ��������
                float animatedU = i.uv.x + time; // ������� �� ��� X
                float animatedV = i.uv.y; // ��������� ��� Y ��� ���������

                // ��������� ���������� �����
                float gridX = step(0.5, fmod(animatedU * _GridSize, 1.0)) - step(0.5, fmod(animatedU * _GridSize, 1.0) - _LineThickness);
                float gridY = step(0.5, fmod(animatedV * _GridSize, 1.0)) - step(0.5, fmod(animatedV * _GridSize, 1.0) - _LineThickness);
                
                // ���������, ��������� �� �� ����� �����
                float grid = gridX + gridY;

                // ���������� ���� ��������, ���� ��������� �� ����� �����
                fixed4 glowColor = _Color * _GlowIntensity;
                return grid > 0.0 ? glowColor : fixed4(0, 0, 0, 0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
