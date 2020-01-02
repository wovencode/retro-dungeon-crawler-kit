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
	// DictionaryCharacterHero
	// ===================================================================================
    public static class DictionaryCharacterHero {
    
        public static Dictionary<string, TemplateCharacterHero> dict { get; private set; }
		static bool loaded;
		
 		// -------------------------------------------------------------------------------
		// load
		// -------------------------------------------------------------------------------
        public static void load() {
        
			if (loaded) return;
			        
            dict = new Dictionary<string, TemplateCharacterHero>();
            
            TemplateCharacterHero[] tmpls = Resources.LoadAll<TemplateCharacterHero>(@Finder.txt.folderNames.folderHeroes);
			foreach (TemplateCharacterHero tmpl in tmpls) {
				if (!dict.ContainsKey(tmpl.name)) {
					dict.Add(tmpl.name, tmpl);
					
				}
			}
			
			loaded = true;
			
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
   		public static TemplateCharacterHero GetHero(string name) {
   			load();
            if (dict.ContainsKey(name))
                return dict[name];
			return null;
        }     
        
 		// -------------------------------------------------------------------------------
 		
    }
}