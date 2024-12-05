Shader "Custom/GlowingGridShader"
{
    Properties
    {
        _Color ("Glow Color", Color) = (0, 1, 0, 1) // Цвет свечения по умолчанию - зеленый
        _GridSize ("Grid Size", Float) = 1.0 // Размер сетки
        _GlowIntensity ("Glow Intensity", Float) = 1.0 // Интенсивность свечения
        _LineThickness ("Line Thickness", Float) = 0.05 // Толщина линий
        _Speed ("Movement Speed", Float) = 1.0 // Скорость движения сетки
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

            fixed4 _Color; // Цвет свечения
            float _GridSize; // Размер сетки
            float _GlowIntensity; // Интенсивность свечения
            float _LineThickness; // Толщина линий
            float _Speed; // Скорость движения сетки

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Добавляем анимацию к координатам UV только по оси X
                float time = _Time.y * _Speed; // Используем время для анимации
                float animatedU = i.uv.x + time; // Двигаем по оси X
                float animatedV = i.uv.y; // Оставляем ось Y без изменений

                // Вычисляем координаты сетки
                float gridX = step(0.5, fmod(animatedU * _GridSize, 1.0)) - step(0.5, fmod(animatedU * _GridSize, 1.0) - _LineThickness);
                float gridY = step(0.5, fmod(animatedV * _GridSize, 1.0)) - step(0.5, fmod(animatedV * _GridSize, 1.0) - _LineThickness);
                
                // Проверяем, находимся ли на линии сетки
                float grid = gridX + gridY;

                // Возвращаем цвет свечения, если находимся на линии сетки
                fixed4 glowColor = _Color * _GlowIntensity;
                return grid > 0.0 ? glowColor : fixed4(0, 0, 0, 0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
