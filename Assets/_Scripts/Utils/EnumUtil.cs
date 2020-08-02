//
// (c) BLACKTRIANGLES 2015
// http://www.blacktriangles.com
//

using UnityEngine;
using System.Collections.Generic;

namespace Cafe 
{
    public static class EnumUtil
    {

        //
        // --------------------------------------------------------------------
        //

        public static bool Parse<EnumType>(string str, out EnumType t)
            where EnumType: System.Enum
        {
            bool result = false;
            t = default(EnumType);
            try
            {
                t = (EnumType)System.Enum.Parse(typeof(EnumType), str);
            }
            catch(System.Exception ex)
            {
                Dbg.Warn(ex.ToString());
            }

            return result;
        }
        
        //
        // --------------------------------------------------------------------
        //
        
        public static EnumType[] GetValues<EnumType>()
            where EnumType: System.Enum
        {
            return (EnumType[])System.Enum.GetValues(typeof(EnumType));
        }

        //
        // --------------------------------------------------------------------
        //

        public static string[] GetNames<EnumType>()
        {
            return System.Enum.GetNames(typeof(EnumType));
        }
        

        //
        // --------------------------------------------------------------------
        //
        
        public static EnumType GetFirst<EnumType>()
            where EnumType: System.Enum
        {
            return default(EnumType);
        }

        //
        // --------------------------------------------------------------------
        //

        public static int Count<EnumType>()
            where EnumType: System.Enum
        {
            return System.Enum.GetValues( typeof( EnumType ) ).Length;
        }

        //
        // --------------------------------------------------------------------
        //

        public static EnumType Random<EnumType>()
            where EnumType: System.Enum
        {
            int count = Count<EnumType>();
            return Convert<EnumType>(btRandom.Range(0, count));
        }
        
        //
        // --------------------------------------------------------------------
        //

        public static EnumType Random<EnumType>(System.Random rnd)
            where EnumType: System.Enum
        {
            int count = Count<EnumType>();
            return Convert<EnumType>(rnd.Range(0, count));
        }

        //
        // --------------------------------------------------------------------
        //

        public static EnumType Convert<EnumType>(string str, bool caseInsensitive = false)
            where EnumType: System.Enum
        {
            return (EnumType)System.Enum.Parse( typeof(EnumType), str, caseInsensitive );
        }

        //
        // --------------------------------------------------------------------
        //

        public static EnumType Convert<EnumType>(object obj)
            where EnumType: System.Enum

        {
            return (EnumType)System.Enum.ToObject( typeof(EnumType), obj );
        }

        //
        // --------------------------------------------------------------------
        //

        public static T ChangeType<EnumType, T>(EnumType e)
        {
            return (T)System.Convert.ChangeType(e, typeof(T));
        }

        //
        // ------------------------------------------------------------------------
        //

        public static void ForEachValue<EnumType>(System.Action<EnumType> action)
            where EnumType: System.Enum
        {
            var enums = GetValues<EnumType>();
            foreach(EnumType e in enums)
            {
                action(e);
            }
        }
        
    }
}
