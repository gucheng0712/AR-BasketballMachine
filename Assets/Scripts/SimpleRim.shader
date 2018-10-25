// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "MyShader/Holographic"
{
	Properties
	{
        _OccPower("Occlusion Power",Range(0,2)) = 0.5 
        _OccColor("Occlusion Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
      
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
            
            float4 _OccColor;
            float _OccPower;

			struct a2v
			{
				float4 vertex : POSITION;
                float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
                float4 worldNormal : TEXCOORD0;
                float4 worldVertex : TEXCOORD1;
			};
			
			v2f vert (a2v v)
			{
				v2f f;
                f.worldNormal = normalize(mul(v.normal, unity_WorldToObject));
                f.worldVertex = normalize(mul(unity_ObjectToWorld, v.vertex));
				f.pos = UnityObjectToClipPos(v.vertex);
				return f;
			}
			
			fixed4 frag (v2f f) : SV_Target
			{
                float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - f.worldVertex.xyz);
                
                float rim = 1-saturate(dot(f.worldNormal,viewDir));

                fixed3 col = pow(rim, _OccPower) * _OccColor.rgb;

                return fixed4(col,0.5f);
			}
			ENDCG
		}
	}
}
