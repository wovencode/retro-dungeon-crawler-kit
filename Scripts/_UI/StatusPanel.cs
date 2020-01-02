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
	// STATUS PANEL
	// ===================================================================================
    public abstract class StatusPanel : MonoBehaviour {
    
    	public Image portraitImg;
		public Image[] buffImg;
		public CharacterBase _character { get; set; }
		public Flash flashComponent;
		public iTweenEffects iTweenComponent;
		
		protected int prevHP;
		protected int prevMP;
       
       	public virtual CharacterBase character { get; set; }
		protected virtual void UpdateAnimation() {}
		
		// -----------------------------------------------------------------------------------
		// UpdatePortrait
		// -----------------------------------------------------------------------------------
        public virtual void UpdatePortrait(bool animated=true) {
        	
        	if (portraitImg == null) return;
        	
        	if (character.HP >= character.MaxHP) {
        	
        		portraitImg.sprite 	= character.template.portraits[0];
        		
        	} else if (character.HP > 0) {
        	
        		if (character.template.portraits.Length == 1) {
        		
        			portraitImg.sprite 	= character.template.portraits[0];
        			
        		} else {
        	
        			float x = character.MaxHP / character.template.portraits.Length;
        			int j = 0;
        		
        			for (int i = character.template.portraits.Length; i --> 0; ) {
        				
        				if (character.HP >= x*i) {
        					portraitImg.sprite 	= character.template.portraits[j];
        					break;
        				}
        				j++;
        			}
        		
        		}
        		
        	} else {
        		if (character.template.portraitDead != null) {
        			portraitImg.sprite 	= character.template.portraitDead;
        		} else {
        			Destroy(portraitImg.gameObject, Finder.battle.combatDelay);
        		}
        	}
        	
        	if (animated) UpdateAnimation();
        	
        }        
        
		// -----------------------------------------------------------------------------------
		// UpdateBuffs
		// -----------------------------------------------------------------------------------
		protected virtual void UpdateBuffs() {
			
			if (buffImg.Length == 0) return;
			
			foreach (Image buff in buffImg) {
				buff.sprite = null;
				buff.gameObject.SetActive(false);
			}
			
			var i = 0;
			
			foreach (InstanceStatus buff in character.buffs) {
				if (buff.template.icon != null) {
					buffImg[i].sprite = buff.template.icon;
					buffImg[i].gameObject.SetActive(true);
				}
				i++;
				if (i > buffImg.Length) return;
			}
		
		}
		
        // -----------------------------------------------------------------------------------
 
    }
}