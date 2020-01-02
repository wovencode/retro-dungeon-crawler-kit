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

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// CONFIG MANAGER
	// ===================================================================================
	[Serializable]
    public class ConfigManager : MonoBehaviour {
		
		[Header("-=- Basic -=-")]
		public int startingGold;
		[Range(1,Constants.MAX_PARTY_MEMBERS)]	public int maxPartyMembers;
		public TemplateCharacterHero[] startingCharacters;
		
		[Header("-=- Experience -=-")]
		public int maxLevel;
		public int attributePointsPerLvl;
		public int abilityPointsPerLvl;
		public int nextLevelExpBase;
		[Range(0,10)]	public float nextLevelExpFactor;
		
		[Header("-=- Environment -=-")]
		public float buffDurationPerStep;
		public int stepsToResetFightChance;
    	
    	[Header("-=- Death -=-")]
    	[Range(0,1)] public float deathGoldPenalty;
    	
    }

    // ===================================================================================
}