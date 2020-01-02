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
	// LootDrop
	// -------------------------------------------------------------------------------
    [CreateAssetMenu (fileName = "Unnamed Loot Drop", menuName = "RDCK/Meta/New LootDrop")]
    [Serializable]
    public class LootDrop : ScriptableObject {
		
		[Range(0,1)]public float dropChance;
        public bool uniqueDrop;
        public TemplateAdvanced lootObject;
        public int minQuantity;
        public int maxQuantity;
        public bool useAcquisitionRules;
       	public bool useClassRequirementRules;
       	public bool useStatRequirementRules;
       
    }
}