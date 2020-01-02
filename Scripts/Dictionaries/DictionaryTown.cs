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

	// -------------------------------------------------------------------------------
	// DictionaryTown
	// -------------------------------------------------------------------------------
    public static class DictionaryTown {
        
        public static Dictionary<string, TemplateMetaTown> dict { get; private set; }
        static bool loaded;
        
		// -------------------------------------------------------------------------------
		// load
		// -------------------------------------------------------------------------------
        public static void load() {
        
        	if (loaded) return;
        	
            dict = new Dictionary<string, TemplateMetaTown>();
            
            TemplateMetaTown[] currencies = Resources.LoadAll<TemplateMetaTown>(@Finder.txt.folderNames.folderTowns);
			foreach (TemplateMetaTown tmpl in currencies) {
				if (!dict.ContainsKey(tmpl.name)) {
					dict.Add(tmpl.name, tmpl);
				}
			}
            
            loaded = true;
            
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public static TemplateMetaTown Get(string name) {
        	load();
            if (dict.ContainsKey(name))
            	return dict[name];
            return null;
        }

        // -------------------------------------------------------------------------------

    }

}