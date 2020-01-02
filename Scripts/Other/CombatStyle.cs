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
	// CombatStyle
	// -----------------------------------------------------------------------------------
    [Serializable]
    public class CombatStyle {
    
    	[NonSerialized]private TemplateMetaCombatStyle _template;
    	public int attackValue;
    	public int defenseValue;
    	protected string name;
    	
    	// -------------------------------------------------------------------------------
		// template
		// -------------------------------------------------------------------------------
    	public TemplateMetaCombatStyle template {
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
       		template = DictionaryCombatStyle.Get(name);
        }
        
    }
    
	// -------------------------------------------------------------------------------
	
}