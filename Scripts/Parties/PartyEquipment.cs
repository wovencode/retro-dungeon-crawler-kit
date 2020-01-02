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

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// PARTY EQUIPMENT
	// ===================================================================================
	[Serializable]
	public class PartyEquipment {
    	
        public List<InstanceEquipment> Equipment { get; set; }
        
		// -------------------------------------------------------------------------------
		// PartyEquipment
		// -------------------------------------------------------------------------------
        public PartyEquipment() {
            
            Equipment = new List<InstanceEquipment>();
        }
        
		// -------------------------------------------------------------------------------
		// LoadTemplates
		// -------------------------------------------------------------------------------
        public void LoadTemplates() {
         	foreach (InstanceEquipment instance in Equipment)
        		instance.loadTemplate();
        }
        
		// -------------------------------------------------------------------------------
		// UnequipAll
		// -------------------------------------------------------------------------------
        public void UnequipAll(CharacterBase target) {
			foreach (InstanceEquipment equip in Equipment) {
				if (equip.IsEquipped && equip.character == target) {
					UnequipItem(equip, target);
				}
			}
		}
		
		// -------------------------------------------------------------------------------
		// AddEquipment
		// -------------------------------------------------------------------------------
        public void AddEquipment(TemplateEquipment tmpl) {
            InstanceEquipment newEquip = new InstanceEquipment
            {
           		template = tmpl,
            	character = null,
            	Level = 0
            };
            Equipment.Add(newEquip);
        }
        
		// -------------------------------------------------------------------------------
		// GetEquipped
		// -------------------------------------------------------------------------------
        public TemplateEquipment GetEquipped(TemplateMetaEquipmentSlot t, CharacterBase target) {
            InstanceEquipment tmp = Equipment.FirstOrDefault(x => x.IsEquipped && x.template.equipmentType == t && x.character == target);
            if (tmp == null) return null;
            return tmp.template;
        }
        
        // -------------------------------------------------------------------------------
		// GetEquippedWeapon
		// -------------------------------------------------------------------------------
        public TemplateEquipmentWeapon GetEquippedWeapon(CharacterBase source) {
            var tmp = Equipment.FirstOrDefault(x => x.character == source && x.template is TemplateEquipmentWeapon);
            if (tmp == null) return null;
            return (TemplateEquipmentWeapon)tmp.template;
        }

        // -------------------------------------------------------------------------------
		// GetEquippedShield
		// -------------------------------------------------------------------------------
        public TemplateEquipmentShield GetEquippedShield(CharacterBase source) {
            var tmp = Equipment.FirstOrDefault(x => x.character == source && x.template is TemplateEquipmentShield);
            if (tmp == null) return null;
            return (TemplateEquipmentShield)tmp.template;
        }

		// -------------------------------------------------------------------------------
		// EquipItem
		// -------------------------------------------------------------------------------
        public void EquipItem(InstanceEquipment item, CharacterBase target) {
        
            if (GetEquipped(item.template.equipmentType, target) != null) return;
            
            int index = Equipment.FindIndex(x => x.template.equipmentType == item.template.equipmentType && !x.IsEquipped);
        
            if (index != -1) {
            	Equipment[index].character = target;
            	target.InflictBuffs(Equipment[index].template.equipBuffType, 0, true);
           	}
           	
        }
        
        // -------------------------------------------------------------------------------
		// UnequipItem
		// -------------------------------------------------------------------------------
        public void UnequipItem(InstanceEquipment item, CharacterBase target) {
        	
			int index = Equipment.FindIndex(x => x.template == item.template && x.character == target);
			
			if (index != -1) {
            	target.InflictBuffs(Equipment[index].template.equipBuffType, 0, true, true);
				Equipment[index].character = null;
			}
			
        }
        
		// -------------------------------------------------------------------------------
		// UnEquipType
		// -------------------------------------------------------------------------------
        public void UnEquipType(TemplateMetaEquipmentSlot t) {
            Equipment.Where(x => x.template.equipmentType == t)
                .ToList()
                .ForEach(x => x.character = null);
        }
        
        // -------------------------------------------------------------------------------
        
    }
  
}