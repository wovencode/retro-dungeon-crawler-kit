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
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// TEMPLATE CHARACTER MONSTER
	// ===================================================================================
    [CreateAssetMenu(fileName = "Unnamed Monster", menuName = "RDCK/Templates/Characters/New Monster")]
    public class TemplateCharacterMonster : TemplateCharacterBase {
    	
    	[Header("-=- Visual -=-")]
    	public GameObject prefab;

		[Header("-=- Loot -=-")]
		public List<LootDrop> lootContent;
		
		[Header("-=- Experience -=-")]
		public int experienceBonus;
		
    }
    
    // ===================================================================================
    
}