// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/FIshSwim" {
	Properties {
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_GlowMap("GlowMap", 2D) = "Black" {}
		[HDR]
		_GlowColor("GlowColor", Color) = (1,1,1,1)
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
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _Pos1;
		fixed4 _Pos2;
		fixed4 _Color1;
		fixed4 _Color2;
		sampler2D _GlowMap;
		fixed4 _GlowColor;
		void myvert(inout appdata_full v)
		{
			float f = length	(float3(unity_ObjectToWorld[0].x, unity_ObjectToWorld[1].x, unity_ObjectToWorld[2].x))*500;
			v.vertex.x += sin((_Time.x * (50 + sin(f)*20)) + v.vertex.z * 3+f*100)*0.1;
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
			fixed4 g = tex2D(_GlowMap, IN.uv_MainTex)*_GlowColor;
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex)*_Color;
			o.Albedo = c.rgb;
			o.Emission = g.xyz;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
