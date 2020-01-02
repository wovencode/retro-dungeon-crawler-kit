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
using WoCo.DungeonCrawler;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// TEMPLATE CUTSCENE
	// ===================================================================================
    [Serializable]
    [CreateAssetMenu(fileName = "Unnamed Cutscene", menuName = "RDCK/Meta/New Cutscene")]
    public class TemplateMetaCutscene : ScriptableObject {

    	public Cutscene[] cutsceneContent;
		
	}
	
    // ===================================================================================
    
}