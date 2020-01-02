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
	// MenuPartySave
	// =================================================================================== 
    public class MenuPartySave : MenuBase<Savegame> {

        public SaveMode Mode { get; set; }
        public Text txtTitle;
        public GameObject btnNew;
        
        protected int currentIndex;
		
		// -------------------------------------------------------------------------------
		// CanActivate
		// -------------------------------------------------------------------------------
  		protected override bool CanActivate(Savegame tmpl) {
  			return true;
  		}
  		
  		// -------------------------------------------------------------------------------
		// CanUpgrade
		// -------------------------------------------------------------------------------
  		protected override bool CanUpgrade(Savegame tmpl) {
  			return false;
  		}
  		
  		// -------------------------------------------------------------------------------
		// CanTrash
		// -------------------------------------------------------------------------------
  		protected override bool CanTrash(Savegame tmpl) {
  			return false;
  		}
  		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected override void LoadContent() {
            content = Finder.save.GetAllSavegames();
            if ((content.Count % 2) != 0)
                content.Add(null);
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public override void OnEnable() {
            base.OnEnable();
            switch (Mode) {
                case SaveMode.Save:
                    txtTitle.text = Finder.txt.namesVocabulary.savegame;
                    btnNew.SetActive(true);
                    break;
                case SaveMode.Load:
                    txtTitle.text = Finder.txt.namesVocabulary.loadgame;
                    btnNew.SetActive(false);
                    break;
            }
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected override void TemplateInitialize(UnityEngine.GameObject templateInstance, Savegame item) {
            SaveGameSlot panel = templateInstance.GetComponent<SaveGameSlot>();
            if (panel != null) {
                if (item != null) {
                    panel.savegame = item;
                    panel.Clicked += panel_Clicked;
                } else {
                    panel.ClearDisplay();
                }
            }
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        void panel_Clicked(Savegame obj) {
            switch (Mode) {
                case SaveMode.Save:
                    currentIndex = obj.Index;
                    Finder.confirm.Show(Finder.txt.commandNames.save+Finder.txt.seperators.dash+Finder.txt.basicVocabulary.areYouSure, Finder.txt.basicVocabulary.yes, Finder.txt.basicVocabulary.no, () => SaveOld(), null);
                    break;
                case SaveMode.Load:
                    Finder.save.LoadState(obj.Index);
                    break;
            }
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void SaveOld() {
        	Finder.save.SaveState(currentIndex);
            Finder.log.Add(Finder.txt.basicVocabulary.saved);
            Refresh();
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void SaveNew() {
            Finder.save.SaveAsNew();
            Refresh();
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected override void DisposeItem(UnityEngine.Transform item) {
            SaveGameSlot panel = item.GetComponent<SaveGameSlot>();
            if (panel != null) {
                if (panel.savegame != null) {
                    panel.savegame = null;
                    panel.Clicked -= panel_Clicked;
                }
            }
        }
    		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected override string GetName(Savegame item) {
            if (item != null)
                return item.Index.ToString();
            else return "empty";
        }

        protected override Sprite GetCurrencyIcon(Savegame item) { return null; }
		protected override Sprite GetIcon(Savegame item) { return null; }
        
        // -------------------------------------------------------------------------------
        
    }
}