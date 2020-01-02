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
	// MenuPartyInventory
	// ===================================================================================
    public class MenuPartyInventory : ShopBase<InventorySlot> {
        
        public bool IsSell { get; set; }
        
        public CharacterBase character { get; set; }
        
        protected InventorySlot selectedSlot;
        protected bool show_equipment = true;
        protected bool show_items = true;
        protected TemplateMetaEquipmentSlot equipmentType;
        protected ItemType itemType;
        
        // -------------------------------------------------------------------------------
		// CanActivate
		// -------------------------------------------------------------------------------
  		protected override bool CanActivate(InventorySlot obj) {
  			
  			if (!IsSell) {
  			
				if (obj is InstanceItem && Finder.party.CanUseItem(((InstanceItem)obj).template)) {
					return true;
				} else if (obj is InstanceEquipment && Finder.party.CanEquipItem(((InstanceEquipment)obj).template)) {
					return true;
				}
				return false;
			
			}
            
            return true;
            
  		}
  		
       	// -------------------------------------------------------------------------------
		// CanUpgrade
		// -------------------------------------------------------------------------------
     	protected override bool CanUpgrade(InventorySlot obj) {
  			return false;
  		}
  		
       	// -------------------------------------------------------------------------------
		// CanTrash
		// -------------------------------------------------------------------------------
  		protected override bool CanTrash(InventorySlot instance) {
  			return true;
  		}
  		
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		protected override void LoadContent() {
			
			btnLeave.SetActive(true);
			
			content = new List<InventorySlot>();
			
			if (show_items) {
			content.AddRange(Finder.party.inventory.Items
				.Where(x => x.template.itemType == itemType || itemType == ItemType.None)
				.OrderBy(x => x.template.tradeCost.buyCost)
				.ThenBy(x => x.template.name)
				.Cast<InventorySlot>()
				.ToList());
			}
			
			if (show_equipment) {
			content.AddRange(Finder.party.equipment.Equipment
				.Where(x => x.IsEquipped == false && (x.template.equipmentType == equipmentType || equipmentType == null))
				.OrderBy(x => x.template.tradeCost.buyCost)
				.ThenBy(x => x.template.name)
				.Cast<InventorySlot>()
				.ToList());
            }
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected override string GetActionText(InventorySlot obj) {
        
            if (IsSell) {
                return Finder.txt.commandNames.sell;
            } else if (obj is InstanceItem) {
            	return Finder.txt.commandNames.use;
            } else if (obj is InstanceEquipment) {
                return Finder.txt.commandNames.equip;
            }
            
            return null;
        }
        
       	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected override Color GetTextColor(InventorySlot obj) {

            if (IsSell) {
                return base.GetTextColor(obj);
        	} else if (obj is InstanceEquipment) {
        		if (!Finder.party.CanEquipItem(((InstanceEquipment)obj).template))
                return Color.grey;
        	} else if (obj is InstanceItem) {
        		if (!Finder.party.CanUseItem(((InstanceItem)obj).template))
                return Color.grey;
            }
            
            return base.GetTextColor(obj);
        	
        }
        
       	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected override string GetCost(InventorySlot obj) {
            
            if (IsSell) {
                
                if (obj is InstanceEquipment) {
        			return ((InstanceEquipment)obj).template.tradeCost.sellValue.ToString();
        		} else if (obj is InstanceItem) {
        			return ((InstanceItem)obj).template.tradeCost.sellValue.ToString();
           		}
            
            }
            
            return obj.Quantity.ToString();
        	
        }
        
       	// -------------------------------------------------------------------------------
		// GetName
		// -------------------------------------------------------------------------------
        protected override string GetName(InventorySlot obj) {
			if (IsSell) {
				if (obj is InstanceEquipment)
        			return obj.fullName;
				return obj.fullName + " x " + obj.Quantity;
			} else {
				return obj.fullName;
			}
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		protected override Sprite GetCurrencyIcon(InventorySlot obj) {
        	
        	if (IsSell) {
                
                if (obj is InstanceEquipment) {
        			return Finder.txt.getCurrencyIcon(((InstanceEquipment)obj).template.tradeCost.currencyType);
        		} else if (obj is InstanceItem) {
        			return Finder.txt.getCurrencyIcon(((InstanceItem)obj).template.tradeCost.currencyType);
           		}
            
            }
        	return null;
		}
        
       	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		protected override Sprite GetIcon(InventorySlot obj) {
        	return obj.icon;
		}
		
		// -------------------------------------------------------------------------------
		// ActionRequestedHandler
		// -------------------------------------------------------------------------------
        protected override void ActionRequestedHandler(object obj) {
            selectedSlot = (InventorySlot)obj;
            if (IsSell) {
            	Finder.confirm.Show(Finder.txt.commandNames.sell+Finder.txt.seperators.dash+Finder.txt.basicVocabulary.areYouSure, Finder.txt.basicVocabulary.yes, Finder.txt.basicVocabulary.no, () => ActionConfirmedHandler(), null);      
            } else {
            	ActionConfirmedHandler();
            }
        }
        
        // -------------------------------------------------------------------------------
		// ActionConfirmedHandler
		// -------------------------------------------------------------------------------
        protected override void ActionConfirmedHandler() {

        	if (IsSell) {
                
               	// -- Sell Item Request
                if (selectedSlot is InstanceItem) {
                	SellHandlerItem((InstanceItem)selectedSlot);
                	
                // -- Sell Equipment Request
                } else if (selectedSlot is InstanceEquipment) {
                	SellHandlerEquipment((InstanceEquipment)selectedSlot);
                }
                
            } else {
            
                // -- Use Item Request
                if (selectedSlot is InstanceItem) {
                	((InstanceItem)selectedSlot).template.getActivationTarget(UIState.Inventory, character);
                
                // -- Equip Equipment Request
                } else if (selectedSlot is InstanceEquipment) {
                	Finder.ui.Prompt(UIState.Inventory, true,false);
                }
                
            }

            Refresh();
        }
        
		// -------------------------------------------------------------------------------
		// SellHandlerItem
		// -------------------------------------------------------------------------------
        protected void SellHandlerItem(InventorySlot selectedSlot) {
        	
        	InstanceItem item = (InstanceItem)selectedSlot;
        	
        	if (item.template.tradeCost.sellValue == 0 ) {
            	Finder.audio.PlaySFX(SFX.ButtonCancel);
                Finder.log.Add(Finder.txt.basicVocabulary.cannotSell);
                return;
            }
                
        	Finder.audio.PlaySFX(SFX.Sell);
        	
        	selectedSlot.Quantity -= 1;
			
			if (selectedSlot.Quantity <= 0) {
				Finder.party.inventory.Items.Remove(item);
				content.Remove(selectedSlot);
			}
			
			Finder.party.currencies.setResource(parent.shop.currencyType, item.template.tradeCost.sellValue);
			
			Finder.log.Add(string.Format("{0} " + Finder.txt.basicVocabulary.soldFor + " {1} " + Finder.txt.getCurrencyName(parent.shop.currencyType), item.template.name, item.template.tradeCost.sellValue));
        }
        
		// -------------------------------------------------------------------------------
		// SellHandlerEquipment
		// -------------------------------------------------------------------------------
        protected void SellHandlerEquipment(InventorySlot selectedSlot) {
        
        	InstanceEquipment equip = (InstanceEquipment)selectedSlot;
        	
        	if (equip.template.tradeCost.sellValue == 0 ) {
            	Finder.audio.PlaySFX(SFX.ButtonCancel);
                Finder.log.Add(Finder.txt.basicVocabulary.cannotSell);
                return;
            }
                
        	Finder.audio.PlaySFX(SFX.Sell);
        	
       		Finder.party.equipment.Equipment.Remove(equip);
			content.Remove(selectedSlot);
			Finder.party.currencies.setResource(parent.shop.currencyType, equip.template.tradeCost.sellValue);
			
			Finder.log.Add(string.Format("{0} " + Finder.txt.basicVocabulary.soldFor + " {1} " + Finder.txt.getCurrencyName(parent.shop.currencyType), equip.template.name, equip.template.tradeCost.sellValue));
        }
        
		// -------------------------------------------------------------------------------
		// ActivationRequestHandler
		// -------------------------------------------------------------------------------
		public void ActivationRequestHandler() {

			if (selectedSlot != null) {
	
				if (selectedSlot is InstanceEquipment) {
					EquipRequestHandler();
				} else if (selectedSlot is InstanceItem) {
					UseRequestHandler();
				}
			
				selectedSlot = null;
				Refresh();
			
            }
            
		}
		
		// -------------------------------------------------------------------------------
		// UseRequestHandler
		// -------------------------------------------------------------------------------
		protected void UseRequestHandler() {
			
			CharacterBase[] targets = Finder.ui.GetSelectedCharacters();
			InstanceItem item = (InstanceItem)selectedSlot;
			
			if (targets != null) {
			
				foreach (CharacterBase target in targets) {
					if (target != null) {
						if (!target.CanUseItem(item.template)) {
							Finder.audio.PlaySFX(SFX.ButtonCancel);
							Finder.log.Add(target.Name+ " " + Finder.txt.basicVocabulary.requirementsNotMet + " " + item.name);
							return;
						}
					}
				}

				if (Finder.battle.InBattle) {
					Finder.battle.PlayerUseItem(item, targets);
				} else {
					targets[0].CommandUseItem(item, targets);
				}
			
				foreach (CharacterBase target in targets) {
					if (target != null)
					target.CalculateDerivedStats();
				}
			
			}

		}
		
		// -------------------------------------------------------------------------------
		// EquipRequestHandler
		// -------------------------------------------------------------------------------
		protected void EquipRequestHandler() {
			
			CharacterBase target = Finder.ui.GetFirstSelectedCharacter();
			InstanceEquipment equip = (InstanceEquipment)selectedSlot;
			
			if (!equip.template.CanEquip(target)) {
				Finder.audio.PlaySFX(SFX.ButtonCancel);
				Finder.log.Add(target.Name+ " " + Finder.txt.basicVocabulary.requirementsNotMet + " " + equip.name);
				return;
			}
			
			Finder.audio.PlaySFX(SFX.Equip);
			Finder.party.equipment.EquipItem(equip, target);
			
			target.CalculateDerivedStats();
			
		}
	
		// -------------------------------------------------------------------------------
		// TrashRequestedHandler
		// -------------------------------------------------------------------------------
        protected override void TrashRequestedHandler(object obj) {
			selectedSlot = (InventorySlot)obj;
            Finder.confirm.Show(Finder.txt.commandNames.trash+Finder.txt.seperators.dash+Finder.txt.basicVocabulary.areYouSure, Finder.txt.basicVocabulary.yes, Finder.txt.basicVocabulary.no, () => TrashConfirmedHandler(), null);      
        }
        
		 // -------------------------------------------------------------------------------
		// TrashConfirmedHandler
		// -------------------------------------------------------------------------------
        protected override void TrashConfirmedHandler() {
           
            if (selectedSlot != null) {
	
				if (selectedSlot is InstanceEquipment) {
					InstanceEquipment equip = (InstanceEquipment)selectedSlot;
					Finder.party.equipment.Equipment.Remove(equip);
					content.Remove(selectedSlot);
				} else if (selectedSlot is InstanceItem) {
					InstanceItem item = (InstanceItem)selectedSlot;
					Finder.party.inventory.Items.Remove(item);
					content.Remove(selectedSlot);
				}
			
				selectedSlot = null;
				Refresh();
			
            }
            
        }
			
		// ============================= FILTER FUNCTIONS ================================
        
        // -------------------------------------------------------------------------------
		// SetFilterEquipment
		// -------------------------------------------------------------------------------
        public void SetFilterEquipment(string filter) {
            equipmentType = DictionaryEquipmentSlot.Get(filter);
            show_equipment = true;
            show_items = false;
            Refresh();
        }
        
        // -------------------------------------------------------------------------------
		// SetFilterItem
		// -------------------------------------------------------------------------------
        public void SetFilterItem(string filter) {
            itemType = (ItemType)Enum.Parse(typeof(ItemType), filter);
            show_items = true;
            show_equipment = false;
            Refresh();
        }
        
        // -------------------------------------------------------------------------------
		// SetFilterAll
		// -------------------------------------------------------------------------------
		public void SetFilterAll() {
			itemType = ItemType.None;
			equipmentType = null;
       		show_items = true;
        	show_equipment = true;
        	Refresh();
        }
        
        // -------------------------------------------------------------------------------
		// SetFilterAllItems
		// -------------------------------------------------------------------------------
        public void SetFilterAllItems() {
        	itemType = ItemType.None;
        	show_items = true;
        	show_equipment = false;
        	Refresh();
        }
        
        // -------------------------------------------------------------------------------
		// SetFilterAllEquipment
		// -------------------------------------------------------------------------------
        public void SetFilterAllEquipment() {
        	equipmentType = null;
        	show_items = false;
        	show_equipment = true;
        	Refresh();
        } 
        
        // -------------------------------------------------------------------------------
        
    }
}