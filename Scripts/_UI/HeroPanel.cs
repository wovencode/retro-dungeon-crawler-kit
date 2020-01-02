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
	// HERO PANEL
	// ===================================================================================
    public class HeroPanel : StatusPanel {
    	
    	public GameObject panel;
    	
        public Image pointsImg;
		public Text nameText;
		public Text levelText;
		public Slider healthSlider;
		public Slider manaSlider;
		
		protected int prevLV;
		protected int prevXP;

 		// -----------------------------------------------------------------------------------
		// character
		// -----------------------------------------------------------------------------------
        public override CharacterBase character {
        
        	get { return _character; }
        
        	set {
        		_character = value;
        		_character.PropertyChanged += HeroPropertyChanged;
        		UpdateAll(false);
        	}
        
        }
        
		// -----------------------------------------------------------------------------------
		// HeroPropertyChanged
		// -----------------------------------------------------------------------------------
        void HeroPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
        	
        	if (panel == null) return;
        	
            switch (e.PropertyName) {
            
                case "LV":
                    levelText.text 		= Finder.txt.basicDerivedStatNames.LV + character.Level.ToString();
                    UpdatePortrait();
                    prevLV				= character.Level;
                    break;
                    
                case "HP":
                    healthSlider.value 	= RPGHelper.getPercentageValue(character.HP, character.MaxHP);
                    UpdatePortrait();
                    prevHP				= character.HP;
                    break;
                    
                case "MP":
                    manaSlider.value 	= RPGHelper.getPercentageValue(character.MP, character.MaxMP);
                    UpdatePortrait();
                    prevMP				= character.MP;
                    break;
                    
                case "AttributePoints":
                    pointsImg.gameObject.SetActive(character.AttributePoints > 0);
                    break;
                    
                case "Buffs":
                	UpdateBuffs();
                	break;
            	
            	case "XP":
            		UpdatePortrait();
            		prevXP				= character.XP;
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
        	prevXP				= character.XP;
        	prevLV				= character.Level;
        	
        	UpdatePortrait(false);
        	
        	nameText.text 		= character.template.fullName;
            levelText.text		= Finder.txt.basicDerivedStatNames.LV + character.Level.ToString();
            healthSlider.value 	= RPGHelper.getPercentageValue(character.HP, character.MaxHP);
            manaSlider.value 	= RPGHelper.getPercentageValue(character.MP, character.MaxMP);
            pointsImg.gameObject.SetActive(character.AttributePoints > 0);
            
            UpdateBuffs();
            
        }
        
		// -----------------------------------------------------------------------------------
		// UpdateAnimation
		// -----------------------------------------------------------------------------------
        protected override void UpdateAnimation() {
        	
        	if (flashComponent) {
				
				// -- Lost HP
        		if (prevHP > character.HP) {
        			
        			if (character.template.portraitHurt != null) portraitImg.sprite = character.template.portraitHurt;
        			flashComponent.OnFlash(Finder.fx.flashColors.decreaseHPColor, false, character.HP - prevHP );
        			if (iTweenComponent) iTweenComponent.Shake();
        		
        		// -- Gained HP
        		} else if (prevHP < character.HP) {
        			
        			if (character.template.portraitHappy != null) portraitImg.sprite = character.template.portraitHappy;
        			flashComponent.OnFlash(Finder.fx.flashColors.increaseHPColor, false, character.HP - prevHP);
        		
        		// -- Gained MP
        		} else if (prevMP < character.MP) {
        		
        			if (character.template.portraitHappy != null) portraitImg.sprite = character.template.portraitHappy;
        			flashComponent.OnFlash(Finder.fx.flashColors.increaseMPColor, false,  character.MP - prevMP);
        		
        		// -- Gained Exp	
        		} else if (prevXP < character.XP) {
        			if (character.template.portraitHappy != null) portraitImg.sprite = character.template.portraitHappy;
        			flashComponent.OnFlash(Finder.fx.flashColors.increaseXPColor, false);
        		
        		// -- Gained Level
        		} else if (prevLV < character.Level) {
        			if (character.template.portraitHappy != null) portraitImg.sprite = character.template.portraitHappy;
        			flashComponent.OnFlash(Finder.fx.flashColors.increaseLVColor, false);
        		
        		}
        	
        	}
        	
        }
        		
		// -----------------------------------------------------------------------------------
		// ShowFullStatus
		// -----------------------------------------------------------------------------------
		protected void ShowFullStatus() {
			if (!Finder.battle.InBattle) {
            	Finder.ui.ForceSelection(new CharacterBase[] { character }, UIState.SelectCharacterToDisplayStatus);
			} else {
				Finder.audio.PlaySFX(SFX.ButtonCancel);
			}
		}
		
		// -----------------------------------------------------------------------------------
		// OnDestroy
		// -----------------------------------------------------------------------------------
        protected void OnDestroy() {
        	if (character != null)
            	character.PropertyChanged -= HeroPropertyChanged;
        }

        // -----------------------------------------------------------------------------------
 
    }
}