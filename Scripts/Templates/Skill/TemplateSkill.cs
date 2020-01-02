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
	// TEMPLATE SKILL
	// ===================================================================================
    [Serializable]
    public abstract class TemplateSkill : TemplateAdvanced {
 		
 		public int maxLevel;
        public virtual int baseManaCost { get; }
        public virtual int bonusManaCost { get; }
        
        // -------------------------------------------------------------------------------
		// CanUse
		// -------------------------------------------------------------------------------
		public bool CanUse(CharacterBase source, int level) {
       		return (!source.IsSilenced && !source.IsStunned && source.IsAlive && CheckCost(source, level) && level > 0 && RPGHelper.getCanUseLocation(useLocation));
        }
        
        // -------------------------------------------------------------------------------
		// CheckCost
		// -------------------------------------------------------------------------------
        public bool CheckCost(CharacterBase source, int level) {
        	if (level > 1)
        		return source.stats.MP >= baseManaCost + (level-1 * bonusManaCost);
        	return source.stats.MP >= baseManaCost;
        }
        
        // -------------------------------------------------------------------------------
		// CanTarget
		// -------------------------------------------------------------------------------
		public bool CanTarget(CharacterBase target) {
       		return base.CanUse(target);
        }
        
        // -------------------------------------------------------------------------------
		// GetCost
		// -------------------------------------------------------------------------------
        public int GetCost(int level) {
        	if (level > 1)
        		return baseManaCost + (level-1 * bonusManaCost);
        	return baseManaCost;
        }
		
    }
    
    // ===================================================================================
    
}