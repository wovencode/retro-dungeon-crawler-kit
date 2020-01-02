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

	// ===================================================================================
	// 
	// ===================================================================================
    public class MenuCharacterSkills : ShopBase<InstanceSkill> {
    	
    	public Text txtLabelSkillPoints;
    	public Text txtSkillPoints;
    	
    	public CharacterBase character { get; set; }
    	
    	protected InstanceSkill selectedSlot;
    	
    	// -------------------------------------------------------------------------------
		// CanActivate
		// -------------------------------------------------------------------------------
  		protected override bool CanActivate(InstanceSkill instance) {
  			return character.CanCastAbility(instance.template, instance.level);
  		}
  		
  		// -------------------------------------------------------------------------------
		// CanUpgrade
		// -------------------------------------------------------------------------------
  		protected override bool CanUpgrade(InstanceSkill instance) {
  			return character.stats.abilityPoints > 0 && instance.level < instance.template.maxLevel;
  		}
  		
  		// -------------------------------------------------------------------------------
		// CanTrash
		// -------------------------------------------------------------------------------
  		protected override bool CanTrash(InstanceSkill instance) {
  			return false;
  		}
  		
   		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected override void LoadContent() {
            this.content = character.abilities
                .OrderBy(x => x.template.baseManaCost)
                .ThenBy(x => x.template.fullName)
                .ToList();
            
            txtLabelSkillPoints.text 		= Finder.txt.basicDerivedStatNames.skillPoints;
            txtSkillPoints.text 			= character.AbilityPoints.ToString();
            
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected override string GetCost(InstanceSkill instance) {
            return instance.template.GetCost(instance.level).ToString() + " " + Finder.txt.basicDerivedStatNames.MP;
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected override string GetActionText(InstanceSkill instance) {
            return Finder.txt.commandNames.cast;
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		protected override Sprite GetCurrencyIcon(InstanceSkill instance) {
			return null;
		}
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		protected override Sprite GetIcon(InstanceSkill instance) {
			return instance.template.icon;
		}
		
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected override string GetName(InstanceSkill instance) {
        	if (instance.template.maxLevel <= 1) {
            	return instance.template.fullName;
            } else {
            	return instance.template.fullName + " " + Finder.txt.basicDerivedStatNames.LV + " " + instance.level + " / " + instance.template.maxLevel;
            }
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected override Color GetTextColor(InstanceSkill instance) {
            if (!character.CanCastAbility(instance.template, instance.level))
                return Color.grey;
            else
                return base.GetTextColor(instance);
        }
        
		// -------------------------------------------------------------------------------
		// ActionRequestedHandler
		// -------------------------------------------------------------------------------
        protected override void ActionRequestedHandler(object obj) {
        
            selectedSlot = (InstanceSkill)obj;
            
			if (!character.CanCastAbility(selectedSlot.template, selectedSlot.level)) {
                Finder.audio.PlaySFX(SFX.ButtonCancel);
                Finder.log.Add(character.Name+ " " + Finder.txt.basicVocabulary.requirementsNotMet + " " + selectedSlot.template.fullName + " " + Finder.txt.basicDerivedStatNames.LV + selectedSlot.level);
                return;
            }
            
            selectedSlot.template.getActivationTarget(UIState.ShowSelectedCharacterAbilities, character);
			
        }
        
        // -------------------------------------------------------------------------------
		// ActivationRequestHandler
		// -------------------------------------------------------------------------------
		public void ActivationRequestHandler(CharacterBase[] targets=null) {

			if (targets == null)
				targets = Finder.ui.GetSelectedCharacters();
			
			if (targets != null) {
				
				var success = false;
				
				foreach (CharacterBase target in targets) {
					
					if (target != null) {
						if (selectedSlot.template.CanTarget(target)) {
							success = true;
						} else {
							Finder.log.Add(target.Name + " " + Finder.txt.basicVocabulary.requirementsNotMet + " " + selectedSlot.template.fullName);
						}
					}
					
				}
				
				// -- we need at least one legal target
				if (success) {
					if (Finder.battle.InBattle) {
						Finder.battle.PlayerCastSpell(selectedSlot, targets);
					} else {
						character.CommandCastSpell(selectedSlot, targets); 
					}
				}
				
			}
		
		}
		
		// -------------------------------------------------------------------------------
		// UpgradeRequestedHandler
		// -------------------------------------------------------------------------------
        protected override void UpgradeRequestedHandler(object obj) {
        	selectedSlot = (InstanceSkill)obj;
        	if (character.AbilityPoints > 0 && selectedSlot.level < selectedSlot.template.maxLevel) {
            	Finder.confirm.Show(Finder.txt.commandNames.upgrade+Finder.txt.seperators.dash+Finder.txt.basicVocabulary.areYouSure, Finder.txt.basicVocabulary.yes, Finder.txt.basicVocabulary.no, () => UpgradeConfirmedHandler(), null);      
			}
        }
        
        // -------------------------------------------------------------------------------
		// UpgradeConfirmedHandler
		// -------------------------------------------------------------------------------
		protected override void UpgradeConfirmedHandler() {
			if (character.AbilityPoints > 0 && selectedSlot.level < selectedSlot.template.maxLevel) {
				selectedSlot.level++;
				character.AbilityPoints--;
				Refresh();
			}
		}
		
		// -------------------------------------------------------------------------------
        
    }
}