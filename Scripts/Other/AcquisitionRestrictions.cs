// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoCo.DungeonCrawler {

    [Serializable]
    public class AcquisitionRestrictions {
    
        public bool exclude;    
		public TemplateMetaDungeon[] RequiresExploredDungeons;
		public bool requiresAllDungeons;
		public TemplateMetaTown[] RequiresExploredTowns;
		public bool requiresAllTowns;
		public bool RequiresTownsAndDungeons;
		public int minLevel;
		public int maxLevel;

    }
    
}