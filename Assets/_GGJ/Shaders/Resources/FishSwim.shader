// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/FIshSwim" {
	Properties {
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_GlowMap("GlowMap", 2D) = "Black" {}
		[HDR]
		_GlowColorA("GlowColorA", Color) = (1,1,1,1)
		[HDR]
		_GlowColorB("GlowColorB", Color) = (1,1,1,1)
		_T("T",Float) = 0
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		#pragma vertex myvert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float seed;
		};
			
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		sampler2D _GlowMap;
		fixed4 _GlowColorA;
		fixed4 _GlowColorB;
		float _T;
		void myvert(inout appdata_full v,out Input i)
		{
			i.uv_MainTex = float2(1, 1);

			i.seed = length	(float3(unity_ObjectToWorld[0].x, unity_ObjectToWorld[1].x, unity_ObjectToWorld[2].x))*500;
			v.vertex.x += sin((_Time.x * (50 + sin(i.seed)*20)) + v.vertex.z * 3+i.seed*100)*0.1;
			i.seed = sin(i.seed * 1000)*0.5 + 0.5;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {/*
			float l1 = distance(_Pos1.xyz, IN.worldPos);
			float l2 = distance(_Pos2.xyz, IN.worldPos);
			fixed4 pColor;
			if (l1 < 30 && l1<l2) {
				pColor = _Color1;
			}
			else if (l2 < 30) {
				pColor = _Color2;
			}
			else {
				pColor = _GlowColor;
			}*
			float div = l1 + l2;
			l1 /= div;*/
			float sweep = pow(sin(IN.uv_MainTex.x * 20 + _Time.x*-150)*0.5 + 0.5,5)+0.1;
			fixed4 glow = lerp(_GlowColorA, _GlowColorB, saturate(IN.seed+_T));
			fixed4 g = tex2D(_GlowMap, IN.uv_MainTex)*glow;
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex)*_Color;
			o.Albedo = c.rgb;
			o.Emission = g.xyz*sweep;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
