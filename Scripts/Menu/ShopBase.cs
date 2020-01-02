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
    public abstract class ShopBase<T> : MenuBase<T> where T : class {
    	
    	public ShopOutsidePanel parent;
    	
        protected abstract string GetCost(T item);
        protected virtual Color GetTextColor(T item) { return Color.white; }
        
        protected virtual void ActionRequestedHandler(object tmpl) { }
        protected virtual void UpgradeRequestedHandler(object tmpl) { }
        protected virtual void TrashRequestedHandler(object tmpl) { }
       	protected virtual void ActionConfirmedHandler() { }
       	protected virtual void UpgradeConfirmedHandler() { }
       	protected virtual void TrashConfirmedHandler() { }
       	
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public override void OnEnable() {
            base.OnEnable();
        }
        
    	// -------------------------------------------------------------------------------
		// GetActionText
		// -------------------------------------------------------------------------------       
        protected virtual string GetActionText(T tmpl) {
        	return Finder.txt.commandNames.buy;
        }
		
   		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected override void TemplateInitialize(GameObject templateInstance, T item) {
            PanelSlot panel = templateInstance.GetComponent<PanelSlot>();
            panel.Content = item;
            panel.imgIcon.sprite = GetIcon(item);
            panel.imgCurrency.sprite = GetCurrencyIcon(item);
            if (panel.imgCurrency.sprite == null) {
            	panel.imgCurrency.enabled = false;
        	} else {
        		panel.imgCurrency.enabled = true;
        	}
            panel.txtCost.text 		= GetCost(item);
            panel.txtCost.color 	= GetTextColor(item);
            panel.txtName.text 		= GetName(item);
            panel.txtName.color 	= GetTextColor(item);
            
            panel.btnTooltip.interactable 	= (item is MockEquipmentSlot) ? false : true;
            panel.btnAction.interactable 	= CanActivate(item);
            panel.btnUpgrade.gameObject.SetActive(CanUpgrade(item));
            panel.btnTrash.gameObject.SetActive(CanTrash(item));
            panel.btnAction.UpdateText(GetActionText(item));
            
            panel.OnTooltipRequested 	+= TooltipRequestedHandler;
            panel.OnActionRequested 	+= ActionRequestedHandler;
            panel.OnUpgradeRequested 	+= UpgradeRequestedHandler;
            panel.OnTrashRequested 		+= TrashRequestedHandler;
            
        }
        
  		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected void RemoveItem(T tmpl) {
            foreach (Transform child in panel.transform) {
                if (child != null) {
                    PanelSlot panel = child.GetComponent<PanelSlot>();
                    if (panel != null) {
                        if (panel.Content == tmpl)
                            GameObject.Destroy(child.gameObject);
                    }
                }
            }
        }
        
  		// -------------------------------------------------------------------------------
		// DisposeItem
		// -------------------------------------------------------------------------------
        protected override void DisposeItem(Transform item) {
            PanelSlot panel = item.GetComponent<PanelSlot>();
            if (panel != null) {
                panel.OnTooltipRequested 	-= TooltipRequestedHandler;
                panel.OnActionRequested 	-= ActionRequestedHandler;
                panel.OnUpgradeRequested 	-= UpgradeRequestedHandler;
                panel.Content = null;
            }
        }
        
  		// -------------------------------------------------------------------------------
		// Buy
		// -------------------------------------------------------------------------------
        protected virtual void Buy(int value, string name, string purchaseNote) {
        	Finder.party.currencies.setResource(parent.shop.currencyType, value*-1);
        	Finder.audio.PlaySFX(SFX.Purchase);
            Finder.log.Add(name + " " + purchaseNote);
            Refresh();
        }
        
        // -------------------------------------------------------------------------------
		// canSell
		// -------------------------------------------------------------------------------
        protected virtual bool canSell(int value) {
        
        	int amount = Finder.party.currencies.getResource(parent.shop.currencyType);
        	
        	if (amount < value) {
                Finder.audio.PlaySFX(SFX.ButtonCancel);
                Finder.log.Add(Finder.txt.basicVocabulary.cannotAfford);
            	return false;
            }
            
            return true;
        }
        
  		// -------------------------------------------------------------------------------
		// Sell
		// -------------------------------------------------------------------------------
        protected virtual void Sell(int value, string name, string purchaseNote) {
        	Finder.party.currencies.setResource(parent.shop.currencyType, value*-1);
        	Finder.audio.PlaySFX(SFX.Sell);       
            Finder.log.Add(name + " " + purchaseNote);
            Refresh();
        }

        // -------------------------------------------------------------------------------
		
    }
}