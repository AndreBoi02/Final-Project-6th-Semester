Shader "Custom/Prueba_2"
{
    Properties
    {
        MyColor("Color", Color) = (1,1,1,1)
        MyRange("Rango",Range(0,10)) = 1
        MyTex("Textura",2D) = "white"{}
        MyCube("Cubo",CUBE) = ""{}
        MyFloat("Float",Float) = 0.5
        MyVector("Vector",Vector) = (1,1,1,1)
    }
        SubShader
        {


            CGPROGRAM
            #pragma surface surf Standard fullforwardshadows

            fixed4 MyColor;
            half MyRange;
            sampler2D MyText;
            samplerCUBE MyCube;
            float MyFloat;
            float4 MyVector

            struct Input {
            float2 uv - MyTex;
            float3 worldRefl;
            };
        
            void surf(Input IN, inout SurfaceOutput o)
            {
                o.Albedo
            }



            ENDCG
        }
    FallBack "Diffuse"
}
