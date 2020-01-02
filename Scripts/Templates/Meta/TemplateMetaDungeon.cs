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
using toinfiniityandbeyond.Tilemapping;

using WoCo.DungeonCrawler;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// TEMPLATE META DUNGEON
	// ===================================================================================
    [CreateAssetMenu(fileName = "Unnamed Dungeon", menuName = "RDCK/Meta/New Dungeon")]
    [Serializable]
    public class TemplateMetaDungeon : TemplateMetaBase {
    
        [Header("-=- Dungeon Options -=-")]
        
        public int monsterLevel;
        [Range(0,1)] public float encounterRate;
        
        public Sprite battleBackground;
        
        public TileMapContainer mapFile;
		
		[Header("-=- Monster Encounters -=-")]
		public MonsterEncounters[] monsterEncounters;

    }

	// ===================================================================================
	// 
	// ===================================================================================

    [Serializable]
    public class MonsterEncounters {
    	public List<MonsterEncounter> monsterEncounter;
    }
    
    // ===================================================================================
    
}