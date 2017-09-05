Shader "Custom/MyDiffuseAlbedo" {

	Properties {
		_TintColor ("Tint Color", Color) = (1,1,1,1)
		_MainTex ("Main Tex", 2D) = "White"{}
	}

	SubShader {

		Pass {
			Tags {
				"LightMode" = "ForwardBase"

			}

			CGPROGRAM
			#include "UnityStandardBRDF.cginc"
			#pragma vertex vert
			#pragma fragment frag
			fixed4 _TintColor;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			struct a2v {

				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD1;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float3 normal : TEXCOORD0;
				float2 uv : TEXCOORD1;

			};

			v2f vert (a2v v) {
				v2f f;
				f.pos = UnityObjectToClipPos (v.vertex);
				f.normal = UnityObjectToWorldNormal(v.normal);
				f.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return f;

			}

			fixed4 frag (v2f f) : SV_Target {

				fixed3 lightdir = normalize(_WorldSpaceLightPos0).xyz;
				fixed3 normal = normalize(f.normal);
				fixed3 color = _TintColor.rgb * _LightColor0.rgb * DotClamped (lightdir, normal) * tex2D(_MainTex, f.uv);
				return fixed4(color, 1);

			}
			ENDCG
			
		}

	}

}