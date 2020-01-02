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
	// BUTTON CHARACTER SELECT
	// ===================================================================================
    public class ButtonCharacterSelect : MonoBehaviour {
    
    	public Image icon;
    	[HideInInspector]public CharacterBase characterBase;
    	[HideInInspector]public SelectionPanel selectionPanel;
    	
        // -------------------------------------------------------------------------------
		// Initialize
		// -------------------------------------------------------------------------------
    	public void Initialize(SelectionPanel panel, CharacterBase character) {
    		
    		characterBase = character;
    		selectionPanel = panel;
    		
    		string name = "";
    		
    		name = characterBase.template.fullName + " [";
    		
    		if (characterBase.IsRearguard) {
    			name += Finder.txt.formationNames.rear;
    		} else {
    			name += Finder.txt.formationNames.front;
    		}
    		
    		name += "]";
    		
    		
    		GetComponentInChildren<Text>().text = name;
    		
    		if (characterBase.template.icon != null) {
    			icon.sprite = characterBase.template.icon;
    			icon.gameObject.SetActive(true);
    		} else {
    			icon.gameObject.SetActive(false);
    		}
    		
    	}
    	
    	// -------------------------------------------------------------------------------
		// onClick
		// -------------------------------------------------------------------------------
    	public void onClick() {
    		selectionPanel.OnSelect(characterBase);
    	}
    	
    	// -------------------------------------------------------------------------------
    	
    }
}