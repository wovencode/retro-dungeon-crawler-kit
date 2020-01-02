// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System.Collections;
using System.ComponentModel;
using System;
using System.Linq;
using System.Collections.Generic;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// CharacterHero
	// ===================================================================================
    [Serializable]
    public class CharacterHero : CharacterBase, INotifyPropertyChanged {
    
        
		// -------------------------------------------------------------------------------
		// DefaultInitialize
		// -------------------------------------------------------------------------------
        public override void DefaultInitialize() {
        	
        	base.DefaultInitialize();
        	
            Finder.navi.PlayerMoved += playerNavigation_PlayerMoved;

            AttributePoints = 0;
            AbilityPoints = 0;
        
            stats.XPToNextLevel = RPGHelper.NextLvlExp(Level);

        }
        
		// -------------------------------------------------------------------------------
		// HeroTurn
		// -------------------------------------------------------------------------------      
        public void HeroTurn() {
			
			// -- Stun and/or flee check
			
			if (buffs.Any(x => x.template.statModifiers.special.stun == true) || IsFleeing || !IsAlive) {
                ActionPassTurn();
            } else {
            	Finder.ui.ActivateBattleCommands(true);
            }
            
            
        }

       	// ===============================================================================
		// EVENTS
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// playerNavigation_PlayerMoved
		// -------------------------------------------------------------------------------
        void playerNavigation_PlayerMoved(Vector3 obj) {

             if (buffs != null && buffs.Count > 0) {
                buffs.ForEach(x => x.remainingDuration -= Finder.config.buffDurationPerStep);
                buffs.ForEach(x => x.StopEffect());
                buffs.RemoveAll(x => !x.template.permanent && x.remainingDuration <= 0);
                CalculateDerivedStats();
                base.UpdateBuffs();
            }
            
        }
        
        // -------------------------------------------------------------------------------
        
    }
}