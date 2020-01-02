using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using WoCo.DungeonCrawler; 

namespace UnityEngine {

	// -------------------------------------------------------------------------------
	// Vector3Helper
	// -------------------------------------------------------------------------------
    public static class Vector3Helper {
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public static Vector3 CardinalDirection(DirectionType cardinalDirection) {
			if (cardinalDirection == DirectionType.North) {
				return new Vector3(0,0,1);
			} else if (cardinalDirection == DirectionType.East) {
				return new Vector3(1,0,0);
			} else if (cardinalDirection == DirectionType.South) {
				return new Vector3(0,0,-1);
			} else if (cardinalDirection == DirectionType.West) {
				return new Vector3(-1,0,0);
			}
			return new Vector3(0,0,0);
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public static DirectionType CardinalDirection(Vector3 cardinalDirection) {
			
			Vector3 north 	= new Vector3(0,0,1);
			Vector3 east 	= new Vector3(1,0,0);
			Vector3 south 	= new Vector3(0,0,-1);
			Vector3 west	= new Vector3(-1,0,0);
			
			if (cardinalDirection == north) {
				return DirectionType.North;
			} else if (cardinalDirection == east) {
				return DirectionType.East;
			} else if (cardinalDirection == south) {
				return DirectionType.South;
			} else if (cardinalDirection == west) {
				return DirectionType.West;
			}
			return DirectionType.None;
		}		
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public static Vector3 Round(this Vector3 vec, int decimals) {
            vec.Set((float)Math.Round(vec.x, decimals),
                (float)Math.Round(vec.y, decimals),
                (float)Math.Round(vec.z, decimals));
            return vec;
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public static Vector3 RoundLaterals(this Vector3 vec, int decimals) {
            vec.Set((float)Math.Round(vec.x, decimals),
                vec.y, 
                (float)Math.Round(vec.z, decimals)); 
            return vec; 
        }
        // -------------------------------------------------------------------------------
    }
}
