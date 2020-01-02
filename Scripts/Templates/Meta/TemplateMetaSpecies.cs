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
	// TEMPLATE SHOP
	// ===================================================================================
    [Serializable]
    [CreateAssetMenu(fileName = "Unnamed Species", menuName = "RDCK/Meta/New Species")]
    public class TemplateMetaSpecies : TemplateBase {
    
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
		
		
		
		
	}
	
    // ===================================================================================
    
}