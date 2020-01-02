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
	// DictionaryAttribute
	// ===================================================================================
    public static class DictionaryAttribute {
    
		public static Dictionary<string, TemplateMetaAttribute> dict { get; private set; }
		static bool loaded;
		
		// -------------------------------------------------------------------------------
		// load
		// -------------------------------------------------------------------------------
        public static void load() {
        
        	if (loaded) return;
        	
            dict = new Dictionary<string, TemplateMetaAttribute>();
            
            TemplateMetaAttribute[] tmpls = Resources.LoadAll<TemplateMetaAttribute>(@Finder.txt.folderNames.folderAttributes);
			foreach (TemplateMetaAttribute tmpl in tmpls) {
				dict.Add(tmpl.name, tmpl);
			}
			
			loaded = true;
			
        }
        
        // -------------------------------------------------------------------------------
		// Get
		// -------------------------------------------------------------------------------
        public static TemplateMetaAttribute Get(string name) {
        	load();
            if (dict.ContainsKey(name))
                return dict[name];
            return null;
        }

        // -------------------------------------------------------------------------------
        
    }

}