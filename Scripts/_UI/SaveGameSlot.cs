// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.ComponentModel;
using System.Text;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// SAVE GAME SLOT
	// ===================================================================================
    public class SaveGameSlot : MonoBehaviour {
    
        public event Action<Savegame> Clicked;
        protected Savegame _savegame;
		
		public Text txtStartDate;
        public Text txtSaveDate;
        public Text txtLocation;
        public Text txtIndex;
        public Text txtGold;
		public Image[] portraits;
		
		// -------------------------------------------------------------------------------
		// Savegame
		// -------------------------------------------------------------------------------
        public Savegame savegame {
            get { return _savegame; }
            set { 
            	if (_savegame != value) {
            		_savegame = value;
            		UpdateDisplay();
            	}
            }
        }
        
		// -------------------------------------------------------------------------------
		// ClickHandler
		// -------------------------------------------------------------------------------
        public void ClickHandler() {
            OnClicked(savegame);
        }
        
		// -------------------------------------------------------------------------------
		// OnClicked
		// -------------------------------------------------------------------------------
        protected void OnClicked(Savegame state) {
            Action<Savegame> handler = Clicked;
            if (handler != null)
                handler(state);
        }
        
		// -------------------------------------------------------------------------------
		// UpdateDisplay
		// -------------------------------------------------------------------------------
        private void UpdateDisplay() {
        
            if (savegame != null) {
            
            	txtStartDate.text = savegame.StartDate.ToString();
                txtSaveDate.text = savegame.SaveDate.ToString();
                txtLocation.text = savegame.mapName;
                
                txtIndex.text 		= savegame.Index.ToString();
                txtGold.text 		= string.Format("{0}: {1}", Finder.txt.getCurrencyName(CurrencyType.Gold, false), savegame.gold);
                
                int i = 0;
                foreach (Image img in portraits) {
                
                	if (savegame.characters.Count > i) {
                		img.sprite = DictionaryCharacterHero.GetHero(savegame.characters[i].templateName).icon;
                		img.gameObject.SetActive(true);
                	} else {
                		img.gameObject.SetActive(false);
                	}
                	i++;
                
                }
                
            }
            
        }
        
		// -------------------------------------------------------------------------------
		// ClearDisplay
		// -------------------------------------------------------------------------------
        public void ClearDisplay() {
        
       		txtStartDate.text 	= "";
            txtSaveDate.text 	= "";
            txtLocation.text 	= "";
            txtGold.text 		= "";
            
            foreach (Image img in portraits) {
           		img.gameObject.SetActive(false);
            }
            
        }
        
        // -------------------------------------------------------------------------------
        
    }
}