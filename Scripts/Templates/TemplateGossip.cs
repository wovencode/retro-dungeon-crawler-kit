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
	// TEMPLATE GOSSIP
	// ===================================================================================
	[Serializable]
    [CreateAssetMenu(fileName = "Gossip", menuName = "RDCK/Templates/New Gossip")]
    public class TemplateGossip : ScriptableObject {
    
    	[Header("-=- Restrictions -=-")]
		public AcquisitionRestrictions restrictions;
    	public ShopType shopType;
        
        [Header("-=- Gossip Text -=-")]
        public GossipText[] _text;
		
		// -------------------------------------------------------------------------------
		// text
		// -------------------------------------------------------------------------------
		public string text {
			get {
				return _text[Finder.txt.language].text;
			}
		}
		
		// -------------------------------------------------------------------------------
		// ShopListingRequirementsMet
		// -------------------------------------------------------------------------------
		public virtual bool ShopListingRequirementsMet {
			get {
				return !restrictions.exclude && AcquisitionRequirementsMet;
			}
		}
		
		// -------------------------------------------------------------------------------
		// AcquisitionRequirementsMet
		// -------------------------------------------------------------------------------
		public bool AcquisitionRequirementsMet {
			get {
			
				bool success = Finder.party.getInLevelRange(restrictions.minLevel, restrictions.maxLevel);
				
				if (success && restrictions.RequiresExploredTowns == null && restrictions.RequiresExploredDungeons == null)
					return true;
				
				if (restrictions.RequiresTownsAndDungeons) {
					success = 	Finder.party.getHasExploredTowns(restrictions.RequiresExploredTowns) &&
								Finder.party.getHasExploredDungeons(restrictions.RequiresExploredDungeons);
				} else {
					success = 	Finder.party.getHasExploredTowns(restrictions.RequiresExploredTowns) ||
								Finder.party.getHasExploredDungeons(restrictions.RequiresExploredDungeons);
				}
				
				return success;
				
			}
		}
		
		// -------------------------------------------------------------------------------
		        
    }
    
    // -----------------------------------------------------------------------------------
	// GossipText
	// -----------------------------------------------------------------------------------
	[Serializable]
	public class GossipText {
		[TextArea]public string[] _text;
		
		public string text {
			get {
				return _text[Finder.txt.language];
			}
		}
		
	}
    
    // ===================================================================================
    
}