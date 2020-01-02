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
	// ShopInn
	// ===================================================================================
    public class ShopInn : MonoBehaviour {
            
        public Text restButtonText;
        public ShopOutsidePanel parent;
        
        // -------------------------------------------------------------------------------
		// OnEnable
		// -------------------------------------------------------------------------------
        public void OnEnable() {
        	restButtonText.text = Finder.txt.commandNames.rest + " " + getRestCost().ToString() + Finder.txt.getCurrencyName(parent.shop.currencyType);
        }
         
        // -------------------------------------------------------------------------------
		// getRestCost
		// -------------------------------------------------------------------------------
        protected int getRestCost() {
    		return parent.shop.level * Finder.party.characters.Count;
        }
        
       	// -------------------------------------------------------------------------------
		// onClickButtonRest
		// -------------------------------------------------------------------------------
    	public void onClickButtonRest() {
    	
    		int restCost = getRestCost();
    		
    		if (!Finder.party.currencies.canAfford(parent.shop.currencyType, restCost)) {
                Finder.log.Add(Finder.txt.basicVocabulary.cannotAfford);
            } else if (Finder.party.IsFullHP && Finder.party.IsFullMP) {
                Finder.log.Add(Finder.txt.basicVocabulary.noNeedToRest);
            } else {
                Finder.audio.PlaySFX(SFX.Purchase);
                Finder.party.currencies.setResource(parent.shop.currencyType, restCost*-1);
                Finder.party.RestoreToFull();
            }
    	}
    	
        // -------------------------------------------------------------------------------
    }
}