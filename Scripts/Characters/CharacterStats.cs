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
	// CHARACTER STATS
	// ===================================================================================
    [Serializable]
    public class CharacterStats {
 		
 		public List<CharacterAttribute> attributes;
 		public List<ElementalResistance> resistances;
        public List<CombatStyle> combatStyles;
        
        public int HP;
        public int MP;
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
         
        public int XP;
        public int level;
        public int attributePoints;
        public int abilityPoints;
        public int XPToNextLevel;
        
       	// -------------------------------------------------------------------------------
		// Initialize
		// -------------------------------------------------------------------------------
        public void Initialize() {
        	
        	attributes = new List<CharacterAttribute>();
        	
        	foreach (var tmpl in DictionaryAttribute.dict)
        		attributes.Add(new CharacterAttribute { template = tmpl.Value, value = 0 });
        	
        	resistances = new List<ElementalResistance>();
        	
        	foreach (var tmpl in DictionaryElement.dict)
        		resistances.Add(new ElementalResistance { template = tmpl.Value, value = 0 });
        	
        	combatStyles = new List<CombatStyle>();
        	
        	foreach (var tmpl in DictionaryCombatStyle.dict)
        		combatStyles.Add(new CombatStyle { template = tmpl.Value, attackValue = 0, defenseValue = 0 });
        		
        }
        
        // -------------------------------------------------------------------------------
		// AttributesSum
		// -------------------------------------------------------------------------------
        public int AttributesSum() {
        	return attributes.Sum(x => x.value);
        }
        
        // -------------------------------------------------------------------------------
		// loadTemplates
		// -------------------------------------------------------------------------------
        public void loadTemplates() {
        
        	foreach (CharacterAttribute attrib in attributes)
        		attrib.loadTemplate();
        	
        	foreach (ElementalResistance resist in resistances)
        		resist.loadTemplate();
        	
        	foreach (CombatStyle style in combatStyles)
        		style.loadTemplate();
        	
        }
        
        // -------------------------------------------------------------------------------
        
    }
    
}