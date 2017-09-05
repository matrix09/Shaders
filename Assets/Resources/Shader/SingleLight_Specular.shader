Shader "Custom/MySpecular" {

	Properties {
		_TintColor ("Tint Color", Color) = (1,1,1,1)
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

			float4 _TintColor;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Smoothness;
			v2f vert (a2v v) {

				v2f f;
				f.pos = UnityObjectToClipPos (v.vertex);

				f.uv = TRANSFORM_TEX (v.uv, _MainTex);

				f.normal = normalize (UnityObjectToWorldNormal(v.normal));

				//f.worldPos = UnityObjectToWorldPos (v.vertex).xyz;
				f.worldPos = mul(unity_ObjectToWorld, v.vertex);
				return f;
			}

			float4 frag (v2f f) : SV_TARGET {

				fixed3 lightdir = normalize(_WorldSpaceLightPos0).xyz;

				fixed3 reflectdir = reflect (-lightdir, f.normal);

				fixed3 viewDir = normalize(_WorldSpaceCameraPos - f.worldPos);
				//viewdir . reflectdir

				fixed3 color = pow(DotClamped (reflectdir, viewDir), _Smoothness * 100) * _TintColor.rgb;

				return float4(color, 1);

			}

			ENDCG
		}

	}

}