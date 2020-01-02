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

   	// -----------------------------------------------------------------------------------
	// ElementalResistance
	// -----------------------------------------------------------------------------------
	[Serializable]
    public class ElementalResistance {
    
    	[NonSerialized]private TemplateMetaElement _template;
    	[Range(-10,10)]public float value;
    	protected string name;
    	
    	// -------------------------------------------------------------------------------
		// template
		// -------------------------------------------------------------------------------
    	public TemplateMetaElement template {
    		get { return _template; }
    		set { 
    			_template = value;
    			name = _template.name;
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
		// loadTemplate
		// -------------------------------------------------------------------------------
        public void loadTemplate() {
        	if (string.IsNullOrEmpty(name)) return;
       		template = DictionaryElement.Get(name);
        }
        
        // -------------------------------------------------------------------------------
        
    }
    

	// -------------------------------------------------------------------------------
	
}