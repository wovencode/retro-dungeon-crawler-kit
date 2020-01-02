using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using WoCo.DungeonCrawler; 

namespace WoCo.DungeonCrawler {

	// -------------------------------------------------------------------------------
	// Utils
	// -------------------------------------------------------------------------------
    public static class Utils {
    
		// -------------------------------------------------------------------------------
		// SetBool
		// -------------------------------------------------------------------------------
		public static bool SetBool(bool original, BoolType boolType) {
			
			if (boolType == BoolType.True) {
				return true;
			} else if (boolType == BoolType.False) {
				return false;
			}
			
			return original;
	
		}

        // -------------------------------------------------------------------------------
    }
}
