Shader "Custom/MyDiffuse" {

	Properties {
		_TintColor ("Tint Color", Color) = (1,1,1,1)
	}


	SubShader {

		Pass {
			Tags {
				"LightModel" = "ForwardBase"

			}

			CGPROGRAM
			#include "UnityStandardBRDF.cginc"
			#pragma vertex vert
			#pragma fragment frag
			float4 _TintColor;
			struct a2v {

				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float3 normal : TEXCOORD0;

			};

			v2f vert (a2v v) {
				v2f f;
				f.pos = UnityObjectToClipPos (v.vertex);
				f.normal = UnityObjectToWorldNormal(v.normal);
				return f;

			}

			float4 frag (v2f f) : SV_TARGET {

				fixed3 lightdir = normalize(_WorldSpaceLightPos0).xyz;
				fixed3 normal = normalize(f.normal);
				float3 color = _TintColor.rgb * _LightColor0.rgb * DotClamped (lightdir, normal);
				return float4(color, 1);

			}
			ENDCG
			
		}

	}

}