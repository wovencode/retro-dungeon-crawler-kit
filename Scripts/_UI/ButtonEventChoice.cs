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
	// BUTTON EVENT CHOICE
	// ===================================================================================
   public class ButtonEventChoice : MonoBehaviour {

		[HideInInspector]public int id;
    	[HideInInspector]public ChoicesPanel parent;
    	
        // -------------------------------------------------------------------------------
		// Initialize
		// -------------------------------------------------------------------------------
    	public void Initialize(string text, int btnid, bool isenabled, ChoicesPanel panel) {
    		GetComponentInChildren<Button>().interactable = isenabled;
    		parent 	= panel;
    		id 		= btnid;
    		GetComponentInChildren<Text>().text = text;    		
    	}
    	
    	// -------------------------------------------------------------------------------
		// onClick
		// -------------------------------------------------------------------------------
    	public void onClick() {
    		parent.OnClickChoice(id);
    	}
    	
    	// -------------------------------------------------------------------------------
    	
    }
}