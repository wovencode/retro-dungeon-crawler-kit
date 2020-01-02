// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// InstanceStatus
	// ===================================================================================
    [Serializable]
    public class InstanceStatus : InstanceBase {
    
        public float remainingDuration;
        [NonSerialized]private TemplateStatus _template;

		// -------------------------------------------------------------------------------
		// StopEffect
		// -------------------------------------------------------------------------------
       	public void StopEffect(bool forced=false) {
        	/*if (template == null) return;
        	if (forced || (remainingDuration <= 0 && !template.permanent))
        		Finder.fx.StopEffect(template.visualEffect);*/
        }
    	
    	// -------------------------------------------------------------------------------
		// IsActive
		// -------------------------------------------------------------------------------
        public bool IsActive {
        	get {
        		return remainingDuration > 0 || template.permanent;
        	}
        }
        
        // -------------------------------------------------------------------------------
		// template
		// -------------------------------------------------------------------------------      
        public TemplateStatus template {
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
       		template = DictionaryStatus.Get(name);     
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