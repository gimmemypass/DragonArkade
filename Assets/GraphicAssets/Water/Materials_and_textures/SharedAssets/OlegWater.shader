// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "OlegWater"
{
	Properties
	{
		_Float0("Float 0", Range( 0 , 1)) = 0.5
		_Float2("Float 2", Float) = 0
		_TextureSample1("Texture Sample 1", 2D) = "bump" {}
		_Cubemap("Cubemap", CUBE) = "white" {}
		_FresnelPower("FresnelPower", Range( 0 , 5)) = 0
		_FresnelScale("FresnelScale", Range( 0 , 5)) = 0
		_FresnelBias("FresnelBias", Range( 0 , 5)) = 0
		_Transparency_Mask("Transparency_Mask", 2D) = "white" {}
		_WaveSpeed("WaveSpeed", Vector) = (0,0,0,0)
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		_Color0("Color 0", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nometa noforwardadd 
		struct Input
		{
			float3 worldRefl;
			INTERNAL_DATA
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float4 _Color0;
		uniform float _Float0;
		uniform samplerCUBE _Cubemap;
		uniform float _Float2;
		uniform float2 _Vector0;
		uniform sampler2D _TextureSample1;
		uniform float2 _WaveSpeed;
		uniform float _FresnelBias;
		uniform float _FresnelScale;
		uniform float _FresnelPower;
		uniform sampler2D _Transparency_Mask;
		uniform float4 _Transparency_Mask_ST;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Normal = float3(0,0,1);
			float2 uv_TexCoord20 = i.uv_texcoord * _Vector0;
			float2 panner22 = ( 1.0 * _Time.y * float2( -0.03,0 ) + uv_TexCoord20);
			float2 panner21 = ( 1.0 * _Time.y * _WaveSpeed + uv_TexCoord20);
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float fresnelNdotV15 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode15 = ( _FresnelBias + _FresnelScale * pow( 1.0 - fresnelNdotV15, _FresnelPower ) );
			o.Emission = ( _Color0 + ( ( 1.0 - _Float0 ) * texCUBE( _Cubemap, WorldReflectionVector( i , BlendNormals( UnpackScaleNormal( tex2D( _TextureSample1, panner22 ) ,_Float2 ) , UnpackScaleNormal( tex2D( _TextureSample1, panner21 ) ,_Float2 ) ) ) ) * fresnelNode15 ) ).rgb;
			float2 uv_Transparency_Mask = i.uv_texcoord * _Transparency_Mask_ST.xy + _Transparency_Mask_ST.zw;
			o.Alpha = tex2D( _Transparency_Mask, uv_Transparency_Mask ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15401
7;1;1906;1130;1133.594;454.8946;1.519494;True;True
Node;AmplifyShaderEditor.CommentaryNode;19;-2915.565,-293.9613;Float;False;1709.198;474.6249;Blend panning normals to fake ripples;9;26;4;25;21;23;22;20;56;57;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;57;-2863.483,-86.69989;Float;False;Property;_Vector0;Vector 0;9;0;Create;True;0;0;False;0;0,0;60,30;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-2566.544,-209.6624;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;44,44;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;56;-2662.483,69.30011;Float;False;Property;_WaveSpeed;WaveSpeed;8;0;Create;True;0;0;False;0;0,0;0.015,0.035;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;22;-2269.845,-224.9613;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.03,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;21;-2267.545,-104.4622;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.04,0.04;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-2259.946,45.13727;Float;False;Property;_Float2;Float 2;1;0;Create;True;0;0;False;0;0;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-1924.926,-40.39712;Float;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;False;0;None;3dd4c7c308baa1943bbb1abdb9169e83;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;25;-1929.841,-235.6624;Float;True;Property;_TextureSample5;Texture Sample 5;2;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Instance;4;Auto;Texture2D;6;0;SAMPLER2D;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BlendNormalsNode;26;-1489.938,-91.16194;Float;True;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-1081.459,472.5049;Float;False;Property;_FresnelBias;FresnelBias;6;0;Create;True;0;0;False;0;0;0.26;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1081.459,556.5049;Float;False;Property;_FresnelScale;FresnelScale;5;0;Create;True;0;0;False;0;0;1.13;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;16;-1103.459,631.5049;Float;False;Property;_FresnelPower;FresnelPower;4;0;Create;True;0;0;False;0;0;4.81;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-913.9019,64.16586;Float;False;Property;_Float0;Float 0;0;0;Create;True;0;0;False;0;0.5;0.081;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldReflectionVector;6;-1016.433,283.7889;Float;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;9;-760.4332,283.7889;Float;True;Property;_Cubemap;Cubemap;3;0;Create;True;0;0;False;0;None;9b5ed8bf758dd0643bc5520d644fe3c7;True;0;False;white;Auto;False;Object;-1;Auto;Cube;6;0;SAMPLER2D;0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;1;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;15;-723.459,489.5049;Float;False;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;7;-552.4332,171.7889;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-360.4333,171.7889;Float;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;60;-221.1376,-122.1259;Float;False;Property;_Color0;Color 0;10;0;Create;True;0;0;False;0;0,0,0,0;0.06928624,0.08392947,0.08490568,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;51;56.77179,527.9171;Float;True;Property;_Transparency_Mask;Transparency_Mask;7;0;Create;True;0;0;False;0;None;a22864fe5c5e3c045be9c055574e9817;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;59;25.02049,66.29132;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;3;572.7733,208.2812;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;OlegWater;False;False;False;False;True;True;True;True;True;False;True;True;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;True;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;20;0;57;0
WireConnection;22;0;20;0
WireConnection;21;0;20;0
WireConnection;21;2;56;0
WireConnection;4;1;21;0
WireConnection;4;5;23;0
WireConnection;25;1;22;0
WireConnection;25;5;23;0
WireConnection;26;0;25;0
WireConnection;26;1;4;0
WireConnection;6;0;26;0
WireConnection;9;1;6;0
WireConnection;15;1;18;0
WireConnection;15;2;17;0
WireConnection;15;3;16;0
WireConnection;7;0;5;0
WireConnection;11;0;7;0
WireConnection;11;1;9;0
WireConnection;11;2;15;0
WireConnection;59;0;60;0
WireConnection;59;1;11;0
WireConnection;3;2;59;0
WireConnection;3;9;51;1
ASEEND*/
//CHKSM=5AF69E446313F816B048F7A5F8A3CBDCD13D1DA0