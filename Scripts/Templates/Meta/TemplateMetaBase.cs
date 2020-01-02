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
	// TEMPLATE META BASE
	// ===================================================================================
    [Serializable]
    public abstract class TemplateMetaBase : TemplateBase {
    
       
        public AudioClip music;
        
        [Header("-=- Restrictions -=-")]
		public AcquisitionRestrictions restrictions;
		
		// -------------------------------------------------------------------------------
		// TravelRequirementsMet
		// -------------------------------------------------------------------------------
		public bool TravelRequirementsMet {
			get {
				return !restrictions.exclude &&
					Finder.party.getInLevelRange(restrictions.minLevel, restrictions.maxLevel) &&
					Finder.party.getHasExploredTowns(restrictions.RequiresExploredTowns) &&
					Finder.party.getHasExploredDungeons(restrictions.RequiresExploredDungeons);
			}
		}
		
		// -------------------------------------------------------------------------------
		
    }
    
    
    
    // ===================================================================================
    
}