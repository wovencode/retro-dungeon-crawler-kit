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
    public class MenuCharacterEquipment : ShopBase<InstanceEquipment> {
    
		public CharacterBase character { get; set; }
		
		protected InstanceEquipment selectedSlot;
		
		// -------------------------------------------------------------------------------
		// CanActivate
		// -------------------------------------------------------------------------------
  		protected override bool CanActivate(InstanceEquipment tmpl) {
  			return (tmpl.IsEquipped);
  		}
  		
   		protected override bool CanUpgrade(InstanceEquipment tmpl) {
  			return false;
  		}
  		
 		protected override bool CanTrash(InstanceEquipment instance) {
  			return false;
  		}
  		
		// -----------------------------------------------------------------------------------
		// LoadContent
		// -----------------------------------------------------------------------------------
        protected override void LoadContent() {
        	
        	content = new List<InstanceEquipment>();
        	
        	foreach (var equipmentSlot in DictionaryEquipmentSlot.dict) {
        	
        		InstanceEquipment slot = Finder.party.equipment.Equipment.FirstOrDefault(x => x.character == character && x.template.equipmentType == equipmentSlot.Value);
        		
        		if (slot != null) {
        			content.Add(slot);
        		} else {
        			MockEquipmentSlot mockslot = new MockEquipmentSlot { name = equipmentSlot.Value.fullName, icon = equipmentSlot.Value.icon };
        			content.Add(mockslot);
        		}
        		
        	}
        
        }
		
		// -----------------------------------------------------------------------------------
		// ActionRequestedHandler
		// -----------------------------------------------------------------------------------
        protected override void ActionRequestedHandler(object obj) {
        
            selectedSlot = (InstanceEquipment)obj;

			if (!selectedSlot.template.CanUnequip(character)) {
				Finder.audio.PlaySFX(SFX.ButtonCancel);
				Finder.log.Add(character.Name+ " " + Finder.txt.basicVocabulary.requirementsNotMet + " " + selectedSlot.template.fullName);
				return;
			}

			Finder.audio.PlaySFX(SFX.Unequip);
			Finder.party.equipment.UnequipItem(selectedSlot, character);
			
           	LoadContent();
           	LoadPage();
            character.CalculateDerivedStats();
        }
                
		// -----------------------------------------------------------------------------------
		// GetCost
		// -----------------------------------------------------------------------------------
        protected override string GetCost(InstanceEquipment item) {
            
            var text = "";
            
			if (item.template == null) {
				return "";
			
			// -- ATK is displayed in case of Weapons
			
			} else if (item.template is TemplateEquipmentWeapon) {
				text = Finder.txt.basicDerivedStatNames.abbrevAttack + " " + item.template.basePower.ToString();
					
			// -- DEF is displayed in case of Armor (and Shields)
			
			} else if (item.template is TemplateEquipment) {
				int defenseValue = 0;
				if (item.template.statModifiers.combatStyles != null && item.template.statModifiers.combatStyles.Length > 0)
					defenseValue = item.template.statModifiers.combatStyles.Max(x => x.defenseValue);
				text = Finder.txt.basicDerivedStatNames.abbrevDefense + " " + defenseValue.ToString();
			}
            
            return text;
        }

		// -----------------------------------------------------------------------------------
		// GetTextColor
		// -----------------------------------------------------------------------------------
        protected override Color GetTextColor(InstanceEquipment item) {
            if (item.IsEquipped) {
                return Color.yellow;
            } else { 
            	return base.GetTextColor(item);
        	}
        }
        
        // -----------------------------------------------------------------------------------
		// GetCurrencyIcon
		// -----------------------------------------------------------------------------------
		protected override Sprite GetCurrencyIcon(InstanceEquipment item) {
			return null;
		}
        
        // -----------------------------------------------------------------------------------
		// GetIcon
		// -----------------------------------------------------------------------------------
		protected override Sprite GetIcon(InstanceEquipment item) {
			return item.icon;
		}
		
		// -----------------------------------------------------------------------------------
		// GetName
		// -----------------------------------------------------------------------------------
        protected override string GetName(InstanceEquipment item) {
            return item.fullName;
        }
        
		// -----------------------------------------------------------------------------------
		// GetActionText
		// -----------------------------------------------------------------------------------
        protected override string GetActionText(InstanceEquipment item) {
            return Finder.txt.commandNames.unequip;
        }
        
        // -----------------------------------------------------------------------------------
        
    }
}