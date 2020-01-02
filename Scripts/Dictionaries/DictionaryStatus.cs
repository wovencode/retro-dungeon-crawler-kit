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
	// DictionaryStatus
	// ===================================================================================
    public static class DictionaryStatus {
    
        public static Dictionary<string, TemplateStatus> dict { get; private set; }
		static bool loaded;
		
		// -------------------------------------------------------------------------------
		// load
		// -------------------------------------------------------------------------------
        public static void load() {
        
        	if (loaded) return;
        	
            dict = new Dictionary<string, TemplateStatus>();
            
            TemplateStatus[] tmpls = Resources.LoadAll<TemplateStatus>(@Finder.txt.folderNames.folderStatus);
			foreach (TemplateStatus tmpl in tmpls) {
				if (!dict.ContainsKey(tmpl.name)) {
					dict.Add(tmpl.name, tmpl);
					
				}
			}
        
        	loaded = true;
        	
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
   		public static TemplateStatus Get(string name) {
   			load();
            if (dict.ContainsKey(name))
                return dict[name];
           	return null;
        }
                
        // -------------------------------------------------------------------------------  
 
    }
}