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

	// ===================================================================================
	// TEMPLATE CHARACTER BASE
	// ===================================================================================
    public abstract class TemplateCharacterBase : TemplateSimple {
    	    	
    	[Header("-=- Images -=-")]
    	public Sprite[] portraits;
    	public Sprite portraitHurt;
    	public Sprite portraitHappy;
    	public Sprite portraitDead;
    	
    	[Header("-=- Class -=-")]
    	public TemplateCharacterClass characterClass;
        public TemplateMetaElement element;
        public TemplateMetaSpecies species;
        public int level;
   		
   		[Header("-=- Defaults -=-")]
   		public List<TemplateItem> 			defaultItems;
   		public List<TemplateEquipment> 		defaultEquipment;
   		public List<TemplateSkill> 		defaultAbilities;
   		public RankType 					defaultRank;
   		
   		[Header("-=- Sounds -=-")]
   		public AudioClip critSFX;
		public AudioClip hitSFX;
    	public AudioClip missSFX;
    	public AudioClip hurtSFX;
    	public AudioClip dieSFX;
    	
    	public GameObject hitEffect;
    	
    	
 		// -------------------------------------------------------------------------------
		// checkQuantity
		// -------------------------------------------------------------------------------
   		public override bool checkQuantity {
			get {
				return Finder.party.GetQuantity;
			}
		}

		// -------------------------------------------------------------------------------
		// ShopListingRequirementsMet
		// -------------------------------------------------------------------------------
		public override bool ShopListingRequirementsMet(TemplateMetaShop shop) {
			return !Finder.party.HasMember(this) && !restrictions.exclude && AcquisitionRequirementsMet && AcquisitionShopLevel(shop);
		}
		
		// -------------------------------------------------------------------------------
		
    }
    
    // ===================================================================================
    
}