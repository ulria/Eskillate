Shader "Custom/surfaceAlphaShader"
{
    Properties
    {
		_MainTex("Sprite Texture", 2D) = "white" {}
		_FirstColor("First Color", Color) = (1,1,1,1)
		_SecondColor("Second Color", Color) = (1,1,1,1)
		_Opacity ("Opacity", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		fixed4 _FirstColor;
		fixed4 _SecondColor;
		half _Opacity;

        struct Input
        {
            float2 uv_MainTex;
        };

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

		void vert(inout appdata_full v)
		{
			//v2f o;
			//o.vertex = UnityObjectToClipPos(v.vertex);
			//v.uv = TRANSFORM_TEX(v.uv, _MainTex);
			UNITY_TRANSFER_FOG(v, v.vertex);
			//return o;
		}

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = lerp(_FirstColor, _SecondColor, IN.uv_MainTex.y) * tex2D(_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Alpha = _Opacity;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
