Shader "Custom/MyDiffuseAndSpecular" {

	Properties {

		_MainTex ("Main Tex", 2D) = "White"{}
		_Color("Color", Color) = (1,1,1,1)
		_Smoothness ("Smooth", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0.5

	}

	SubShader {

		Pass {

			Tags {"LightModel" = "ForwardBase"}

			CGPROGRAM

			#include "UnityStandardBRDF.cginc"

			#pragma vertex vert
			#pragma fragment frag
			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float _Smoothness;
			float _Metallic;
			struct a2v {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
			};

			v2f vert (a2v v) {

				v2f f;

				f.pos = UnityObjectToClipPos (v.vertex);

				f.uv = TRANSFORM_TEX(v.uv, _MainTex);

				f.normal = normalize (UnityObjectToWorldNormal(v.normal));

				f.worldPos = mul(unity_ObjectToWorld, v.vertex);

				return f;
			}

			float4 frag (v2f f) : SV_TARGET
			 {

				//diffuse : lightdir * normal
				fixed3 lightdir = _WorldSpaceLightPos0.xyz;
				fixed3 normal = f.normal;
				float3 albedo = tex2D (_MainTex, f.uv).rgb;

				float3 specularTint = albedo * _Metallic;
				albedo *= (1 - _Metallic);

				float3 diffuseColor = _Color.rgb * _LightColor0.rgb * DotClamped(lightdir, normal) * albedo;

				//specular : reflect dir * view dir
				fixed3 reflectdir = reflect (-lightdir, f.normal);

				fixed3 viewDir = normalize(_WorldSpaceCameraPos - f.worldPos);
				fixed3 specularColor = pow(DotClamped (reflectdir, viewDir), _Smoothness * 100) * specularTint;
				return float4(diffuseColor + specularColor, 1);

			}	



			ENDCG	

		}

	}

}