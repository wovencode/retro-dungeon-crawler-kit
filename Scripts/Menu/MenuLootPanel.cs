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
	// MenuLootPanel
	// ===================================================================================
    public class MenuLootPanel : MonoBehaviour {
        
        public GameObject panel;
        public Transform content;
        public GameObject slotPrefab;
        
        public List<InventorySlot> loot;
        
        // -------------------------------------------------------------------------------
		// Show
		// -------------------------------------------------------------------------------       
        public void Show(List<InventorySlot> lootList) {
        	loot = new List<InventorySlot>();
        	loot = lootList;
			Refresh();
        	panel.gameObject.SetActive(true);
        	
        }
        
        // -------------------------------------------------------------------------------
		// Hide
		// -------------------------------------------------------------------------------       
        public void Hide() {
        	panel.gameObject.SetActive(false);
        	loot.Clear();
        }
       
        // -------------------------------------------------------------------------------
		// Refresh
		// -------------------------------------------------------------------------------       
        public void Refresh() {
       		
        	foreach (Transform t in content)
     			Destroy(t.gameObject);
       		
       		if (loot != null) {
           		foreach (InventorySlot instance in loot) {
                	
                	GameObject newObj = (GameObject)Instantiate(slotPrefab, content);
        			PanelSlot slot = newObj.GetComponent<PanelSlot>();
					
                	if (slot != null) {
                	    
                	    if (instance is InstanceResource) {
                	    	slot.txtName.text	= Finder.txt.getCurrencyName(((InstanceResource)instance).currencyType, false);
                	    	slot.imgIcon.sprite	= Finder.txt.getCurrencyIcon(((InstanceResource)instance).currencyType);
                	    } else {
							slot.txtName.text 	= instance.fullName;
							slot.imgIcon.sprite = instance.icon;
                	    }
                	    
                	    if (instance.Quantity > 0) {
							slot.txtCost.text = "x" + instance.Quantity.ToString();		
						} else {
							slot.txtCost.text = " ";
						}
                	    
                	    slot.imgCurrency.gameObject.SetActive(false);
                	    slot.btnAction.gameObject.SetActive(false);
                	    slot.btnUpgrade.gameObject.SetActive(false);
                	    slot.btnTooltip.gameObject.SetActive(false);
                	    slot.btnTrash.gameObject.SetActive(false);
                	    slot.transform.SetParent(content, false);
                	    slot.gameObject.SetActive(true);
                	    
                	}
           		 }
            }
        }
        
        // -------------------------------------------------------------------------------
             
    }
}