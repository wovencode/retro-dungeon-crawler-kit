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
	// DictionaryGossip
	// ===================================================================================
    public static class DictionaryGossip {
    
		public static Dictionary<string, TemplateGossip> dict { get; private set; }
		static bool loaded;
		
		// -------------------------------------------------------------------------------
		// load
		// -------------------------------------------------------------------------------
        public static void load() {
        
			if (loaded) return;
			        
            dict = new Dictionary<string, TemplateGossip>();
            
            TemplateGossip[] tmpls = Resources.LoadAll<TemplateGossip>(@Finder.txt.folderNames.folderGossip);
			foreach (TemplateGossip tmpl in tmpls) {
				dict.Add(tmpl.name, tmpl);
			}
			
			loaded = true;
			
        }
        
        // -------------------------------------------------------------------------------
		// Get
		// -------------------------------------------------------------------------------
        public static TemplateGossip Get(string name) {
        	load();
            if (dict.ContainsKey(name))
                return dict[name];
            return null;
        }
        
        // -------------------------------------------------------------------------------
		// GetGossip
		// -------------------------------------------------------------------------------
        public static IEnumerable<string> GetGossip(ShopType shopType) {
        	load();
        	return dict.Values.Where(x => x.ShopListingRequirementsMet &&
                x.shopType == shopType)
                .Select(x => x.text);
        }

        // -------------------------------------------------------------------------------
        
    }

}