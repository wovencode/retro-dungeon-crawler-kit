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
	// PARTY INVENTORY
	// ===================================================================================
	[Serializable]
	public class PartyInventory {
    	
        public List<InstanceItem> Items { get; set; }
        
		// -------------------------------------------------------------------------------
		// PartyInventory
		// -------------------------------------------------------------------------------
        public PartyInventory() {
            Items = new List<InstanceItem>();
        }
        
        // -------------------------------------------------------------------------------
		// LoadTemplates
		// -------------------------------------------------------------------------------
        public void LoadTemplates() {
        	foreach (InstanceItem instance in Items)
        		instance.loadTemplate();
        }
        
        // -------------------------------------------------------------------------------
		// GetQuantity
		// -------------------------------------------------------------------------------
        public int GetQuantity(TemplateItem tmpl) {
            InstanceItem item = Items.FirstOrDefault(x => x.template == tmpl);
            if (item != null) return item.Quantity;
            return 0;
        }
        
		// -------------------------------------------------------------------------------
		// AddItem
		// -------------------------------------------------------------------------------
        public void AddItem(TemplateItem tmpl, int quantity) {
        
            InstanceItem item = Items.FirstOrDefault(x => x.template == tmpl);
            
            if (item != null) {
                item.Quantity += quantity;
                if (item.Quantity <= 0) {
                	Items.Remove(item);
                }
            } else {
            
                InstanceItem newItem = new InstanceItem
                {
                    Quantity = quantity,
                    template = tmpl
                };
                this.Items.Add(newItem);
                
            }
        }
        
        // -------------------------------------------------------------------------------
        
    }
  
}