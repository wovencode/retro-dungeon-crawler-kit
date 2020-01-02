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
	// CharacterAttribute
	// -----------------------------------------------------------------------------------
	[Serializable]
    public class CharacterAttribute {
    	
    	[NonSerialized]private TemplateMetaAttribute _template;
    	public int value;
    	protected string name;
    	
    	// -------------------------------------------------------------------------------
		// template
		// -------------------------------------------------------------------------------
    	public TemplateMetaAttribute template {
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
       		template = DictionaryAttribute.Get(name);
        }
        
        // -------------------------------------------------------------------------------
    	
    	
    }

	// -------------------------------------------------------------------------------
	
}