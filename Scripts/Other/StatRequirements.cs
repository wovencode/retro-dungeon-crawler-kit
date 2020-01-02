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

	// ===================================================================================
	// STAT REQUIREMENTS
	// ===================================================================================
    [Serializable]
    public class StatRequirements {
    
    	public CharacterAttribute[] attributeRequirements;
    	
		// -------------------------------------------------------------------------------
		// GetDescription
		// -------------------------------------------------------------------------------
		public string GetDescription(string separation = "\n", bool showNull = false) {
			
			string text = "";
			
			foreach (CharacterAttribute attrib in attributeRequirements) {
				if (showNull || attrib.value > 0)
					text += string.Format("{0}: {1}{2}", attrib.template.fullName, attrib.value, separation);
			}
			
			if (!string.IsNullOrEmpty(text))
				text = "<b>"+Finder.txt.namesVocabulary.requirements+"</b>" + separation + text;
		
			return text;
		}
		
        // -------------------------------------------------------------------------------
        
    }
}