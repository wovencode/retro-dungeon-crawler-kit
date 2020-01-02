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
	// TEMPLATE SIMPLE
	// ===================================================================================
    [Serializable]
    public abstract class TemplateSimple : TemplateBase {
    
    	[Header("-=- Basic -=-")]
    	public Sprite icon;
        [TextArea(3,20)]
        public string[] _description;
        
		[Header("-=- Restrictions -=-")]
		public AcquisitionRestrictions restrictions;
		
		public virtual CurrencyCost tradeCost 	{ get { return null; } }
		public virtual int MaxStack				{ get { return 1; } }
		public virtual bool checkQuantity 		{ get { return true; } }
		
		
		// -------------------------------------------------------------------------------
		// description
		// -------------------------------------------------------------------------------
		public string description {
			get {
				if (_description.Length > 0 && _description.Length >= Finder.txt.language && _description[Finder.txt.language] != null ) {
					return _description[Finder.txt.language];
				} else {
					Debug.LogWarning("description missing for language ID:"+Finder.txt.language+" on "+this.name);
					return "-missing description-";
				}
			}
		}
		
		// -------------------------------------------------------------------------------
		// ShopListingRequirementsMet
		// -------------------------------------------------------------------------------
		public virtual bool ShopListingRequirementsMet(TemplateMetaShop shop) {
			return !restrictions.exclude && AcquisitionRequirementsMet && AcquisitionShopLevel(shop);
		}
		
		// -------------------------------------------------------------------------------
		// ShopPurchaseRequirementsMet
		// -------------------------------------------------------------------------------
		public bool ShopPurchaseRequirementsMet {
			get {
				return !restrictions.exclude && checkQuantity && checkPrice && AcquisitionRequirementsMet;
			}
		}
		
		// -------------------------------------------------------------------------------
		// checkPrice
		// -------------------------------------------------------------------------------
		public bool checkPrice {
			get {
				return Finder.party.currencies.canAfford(tradeCost.currencyType, tradeCost.buyCost);
			}
		}
		
		// -------------------------------------------------------------------------------
		// AcquisitionShopLevel
		// -------------------------------------------------------------------------------
		public bool AcquisitionShopLevel(TemplateMetaShop shop) {
			return restrictions.minLevel <= shop.level;			
		}		

		// -------------------------------------------------------------------------------
		// AcquisitionRequirementsMet
		// -------------------------------------------------------------------------------
		public bool AcquisitionRequirementsMet {
			get {
			
				bool success = Finder.party.getInLevelRange(restrictions.minLevel, restrictions.maxLevel);
				
				if (success && restrictions.RequiresExploredTowns == null && restrictions.RequiresExploredDungeons == null)
					return true;
				
				if (success) {
				
					if (restrictions.RequiresTownsAndDungeons) {
						success = 	Finder.party.getHasExploredTowns(restrictions.RequiresExploredTowns) &&
									Finder.party.getHasExploredDungeons(restrictions.RequiresExploredDungeons);
					} else {
						success = 	Finder.party.getHasExploredTowns(restrictions.RequiresExploredTowns) ||
									Finder.party.getHasExploredDungeons(restrictions.RequiresExploredDungeons);
					}
				
				}
				
				return success;
				
			}
		}
		
		// -------------------------------------------------------------------------------
      
    }
    
    // ===================================================================================
    
}