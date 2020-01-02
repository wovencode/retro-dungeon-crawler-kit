// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;
using WoCo.DungeonCrawler;

namespace WoCo.DungeonCrawler {

    // ===================================================================================
	// SAVEGAME CHARACTER
	// ===================================================================================
    [Serializable]
    public class SavegameCharacter {
    
        public string templateName;
		
		public bool IsRearguard;
	 	
		public CharacterStats stats;
    	public List<InstanceSkill> abilities;
        public List<InstanceStatus> buffs;
        
    }
    
    
}