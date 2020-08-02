//
// (c) BLACKTRIANGLES 2020
// http://www.blacktriangles.com
//

Shader "blacktriangles/FlatColorMap"
{
    //
    // properties /////////////////////////////////////////////////////////////
    //
    
    Properties
    {
        _MainTex("Atlas", 2D) = "white" {}
        _EmissTex("Emission", 2D) = "black" {}

        _OutlineColor("_OutlineColor", Color) = (1,0,0,1)
        _OutlineWidth("_OutlineWidth", float) = 1

        _Color0("Color0", Color) = (0,0,0,1)
        _Color1("Color1", Color) = (1,0,0,1)
        _Color2("Color2", Color) = (0,1,0,1)
        _Color3("Color3", Color) = (0,0,1,1)
        _Color4("Color4", Color) = (1,1,1,1)
    }

    //
    // ########################################################################
    //
    
    CGINCLUDE
    
    //
    // includes ///////////////////////////////////////////////////////////////
    //

    #include "UnityCG.cginc"

    #pragma multi_compile_instancing

    //
    // variables //////////////////////////////////////////////////////////////
    //
    
    sampler2D _MainTex;
    sampler2D _EmissTex;

    UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _OutlineColor)
        UNITY_DEFINE_INSTANCED_PROP(float, _OutlineWidth)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color0)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color1)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color2)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color3)
        UNITY_DEFINE_INSTANCED_PROP(fixed4, _Color4)
    UNITY_INSTANCING_BUFFER_END(Props)

    //
    // types //////////////////////////////////////////////////////////////////
    //
    
    struct Input 
    {
        half2 uv_MainTex;
	};

    ENDCG

    //
    // unity subshader ########################################################
    //
    
    SubShader
    {
        //
        // Opaque Pass ////////////////////////////////////////////////////////
        //
        
        Tags 
        { 
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
        }

        Cull Back
        ZTest LEqual
        ZWrite On

        CGPROGRAM
        #pragma surface surf Lambert 
        #pragma target 3.0
        #pragma exclude_renderers flash

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed3 color = tex2D(_MainTex, IN.uv_MainTex);
            fixed3 emiss = tex2D(_EmissTex, IN.uv_MainTex);

            // color0 == black
            if(color.r <= 0 && color.g <= 0 && color.b <= 0)
            {
                color = UNITY_ACCESS_INSTANCED_PROP(Props, _Color0);
            }

            // color1 == red
            else if(color.r >= 0.99 && color.g <= 0 && color.b <= 0)
            {
                color = UNITY_ACCESS_INSTANCED_PROP(Props, _Color1);
            }

            // color2 == green
            else if(color.r <= 0 && color.g > 0.99 && color.b <= 0)
            {
                color = UNITY_ACCESS_INSTANCED_PROP(Props, _Color2);
            }

            // color3 == blue
            else if(color.r <= 0 && color.g <= 0 && color.b > 0.99)
            {
                color = UNITY_ACCESS_INSTANCED_PROP(Props, _Color3);
            }

            // color4 == white
            if(color.r >= 0.99 && color.g >= 0.99 && color.b >= 0.99)
            {
                color = UNITY_ACCESS_INSTANCED_PROP(Props, _Color4);
            }

            o.Albedo = color;
            //o.Emissive = color * emiss;
        }
        ENDCG

        //
        // outlne pass ////////////////////////////////////////////////////////
        //

        Pass {
            Tags 
            {
                "RenderType" = "Opaque"
                "Queue" = "Transparent"
            }
            
            Cull Front

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            //
            // -----------------------------------------------------------------
            //

            struct appdata
            {
                float4 pos: POSITION;
                float3 normal: NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            //
            // ----------------------------------------------------------------
            //

            struct v2f
            {
                float4 pos: SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            

            //
            // ----------------------------------------------------------------
            //
            
            v2f vert(appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);

                float4 clipPos = UnityObjectToClipPos(v.pos);
                float3 clipNormal = mul((float3x3)UNITY_MATRIX_VP, mul((float3x3)UNITY_MATRIX_M, v.normal));

                float width = UNITY_ACCESS_INSTANCED_PROP(Props, _OutlineWidth);
                float2 offset = normalize(clipNormal.xy) / _ScreenParams.xy * width * clipPos.w * 2;
                clipPos.xy += offset;
                o.pos = clipPos;

                return o;
            }

            //
            // ----------------------------------------------------------------
            //
            

            void frag(v2f i, out half4 outEmission : SV_Target3)
            {
                UNITY_SETUP_INSTANCE_ID(i);
                half3 color = UNITY_ACCESS_INSTANCED_PROP(Props, _OutlineColor);
                outEmission = half4(color.rgb, 1.0);
            }

            ENDCG
        }
    }
    Fallback "Diffuse"
}
