Shader "Unlit/ProgressFill"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_SubTex("SubTex", 2D) = "white" {}
		[HDR]
		_Clr1("Clr1",Color) = (1,1,1,1)
		[HDR]
		_Clr2("Clr2",Color) = (1,1,1,1)
		_T("T",Vector) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "Queue" = "Transparent"  "RenderType"="Transparent" }
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _SubTex;
			fixed3 _Clr1;
			fixed3 _Clr2;
			float4 _T;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed depth = (1-tex2D(_MainTex, i.uv).y)*1;
				float t1 = smoothstep(1,0,(i.uv.x * 10) + (-_T.x-0.06)*10 + depth);
				float t2 = smoothstep(0,1,(i.uv.x * 10) + (_T.y-1)*10 + depth);
				fixed alpha = tex2D(_MainTex, i.uv).z;
				fixed glow = tex2D(_MainTex, i.uv).x;

				fixed sub = 
					(tex2D(_SubTex, i.uv*fixed2(4,1)+fixed2(_Time.x*2,0)).x * 
					tex2D(_SubTex, i.uv*fixed2(3.6, 0.9) + fixed2(_Time.x * -1.8, 0)).x)*glow;
				sub = pow(sub*5,1);
				fixed3 clr = fixed3(1, 1, 1);
				clr *= 
					saturate(pow(t1+sub*0.1, 20))*_Clr1 + 
					saturate(pow(t2+sub*0.1, 20))*_Clr2;
				clr += pow(alpha,5)*0.5;
				return fixed4(clr*0.9, alpha) +sub*pow(max(t1, t2), 10);
			}
			ENDCG
		}
	}
}
