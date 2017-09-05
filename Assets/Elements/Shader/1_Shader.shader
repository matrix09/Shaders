// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/1_Shader" {

	Properties{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Main Tex", 2D) = "White"{}

	}

	SubShader {


		Pass {


			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag

			float4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct a2v {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0; 
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0; 
			};

			v2f vert (a2v v ) {

				v2f f;
				f.pos = UnityObjectToClipPos (v.vertex);
				f.uv = _MainTex_ST.xy * v.uv;
				f.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return f;

			}

			float4 frag (v2f f) : SV_TARGET {

				float4 color = tex2D(_MainTex, f.uv) * _Color;

				return color;
			}

			ENDCG

		}


	}


}