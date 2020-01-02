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
	// TOWN PANEL
	// ===================================================================================
    public class TownPanel : MonoBehaviour {
    	
    	public Image imageBackground;
    	
    	public Text labelShopItem;
    	public GameObject btnShopItem;
    	
    	public Text labelShopEquipment;
    	public GameObject btnShopEquipment;
    	
    	public Text labelShopAbility;
    	public GameObject btnShopAbility;
    	
    	public Text labelShopTemple;
    	public GameObject btnTemple;
    	
    	public Text labelShopInn;
    	public GameObject btnInn;
    	
    	public Text labelShopTavern;
    	public GameObject btnTavern;
    	
    	protected TemplateMetaTown town;
    	
  		// -------------------------------------------------------------------------------
		// OnEnable
		// -------------------------------------------------------------------------------
 		void OnEnable() {
 			
 			town = Finder.map.currentTownConfig;
 			
 			imageBackground.sprite = town.imageBackground;

 			TemplateMetaShop shop;
 			
 			// -- Shop: Item
 			shop = town.shops.FirstOrDefault(x => x.shopType == ShopType.ShopItem);
 			if (shop != null && shop.level > 0) {
 				labelShopItem.text = shop.name;
 				btnShopItem.SetActive(true);
 			} else {
 				btnShopItem.SetActive(false);
 			}
 			
 			// -- Shop: Equipment
 			shop = town.shops.FirstOrDefault(x => x.shopType == ShopType.ShopEquipment);
 			if (shop != null && shop.level > 0) {
 				labelShopEquipment.text = shop.name;
 				btnShopEquipment.SetActive(true);
 			} else {
 				btnShopEquipment.SetActive(false);
 			}
 			
 			// -- Shop: Ability
 			shop = town.shops.FirstOrDefault(x => x.shopType == ShopType.ShopItemAbility);
 			if (shop != null && shop.level > 0) {
 				labelShopAbility.text = shop.name;
 				btnShopAbility.SetActive(true);
 			} else {
 				btnShopAbility.SetActive(false);
 			}
 			
 			// -- Shop: Tavern (Characters)
 			shop = town.shops.FirstOrDefault(x => x.shopType == ShopType.ShopCharacter);
 			if (shop != null && shop.level > 0) {
 				labelShopTavern.text = shop.name;
 				btnTavern.SetActive(true);
 			} else {
 				btnTavern.SetActive(false);
			}
			
			// -- Shop: Inn (Rest)
			shop = town.shops.FirstOrDefault(x => x.shopType == ShopType.ShopInn);
			if (shop != null && shop.level > 0) {
 				labelShopInn.text = shop.name;
 				btnInn.SetActive(true);
 			} else {
 				btnInn.SetActive(false);
			}
			
			// -- Shop: Temple (Curative)
			shop = town.shops.FirstOrDefault(x => x.shopType == ShopType.ShopItemCurative);
			if (shop != null && shop.level > 0) {
 				labelShopTemple.text = shop.name;
 				btnTemple.SetActive(true);
 			} else {
 				btnTemple.SetActive(false);
 			}
 				
		}       
        
        // -------------------------------------------------------------------------------
        
    }
    
    // -------------------------------------------------------------------------------
    
}