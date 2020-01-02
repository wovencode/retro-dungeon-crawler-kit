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

namespace WoCo.DungeonCrawler{

    public class ChoicesPanel : MonoBehaviour {
    
        public Text text;
        public Image icon;
        public GameObject buttonPrefab;
        public Transform content;
        
        private IEvent currentEvent;
        
 	    // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        void OnEnable() {
        	
        	foreach (Transform t in content)
     			Destroy(t.gameObject);
        	
        	int i = 0;
        	
        	foreach (EventChoice choice in currentEvent.choices) {
        		GameObject newObj = (GameObject)Instantiate(buttonPrefab, content);
        		ButtonEventChoice button = newObj.GetComponent<ButtonEventChoice>();
				button.Initialize(choice.label, i, choice.enabled, this);
				i++;
        	}
        	
        }
        
	    // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		void Update() {
			if (gameObject.activeInHierarchy) {
				Finder.ui.ActivateMenuButtons(false);
			}
		}
		
	    // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------	
        public void OnClickChoice(int id) {
            gameObject.SetActive(false);
            
            currentEvent.OnClickChoice(id);
            
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void Initialize(IEvent interactable) {
        	currentEvent = interactable;
            text.text = interactable.text;
            icon.sprite = interactable.icon;
        }
        
        // -------------------------------------------------------------------------------
        
    }
}