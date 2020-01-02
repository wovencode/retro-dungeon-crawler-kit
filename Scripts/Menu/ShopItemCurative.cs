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
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// 
	// ===================================================================================
    public class ShopItemCurative : ShopBase<TemplateItem> {
        
        protected TemplateItem currentTemplate;
        protected override Sprite GetCurrencyIcon(TemplateItem tmpl) { return Finder.txt.getCurrencyIcon(parent.shop.currencyType); }
		protected override Sprite GetIcon(TemplateItem tmpl) { return tmpl.icon; }
        protected override string GetCost(TemplateItem tmpl) { return tmpl.tradeCost.buyCost.ToString("N0"); }
        protected override string GetName(TemplateItem tmpl) { return tmpl.fullName; }
		
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
            
            content = DictionaryItem.dict.Values.Where(x => x.ShopListingRequirementsMet(parent.shop) && x.tradeCost.currencyType == parent.shop.currencyType && x is TemplateItemCurative)
                .OrderBy(x => x.tradeCost.buyCost)
                .ThenBy(x => x.fullName)
                .Cast<TemplateItem>()
                .ToList();
        }
        
        // -------------------------------------------------------------------------------
		// ActionRequestedHandler
		// -------------------------------------------------------------------------------
        protected override void ActionRequestedHandler(object obj) {
        	currentTemplate = (TemplateItem)obj;
            Finder.confirm.Show(Finder.txt.commandNames.buy+Finder.txt.seperators.dash+Finder.txt.basicVocabulary.areYouSure, Finder.txt.basicVocabulary.yes, Finder.txt.basicVocabulary.no, () => ActionConfirmedHandler(), null);    
        } 
        
        // -------------------------------------------------------------------------------
		// ActionConfirmedHandler
		// -------------------------------------------------------------------------------
        protected override void ActionConfirmedHandler() {
            if (CanActivate(currentTemplate)) {
            	Buy(currentTemplate.tradeCost.buyCost, currentTemplate.fullName, Finder.txt.basicVocabulary.purchased);
          	 	Finder.party.inventory.AddItem(currentTemplate, 1);
     		}
        }
        
        // -------------------------------------------------------------------------------
		// CanActivate
		// -------------------------------------------------------------------------------
  		protected override bool CanActivate(TemplateItem tmpl) {
  			return tmpl.ShopPurchaseRequirementsMet;
  		}
  		
  		// -------------------------------------------------------------------------------
		// CanUpgrade
		// -------------------------------------------------------------------------------
  		protected override bool CanUpgrade(TemplateItem tmpl) {
  			return false;
  		}
  		
  		// -------------------------------------------------------------------------------
		// CanTrash
		// -------------------------------------------------------------------------------
  		protected override bool CanTrash(TemplateItem tmpl) {
  			return false;
  		}
  		
        // -------------------------------------------------------------------------------
        
    }
}