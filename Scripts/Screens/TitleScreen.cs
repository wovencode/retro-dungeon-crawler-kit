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
    public class TitleScreen : MonoBehaviour {
    
        public GameObject btnNewGame;
        public GameObject btnLoad;
        public GameObject btnSettings;
        public GameObject btnCredits;
        public GameObject btnQuit;
        
        public Text labelBtnNewGame;
       	public Text labelBtnLoad;
       	public Text labelBtnSettings;
       	public Text labelBtnCredits;
        public Text labelBtnQuit;
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private void OnEnable() {
        
        	labelBtnNewGame.text	= Finder.txt.buttonNames.NewGame;
			labelBtnLoad.text		= Finder.txt.buttonNames.Load;
			labelBtnSettings.text	= Finder.txt.buttonNames.Options;
			labelBtnCredits.text	= Finder.txt.buttonNames.Credits;
			labelBtnQuit.text		= Finder.txt.buttonNames.Quit;
			
            if (Finder.save.HasSaveFiles()) {
                btnLoad.SetActive(true);
            } else {
            	btnLoad.SetActive(false);
            }
            
            // -- Builds the Dictionaries
            DictionaryAttribute.load();
            DictionaryCharacterClass.load();
            DictionaryCharacterHero.load();
            DictionaryCombatStyle.load();
            DictionaryDungeon.load();
            DictionaryElement.load();
            DictionaryEquipment.load();
            DictionaryEquipmentSlot.load();
            DictionaryGossip.load();
            DictionaryItem.load();
            DictionarySkill.load();
            DictionarySpecies.load();
            DictionaryStatus.load();
            DictionaryTile.load();
            DictionaryTown.load();
            
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void OnNewGame() {
        	Finder.party.Clear();
            Finder.battle.Reset();
            Finder.ui.DeactivateAll();
            Finder.party.DefaultInitialize();
            Finder.ui.OverrideState(UIState.OpeningScene);
            Finder.save.StartDate = DateTime.Now;
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void OnQuit() {
            Finder.confirm.Show(Finder.txt.buttonNames.Quit+Finder.txt.seperators.dash+Finder.txt.basicVocabulary.areYouSure, Finder.txt.basicVocabulary.yes, Finder.txt.basicVocabulary.no, () => Application.Quit(), null);       
        }
        
        // -------------------------------------------------------------------------------
        
    }
}