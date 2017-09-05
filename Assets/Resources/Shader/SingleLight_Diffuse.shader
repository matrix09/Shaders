Shader "Custom/MyDiffuse" {

	Properties {
		_TintColor ("Tint Color", Color) = (1,1,1,1)
	}


	SubShader {

		Pass {
			Tags {
				"LightMode" = "ForwardBase"

			}

			CGPROGRAM
			//#include "UnityStandardBRDF.cginc"
			#include "Lighting.cginc"
			#pragma vertex vert
			#pragma fragment frag
			fixed4 _TintColor;
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
				//f.normal = UnityObjectToWorldNormal(v.normal);
				f.normal = mul(v.normal, (float3x3)unity_WorldToObject);
				return f;

			}

			fixed4 frag (v2f f) : SV_Target {

				fixed3 lightdir = normalize(_WorldSpaceLightPos0.xyz);
				fixed3 normal = normalize(f.normal);
				//float3 color = _TintColor.rgb * _LightColor0.rgb * DotClamped (lightdir, normal);
				fixed3 color = _TintColor.rgb * _LightColor0.rgb * saturate (dot(normal, lightdir));
				return fixed4(color, 1);

			}
			ENDCG
			
		}

	}
	FallBack "Diffuse"

}