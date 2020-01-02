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
	// TEMPLATE CHARACTER CLASS
	// ===================================================================================
    [CreateAssetMenu(fileName = "Unnamed Character Class", menuName = "RDCK/Templates/Characters/New Character Class")]
    public class TemplateCharacterClass : TemplateBase {
    
    	[Header("-=- Basic -=-")]
    	public Sprite icon;
    		
    	[Header("-=- Default Attributes -=-")]
    	public CharacterAttributeWeight[] attributes;
    	        
        [Header("-=- Default Resistances -=-")]
        public ElementalResistanceModifier[] resistances;
        
        [Header("-=- Default Derived Stats -=-")]
        public DerivedStat[] derivedStats;
        
        [Header("-=- Default Combat Styles -=-")]
        public CombatStyleModifier[] combatStyles;
        
        [Header("-=- Default Abilities -=-")]
		public AbilityLevel[] defaultAbilities;
		
		[Header("-=- Level-Up Abilities -=-")]
		public AbilityList[] levelupAbilities;
		
		// -------------------------------------------------------------------------------
		// GetDescription
		// -------------------------------------------------------------------------------
		public string GetDescription(string separation = "\n", bool showNull = true) {
			
			string text = "";
			
			foreach (CharacterAttributeWeight attrib in attributes) {
				if (showNull || attrib.value > 0)
					text += string.Format("{0}: {1}{2}", attrib.template.fullName, attrib.value, separation);
			}
			
			if (!string.IsNullOrEmpty(text))
				text = "<b>"+Finder.txt.namesVocabulary.statistics+"</b>" + separation + text;
		
			return text;
		}
	
	}

    // ===================================================================================
    
}