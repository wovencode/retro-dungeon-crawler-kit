// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// GOLD PANEL
	// ===================================================================================
    public class GoldPanel : MonoBehaviour {
        
        public Text partyGold;
        
 		// -----------------------------------------------------------------------------------
		// Start
		// -----------------------------------------------------------------------------------
        private void Start() {
            Finder.party.currencies.PropertyChanged += party_PropertyChanged;
            partyGold.text = Finder.party.currencies.getResource(CurrencyType.Gold).ToString();
        }
        
		// -----------------------------------------------------------------------------------
		// OnDestroy
		// -----------------------------------------------------------------------------------
        private void OnDestroy() {
        	if (Finder.party != null)
            	Finder.party.PropertyChanged -= party_PropertyChanged;
        }
        
        // -----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        void party_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                case "Gold":
                    partyGold.text = Finder.party.currencies.getResource(CurrencyType.Gold).ToString();
                    break;
            }
        }
        
        // -----------------------------------------------------------------------------------
        
    }
}