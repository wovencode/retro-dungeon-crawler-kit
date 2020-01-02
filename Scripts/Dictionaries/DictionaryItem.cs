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

	// -------------------------------------------------------------------------------
	// DictionaryItem
	// -------------------------------------------------------------------------------
    public class DictionaryItem {
    
        public static Dictionary<string, TemplateItem> dict;
		static bool loaded;
		
		// -------------------------------------------------------------------------------
		// load
		// -------------------------------------------------------------------------------
        public static void load() {
        
        	if (loaded) return;
        	
            dict = new Dictionary<string, TemplateItem>();
            
            TemplateItem[] tmpls = Resources.LoadAll<TemplateItem>(@Finder.txt.folderNames.folderItems);
			foreach (TemplateItem tmpl in tmpls) {
				if (!dict.ContainsKey(tmpl.name)) {
					dict.Add(tmpl.name, tmpl);
					
				}
			}
			
			loaded = true;
			
        }
        
  		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public static TemplateItem Get(string name) {
        	load();
        	if (dict.ContainsKey(name))
        		return dict[name];
        	return null;
        }
 
		// -------------------------------------------------------------------------------
            
    }
}