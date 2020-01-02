// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// MONSTER PANEL
	// ===================================================================================
    public class MonsterPanel : StatusPanel {

		// -----------------------------------------------------------------------------------
		// DefaultInitialize
		// -----------------------------------------------------------------------------------
        public void DefaultInitialize(CharacterBase chara) {
            character = chara;
        }
		
      	// -----------------------------------------------------------------------------------
		// character
		// -----------------------------------------------------------------------------------
        public override CharacterBase character {
        
        	get { return _character; }
        
        	set {
        		_character = value;
        		_character.PropertyChanged += monster_PropertyChanged;
        		UpdateAll(false);
        	}
        
        }
        
		// -----------------------------------------------------------------------------------
		// monster_PropertyChanged
		// -----------------------------------------------------------------------------------
        void monster_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
        
            switch (e.PropertyName) {
                
                case "HP":
                    UpdatePortrait();
                    break;
                    
                case "Buffs":
                	UpdateBuffs();
                	break;
            }
        }
        
		// -----------------------------------------------------------------------------------
		// UpdateAll
		// -----------------------------------------------------------------------------------
        public void UpdateAll(bool flashing=true) {
        	
        	if (character == null) return;
        	
        	prevHP				= character.HP;
        	prevMP				= character.MP;
        	
        	UpdatePortrait(false);
        	
            UpdateBuffs();
            
        }
        
		// -----------------------------------------------------------------------------------
		// UpdateAnimation
		// -----------------------------------------------------------------------------------
        protected override void UpdateAnimation() {
        	
        	if (flashComponent) {

        		if (prevHP > character.HP) {	
        			flashComponent.OnFlash(Finder.fx.flashColors.decreaseHPColor, false, character.HP - prevHP );
        			if (iTweenComponent) iTweenComponent.Shake();
        			
        		} else if (prevHP < character.HP) {
        			flashComponent.OnFlash(Finder.fx.flashColors.increaseHPColor, false, character.HP - prevHP);
        		
        		} else if (prevMP < character.MP) {
        			flashComponent.OnFlash(Finder.fx.flashColors.increaseMPColor, false, character.MP - prevMP);
        		}
        	
        	}
        	
        }
       
		// -----------------------------------------------------------------------------------
		// OnDestroy
		// -----------------------------------------------------------------------------------
        protected void OnDestroy() {
        	if (character != null)
            	character.PropertyChanged -= monster_PropertyChanged;
        }

        // -----------------------------------------------------------------------------------
 
    }
}