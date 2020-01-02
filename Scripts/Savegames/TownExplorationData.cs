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

    [Serializable]
    public class TownExplorationData {
    
        protected List<string> ExploredTowns { get; private set; }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public TownExplorationData() {
            ExploredTowns = new List<string>();
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public void AddTown(TemplateMetaTown town) {
			if (town != null && !ExploredTowns.Contains(town.name)) {
				ExploredTowns.Add(town.name);
			}
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
   		public TemplateMetaTown GetTown(string name) {
            string town = ExploredTowns.Find(x => x == name);
            return DictionaryTown.Get(town);
        }     
	
		// -------------------------------------------------------------------------------
    
    }
}