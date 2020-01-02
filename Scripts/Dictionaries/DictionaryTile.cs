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
    public class DictionaryTile {
    
        public static Dictionary<string, DungeonTileBase> dict;
		static bool loaded;
		
		// -------------------------------------------------------------------------------
		// load
		// -------------------------------------------------------------------------------
        public static void load() {
        
        	if (loaded) return;
        	
            dict = new Dictionary<string, DungeonTileBase>();
            
            DungeonTileBase[] tmpls = Resources.LoadAll<DungeonTileBase>(@Finder.txt.folderNames.folderTiles);
			foreach (DungeonTileBase tmpl in tmpls) {
				if (!dict.ContainsKey(tmpl.name)) {
					dict.Add(tmpl.name, tmpl);
					
				}
			}
			
			loaded = true;
			
        }
        
  		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public static DungeonTileBase Get(string name) {
        	load();
        	if (dict.ContainsKey(name))
        		return dict[name];
        	return null;
        }  
        
		// -------------------------------------------------------------------------------
            
    }
}