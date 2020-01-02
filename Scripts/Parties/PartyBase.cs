// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.ComponentModel;

namespace WoCo.DungeonCrawler {

 	// ===================================================================================
	// PartyBase
	// ===================================================================================
	[Serializable]
	public class PartyBase : MonoBehaviour {
    	
    	public List<CharacterBase> characters { get; set; }
      	
    	// -------------------------------------------------------------------------------
		// CharacterPartyBase
		// -------------------------------------------------------------------------------
     	public PartyBase() {
     		characters = new List<CharacterBase>();   
     	}
     	
  		// -------------------------------------------------------------------------------
		// DefaultInitialize
		// -------------------------------------------------------------------------------
        public virtual void DefaultInitialize() {
        	
        	
        }
        
		// ===============================================================================
		// FUNCTIONS
		// ===============================================================================

		// -------------------------------------------------------------------------------
		// RestoreToFull
		// -------------------------------------------------------------------------------
 		public void RestoreToFull() {
            foreach (CharacterBase character in characters)
        		character.RestoreToFull();
        }

		// -------------------------------------------------------------------------------
		// RestoreMP
		// -------------------------------------------------------------------------------
        public void RestoreMP(int amount) {
            foreach (CharacterBase character in characters)
        		character.RestoreMP(amount);
        }

      	// -------------------------------------------------------------------------------
		// RestoreHP
		// -------------------------------------------------------------------------------
		public void RestoreHP(int amount) {
			foreach (CharacterBase character in characters)
        		character.RestoreHP(amount);
        }
        
      	// -------------------------------------------------------------------------------
		// InflictDamage
		// -------------------------------------------------------------------------------
        public void InflictDamage(int amount) {
        	foreach (CharacterBase character in characters)
        		character.InflictDamage(amount);
        }

         // -------------------------------------------------------------------------------
		// InflictBuffs
		// -------------------------------------------------------------------------------
      	public bool InflictBuffs(TemplateStatus[] buffs, int accuracy, bool force=false, bool remove=false) {
        	bool success = false;
        	foreach (CharacterBase character in characters)
        		success = character.InflictBuffs(buffs, accuracy, force, remove);
        	return success;
        }
        
        // -------------------------------------------------------------------------------
		// InflictBuff
		// -------------------------------------------------------------------------------
        public bool InflictBuff(TemplateStatus buff, int accuracy, bool force=false, bool remove=false) {
			bool success = false;
			foreach (CharacterBase character in characters)
        		success = character.InflictBuff(buff, accuracy, force, remove);
        	return success;
        }
               
 	    // ===============================================================================
		// GETTERS / SETTERS
		// ===============================================================================
        
        // -------------------------------------------------------------------------------
		// IsFullHP
		// -------------------------------------------------------------------------------
        public bool IsFullHP {
        	get {
        		var success = false;
        		foreach (CharacterBase character in characters) {
        			success = (character.HP >= character.MaxHP);
        		}
        		return success;
        	}
        }
        
        // -------------------------------------------------------------------------------
		// IsFullMP
		// -------------------------------------------------------------------------------
        public bool IsFullMP {
        	get {
        		var success = false;
        		foreach (CharacterBase character in characters) {
        			success = (character.MP >= character.MaxMP);
        		}
        		return success;
        	}
        }
        
       	// -------------------------------------------------------------------------------
		// IsAlive
		// -------------------------------------------------------------------------------
        public bool IsAlive {
        	get {
        		var hp = 0;
        		foreach (CharacterBase character in characters) {
       				hp += character.HP;
       			}
        		return (hp > 0);
        	}
        }
               
        // ===============================================================================
		// MISC FUNCTIONS
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// Clear
		// -------------------------------------------------------------------------------
		public void Clear() {
			characters.Clear();
		}
		
		// -------------------------------------------------------------------------------
		// HasMember
		// -------------------------------------------------------------------------------
		public bool HasMember(TemplateCharacterBase tmpl) {
			return characters.Any(x => x.template == tmpl);
		}
		
		// -------------------------------------------------------------------------------
		// GetMember
		// -------------------------------------------------------------------------------
		public CharacterBase GetMember(string name) {
			if (characters.Any(x => x.template.name == name))
				return characters.FirstOrDefault(x => x.template.name == name);
			return null;
		}
		
		// -------------------------------------------------------------------------------
		// AverageLevel
		// -------------------------------------------------------------------------------
		public int AverageLevel {
			get {
				int tmp = 0;
				foreach (CharacterBase character in characters)
       				tmp += character.Level;
				tmp = tmp / characters.Count;
				return tmp;
			}
		}
		
		// -------------------------------------------------------------------------------
		// AverageExperience
		// -------------------------------------------------------------------------------
		public int AverageExperience {
			get {
				int tmp = 0;
				foreach (CharacterBase character in characters)
       				tmp += character.Level;
				tmp = tmp / characters.Count;
				return tmp;
			}
		}
		
		// -------------------------------------------------------------------------------
		// TotalExperience
		// -------------------------------------------------------------------------------
		public virtual int TotalExperience {
			get {
				int tmp = 0;
				foreach (CharacterBase character in characters)
       				tmp += character.Level;
				return tmp;
			}
		}
		
		// -------------------------------------------------------------------------------
		// ForceClearBuffs
		// -------------------------------------------------------------------------------
		public void ForceClearBuffs() {
			
			foreach (CharacterBase character in characters) {
       			character.buffs.Clear();
       		}
			
		}
				
		// -------------------------------------------------------------------------------
		// CalculateDerivedStats
		// -------------------------------------------------------------------------------
		public void CalculateDerivedStats() {
			
			foreach (CharacterBase character in characters) {
       			character.CalculateDerivedStats();
       		}
			
		}
		
        // -------------------------------------------------------------------------------
		// BattlePreperations
		// -------------------------------------------------------------------------------
		public void BattlePreperations() {
			
			foreach (CharacterBase character in characters) {
       			character.BattlePreperations();
       		}
			
		}
		
 	    // ===============================================================================
		// ABILITIES
		// ===============================================================================
		
  		// -------------------------------------------------------------------------------
		// HasAbility
		// -------------------------------------------------------------------------------
       	public CharacterBase HasAbility(TemplateSkill tmpl, int level) {
       		foreach (CharacterBase character in characters) {
       			if (character.HasAbility(tmpl, level)) return character;
       		}
       		return null;	
       	}

		// -------------------------------------------------------------------------------
		// CanCastAbility
		// -------------------------------------------------------------------------------
		public bool CanCastAbility(TemplateSkill tmpl, int level) {
            var success = false;
            foreach (CharacterBase character in characters) {
       			success = character.CanCastAbility(tmpl, level);
       		}
       		return success;	
        }	 
        
 		// -------------------------------------------------------------------------------
		// CanUseItem
		// -------------------------------------------------------------------------------
		public bool CanUseItem(TemplateItem tmpl) {
            foreach (CharacterBase character in characters) {
       			if (character.CanUseItem(tmpl)) return true;
       		}
       		return false;	
        }	    
		
		// -------------------------------------------------------------------------------
		// CanEquipItem
		// -------------------------------------------------------------------------------
		public bool CanEquipItem(TemplateEquipment tmpl) {
            foreach (CharacterBase character in characters) {
       			if (tmpl.CanEquip(character)) return true;
       		}
       		return false;	
        }	  
		
 	    // ===============================================================================
		// SETTERS
		// ===============================================================================
		
		
	    // ===============================================================================
		// GETTERS
		// ===============================================================================
				
		// -------------------------------------------------------------------------------
		// getInLevelRange
		// -------------------------------------------------------------------------------
		public bool getInLevelRange(int minLevel, int maxLevel) {
			if (minLevel == 0 && maxLevel == 0) return true;
			return (AverageLevel >= minLevel || minLevel == 0) && (AverageLevel <= maxLevel || maxLevel == 0);
		}
		
		// -------------------------------------------------------------------------------
		// Level
		// -------------------------------------------------------------------------------
		public int Level {
			get {
				return characters.Sum(x => x.Level);
			}
		}
        
    }

    // ===================================================================================
}