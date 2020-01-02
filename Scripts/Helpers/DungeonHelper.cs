using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using WoCo.DungeonCrawler; 

namespace WoCo.DungeonCrawler {

	// -------------------------------------------------------------------------------
	// DungeonHelper
	// -------------------------------------------------------------------------------
    public static class DungeonHelper {

		// -------------------------------------------------------------------------------
		// IsOppositeDirection
		// -------------------------------------------------------------------------------
		public static bool IsOppositeDirection(DirectionType dir1, DirectionType dir2, bool bothSides = false) {
		
			if (dir1 == DirectionType.North && dir2 == DirectionType.South ||
				bothSides && dir1 == DirectionType.North && dir2 == DirectionType.North
				) {
				return true;
			} else if (dir1 == DirectionType.East && dir2 == DirectionType.West ||
				bothSides && dir1 == DirectionType.East && dir2 == DirectionType.East
				) {
				return true;
			} else if (dir1 == DirectionType.South && dir2 == DirectionType.North ||
				bothSides && dir1 == DirectionType.South && dir2 == DirectionType.South
				) {
				return true;
			} else if (dir1 == DirectionType.West && dir2 == DirectionType.East ||
				bothSides && dir1 == DirectionType.West && dir2 == DirectionType.West
				) {
				return true;
			} else if (dir1 == DirectionType.None || dir2 == DirectionType.None) {
				return true;
			}
			
			return false;
		
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public static bool IsOppositeDirection(Vector3 v1, Vector3 v2) {
			
			DirectionType dir1 = Vector3Helper.CardinalDirection(v1);
			DirectionType dir2 = Vector3Helper.CardinalDirection(v2);
			
			return IsOppositeDirection(dir1, dir2);
		
		}
		
        // -------------------------------------------------------------------------------
    }
}
