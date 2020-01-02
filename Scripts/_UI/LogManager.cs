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
	// LOG MANAGER
	// ===================================================================================
    public class LogManager : MonoBehaviour {
    
        public GameObject panel;
        public ScrollRect scrollRect;
        public Text textBox;
        
		// -------------------------------------------------------------------------------
		// Show
		// -------------------------------------------------------------------------------
        public void Show() {
            panel.SetActive(true);
        }
        
		// -------------------------------------------------------------------------------
		// Hide
		// -------------------------------------------------------------------------------
        public void Hide() {
            panel.SetActive(false);
            textBox.text = "";
        }
        
		// -------------------------------------------------------------------------------
		// Add
		// -------------------------------------------------------------------------------
        public void Add(string msg, string color="white") {
        	if (string.IsNullOrEmpty(msg)) return;
        	Show();
            textBox.text += "<color='"+color+"'>"+msg+"</color>\n";
			Canvas.ForceUpdateCanvases();
        	scrollRect.verticalNormalizedPosition = 0;
        }
                
		// -------------------------------------------------------------------------------
		// Clear
		// -------------------------------------------------------------------------------
        public void Clear() {
            textBox.text = "";
        }
                
        // -------------------------------------------------------------------------------
        
    }
}