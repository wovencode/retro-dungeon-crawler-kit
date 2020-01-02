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
	// SHORTCUT PANEL
	// ===================================================================================
    public class ShortcutPanel : MonoBehaviour {
    
       	public GameObject panel;
       	
       	public GameObject btnLoad;
       	public GameObject btnSave;
       	public GameObject btnMinimapShow;
       	public GameObject btnMinimapHide;
       	public GameObject btnQuit;
       	public GameObject btnSwitch;
       	
       	public Text labelBtnStatus;
       	public Text labelBtnInventory;
       	public Text labelBtnEquipment;
       	public Text labelBtnAbilities;
       	public Text labelBtnOptions;
       	public Text labelBtnMinimapShow;
       	public Text labelBtnMinimapHide;
       	public Text labelBtnSave;
       	public Text labelBtnLoad;
       	public Text labelBtnQuit;
       	public Text labelBtnSwitch;
       	
        // -------------------------------------------------------------------------------
		// OnEnable
		// -------------------------------------------------------------------------------
       	public void OnEnable() {
       		
			labelBtnStatus.text			= Finder.txt.buttonNames.Status;
			labelBtnInventory.text		= Finder.txt.buttonNames.Inventory;
			labelBtnEquipment.text		= Finder.txt.buttonNames.Equipment;
			labelBtnAbilities.text		= Finder.txt.buttonNames.Skills;
			labelBtnOptions.text		= Finder.txt.buttonNames.Options;
			labelBtnMinimapShow.text	= Finder.txt.buttonNames.Minimap;
			labelBtnMinimapHide.text	= Finder.txt.buttonNames.Minimap;
			labelBtnSave.text			= Finder.txt.buttonNames.Save;
			labelBtnLoad.text			= Finder.txt.buttonNames.Load;
       		labelBtnQuit.text			= Finder.txt.buttonNames.Quit;
       		labelBtnSwitch.text			= Finder.txt.buttonNames.Switch;
       		
       		// -- Load Button
            if (Finder.save.HasSaveFiles()) {
                btnLoad.SetActive(true);
            } else {
            	btnLoad.SetActive(false);
       		}
       		
       		// -- Save Button
       		if (Finder.battle.InBattle || (Finder.map.locationType != LocationType.Town && Finder.save.savegameSettings == SavegameSettings.TownOnly ) ) {
                btnSave.SetActive(false);
            } else if (Finder.save.GameInProgress) {
                btnSave.SetActive(true);
       		}
       		
       		// -- Minimap Buttons
       		if (Finder.map.locationType != LocationType.Dungeon) {
       		
       			btnMinimapShow.SetActive(false);
       			btnMinimapHide.SetActive(false);
       			
       		} else {
       		
				if (Finder.ui.MapOpened) {
					btnMinimapShow.SetActive(false);
					btnMinimapHide.SetActive(true);
				} else {
					btnMinimapShow.SetActive(true);
					btnMinimapHide.SetActive(false);
				}
       		
       		}
       		
       	}
       	
       	// -------------------------------------------------------------------------------
		// Toggle
		// -------------------------------------------------------------------------------
        public void Toggle(bool hide=false) {
            if (panel.activeInHierarchy || hide) {
            	panel.SetActive(false);
            } else {
            	panel.SetActive(true);
            }
        }
        
       	// -------------------------------------------------------------------------------
		// OnQuit
		// -------------------------------------------------------------------------------
        public void OnQuit() {
        	Finder.confirm.Show(Finder.txt.buttonNames.Quit+Finder.txt.seperators.dash+Finder.txt.basicVocabulary.areYouSure, Finder.txt.basicVocabulary.yes, Finder.txt.basicVocabulary.no, () => Finder.ui.PushState(UIState.Title), null);
        }
        
		// -------------------------------------------------------------------------------
		
    }
}