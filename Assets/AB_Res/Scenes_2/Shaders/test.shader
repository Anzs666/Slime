//Shader开始
Shader "Commom/TestShader01"
{
	Properties{
		_Color("sad",color)=(1,1,1,1)
	}
	SubShader{
		  Tags { "RenderType" = "Opaque" }
		  CGPROGRAM
		  #pragma surface suft Lambert
		  struct Input {
			  float4 color : COLOR;
		  };
		  void suft(Input IN, inout SurfaceOutput o) {
			  o.Albedo = 1;			  
		  }
		  ENDCG
	}
	//Fallback "TestShader01"
}
