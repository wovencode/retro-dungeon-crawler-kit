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
	// DictionaryCombatStyle
	// ===================================================================================
    public static class DictionaryCombatStyle {
    
		public static Dictionary<string, TemplateMetaCombatStyle> dict { get; private set; }
		static bool loaded;
		
		// -------------------------------------------------------------------------------
		// load
		// -------------------------------------------------------------------------------
        public static void load() {
        
			if (loaded) return;
			        
            dict = new Dictionary<string, TemplateMetaCombatStyle>();
            
            TemplateMetaCombatStyle[] tmpls = Resources.LoadAll<TemplateMetaCombatStyle>(@Finder.txt.folderNames.folderCombatStyles);
			foreach (TemplateMetaCombatStyle tmpl in tmpls) {
				dict.Add(tmpl.name, tmpl);
			}
			
			loaded = true;
			
        }
        
        // -------------------------------------------------------------------------------
		// Get
		// -------------------------------------------------------------------------------
        public static TemplateMetaCombatStyle Get(string name) {
        	load();
            if (dict.ContainsKey(name))
                return dict[name];
            return null;
        }

        // -------------------------------------------------------------------------------
        
    }

}