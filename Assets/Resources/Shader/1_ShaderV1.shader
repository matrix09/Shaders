Shader "Custom/1_ShaderV1" {

	

	SubShader {

		Pass {

			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			struct a2v {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 position : SV_POSITION;
				float3 localposition : TEXCOORD0;
			};

			v2f vert (a2v v) {

				v2f f;
				f.position = UnityObjectToClipPos(v.vertex);
				f.localposition = normalize (v.vertex).xyz;
				return f;
				//return position;
			}

			float4 frag(v2f f) : SV_TARGET {

				return float4(f.localposition + 0.5f, 1);

				return 0;
			}

			ENDCG

		}

	}

}