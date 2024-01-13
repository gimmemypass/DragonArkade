// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Oleg/WaterFoamAnim"
{
	Properties
	{
		_MainTex("_MainTex", 2D) = "white" {}
		_p("p", 2D) = "white" {}
		_Float0("Float 0", Range( 0 , 2)) = 1.722749
		_FoamSpeed("FoamSpeed", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Overlay+0" "IsEmissive" = "true"  }
		Cull Back
		Blend One One
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow exclude_path:deferred noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform sampler2D _p;
		uniform float _FoamSpeed;
		uniform float4 _p_ST;
		uniform float _Float0;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 tex2DNode1 = tex2D( _MainTex, uv_MainTex );
			float2 appendResult29 = (float2(-( _FoamSpeed * 2.0 ) , 0.05));
			float2 uv_p = i.uv_texcoord * _p_ST.xy + _p_ST.zw;
			float2 panner3 = ( 1.0 * _Time.y * appendResult29 + uv_p);
			float2 appendResult30 = (float2(-_FoamSpeed , -0.05));
			float2 panner11 = ( 1.0 * _Time.y * appendResult30 + ( uv_p * 1.5 ));
			float4 temp_cast_0 = (_Float0).xxxx;
			o.Emission = pow( ( ( tex2DNode1.g * ( tex2D( _p, panner3 ) * tex2D( _p, panner11 ) ) ) + ( tex2DNode1.g * 0.07 ) ) , temp_cast_0 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
1599;7;1279;1124;1935.931;420.7237;1.647468;True;False
Node;AmplifyShaderEditor.RangedFloatNode;28;-1770.43,764.4398;Float;False;Property;_FoamSpeed;FoamSpeed;4;0;Create;True;0;0;False;0;0;0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-1614.43,696.4398;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;16;-1779.226,125.3043;Float;True;Property;_p;p;1;0;Create;True;0;0;False;0;None;324bbbfd9ac54bc4aa92cc79e0c160b2;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1395.4,589.4607;Float;False;Constant;_Float1;Float 1;3;0;Create;True;0;0;False;0;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;33;-1491.43,694.4398;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;34;-1496.43,771.4398;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-1486.599,462.6593;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-1213.399,468.76;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;17;-1486.696,344.5215;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;30;-1351.196,764.0571;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;-0.05;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;29;-1356.43,670.4398;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0.05;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;3;-1060,355.704;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,0.03;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;11;-1060.159,466.5032;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.5,-0.03;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;10;-812.8708,470.9692;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Instance;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-810.2488,269.7418;Float;True;Property;_FoamTex;_FoamTex;1;0;Create;True;0;0;False;0;None;095cb6b629c4e2a42a8ac2652ba498ad;True;0;False;white;Auto;False;Instance;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-408.2605,398.6488;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-762,-14;Float;True;Property;_MainTex;_MainTex;0;0;Create;True;0;0;False;0;None;a22864fe5c5e3c045be9c055574e9817;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-191.1398,193.3043;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-320.4684,55.83486;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.07;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-340.986,669.8464;Float;False;Property;_Float0;Float 0;2;0;Create;True;0;0;False;0;1.722749;1.7;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;35;-87.30128,28.17092;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;18;-22.1861,203.9467;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;38;183,85;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Oleg/WaterFoamAnim;False;False;False;False;True;True;True;True;True;True;True;True;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Opaque;;Overlay;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;3;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;32;0;28;0
WireConnection;33;0;32;0
WireConnection;34;0;28;0
WireConnection;20;2;16;0
WireConnection;23;0;20;0
WireConnection;23;1;24;0
WireConnection;17;2;16;0
WireConnection;30;0;34;0
WireConnection;29;0;33;0
WireConnection;3;0;17;0
WireConnection;3;2;29;0
WireConnection;11;0;23;0
WireConnection;11;2;30;0
WireConnection;10;0;16;0
WireConnection;10;1;11;0
WireConnection;2;0;16;0
WireConnection;2;1;3;0
WireConnection;12;0;2;0
WireConnection;12;1;10;0
WireConnection;9;0;1;2
WireConnection;9;1;12;0
WireConnection;36;0;1;2
WireConnection;35;0;9;0
WireConnection;35;1;36;0
WireConnection;18;0;35;0
WireConnection;18;1;19;0
WireConnection;38;2;18;0
ASEEND*/
//CHKSM=8FA9DB1A855B39E264840663C093248A068F718F