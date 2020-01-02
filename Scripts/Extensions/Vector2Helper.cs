using UnityEngine;
using System.Collections;
using System;

namespace UnityEngine {

    public static class Vector2Helper {
    
        public static Vector2 Parse(string str) {
            if(string.IsNullOrEmpty(str))
                throw new ArgumentException("Str");
            if (!str.Contains(","))
                throw new InvalidOperationException("no comma");

            string[] parts = str.Split(',');
            if (parts.Length != 2)
                throw new InvalidOperationException("only two comma separated parts allowed");

            float x = float.Parse(parts[0]);
            float y = float.Parse(parts[1]);
            return new Vector2(x, y);            
        }

        public static Vector2 Round(this Vector2 vec, int decimals){            
            vec.Set((float)Math.Round(vec.x, decimals),
                (float)Math.Round(vec.y, decimals));
            return vec; 
        }
        
    }
}
