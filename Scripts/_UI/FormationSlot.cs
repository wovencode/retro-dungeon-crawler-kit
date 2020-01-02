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
	// FORMATION SLOT
	// ===================================================================================
    public class FormationSlot : MonoBehaviour {
    
    	public Image icon;
    	public Text text;
    	
    	[HideInInspector]public CharacterBase characterBase;
    	
        // -------------------------------------------------------------------------------
		// Initialize
		// -------------------------------------------------------------------------------
    	public void Initialize(CharacterBase character) {
    		
    		characterBase = character;
    		
    		if (characterBase.template.icon != null) {
    			icon.sprite = characterBase.template.icon;
    			icon.gameObject.SetActive(true);
    		} else {
    			icon.gameObject.SetActive(false);
    		}
    		
    	}

    	// -------------------------------------------------------------------------------
    	
    }
}