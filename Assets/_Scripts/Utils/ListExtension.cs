//=============================================================================
//
// (C) BLACKTRIANGLES 2015
// http://www.blacktriangles.com
// contact@blacktriangles.com
//
// Howard N Smith | hsmith | howard@blacktriangles.com
//
//=============================================================================

using System.Collections.Generic;

namespace Cafe
{
    public static class ListExtension
    {

        //
        // --------------------------------------------------------------------
        //
        
        public static List<U> Map<T,U>( this List<T> self, System.Func<T, U> action)
        {
            List<U> result = new List<U>();
            foreach(T item in self)
            {
                result.Add(action(item));
            }
            return result;
        }

        //
        // --------------------------------------------------------------------
        //

        public static T Pop<T>( this List<T> self )
        {
            T result = self[0];
            self.RemoveAt( 0 );
            return result;
        }

        //
        // --------------------------------------------------------------------
        //

        public static void Push<T>( this List<T> self, T item )
        {
            self.Insert( 0, item );
        }

        //
        // --------------------------------------------------------------------
        //

        public static void SwapItems<T>( this List<T> self, int from, int to )
        {
            if( self.IsValidIndex(from) && self.IsValidIndex(to) )
            {
                T oldItem = self[to];
                self[to] = self[from];
                self[from] = oldItem;
            }
        }

        //
        // --------------------------------------------------------------------
        //

        public static bool IsValidIndex<T>( this List<T> self, int index )
        {
            return ( self != null && self.Count > 0 && index >= 0 && index < self.Count );
        }

        //
        // --------------------------------------------------------------------
        //

        public static void SetMinimumLength<T>( this List<T> self, int minLength )
        {
            if( self == null ) return;
            else if( minLength <= 0 ) return;
            else if( self.Count < minLength )
            {
                int addCount = minLength - self.Count;
                self.AddRange( new T[addCount] );
            }
        }

        //
        // --------------------------------------------------------------------
        //

        public static void SetMaximumLength<T>( this List<T> self, int maxLength )
        {
            if( self == null ) return;
            else if( maxLength <= 0 ) self.Clear();
            else if( self.Count > maxLength )
            {
                int removeCount = self.Count - maxLength;
                self.RemoveRange( maxLength, removeCount );
            }
        }

        //
        // --------------------------------------------------------------------
        //
        
        public static void SetLength<T>( this List<T> self, int length )
        {
            self.SetMinimumLength( length );
            self.SetMaximumLength( length );
        }

        //
        // --------------------------------------------------------------------
        //

        public static void SetLength<T>( this List<T> self, int length, System.Func<int,T> createItem, System.Action<T,int> destroyItem )
        {
            self.SetMinimumLength( length, createItem );
            self.SetMaximumLength( length, destroyItem );
        }

        //
        // --------------------------------------------------------------------
        //

        public static List<T> Slice<T>( this List<T> self, int min, int max )
        {
            return new List<T>( self.ToArray().Slice( min, max ) );
        }

        //
        // --------------------------------------------------------------------
        //

        public static void SetMinimumLength<T>( this List<T> self, int minLength, System.Func<int,T> createItem )
        {
            if( self == null ) return;
            else if( minLength <= 0 ) return;

            int start = self.Count;
            self.SetMinimumLength( minLength );

            for( int i = start; i < minLength; ++i )
            {
                self[i] = createItem( i );
            }
        }

        //
        // --------------------------------------------------------------------
        //

        public static void SetMaximumLength<T>( this List<T> self, int maxLength, System.Action<T,int> destroyItem )
        {
            if( self == null ) return;
            else if( self.Count <= maxLength ) return;

            int start = System.Math.Max( 0, maxLength );
            for( int i = start; i < self.Count; ++i )
            {
                destroyItem( self[i], i );
            }

            self.SetMaximumLength( maxLength );
        }

        //
        // --------------------------------------------------------------------
        //

        public static T Random<T>( this List<T> self, System.Random rnd )
        {
            if( self.Count <= 0 ) return default(T);
            return self[ rnd.Next( self.Count )];
        }

        //
        // --------------------------------------------------------------------
        //
        
        public static void Set<T>(this List<T> self, int idx, T item)
        {
            self.SetMinimumLength(idx);
            self[idx] = item;
        }

        //
        // --------------------------------------------------------------------
        //

        public static int SortedInsert<T>( this List<T> self, T item, System.Func<T,T,bool> compare )
        {
            if( self.Count > 0 )
            {
                for( int i = 0; i < self.Count; ++i )
                {
                    T other = self[i];
                    if( compare( item, other ) )
                    {
                        self.Insert( i, item );
                        return i;
                    }
                }
            }

            self.Add( item );
            return self.Count - 1;
        }

        //
        // --------------------------------------------------------------------
        //

        public static bool UniqueInsert<T>( this List<T> self, T item)
        {
            if(self.Count > 0)
            {
                for(int i = 0; i < self.Count; ++i)
                {
                    if(self[i].Equals(item))
                        return false;
                }
            }

            self.Add(item);
            return true;
        }

        //
        // --------------------------------------------------------------------
        //

        public static void DebugDump<T>( this List<T> self )
        {
            foreach( T t in self )
            {
                Dbg.Log( t.ToString() );
            }
        }

        //
        // --------------------------------------------------------------------
        //

        public static int IndexOf<T>( this List<T> self, System.Func<T,bool> compare )
        {
            for( int i = 0; i < self.Count; ++i )
            {
                if( compare( self[i] ) )
                {
                    return i;
                }
            }

            return -1;
        }

        //
        // --------------------------------------------------------------------
        //

        public static T[] SubArray<T>( this List<T> self, int count, int offset = 0 )
        {
            T[] result = new T[count];
            for( int i = 0; i < count; ++i )
            {
                result[i] = self[offset+i];
            }

            return result;
        }
    }
}
