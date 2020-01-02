// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// DictionaryElement
	// ===================================================================================
    public static class DictionaryElement {
    
		public static Dictionary<string, TemplateMetaElement> dict { get; private set; }
		static bool loaded;
		
		// -------------------------------------------------------------------------------
		// load
		// -------------------------------------------------------------------------------
        public static void load() {
        
        	if (loaded) return;
        	
            dict = new Dictionary<string, TemplateMetaElement>();
            
            TemplateMetaElement[] tmpls = Resources.LoadAll<TemplateMetaElement>(@Finder.txt.folderNames.folderElements);
			foreach (TemplateMetaElement tmpl in tmpls) {
				dict.Add(tmpl.name, tmpl);
			}
			
			loaded = true;
			
        }
        
        // -------------------------------------------------------------------------------
		// Get
		// -------------------------------------------------------------------------------
        public static TemplateMetaElement Get(string name) {
        	load();
            if (dict.ContainsKey(name))
                return dict[name];
            return null;
        }
        
        // -------------------------------------------------------------------------------
        
    }

}