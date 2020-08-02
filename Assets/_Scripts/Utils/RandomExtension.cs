//=============================================================================
//
// (C) BLACKTRIANGLES 2015
// http://www.blacktriangles.com
// contact@blacktriangles.com
//
// Howard N Smith | hsmith | howard@blacktriangles.com
//
//=============================================================================

public static class RandomExtension
{
    //
    // ------------------------------------------------------------------------
    //
    
    public static double NextDouble( this System.Random self, double min, double max )
    {
        double rng = self.NextDouble();
        return min + rng * (max-min);
    }

    //
    // ------------------------------------------------------------------------
    //
    

    public static float NextFloat( this System.Random self )
    {
        return (float)(self.NextDouble() % System.Single.MaxValue);
    }

    //
    // ------------------------------------------------------------------------
    //
    

    public static float NextFloat( this System.Random self, float min, float max )
    {
        float rng = self.NextFloat();
        return min + rng * (max-min);
    }

    //
    // ------------------------------------------------------------------------
    //
    
    public static int Range( this System.Random self, int min, int max)
    {
        return self.Next(min, max);
    }

    //
    // ------------------------------------------------------------------------
    //
    
    public static float Range( this System.Random self, float min, float max)
    {
        return NextFloat(self, min, max);
    }

    //
    // ------------------------------------------------------------------------
    //
    
    public static double Range( this System.Random self, double min, double max)
    {
        return NextDouble(self, min, max);
    }
}
