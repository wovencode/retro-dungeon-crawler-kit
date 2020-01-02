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
	// 
	// ===================================================================================
    public class ShopCharacter : ShopBase<TemplateCharacterBase> {
    
    	public bool IsSell { get; set; }
		
		protected TemplateCharacterBase currentTemplate;
		protected override Sprite GetCurrencyIcon(TemplateCharacterBase tmpl) { return Finder.txt.getCurrencyIcon(parent.shop.currencyType); }
		protected override Sprite GetIcon(TemplateCharacterBase tmpl) { return tmpl.icon; }
        protected override string GetCost(TemplateCharacterBase tmpl) { return tmpl.tradeCost.buyCost.ToString("N0"); }
       	
       	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public override void OnEnable() {
            base.OnEnable();
        }
        
       	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public override void OnDisable() {
        	IsSell = false;
        	base.OnDisable();
        }
        
        // -------------------------------------------------------------------------------
		// LoadContent
		// -------------------------------------------------------------------------------
        protected override void LoadContent() {
			
			if (IsSell) {
				
				List<TemplateCharacterBase> tmp_content = new List<TemplateCharacterBase>();
				
				foreach (CharacterBase tmpl in Finder.party.characters)
					tmp_content.Add(tmpl.template);
				
				content = tmp_content.Where(x => x.tradeCost.currencyType == parent.shop.currencyType)
					.OrderBy(x => x.tradeCost.buyCost)
					.ThenBy(x => x.fullName)
					.Cast<TemplateCharacterBase>()
					.ToList();
			
			} else {

				content = DictionaryCharacterHero.dict.Values.Where(x => x.ShopListingRequirementsMet(parent.shop) && x.tradeCost.currencyType == parent.shop.currencyType)
					.OrderBy(x => x.tradeCost.buyCost)
					.ThenBy(x => x.fullName)
					.Cast<TemplateCharacterBase>()
					.ToList();
                
            }
                
        }
        
        // -------------------------------------------------------------------------------
		// ActionRequestedHandler
		// -------------------------------------------------------------------------------
        protected override void ActionRequestedHandler(object obj) {
        	currentTemplate = (TemplateCharacterBase)obj;
            Finder.confirm.Show(Finder.txt.commandNames.hire+Finder.txt.seperators.dash+Finder.txt.basicVocabulary.areYouSure, Finder.txt.basicVocabulary.yes, Finder.txt.basicVocabulary.no, () => ActionConfirmedHandler(), null);      
        }
        
		 // -------------------------------------------------------------------------------
		// ActionConfirmedHandler
		// -------------------------------------------------------------------------------
        protected override void ActionConfirmedHandler() {
           
            if (IsSell) {
            
            	if (currentTemplate.tradeCost.buyCost == 0 || Finder.party.characters.Count <= 1 ) {
                    Finder.audio.PlaySFX(SFX.ButtonCancel);
                    Finder.log.Add(Finder.txt.basicVocabulary.cannotDismiss);
                    return;
                }
                
                Finder.audio.PlaySFX(SFX.Purchase);
                Finder.party.currencies.setResource(parent.shop.currencyType, currentTemplate.tradeCost.sellValue);
                Finder.log.Add(string.Format("{0} " + Finder.txt.basicVocabulary.soldFor + " {1} " + Finder.txt.getCurrencyName(parent.shop.currencyType), currentTemplate.fullName, currentTemplate.tradeCost.sellValue));
                Finder.party.DismissHero(currentTemplate);
                
            } else {
            
            	if (CanActivate(currentTemplate)) {
					Finder.audio.PlaySFX(SFX.Purchase);
					Finder.party.currencies.setResource(parent.shop.currencyType, currentTemplate.tradeCost.buyCost*-1);
					Finder.party.AddHero((TemplateCharacterHero)currentTemplate);
					Finder.log.Add(currentTemplate.fullName + " " + Finder.txt.basicVocabulary.hired);
				}
            
            }
            
            Refresh();
        }
        
        // -------------------------------------------------------------------------------
		// GetActionText
		// -------------------------------------------------------------------------------
        protected override string GetActionText(TemplateCharacterBase tmpl) {
            if (IsSell) return Finder.txt.commandNames.dismiss;
            return Finder.txt.commandNames.hire;
        }
        
        // -------------------------------------------------------------------------------
		// GetName
		// -------------------------------------------------------------------------------
        protected override string GetName(TemplateCharacterBase tmpl) {
        	return tmpl.fullName + " " + Finder.txt.basicDerivedStatNames.LV + tmpl.level + " " + tmpl.characterClass.fullName;
        }
        
        // -------------------------------------------------------------------------------
		// CanActivate
		// -------------------------------------------------------------------------------
  		protected override bool CanActivate(TemplateCharacterBase tmpl) {
  			return tmpl.ShopPurchaseRequirementsMet;
  		}
  		
		// -------------------------------------------------------------------------------
		// CanUpgrade
		// -------------------------------------------------------------------------------
  		protected override bool CanUpgrade(TemplateCharacterBase tmpl) {
  			return false;
  		}
		
		// -------------------------------------------------------------------------------
		// CanTrash
		// -------------------------------------------------------------------------------
  		protected override bool CanTrash(TemplateCharacterBase tmpl) {
  			return false;
  		}
		
        // -------------------------------------------------------------------------------

    }
}