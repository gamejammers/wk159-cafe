//
// (c) BLACKTRIANGLES 2020
// http://www.blacktriangles.com
//

Shader "CafeJam/Gradient Sky"
{
    //
    // properties /////////////////////////////////////////////////////////////
    //
    
    Properties
    {
        _SkyColor("Sky Color", Color) = (.5, .5, .5, 1)
        _GroundColor("Ground Color", Color) = (.1, .1, .1, 1)
        _VerticalOffset("Vertical Offset", Range(-1.0, 1.0)) = 0.0
    }

    //
    // ########################################################################
    //
    
    CGINCLUDE

    //
    // includes ///////////////////////////////////////////////////////////////
    //

    #include "Lighting.cginc"
    #include "UnityCG.cginc"

    //
    // variables //////////////////////////////////////////////////////////////
    //
    
    uniform half3 _SkyColor, _GroundColor;
    uniform half _VerticalOffset;

    //
    // types //////////////////////////////////////////////////////////////////
    //
    
    // app -> vert
    struct appdata
    {
        float4 vertex : POSITION;
        float4 texcoord: TEXCOORD0;
    };

    //
    // ------------------------------------------------------------------------
    //
    
    // vert -> frag
    struct v2f
    {
        float4 position: SV_POSITION;
        float4 texcoord: TEXCOORD0;
    };

    //
    // vertex program /////////////////////////////////////////////////////////
    //

    v2f vert(appdata v)
    {
        v2f o;
        o.position = UnityObjectToClipPos(v.vertex);
        o.texcoord = v.texcoord;
        return o;
    }

    //
    // fragment program ///////////////////////////////////////////////////////
    //
    
    half4 frag(v2f i) : COLOR
    {
        half pos = clamp(i.texcoord.y + _VerticalOffset, 0.0, 1.0);

        half3 col = lerp(_GroundColor, _SkyColor, pos);

        return half4(col, 1.0);
    }

    ENDCG

    //
    // ########################################################################
    //
    
    SubShader
    {
        Tags 
        { 
            "RenderType" = "Skybox"
            "Queue" = "Background"
        }

        Pass
        {
            ZWrite Off
            Cull Off
            Fog { Mode Off }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
}
