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
	// PARTY PANEL
	// ===================================================================================
    public class PartyPanel : MonoBehaviour {
    
    	public GameObject panelPrefab;
    	public Transform content;
       
  		// -----------------------------------------------------------------------------------
		// Start
		// -----------------------------------------------------------------------------------
		private void Start() {
            Finder.party.PropertyChanged += party_PropertyChanged;
        }
       
		// -----------------------------------------------------------------------------------
		// OnEnable
		// -----------------------------------------------------------------------------------
        void OnEnable() {
           Refresh();
        }
        
 		// -----------------------------------------------------------------------------------
		// Refresh
		// -----------------------------------------------------------------------------------
        protected void Refresh() {
        	
        	foreach (Transform t in content)
     			Destroy(t.gameObject);
            
            // -- Iterate and link characters in party
            
			foreach (CharacterBase character in Finder.party.characters) {
				GameObject newObj = (GameObject)Instantiate(panelPrefab, content);
				newObj.GetComponentInChildren<HeroPanel>().character = character;
				character.parent = newObj;
			}
			
        }
        
        // -----------------------------------------------------------------------------------
		// party_PropertyChanged
		// -----------------------------------------------------------------------------------
        void party_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            
            switch (e.PropertyName) {
            
                case "Member":
                    Refresh();
                    break;
            
            }
            
        }
        
        // -----------------------------------------------------------------------------------
 
    }
}