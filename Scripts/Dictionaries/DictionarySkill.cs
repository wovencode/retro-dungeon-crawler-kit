// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// DictionarySkill
	// ===================================================================================
    public static class DictionarySkill {
    
        public static Dictionary<string, TemplateSkill> dict { get; private set; }
		static bool loaded;
		
		// -------------------------------------------------------------------------------
		// load
		// -------------------------------------------------------------------------------
        public static void load() {
        
        	if (loaded) return;
        	
            dict = new Dictionary<string, TemplateSkill>();
            
            TemplateSkill[] tmpls = Resources.LoadAll<TemplateSkill>(@Finder.txt.folderNames.folderSkills);
			foreach (TemplateSkill tmpl in tmpls) {
				if (!dict.ContainsKey(tmpl.name)) {
					dict.Add(tmpl.name, tmpl);
					
				}
			}
			
			loaded = true;
			
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
   		public static TemplateSkill Get(string name) {
   			load();
            if (dict.ContainsKey(name))
                return dict[name];
           	return null;
        }
        
        // -------------------------------------------------------------------------------  
 
    }
}