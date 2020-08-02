//
// (c) BLACKTRIANGLES 2015
// http://www.blacktriangles.com
//

using System.Collections.Generic;
using UnityEngine;

namespace Cafe
{
    public static class ArrayExtension
    {
        //
        // --------------------------------------------------------------------
        //

        public static bool Contains( this System.Array self, object item )
        {
            return self.IndexOf( item ) >= 0;
        }

        //
        // --------------------------------------------------------------------
        //

        public static bool IsValidIndex( this System.Array self, int index )
        {
            return ( index >= 0 && index < self.Length );
        }

        //
        // --------------------------------------------------------------------
        //

        public static bool IsValidIndex( this System.Array self, Vector2Int index )
        {
            return ( index.x >= 0 && index.x < self.GetLength(0) &&
                     index.y >= 0 && index.y < self.GetLength(1) );
        }
        

        //
        // --------------------------------------------------------------------
        //

        public static int IndexOf( this System.Array self, object item )
        {
            return System.Array.IndexOf( self, item );
        }

        //
        // --------------------------------------------------------------------
        //

        public static int IndexOfFirstNull( this System.Array self )
        {
            for( int i = 0; i < self.Length; ++i )
            {
                if( self.GetValue(i) == null ) return i;
            }

            return -1;
        }

        //
        // --------------------------------------------------------------------
        //

        public static void Shuffle( this System.Array self )
        {
            System.Random rnd = new System.Random();
            Shuffle( self, rnd );
        }
        
        //
        // --------------------------------------------------------------------
        //

        public static void Shuffle( this System.Array self, System.Random rnd )
        {
            for (int i = self.Length; i > 1; i--)
            {
                // Pick random element to swap.
                int j = rnd.Next(0,i); // 0 <= j <= i-1
                // Swap.
                object tmp = self.GetValue(j);
                self.SetValue( self.GetValue( i - 1 ), j );
                self.SetValue( tmp, i - 1 );
            }
        }

        //
        // --------------------------------------------------------------------
        //

        public static int RandomIndex<T>( this T[] self )
        {
            Dbg.Assert(self != null && self.Length >= 0, "Cannot get random index for empty array");

            System.Random rnd = new System.Random();
            return rnd.Next(self.Length);
        }
        

        //
        // --------------------------------------------------------------------
        //

        public static T Random<T>( this T[] self )
        {
            return self[ RandomIndex(self) ];
        }

        //
        // --------------------------------------------------------------------
        //

        public static T Random<T>( this T[] self, System.Random rng )
        {
            if( self.Length <= 0 ) return default(T);
            return self[ rng.Next( self.Length ) ];
        }

        //
        // --------------------------------------------------------------------
        //

        public static void DebugDump( this byte[] self )
        {
            System.Array.ForEach( self, (b)=>{ Dbg.Log( b.ToString() ); } );
        }

        //
        // --------------------------------------------------------------------
        //

        public static string ToDebugString<T>( this T[] self )
        {
          System.String res = "Array["+self.Length.ToString()+"]";
          System.Array.ForEach( self, (b)=>{ res += b.ToString() + "\n"; } );
          return res;
        }

        //
        // --------------------------------------------------------------------
        //

        public static T[] Slice<T>(this T[] source, int start, int end = -1)
        {
            // Handles negative ends.
            if (end < 0)
            {
              end = source.Length;
            }
            int len = end - start;

            // Return new array.
            T[] res = new T[len];
            for (int i = 0; i < len; i++)
            {
              res[i] = source[i + start];
            }
            return res;
        }

        //
        // --------------------------------------------------------------------
        //

        public static T[] ConvertAll<T,U>( this U[] self )
          where T: U
        {
            return System.Array.ConvertAll<U,T>( self, (i)=>{ return (T)i; } );
        }

        //
        // --------------------------------------------------------------------
        //

        public static string[] ConvertAll<U>( this U[] self)
            where U: class
        {
            return System.Array.ConvertAll<U, string>(self, (i)=>{ return i.ToString(); });
        }

        //
        // --------------------------------------------------------------------
        //

        public static T[] ShallowClone<T>( this T[] self )
        {
            T[] result = new T[ self.Length ];
            System.Array.Copy( self, result, self.Length );
            return result;
        }

        //
        // --------------------------------------------------------------------
        //
        

        public static List<T> Map<T,U>( this U[] self, System.Func<U, T> mapper )
        {
            List<T> result = new List<T>();
            foreach(U item in self)
            {
                result.Add(mapper(item));
            }
            return result;
        }

        //
        // ------------------------------------------------------------------------
        //
        
        public static List<T> Filter<T>( this T[] self, System.Func<T,bool> filter)
        {
            List<T> result = new List<T>();
            foreach(T t in self)
            {
                if(filter(t))
                    result.Add(t);
            }

            return result;
        }

        //
        // --------------------------------------------------------------------
        //
        
        public static void Fill<T>( this T[] self, T val )
        {
            for(int i = 0; i < self.Length; ++i)
            {
                self[i] = val;
            }
        }
    }
}
