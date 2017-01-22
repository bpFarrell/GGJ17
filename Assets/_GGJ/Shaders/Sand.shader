Shader "Custom/Sand" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_Flake("Flake", 2D) = "normal"{}
		_Spots("Spots", 2D) = "white"{}
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
			float3 worldNormal;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		sampler2D _Flake;
		sampler2D _Spots;
		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float3 dir2Cam = normalize(IN.worldPos - _WorldSpaceCameraPos);
			fixed spot = tex2D(_Spots, IN.uv_MainTex*0.5);
			fixed3 normal = normalize((tex2D(_Flake, IN.uv_MainTex*30)*2-1)+IN.worldNormal);
			float3 ref = normalize(reflect(normal,dir2Cam));
			float d = max(pow(dot(ref, normalize(fixed3(1, -1, 1))),2),0)*0.5;
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c+(d*spot)*fixed3(0.9,1,1.5);
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
