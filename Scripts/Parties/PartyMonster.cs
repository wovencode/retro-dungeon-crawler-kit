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
using System.ComponentModel;

namespace WoCo.DungeonCrawler {

 	// ===================================================================================
	// PartyMonster
	// ===================================================================================
	[Serializable]
	public class PartyMonster : PartyBase {

    	// -------------------------------------------------------------------------------
		// PartyMonster
		// -------------------------------------------------------------------------------
     	public PartyMonster() : base() {}
     	
		// ===============================================================================
		// FUNCTIONS
		// ===============================================================================
		
		
		          
 	    // ===============================================================================
		// GETTERS / SETTERS
		// ===============================================================================
        
       
 	    
 	    // -------------------------------------------------------------------------------
		// TotalExperience
		// -------------------------------------------------------------------------------
		public override int TotalExperience {
			get {
				int tmp = 0;
				foreach (CharacterMonster character in characters)
       				tmp += character.CalculateMonsterXP();
				return tmp;
			}
		}
 	    
    }

    // ===================================================================================
}