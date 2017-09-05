Shader "Custom/Normals" {

	SubShader {

		Pass {

			CGPROGRAM

			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

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
				//f.normal = v.normal;
				f.normal = mul(unity_ObjectToWorld, float4(v.normal, 0));
				f.normal = normalize (f.normal);
				return f;
			}

			float4 frag (v2f f) : SV_TARGET
			{

				return float4 (f.normal * 0.5 + 0.5, 1);
			}

			ENDCG


		}

	}

}