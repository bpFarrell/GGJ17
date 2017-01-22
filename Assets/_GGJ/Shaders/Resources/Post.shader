Shader "Hidden/Post"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			sampler2D_float _CameraDepthTexture;
			half4 _CameraDepthTexture_ST;
			fixed4 Sample(float depth, float2 uv,float x, float y) {
				float size = 0.00005+0.01*depth;
				return max(tex2D(_MainTex, uv + float2(x, y)*size)-1,fixed4(0,0,0,0));
			}
			fixed4 frag (v2f i) : SV_Target
			{
				float v = 1-distance(i.uv,fixed2(0.5,0.5));
				float d = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv));
				fixed4 col = tex2D(_MainTex, i.uv);
			col += Sample(d,i.uv, +0, +1);
			col += Sample(d,i.uv, +1, +0);
			col += Sample(d,i.uv, +0, -1);
			col += Sample(d,i.uv, -1, +0);
			col += Sample(d,i.uv, +1, +1);
			col += Sample(d,i.uv, -1, -1);
			col += Sample(d,i.uv, +1, -1);
			col += Sample(d,i.uv, -1, +1);

			col += Sample(d,i.uv, +0, +2)*0.2;
			col += Sample(d,i.uv, +2, +0)*0.2;
			col += Sample(d,i.uv, +0, -2)*0.2;
			col += Sample(d,i.uv, -2, +0)*0.2;

			float fog = saturate((d * 10) - 0.5);
			fixed4 fogColA = fixed4(0.8, 0.95, 0.95, 1);
			fixed4 fogColB = fixed4(0.2, 0.5, 0.45, 1);


			return col*lerp(fogColA,fogColB,fog)*saturate(v+0.2);
			}
			ENDCG
		}
	}
}
