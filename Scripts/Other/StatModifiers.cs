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
	// STAT MODIFIERS
	// ===================================================================================
    [Serializable]
    public class StatModifiers {
    
    	public CharacterAttributeModifier[]		attributes;
		public StatModifiersDerivedStats 		statistics;
		public StatModifiersOther				other;
		public StatModifiersSpecial				special;
		public ElementalResistance[] 			resistances;
		public CombatStyleModifier[]			combatStyles;
		
        public StatModifiers() { }

  		// -------------------------------------------------------------------------------
		// applyModifiers
		// -------------------------------------------------------------------------------
		public void applyModifiers(CharacterBase target) {

			// -- Attribute Modifiers
			foreach (CharacterAttributeModifier mod_attrib in attributes) {
				CharacterAttribute target_attrib = target.stats.attributes.FirstOrDefault(x => x.template == mod_attrib.template);
				if (target_attrib != null)
					target_attrib.value += mod_attrib.percentageBased ? (target_attrib.value * mod_attrib.value) : mod_attrib.value;
			}
			
			// -- Combat Style Modifiers
			foreach (CombatStyleModifier style in combatStyles) {
				CombatStyle target_style = target.stats.combatStyles.FirstOrDefault(x => x.template == style.template);
				int attackValue = style.percentageBased ? (int)(target_style.attackValue * style.attackValue) : style.attackValue;
				int defenseValue = style.percentageBased ? (int)(target_style.defenseValue * style.defenseValue) : style.defenseValue;
				target_style.attackValue += attackValue;
				target_style.defenseValue += defenseValue;
			}
			
			// -- Statistics Modifiers
			target.MaxHP 					+= statistics.percentageBased ? (int)(target.stats.MaxHP 			* statistics.MaxHP) : statistics.MaxHP;
			target.MaxMP 					+= statistics.percentageBased ? (int)(target.stats.MaxMP 			* statistics.MaxMP) : statistics.MaxMP;
			target.stats.Accuracy 			+= statistics.percentageBased ? (int)(target.stats.Accuracy 		* statistics.Accuracy) : statistics.Accuracy;
			target.stats.Resistance 		+= statistics.percentageBased ? (int)(target.stats.Resistance 		* statistics.Resistance) : statistics.Resistance;
			target.stats.CritRate 			+= statistics.percentageBased ? (int)(target.stats.CritRate 		* statistics.CritRate) : statistics.CritRate;
			target.stats.CritFactor 		+= statistics.percentageBased ? (int)(target.stats.CritFactor 		* statistics.CritFactor) : statistics.CritFactor;
			target.stats.BlockRate 			+= statistics.percentageBased ? (int)(target.stats.BlockRate 		* statistics.BlockRate) : statistics.BlockRate;
			target.stats.BlockFactor 		+= statistics.percentageBased ? (int)(target.stats.BlockFactor		* statistics.BlockFactor) : statistics.BlockFactor;
			target.stats.Initiative 		+= statistics.percentageBased ? (int)(target.stats.Initiative		* statistics.Initiative) : statistics.Initiative;
			target.stats.Attacks 			+= statistics.percentageBased ? (int)(target.stats.Attacks		* statistics.Attacks) : statistics.Attacks;
		
			// -- Resistance Modifiers
			foreach (ElementalResistance resistance in resistances) {
				target.stats.resistances.FirstOrDefault(x => x.template == resistance.template).value += resistance.value;
			}
	
		}
		
  		// -------------------------------------------------------------------------------
		// getDescription
		// -------------------------------------------------------------------------------
        public string GetDescription(string separation = "\n", bool showNull = false) {
            
            string text = "";
            string attackValueString = "";
            string defenseValueString = "";
            
            foreach (CharacterAttributeModifier attrib in attributes) {
				if (showNull || attrib.value > 0)
					text += string.Format("{0}: {1}{2}", attrib.template.fullName, attrib.value, separation);
			}
            
            if (showNull || statistics.MaxHP > 0)
                text += string.Format(Finder.txt.getDerivedStatName(DerivedStatType.maxHP,false)+": {0}{1}", statistics.MaxHP, separation);
            if (showNull || statistics.MaxMP > 0)
                text += string.Format(Finder.txt.getDerivedStatName(DerivedStatType.maxMP,false)+": {0}{1}", statistics.MaxMP, separation);
            
            // -- Combat Style Modifiers
			foreach (CombatStyleModifier style in combatStyles) {
				attackValueString = style.percentageBased ? style.attackValue.ToString()+"%" : style.attackValue.ToString();
				defenseValueString = style.percentageBased ? style.defenseValue.ToString()+"%" : style.defenseValue.ToString();
			}
			
            if (showNull || attackValueString != "")
                text += string.Format("{0}: {1}{2}", Finder.txt.basicDerivedStatNames.fullAttack, attackValueString, separation);
                
            if (showNull || defenseValueString != "")
                text += string.Format("{0}: {1}{2}", Finder.txt.basicDerivedStatNames.fullDefense, defenseValueString, separation);
                
            if (showNull || statistics.Accuracy > 0)
                text += string.Format(Finder.txt.getDerivedStatName(DerivedStatType.accuracy,false)+": {0}%{1}", statistics.Accuracy, separation);
            if (showNull || statistics.Resistance > 0)
                text += string.Format(Finder.txt.getDerivedStatName(DerivedStatType.resistance,false)+": {0}%{1}", statistics.Resistance, separation);
            if (showNull || statistics.CritRate > 0)
                text += string.Format(Finder.txt.getDerivedStatName(DerivedStatType.critRate,false)+": {0}%{1}", statistics.CritRate, separation);
           	if (showNull || statistics.CritFactor > 0)
                text += string.Format(Finder.txt.getDerivedStatName(DerivedStatType.critFactor,false)+": {0}%{1}", statistics.CritFactor, separation);
            if (showNull || statistics.BlockRate > 0)
                text += string.Format(Finder.txt.getDerivedStatName(DerivedStatType.blockRate,false)+": {0}%{1}", statistics.BlockRate, separation);
            if (showNull || statistics.BlockFactor > 0)
                text += string.Format(Finder.txt.getDerivedStatName(DerivedStatType.blockFactor,false)+": {0}%{1}", statistics.BlockFactor, separation);
            
            // -- Resistance Modifiers
			foreach (ElementalResistance resistance in resistances) {
				if (showNull || resistance.value > 0)
					text += string.Format(resistance.template.fullName +" "+ Finder.txt.namesVocabulary.resistance+": {0}%{1}", resistance.value*100, separation);
			}
            
            if (!string.IsNullOrEmpty(text))
                text = "<b>"+Finder.txt.namesVocabulary.statistics+"</b>" + separation + text;
            
            return text;
        }
        
    }
    
 	// ===================================================================================
	// TINY CLASSES (only used by StatModifiers)
	// ===================================================================================
  	
    // -----------------------------------------------------------------------------------
	// StatModifiersDerivedStats
	// -----------------------------------------------------------------------------------
    [Serializable]
    public class StatModifiersDerivedStats {
    	public bool percentageBased;
    	public int MaxHP;
        public int MaxMP;
		public int Accuracy;
		public int Resistance;
		public float CritRate;
		public float CritFactor;
		public float BlockRate;
		public float BlockFactor;
		public int Initiative;
		public int Attacks;
    }
      
    // -----------------------------------------------------------------------------------
	// StatModifiersOther
	// -----------------------------------------------------------------------------------
    [Serializable]
    public class StatModifiersOther {
    	public float depleteHP;
    	public float depleteMP;
    	public bool changeElement;
    	public TemplateMetaElement element;
    }
      
    // -----------------------------------------------------------------------------------
	// StatModifiersSpecial
	// -----------------------------------------------------------------------------------
    [Serializable]
    public class StatModifiersSpecial {
    	public bool silence;
    	public bool stun;
    }
    
    // ===================================================================================
    
}