// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoCo.DungeonCrawler {

	// -------------------------------------------------------------------------------
	// MonsterEncounter
	// -------------------------------------------------------------------------------
   	[CreateAssetMenu (fileName = "Unnamed MonsterEncounter", menuName = "RDCK/Meta/New MonsterEncounter")]
    [Serializable]
    public class MonsterEncounter : ScriptableObject {
		
		public TemplateCharacterMonster monster;
 
        [Range(0,1)]	public float encounterRate;
        public bool useAcquisitionRules;
        public EncounterType encounterType;
        
       
    }
    
}