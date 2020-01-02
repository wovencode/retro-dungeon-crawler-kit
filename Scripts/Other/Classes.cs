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
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

	// -----------------------------------------------------------------------------------
	// AbilityList
	// -----------------------------------------------------------------------------------
   	[Serializable]
    public class AbilityList {
    	public List<AbilityLevel> abilities;
    }
  	
  	// -----------------------------------------------------------------------------------
	// EventChoice
	// -----------------------------------------------------------------------------------
  	public class EventChoice {
  		public string label;
  		public bool enabled;
  	}

    // -----------------------------------------------------------------------------------
	// CharacterAttributeWeight
	// -----------------------------------------------------------------------------------
	[Serializable]
    public class CharacterAttributeWeight {
    	/*
		Cannot be derived from "CharacterAttribute", as it would turn the "template"
		"NonSerialized". It would therefore not show up in the Inspector anymore.
		*/
    	public TemplateMetaAttribute template;
    	public int value;
    	[Range(0,1)]public float weight;
    }
    
    // -----------------------------------------------------------------------------------
	// CharacterAttributeModifier
	// -----------------------------------------------------------------------------------
	[Serializable]
    public class CharacterAttributeModifier {
    	/*
		Cannot be derived from "CharacterAttribute", as it would turn the "template"
		"NonSerialized". It would therefore not show up in the Inspector anymore.
		*/
    	public TemplateMetaAttribute template;
    	public int value;
    	public bool percentageBased;
    }
    
    // -----------------------------------------------------------------------------------
	// ElementalResistanceModifier
	// -----------------------------------------------------------------------------------
	[Serializable]
    public class ElementalResistanceModifier {
    	/*
		Cannot be derived from "ElementalResistance", as it would turn the "template"
		"NonSerialized". It would therefore not show up in the Inspector anymore.
		*/
    	public TemplateMetaElement template;
    	public float value;
    	public bool percentageBased;
    }
    
    // -----------------------------------------------------------------------------------
	// CombatStyleModifier
	// -----------------------------------------------------------------------------------
    [Serializable]
    public class CombatStyleModifier {
    	/*
		Cannot be derived from "ElementalResistance", as it would turn the "template"
		"NonSerialized". It would therefore not show up in the Inspector anymore.
		*/
		public TemplateMetaCombatStyle template;
    	public int attackValue;
    	public int defenseValue;
    	public bool percentageBased;
    }
    
   	// -----------------------------------------------------------------------------------
	// AbilityLevel
	// -----------------------------------------------------------------------------------
    [Serializable]
    public class AbilityLevel {
    	public TemplateSkill template;
    	public int level;
    }
    
    // -----------------------------------------------------------------------------------
	// DerivedStatModifier
	// -----------------------------------------------------------------------------------
    [Serializable]
    public class DerivedStatModifier : DerivedStat {
    	public bool percentageBased;
    }
    
    // -----------------------------------------------------------------------------------
	// DerivedStat
	// -----------------------------------------------------------------------------------
    [Serializable]
    public class DerivedStat {
    	public DerivedStatType template;
    	public float value;
    }

    // -----------------------------------------------------------------------------------
	// StatWeight
	// -----------------------------------------------------------------------------------
    [Serializable]	
    public class StatWeight {
        public TemplateMetaAttribute template;
        [Range(0,1)]public float weight;
        [HideInInspector]public float adjustedWeight;
    }

	// -------------------------------------------------------------------------------
	
}