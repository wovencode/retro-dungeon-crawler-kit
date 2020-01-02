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
	// 
	// ===================================================================================
    public class ShopEquipment : ShopBase<TemplateEquipment> {
    	
    	protected TemplateEquipment currentTemplate;
    	protected override Sprite GetCurrencyIcon(TemplateEquipment tmpl) { return Finder.txt.getCurrencyIcon(parent.shop.currencyType); }
		protected override Sprite GetIcon(TemplateEquipment tmpl) { return tmpl.icon; }
        protected override string GetCost(TemplateEquipment tmpl) { return tmpl.tradeCost.buyCost.ToString("N0"); }
        protected override string GetName(TemplateEquipment tmpl) { return tmpl.fullName; }
       
       	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public override void OnEnable() {
            base.OnEnable();
        }
        
        // -------------------------------------------------------------------------------
		// LoadContent
		// -------------------------------------------------------------------------------
        protected override void LoadContent() {
			
            content = DictionaryEquipment.dict.Values.Where(x => x.ShopListingRequirementsMet(parent.shop) && x.tradeCost.currencyType == parent.shop.currencyType )
                .OrderBy(x => x.tradeCost.buyCost)
                .ThenBy(x => x.fullName)
                .Cast<TemplateEquipment>()
                .ToList();
        }
        
        // -------------------------------------------------------------------------------
		// ActionRequestedHandler
		// -------------------------------------------------------------------------------
        protected override void ActionRequestedHandler(object obj) {
        	currentTemplate = (TemplateEquipment)obj;
            Finder.confirm.Show(Finder.txt.commandNames.buy+Finder.txt.seperators.dash+Finder.txt.basicVocabulary.areYouSure, Finder.txt.basicVocabulary.yes, Finder.txt.basicVocabulary.no, () => ActionConfirmedHandler(), null);    
        }
        
        // -------------------------------------------------------------------------------
		// ActionConfirmedHandler
		// -------------------------------------------------------------------------------
        protected override void ActionConfirmedHandler() {
         	 if (CanActivate(currentTemplate)) {
           		Buy(currentTemplate.tradeCost.buyCost, currentTemplate.fullName, Finder.txt.basicVocabulary.purchased);
            	Finder.party.equipment.AddEquipment(currentTemplate);
            }
        }
        
 		// -------------------------------------------------------------------------------
		// CanActivate
		// -------------------------------------------------------------------------------
  		protected override bool CanActivate(TemplateEquipment tmpl) {
  			return tmpl.ShopPurchaseRequirementsMet;
  		}
  		
  		// -------------------------------------------------------------------------------
		// CanUpgrade
		// -------------------------------------------------------------------------------
  		protected override bool CanUpgrade(TemplateEquipment tmpl) {
  			return false;
  		}
  		
  		// -------------------------------------------------------------------------------
		// CanTrash
		// -------------------------------------------------------------------------------
  		protected override bool CanTrash(TemplateEquipment tmpl) {
  			return false;
  		}
  		
        // -------------------------------------------------------------------------------

    }
}