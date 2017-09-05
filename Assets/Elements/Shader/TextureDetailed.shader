Shader "Custom/TextureDetailed" {

	Properties {
			_MainTex ("Main Tex", 2D) = "White"{}
			_DetailedTex ("Detailed Tex", 2D) = "White"{}
			_TintColor("Tint Color", Color) = (1,1,1,1)

	}

	SubShader {
		Pass {

			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _DetailedTex;
			float4 _DetailedTex_ST;
			float4 _TintColor;
			struct a2v {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uv_detailed : TEXCOORD1;

			};


			v2f vert (a2v v)
			{
				v2f f;
				f.pos = UnityObjectToClipPos (v.vertex);
				f.uv = TRANSFORM_TEX(v.uv, _MainTex);
				f.uv_detailed = TRANSFORM_TEX(v.uv, _DetailedTex);
				return f;
			}

			float4 frag (v2f f) : SV_TARGET {
				fixed3 color = tex2D (_MainTex, f.uv);
				color *= tex2D(_DetailedTex, f.uv_detailed) * unity_ColorSpaceDouble;
				color *= _TintColor.rgb;
				return float4(color, 1);
			}

			ENDCG

		}
	}
}