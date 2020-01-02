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
using WoCo.DungeonCrawler;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// TEMPLATE ATTRIBUTE
	// ===================================================================================
    [Serializable]
    [CreateAssetMenu(fileName = "Unnamed Attribute", menuName = "RDCK/Meta/New Attribute")]
    public class TemplateMetaAttribute : TemplateBase {
    
    	public Sprite								icon;
    	public string[]								_abbrName;
    	
    	public DerivedStatModifier[]	 			statModifiers;
		public ElementalResistanceModifier[]		resistanceModifiers;
		public CombatStyleModifier[]				combatStyleModifiers;
		
		// -------------------------------------------------------------------------------
		// abbrName
		// -------------------------------------------------------------------------------
		public string abbrName {
			get {
				return _abbrName[Finder.txt.language];
			}
		}
		
	}
	
    // ===================================================================================
    
}