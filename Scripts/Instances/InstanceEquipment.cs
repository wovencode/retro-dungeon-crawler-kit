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
	// InstanceEquipment
	// ===================================================================================
    [Serializable]
    public class InstanceEquipment : InventorySlot {
    
    	[NonSerialized]private TemplateEquipment _template;
      	
      	// -------------------------------------------------------------------------------
		// template
		// -------------------------------------------------------------------------------  	
    	public TemplateEquipment template {
    		get { return _template; }
    		set { 
    			_template = value;
    			name = _template.name;
    			icon = _template.icon;
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
		// loadTemplate
		// -------------------------------------------------------------------------------
        public override void loadTemplate() {
       		if (string.IsNullOrEmpty(name)) return;
       		template = DictionaryEquipment.Get(name);       
        }
        
        // -------------------------------------------------------------------------------
		// fullName
		// -------------------------------------------------------------------------------      
        public override string fullName {
        	get {
        		if (template != null)
        			return template.fullName; 
        		return name;
        	}
        }
        
        // -------------------------------------------------------------------------------
    	
    }
    
    // ===================================================================================
    
}