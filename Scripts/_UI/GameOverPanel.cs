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
	// GAMEOVER PANEL
	// ===================================================================================
    public class GameOverPanel : MonoBehaviour {
        
        public Button btnReturnToTown;
        public Button btnLoadGame;
        
        // -------------------------------------------------------------------------------
		// OnEnable
		// -------------------------------------------------------------------------------
        private void OnEnable()  {
            		
			// -- Return to Town Button
			if (Finder.party.lastTown != null) {
				string price = ((int)(Finder.party.currencies.getResource(CurrencyType.Gold) * Finder.config.deathGoldPenalty)).ToString();
				string text = Finder.txt.basicVocabulary.returnToTown + "\n";
				text += price + Finder.txt.getCurrencyName(CurrencyType.Gold);
				btnReturnToTown.UpdateText(text);
				btnReturnToTown.gameObject.SetActive(true);
			} else {
				btnReturnToTown.gameObject.SetActive(false);
			}
			
			// -- Load Last Savegame Button
			
            if (Finder.save.LastLoadedIndex != -1) {
                btnLoadGame.gameObject.SetActive(true);
            } else {
            	btnLoadGame.gameObject.SetActive(false);
            }
            
            
        }
        
        // -------------------------------------------------------------------------------
        
    }
}