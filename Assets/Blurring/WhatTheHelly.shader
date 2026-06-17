Shader "Hidden/Shader/WhatTheHelly"
{
	Properties
	{
		_KernelSize("KernelSize", int) = 0
		_Spread("Spread", float) = 0
		_BlurStepSize("BlurStepSize", int) = 0
	}
	
	SubShader
	{
		Tags
		{
			"RenderPipeline" = "HDRenderPipeline"
			"RenderType" = "Transparent"
			"Queue" = "Transparent"
		}
		ZWrite Off
		Cull Off
		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			
			struct Attributes
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			
			TEXTURE2D_X(_InputTexture);
			SAMPLER(sampler_InputTexture);
			float4 _InputTexture_TexelSize;
			int _KernelSize;
			int _BlurStepSize;
			float2 _BlurDirection;
			float _Weights[100];
			
			struct Varyings
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
			};
			
			Varyings vert(uint vertexID : SV_VertexID)
			{
				Varyings o;
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				o.vertex = GetFullScreenTriangleVertexPosition(vertexID);
				o.uv = GetFullScreenTriangleTexCoord(vertexID);
				return o;
			}
			
			float4 frag(Varyings i) : SV_Target
			{
				float3 col = 0;
				float kernelSum = 0;

				int upper = (_KernelSize - 1) / 2;
				int lower = -upper;

				float2 texel = _InputTexture_TexelSize.xy;

				for (int inti = lower; inti <= upper; inti++)
				{
					float weight = _Weights[inti + upper];
					kernelSum += weight;

					float2 offset = _BlurDirection * inti * texel;

					float3 sampleCol =
					SAMPLE_TEXTURE2D_X(_InputTexture, sampler_InputTexture, i.uv + offset).xyz;

					col += sampleCol * weight;
				}

				col /= kernelSum;

				return float4(col, 1.0f);
			}
			ENDHLSL
		}
	}	
}