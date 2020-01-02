// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// 
	// =================================================================================== 
    public class ConfirmationBox : MonoBehaviour {
    
    	public GameObject panel;
        public Text txtControl;
        public Button btnYes;
        public Button btnNo;

        private Action onNo;
        private Action onYes;
        
      	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void YesClicked() {
            btnYes.interactable = false;
            btnNo.interactable = false;
            panel.gameObject.SetActive(false);
            Action act = onYes;
            Clear();
            if (act != null)
                act();

        }
        
      	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void NoClicked() {
            btnYes.interactable = false;
            btnNo.interactable = false;
            panel.gameObject.SetActive(false);
            Action act = onNo;
            Clear();
            if (act != null)
                act();
        }
        
      	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void Clear() {
            txtControl.text = "";
            btnYes.UpdateText("");
            btnNo.UpdateText("");
            onNo = null;
            onYes = null;
        }
        
      	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void Show(string text, string confirmText, string declineText, Action yesAction, Action noAction) {
            
            panel.gameObject.SetActive(true);
            txtControl.text = text;
            
            if (onYes != null || !string.IsNullOrEmpty(confirmText)) {
                btnYes.gameObject.SetActive(true);
                onYes = yesAction;
                btnYes.interactable = true;
                btnYes.UpdateText(confirmText);
            }

            if (onNo != null || !string.IsNullOrEmpty(declineText)) {
                btnNo.gameObject.SetActive(true);
                btnNo.interactable = true;
                onNo = noAction;
                btnNo.UpdateText(declineText);
            } else {
            	btnNo.gameObject.SetActive(false);
        	}
        	
        }
        
		// -------------------------------------------------------------------------------
		
    }
}
