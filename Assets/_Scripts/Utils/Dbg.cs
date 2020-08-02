//
// (C) BLACKTRIANGLES 2015
// http://www.blacktriangles.com
//

#if UNITY_ENGINE || UNITY_EDITOR
using UnityEngine;
#endif

using System.Collections.Generic;

namespace Cafe
{
    public static class Dbg
    {
        //
        // Assert /////////////////////////////////////////////////////////////
        //

        public static bool Assert( bool condition, string format, params object[] args )
        {
            if( !condition ) Error( format, args );
            return condition;
        }

        //
        // --------------------------------------------------------------------
        //

        public static bool Assert( string str, string format, params object[] args )
        {
            return Assert( System.String.IsNullOrEmpty( str ) == false, format, args );
        }

        //
        // Error //////////////////////////////////////////////////////////////
        //

        public static void Error( string format, params object[] args )
        {
            #if UNITY_ENGINE || UNITY_EDITOR
                Debug.LogError( System.String.Format(format, args) );
            #else
                System.Console.Error.WriteLine( format, args );
            #endif
        }

        //
        // Warning ////////////////////////////////////////////////////////////
        //

        public static void Warn( bool condition, string format, object[] args )
        {
            if(!condition) Warn(format, args);
        }

        //
        // --------------------------------------------------------------------
        //

        public static void Warn( string format, params object[] args )
        {
            #if UNITY_ENGINE || UNITY_EDITOR
                Debug.LogWarning( System.String.Format(format, args) );
            #else
                System.Console.WriteLine( "WARNING! " + format, args );
            #endif
        }

        //
        // Log ////////////////////////////////////////////////////////////////
        //

        public static void Log(System.Collections.IEnumerable list)
        {
            foreach(System.Object obj in list)
            {
                Dbg.Log(obj.ToString());
            }
        }

        //
        // --------------------------------------------------------------------
        //

        public static void Log( string format, params object[] args )
        {
            #if UNITY_ENGINE || UNITY_EDITOR
                Debug.Log( System.String.Format(format, args) );
            #else
                System.Console.Error.WriteLine( format, args );
            #endif
        }

        //
        // Draw ///////////////////////////////////////////////////////////////
        //

        #if UNITY_ENGINE || UNITY_EDITOR
            public static void Draw(Line line)
            {
                Draw(line, Color.white);
            }

            public static void Draw(Line line, Color color, float duration = 1.0f)
            {
                Debug.DrawLine(line.start, line.end, color, duration);
            }

            public static void Draw(IEnumerable<Line> lines)
            {
                Draw(lines, Color.white);
            }

            public static void Draw(IEnumerable<Line> lines, Color color, float duration = 1.0f)
            {
                foreach(Line line in lines)
                {
                    Draw(line, color, duration);
                }
            }
        #endif

        //
        // Primitives /////////////////////////////////////////////////////////
        //

        #if UNITY_ENGINE || UNITY_EDITOR
            public static void CreateSphere( string name, UnityEngine.Vector3 pos, UnityEngine.Quaternion rot, float scale )
            {
                UnityEngine.GameObject go = UnityEngine.GameObject.CreatePrimitive( UnityEngine.PrimitiveType.Sphere );
                go.name = name;
                go.transform.position = pos;
                go.transform.localScale = UnityEngine.Vector3.one * scale;
                go.transform.rotation = rot;
                UnityEngine.GameObject.Destroy( go.GetComponent<UnityEngine.Collider>() );
            }
        #endif
    }
}
