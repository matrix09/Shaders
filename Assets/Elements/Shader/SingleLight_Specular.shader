Shader "Custom/MySpecular" {

	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Main Tex", 2D) = "White"{}
		_Smoothness ("Smoothness", Range(0, 1)) = 0.5
	}

	SubShader {

		Pass {

			Tags {"LightModel" = "ForwardBase"}

			CGPROGRAM

			#include "UnityStandardBRDF.cginc"

			#pragma vertex vert
			#pragma fragment frag

			struct a2v {

				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : TEXCOORD1;
				float3 worldPos : TEXCOORD2;

			};

			float4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Smoothness;
			v2f vert (a2v v) {

				v2f f;
				f.pos = UnityObjectToClipPos (v.vertex);

				f.uv = TRANSFORM_TEX (v.uv, _MainTex);
                //法线转化为世界坐标
				f.normal = normalize (UnityObjectToWorldNormal(v.normal));
                //顶点转化为世界坐标
				//f.worldPos = UnityObjectToWorldPos (v.vertex).xyz;
				f.worldPos = mul(unity_ObjectToWorld, v.vertex);
				return f;
			}

			float4 frag (v2f f) : SV_TARGET {

				fixed3 lightdir = normalize(_WorldSpaceLightPos0).xyz;
                //通过入射光的反方向和法线获得反射光方向向量
				fixed3 reflectdir = reflect (-lightdir, f.normal);
                //获得顶点到相机视野点的方向向量
				fixed3 viewDir = normalize(_WorldSpaceCameraPos - f.worldPos);
				//viewdir . reflectdir
                //根据顶点到视野的方向和反射光的方向的到顶点要绘制的颜色
				fixed3 color = pow(DotClamped (reflectdir, viewDir), _Smoothness * 100) * _Color.rgb*_LightColor0.rgb;

				return float4(color, 1);

			}

			ENDCG
		}

	}

}