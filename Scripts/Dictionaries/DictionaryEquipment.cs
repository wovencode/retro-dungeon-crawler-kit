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
	// DictionaryEquipment
	// ===================================================================================
    public static class DictionaryEquipment {
        
        public static Dictionary<string, TemplateEquipment> dict { get; private set; }
        static bool loaded;
        
		// -------------------------------------------------------------------------------
		// load
		// -------------------------------------------------------------------------------
        public static void load() {
        
			if (loaded) return;
			        
            dict = new Dictionary<string, TemplateEquipment>();
            
            TemplateEquipment[] tmpls = Resources.LoadAll<TemplateEquipment>(@Finder.txt.folderNames.folderEquipment);
			foreach (TemplateEquipment tmpl in tmpls) {
				if (!dict.ContainsKey(tmpl.name)) {
					dict.Add(tmpl.name, tmpl);
				}
			}
            
            loaded = true;
            
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public static TemplateEquipment Get(string name) {
        	load();
            if (dict.ContainsKey(name))
                return dict[name];
            return null;
        }
 
        // -------------------------------------------------------------------------------
    
    }
    
}