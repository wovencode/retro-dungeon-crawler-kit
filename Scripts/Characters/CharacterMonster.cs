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
	// CharacterMonster
	// ===================================================================================
    public class CharacterMonster : CharacterBase {

		public Sprite image 		{ get; set; }

		public List<LootDrop> lootContent;

		protected int prevHP;
		protected int prevMP;
		
       	// ===============================================================================
        // INITIALIZATION
        // ===============================================================================

        // -------------------------------------------------------------------------------
		// DefaultInitialize
		// -------------------------------------------------------------------------------
        public override void DefaultInitialize() {
        	
        	base.DefaultInitialize();
	
			parent.GetComponent<MonsterPanel>().DefaultInitialize(this);
			stats.XP = CalculateMonsterXP();
            AttributePoints = 0;
            AbilityPoints = 0;
            stats.XPToNextLevel = RPGHelper.NextLvlExp(Level);
			
        }
        
        // ===============================================================================
        // AI FUNCTIONS
        // ===============================================================================
   		
  		// -------------------------------------------------------------------------------
		// ProcessEnemyTurn
		// -------------------------------------------------------------------------------      
        public void EnemyTurn() {
			 
			
			if (IsAlive)
			{
			
				// -- Stun check
				if (buffs.Any(x => x.template.statModifiers.special.stun == true) || IsFleeing) {
					ActionPassTurn();
				}
				
				/*
				this can be replaced with a proper AI later
				*/
			
				if (UnityEngine.Random.value <= .3) {
				
					int c = abilities.Count;
				
					if (c > 0) {
						for (int i = 0; i < c; i++) {
							if (CanCastAbility(abilities[i].template, abilities[i].level)) {
								CommandCastSpell(abilities[i], RPGHelper.getRandomPlayer() );
								return;
							}
						}
					}
			
				}
			
				CommandAttack(RPGHelper.getRandomPlayer() );
             
            }
            
        }
        
        // ===============================================================================
        // OTHER
        // ===============================================================================
        
		// -------------------------------------------------------------------------------
		// CalculateMonsterXP
		// -------------------------------------------------------------------------------      
		public int CalculateMonsterXP() {
		
			int xp = ((TemplateCharacterMonster)template).experienceBonus;
			xp += stats.AttributesSum();
			xp += abilities.Count;
			
			return xp;
		
		}
		
		// -------------------------------------------------------------------------------   
        
    }
}