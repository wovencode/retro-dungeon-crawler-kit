// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// PANEL SLOT
	// =================================================================================== 
    public class PanelSlot : MonoBehaviour {
    
        public event Action<object> OnActionRequested;
        public event Action<object> OnUpgradeRequested;
        public event Action<object> OnTooltipRequested;
		public event Action<object> OnTrashRequested;
		
		public Image imgIcon;
		public Image imgCurrency;
        public Text txtName;
        public Text txtCost;
        
        public Button btnAction;
        public Button btnUpgrade;
        public Button btnTooltip;
        public Button btnTrash;
        
        public object Content { get; set; }
        
        // -------------------------------------------------------------------------------
		// OnAction
		// -------------------------------------------------------------------------------
		public void OnAction() {
            Action<object> temp = OnActionRequested;
            if (temp != null) temp(Content);
        }
        
        // -------------------------------------------------------------------------------
		// OnUpgrade
		// -------------------------------------------------------------------------------
		public void OnUpgrade() {
            Action<object> temp = OnUpgradeRequested;
            if (temp != null) temp(Content);
        }
        
		// -------------------------------------------------------------------------------
		// OnTooltip
		// -------------------------------------------------------------------------------
        public void OnTooltip() {
            Action<object> temp = OnTooltipRequested;
            if (temp != null) temp(Content);
        }
        
        // -------------------------------------------------------------------------------
		// OnTrash
		// -------------------------------------------------------------------------------
        public void OnTrash() {
            Action<object> temp = OnTrashRequested;
            if (temp != null) temp(Content);
        }
        
        // -------------------------------------------------------------------------------
        
    }
}