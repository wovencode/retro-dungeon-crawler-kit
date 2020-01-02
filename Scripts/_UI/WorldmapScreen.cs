// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// WORLDMAP SCREEN
	// ===================================================================================
    public class WorldmapScreen : MonoBehaviour {
    	
    	public GameObject buttonPrefab;
    	public Transform content;
    	
  		// -------------------------------------------------------------------------------
		// OnEnable
		// -------------------------------------------------------------------------------
 		void OnEnable() {
 			
 			foreach (Transform t in content)
     			Destroy(t.gameObject);
 			
			// -- Iterate and Add travelable Towns
			foreach (TemplateMetaTown town in DictionaryTown.dict.Values) {
				if (town.TravelRequirementsMet) {
					GameObject newObj = (GameObject)Instantiate(buttonPrefab, content);
					newObj.GetComponentInChildren<Text>().text = town.fullName;
					ButtonTravel button = newObj.GetComponent<ButtonTravel>();
					button.icon.sprite = Finder.fx.worldmapTownIcon;
					button.targetTown = town;
				}
			}
			
			// -- Iterate and Add travelable Dungeons
			foreach (TemplateMetaDungeon dungeon in DictionaryDungeon.dict.Values) {
				if (dungeon.TravelRequirementsMet) {
					GameObject newObj = (GameObject)Instantiate(buttonPrefab, content);
					newObj.GetComponentInChildren<Text>().text = dungeon.fullName;
					ButtonTravel button = newObj.GetComponent<ButtonTravel>();
					button.icon.sprite = Finder.fx.worldmapDungeonIcon;
					button.targetDungeon = dungeon;
				}
			}
		
		}       
        
        // -------------------------------------------------------------------------------
        
    }
    
    // -------------------------------------------------------------------------------
    
}