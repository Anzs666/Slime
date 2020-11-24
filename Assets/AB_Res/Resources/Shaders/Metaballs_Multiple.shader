// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Metaballs_Lava" {
Properties {    
    _MainTex ("Texture", 2D) = "white" { }    
}
/// <summary>

/// </summary>
SubShader {
	Tags {"Queue" = "Transparent" }
    Pass {
    Blend SrcAlpha OneMinusSrcAlpha     
	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag	
	#include "UnityCG.cginc"	
	float4 _Color;
	sampler2D _MainTex;	
	struct v2f {
	    float4  pos : SV_POSITION;
	    float2  uv : TEXCOORD0;
	};	
	float4 _MainTex_ST;		
	v2f vert (appdata_base v){
	    v2f o;
	    o.pos = UnityObjectToClipPos (v.vertex);
	    o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
	    return o;
	}	
	
	float COLOR_TRESHHOLD=0.2;		
	half4 frag (v2f i) : COLOR{		
		half4 texcol= tex2D (_MainTex, i.uv); 
		half4 finalColor= texcol;			
		if(texcol.r>0.3){
			finalColor=half4(texcol.r,texcol.r/9.5,0.0,texcol.a);
			finalColor=floor(finalColor/0.05)*0.5;     
	    }
	    else if(texcol.g>0.3){ 	
		    finalColor=half4(texcol.g,texcol.g,texcol.g,0.5f);
		     finalColor.a=0.5f;  
	    }	    
	    else if(texcol.b>0.3){	
	    		finalColor=half4(0.0,texcol.b/2.0,texcol.b,0.5);				
				finalColor.b=floor((finalColor.b/0.1)*0.5);
		}	    
	    return finalColor;
	}
	ENDCG

    }
}
Fallback "VertexLit"
} 