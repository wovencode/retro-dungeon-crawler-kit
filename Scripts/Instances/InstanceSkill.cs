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
	// InstanceSkill
	// ===================================================================================
    [Serializable]
    public class InstanceSkill : InstanceBase {
    
    	public int level;
    	
        [NonSerialized]private TemplateSkill _template;
                
        // -------------------------------------------------------------------------------
        // InstanceSkill (Constructor)
        // -------------------------------------------------------------------------------
        public InstanceSkill(TemplateSkill tmpl, int lvl) {
        	if (tmpl != null) {
        		template = tmpl;
        		name = tmpl.name;
        		icon = tmpl.icon;
        		level = lvl;
        	}
        }
        
      	// -------------------------------------------------------------------------------
		// template
		// -------------------------------------------------------------------------------      
        public TemplateSkill template {
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
       		template = DictionarySkill.Get(name);     
        }
        
         // -------------------------------------------------------------------------------
		// fullName
		// -------------------------------------------------------------------------------      
        public override string fullName {
        	get { return template.fullName; }
        }
        
        // -------------------------------------------------------------------------------
        
    }
    
    // ===================================================================================
    
}