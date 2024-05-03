Shader "Clase2/Prueba"
{
    Properties
    {
        Mycolor("Color", Color) = (1,1,1,1)
        MyEmission("Emission", Color) = (1,1,1,1)
        MyNormal("Normal", Color) = (1,1,1,1)
        MyMult("Multiplication", int) = 2
    }
    SubShader
    { 
        CGPROGRAM
        #pragma surface surf Lambert

        struct Input {
            float2 uvMainTex;
        };
        
        fixed4 Mycolor;
        fixed4 MyEmission;
        fixed4 MyNormal;
        fixed MyMult;

        void surf(Input IN, inout SurfaceOutput o) {
            o.Albedo = Mycolor.rgb * MyMult;
            o.Emission = MyEmission.xyz;
            o.Normal = MyNormal.rgb;
        }
        
        
        ENDCG
    }
    FallBack "Diffuse"
}
