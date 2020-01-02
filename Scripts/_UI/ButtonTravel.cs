// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// BUTTON TRAVEL
	// ===================================================================================
    public class ButtonTravel : MonoBehaviour {
    	
    	public Image icon;
    	[HideInInspector]public TemplateMetaTown targetTown;
    	[HideInInspector]public TemplateMetaDungeon targetDungeon;
    	
        // -------------------------------------------------------------------------------
		// onClick
		// -------------------------------------------------------------------------------
    	public void onClick() {
    	
    		if (targetTown != null) {
    			Finder.ui.ActivateWorldmap(false);
    			Finder.map.WarpTown(targetTown);
    		} else if (targetDungeon != null) {
    			Finder.ui.ActivateWorldmap(false);
    			Finder.map.WarpDungeon(targetDungeon);
    		}
    	
    	}
    	
   		// -------------------------------------------------------------------------------
   		
    }
}