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
	// DictionaryCharacterClass
	// ===================================================================================
    public static class DictionarySpecies {
        
        public static Dictionary<string, TemplateMetaSpecies> dict { get; private set; }
        public static Dictionary<string, StatWeights> weights { get; private set; }
        static bool loaded;
        
		// -------------------------------------------------------------------------------
		// load
		// -------------------------------------------------------------------------------
        public static void load() {
        
			if (loaded) return;
			        	
        	dict = new Dictionary<string, TemplateMetaSpecies>();
        	
        	TemplateMetaSpecies[] tmpls = Resources.LoadAll<TemplateMetaSpecies>(@Finder.txt.folderNames.folderSpecies);
			foreach (TemplateMetaSpecies tmpl in tmpls) {
				if (!dict.ContainsKey(tmpl.name)) {
					dict.Add(tmpl.name, tmpl);
					
				}
			}
			
            weights = new Dictionary<string, StatWeights>();
            
			foreach (TemplateMetaSpecies tmpl in tmpls) {
				if (!weights.ContainsKey(tmpl.name)) {
					weights.Add(tmpl.name, new StatWeights());
					
					foreach (CharacterAttributeWeight weight_attrib in tmpl.attributes)
						weights[tmpl.name].Add(new StatWeight { template = weight_attrib.template, weight = weight_attrib.weight });
						
				}
			}
            
            loaded = true;
            
        }
        
        // -------------------------------------------------------------------------------
		// GetSpecies
		// -------------------------------------------------------------------------------
        public static TemplateMetaSpecies GetSpecies(string name) {
        	load();
            if (weights.ContainsKey(name))
                return dict[name];
            return null;
        }
        
		// -------------------------------------------------------------------------------
		// GetWeight
		// -------------------------------------------------------------------------------
        public static StatWeights GetWeight(string name) {
        	load();
            if (weights.ContainsKey(name))
                return weights[name];
            return null;
        }
        
        // -------------------------------------------------------------------------------
    
    }
    
}