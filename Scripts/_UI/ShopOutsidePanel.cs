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

	// -------------------------------------------------------------------------------
	// ShopOutsidePanel
	// -------------------------------------------------------------------------------
    public class ShopOutsidePanel : MonoBehaviour {
    	
    	public GameObject shopInsidePanel;
    	public GameObject buttonGroupOutside;
    	public GameObject buttonGroupInside;
    	public Text headerText;
    	public GameObject buttonGossip;
    	public GameObject buttonBuy;
    	public GameObject buttonSell;
    	public GameObject buttonRest;
    	public Image imageBackground;
    	public Image imageShopkeeper;
    	
    	public TemplateMetaShop shop { get; set; }
    	public bool IsSell { get; set; }
    	
    	protected IEnumerable<string> gossip;
        protected int gossipIndex;
        
        // -------------------------------------------------------------------------------
		// OnEnable
		// -------------------------------------------------------------------------------
        void OnEnable() {
		
        	SetShopType(true);
        	
        	gossipIndex = 0;
            gossip = DictionaryGossip.GetGossip(shop.shopType);
            
            imageBackground.sprite = shop.imageBackground;
            imageShopkeeper.sprite = shop.imageShopkeeper;
            
            Finder.log.Clear();
            
           	if (shop.welcomeText.Length > 0)
           		Finder.log.Add(shop.welcomeText);
                        
            SetHeaderText(shop.fullName);
            
            if (buttonRest != null && shop.shopType != ShopType.ShopInn) {
            	buttonRest.SetActive(false);
            } else {
            	buttonBuy.SetActive(false);
            	buttonRest.SetActive(true);
            }
            
            if (buttonSell != null)
            	buttonSell.SetActive(shop.canSell);
            
            if (buttonGossip != null) {
            	if (!gossip.Any()) {
            		buttonGossip.SetActive(false);
           	 	} else {
           	 		buttonGossip.SetActive(true);
        		}
        	}
        	
        	if (!shopInsidePanel.activeInHierarchy) {
        		
        		if (IsSell) {
        			buttonGroupOutside.SetActive(false);
        			if (imageShopkeeper != null) imageShopkeeper.enabled = false;
        		} else {
        			buttonGroupOutside.SetActive(true);
        			if (imageShopkeeper != null) imageShopkeeper.enabled = true;
        		}
        			
        	} else {
        	
        		if (buttonGroupInside != null)
        			buttonGroupInside.SetActive(true);
        			
        		if (imageShopkeeper != null)
        			imageShopkeeper.enabled = false;
        			
        	}
        	
        }
        
        // -------------------------------------------------------------------------------
		// OnDisable
		// -------------------------------------------------------------------------------
        void OnDisable() {
        	buttonGroupOutside.SetActive(false);
			buttonGroupInside.SetActive(false);
    		imageShopkeeper.enabled = false;
    		IsSell = false;
    		SetShopType(false);
        }
        
        // -------------------------------------------------------------------------------
		// SetHeaderText
		// -------------------------------------------------------------------------------
        protected void SetHeaderText(string text) {
        	if (headerText != null) headerText.text = text;
        }
        
        // -------------------------------------------------------------------------------
		// SetShopType
		// -------------------------------------------------------------------------------
        protected void SetShopType(bool enable) {

        	switch (shop.shopType) {
        	
        		case ShopType.ShopInn:
    				shopInsidePanel.GetComponent<ShopInn>().enabled = enable;
    				if (enable) shopInsidePanel.GetComponent<ShopInn>().OnEnable();
        			break;
        			
    			case ShopType.ShopItemAbility:
    				shopInsidePanel.GetComponent<ShopItemAbility>().enabled = enable;
        			break;
        			
    			case ShopType.ShopCharacter:
    				shopInsidePanel.GetComponent<ShopCharacter>().enabled = enable;
        			break;
        			
    			case ShopType.ShopItemCurative:
    				shopInsidePanel.GetComponent<ShopItemCurative>().enabled = enable;
        			break;
        		
        		case ShopType.ShopItem:
        			shopInsidePanel.GetComponent<ShopItem>().enabled = enable;
        			break;
        			
    			case ShopType.ShopEquipment:
    				shopInsidePanel.GetComponent<ShopEquipment>().enabled = enable;
        			break;

        	}
        	
        }

        // ========================= BUTTON RELATED FUNCTIONS ============================
        
    	// -------------------------------------------------------------------------------
		// onClickButtonSell
		// -------------------------------------------------------------------------------
    	public void onClickButtonSell() {
    	
    		imageShopkeeper.enabled = false;
    		
    		if (shop.shopType == ShopType.ShopCharacter) {
    			Finder.ui.PushState(UIState.CharacterSell);
    		} else {
    			Finder.ui.PushState(UIState.InventorySell);
    		}
    		
    	}
    	
      	// -------------------------------------------------------------------------------
		// onClickButtonGossip
		// -------------------------------------------------------------------------------
    	public void onClickButtonGossip() {
    		if (gossipIndex >= gossip.Count()) gossipIndex = 0;
            Finder.log.Clear();
            Finder.log.Add(gossip.ElementAt(gossipIndex), "yellow");
            gossipIndex++;
    	}
    	
        // -------------------------------------------------------------------------------
        
    }
    
    // -------------------------------------------------------------------------------
    
}