﻿Shader "Fractal/Mandelbrot/CustomColor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Area ("Area", vector) = (0,0,4,4)
        _Angle("Angle",range(-3.1415,3.1415)) = 0
        _MaxIter("Max Iterations",range(4,1000)) = 255
        _Color("Color", range(0,1)) = 0.5
        _Repeat("Repeat", float) = 1
        _Speed("Speed", float) = 1

        _Color1("Color 1", Color) = (1,1,1,1)
        _Color2("Color 2", Color) = (1,1,1,1)
        _Color3("Color 3", Color) = (1,1,1,1)
        _Color4("Color 4", Color) = (1,1,1,1)
    }

    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float4 _Area;
            float _Angle, _MaxIter, _Color, _Repeat, _Speed;

            float4 _Color1, _Color2, _Color3, _Color4;

            float2 rotation(float2 p,float2 pivot, float angle)
            {
                float s = sin(angle);
                float c = cos(angle);
                p -=pivot;
                p = float2(p.x*c - p.y*s, p.x*s + p.y*c);
                p+=pivot;
                return p;
            }

            float4 colorGradient(float2 pos)
            {
                float t = abs(sin(pos.x));

                if(t >= 0 && t<0.25)
                    return lerp(_Color1,_Color2,t);
                else if(t >= 0.25 && t<0.5)
                    return lerp(_Color2,_Color3,t);
                else if(t >= 0.5 && t<0.75)
                    return lerp(_Color3,_Color4,t);
                else
                    return lerp(_Color4,_Color1,t);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 c = _Area.xy + (i.uv - 0.5) * _Area.zw;
                c= rotation(c,_Area.xy,_Angle);

                float r = 20;            // Escape Radius.
                float r2 = r*r;

                float2 z, zPrevious;
                float iter;

                for(iter = 0; iter < _MaxIter; iter++)
                {
                    zPrevious = z;
                    z = float2(z.x * z.x - z.y * z.y, 2 * z.x * z.y) + c;
                    if(length(z) > r) break;
                }
                if(iter >= _MaxIter) return 0;

                float dist = length(z);
                float fracIter = (dist - r) / (r2 - r);                     // Linear Interpolation
                fracIter = log2(log(dist)/log(r));                          // Double Exponential Interpolation
                iter -= fracIter;
                float m = sqrt(iter / _MaxIter);
                float4 col = sin(float4(.3, .45, .65, 1) * m * 20)*0.5 + 0.5;
                col = colorGradient(float2(m * _Repeat + _Time.y * _Speed, _Color));
                return col;
            }
            ENDCG
        }
    }
}
