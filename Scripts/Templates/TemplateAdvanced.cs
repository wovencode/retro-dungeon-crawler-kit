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
using WoCo.DungeonCrawler;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// TEMPLATE ADVANCED
	// ===================================================================================
    public abstract class TemplateAdvanced : TemplateSimple {
    	
    	[Header("-=- Requirements -=-")]
        public StatRequirements 									statRequirements;
        public TemplateCharacterClass[] 							characterClasses;
        
        /*
        	This rather complicated approach of virtualizing and overriding properties
        	has been chosen because I do not want to use a custom inspector. Now,
        	using the standard inspector you would see ALL stats of ALL templates,
        	even though most classes make use of just a few of them. By virtualizing and
        	overriding we can "hide" properties and "show" them only where required.
        	-Fhiz
        */
        
		public virtual CanUseType useType							{ get; }
		public virtual CanUseLocation useLocation					{ get; }

		public virtual AudioClip useSFX								{ get; }
		    	
		public virtual CanUseTarget targetType						{ get; }
		public virtual float basePowerPercentage					{ get; }
		public virtual int basePower 								{ get; }
		public virtual int bonusPower 								{ get; }
		public virtual float bonusPowerPercentage					{ get; }
		public virtual TemplateMetaAttribute baseStat 				{ get; }
		public virtual float statPower 								{ get; }
		
		public virtual SpecialActionType useEffectType 				{ get; }
		
		public virtual GameObject hitEffect 						{ get; }
		public virtual TemplateMetaCombatStyle attackType 			{ get; }
    	public virtual TemplateMetaElement element 					{ get; }
    	public virtual TemplateMetaSpecies strongVsSpecies			{ get; }
       	public virtual TemplateStatus[] useBuffType 				{ get; }
        public virtual bool removeBuff 								{ get; }
        
		public virtual RecoveryType recoveryType 					{ get; }
		public virtual TemplateMetaAttribute recoveryStat 			{ get; }
		
		public virtual AudioClip critSFX							{ get; }
		public virtual AudioClip hitSFX								{ get; }
		public virtual AudioClip missSFX							{ get; }
		
        // -------------------------------------------------------------------------------
		// CanUse
		// -------------------------------------------------------------------------------
		public virtual bool CanUse(CharacterBase target) {
       		
			if (useType == CanUseType.None && useLocation == CanUseLocation.None) return false;
			
			if ((this is TemplateSkillSpecial || this is TemplateItemSpecial) && useEffectType == SpecialActionType.PlayerWarpDungeon) {
				if (Finder.party.MapExplorationInfo.GetExploredMapCount == 0) return false;
			}
						
			return RPGHelper.getCanUseType(useType, target) && RPGHelper.getCanUseLocation(useLocation);
			
        }
      
        // -------------------------------------------------------------------------------
		// getActivationTarget
		// -------------------------------------------------------------------------------
        public void getActivationTarget(UIState sourceState, CharacterBase source) {
        	
        	CharacterBase target = null;
        	CharacterBase[] targets;
        	CharacterBase[] legalTargets;
        	
        	switch (targetType) {
        	
				case CanUseTarget.AtSelf:
			
					if (source == null) {
						targets = new CharacterBase[] { Finder.party.characters[0] };
						Finder.ui.ForceSelection(targets, sourceState);
					} else {
						targets = new CharacterBase[] { source };
						Finder.ui.ForceSelection(targets, sourceState);
					}
					break;
				
				case CanUseTarget.AtTargetAlly:
					Finder.ui.Prompt(sourceState, true, false);
					break;
				
				case CanUseTarget.AtTargetFrontrowAlly:
					Finder.ui.Prompt(sourceState, true, false, RankTargetType.Front);
					break;
				
				case CanUseTarget.AtTargetRearrowAlly:
					Finder.ui.Prompt(sourceState, true, false, RankTargetType.Rear);
					break;
				
				case CanUseTarget.AtRandomAlly:
					target = Finder.party.characters[UnityEngine.Random.Range(0, Finder.party.characters.Count)];
					targets = new CharacterBase[] { target };
					Finder.ui.ForceSelection(targets, sourceState);
					break;
					
				case CanUseTarget.AtRandomFrontrowAlly:
					legalTargets = Finder.party.characters.Where(x => !x.IsRearguard).ToArray();
					target = legalTargets[UnityEngine.Random.Range(0, legalTargets.Length)];
					targets = new CharacterBase[] { target };
					Finder.ui.ForceSelection(targets, sourceState);
					break;
					
				case CanUseTarget.AtRandomRearrowAlly:
					legalTargets = Finder.party.characters.Where(x => x.IsRearguard).ToArray();
					target = legalTargets[UnityEngine.Random.Range(0, legalTargets.Length)];
					targets = new CharacterBase[] { target };
					Finder.ui.ForceSelection(targets, sourceState);
					break;
					
				case CanUseTarget.AtAllAllies:
					targets = Finder.party.characters.ToArray();
					Finder.ui.ForceSelection(targets, sourceState);
					break;
				
				case CanUseTarget.AtAllFrontrowAllies:
					targets = Finder.party.characters.Where(x => !x.IsRearguard).ToArray();
					Finder.ui.ForceSelection(targets, sourceState);
					break;
					
				case CanUseTarget.AtAllRearrowAllies:
					targets = Finder.party.characters.Where(x => x.IsRearguard).ToArray();
					Finder.ui.ForceSelection(targets, sourceState);
					break;
					
				case CanUseTarget.AtTargetEnemy:
					Finder.ui.Prompt(sourceState, false, false);
					break;
				
				case CanUseTarget.AtTargetFrontrowEnemy:
					Finder.ui.Prompt(sourceState, false, false, RankTargetType.Front);
					break;
					
				case CanUseTarget.AtTargetRearrowEnemy:
					Finder.ui.Prompt(sourceState, false, false, RankTargetType.Rear);
					break;
					
				case CanUseTarget.AtRandomEnemy:
					target = Finder.battle.monsterParty.characters[UnityEngine.Random.Range(0, Finder.party.characters.Count)];
					targets = new CharacterBase[] { target };
					Finder.ui.ForceSelection(targets, sourceState);
					break;
					
				case CanUseTarget.AtRandomFrontrowEnemy:
					legalTargets = Finder.battle.monsterParty.characters.Where(x => !x.IsRearguard).ToArray();
					target = legalTargets[UnityEngine.Random.Range(0, legalTargets.Length)];
					targets = new CharacterBase[] { target };
					Finder.ui.ForceSelection(targets, sourceState);
					break;
					
				case CanUseTarget.AtRandomRearrowEnemy:
					legalTargets = Finder.battle.monsterParty.characters.Where(x => x.IsRearguard).ToArray();
					target = legalTargets[UnityEngine.Random.Range(0, legalTargets.Length)];
					targets = new CharacterBase[] { target };
					Finder.ui.ForceSelection(targets, sourceState);
					break;
					
				case CanUseTarget.AtAllEnemies:
					targets = Finder.battle.monsterParty.characters.ToArray();
					Finder.ui.ForceSelection(targets, sourceState);
					break;
				
				case CanUseTarget.AtAllFrontrowEnemies:
					targets = Finder.battle.monsterParty.characters.Where(x => !x.IsRearguard).ToArray();
					Finder.ui.ForceSelection(targets, sourceState);
					break;
					
				case CanUseTarget.AtAllRearrowEnemies:
					targets = Finder.battle.monsterParty.characters.Where(x => x.IsRearguard).ToArray();
					Finder.ui.ForceSelection(targets, sourceState);
					break;
					
				case CanUseTarget.AtAll:
					CharacterBase[] targetPlayers = Finder.party.characters.ToArray();
					CharacterBase[] targetMonsters = Finder.battle.monsterParty.characters.ToArray();
					targets = targetPlayers.Concat(targetMonsters).ToArray();
					Finder.ui.ForceSelection(targets, sourceState);
					break;
					
			}
			
        }
        
        // -------------------------------------------------------------------------------
		// getCharacterActionType
		// -------------------------------------------------------------------------------
        public CharacterActionType getCharacterActionType {
        
        	get {
				if (this is TemplateSkillAttack || this is TemplateItemAttack) {
					return CharacterActionType.Damage;
				} else if (this is TemplateSkillCurative || this is TemplateItemCurative) {
					return CharacterActionType.Recover;
				} else if (this is TemplateItemAbility) {
					return CharacterActionType.LearnAbility;
				} else if (this is TemplateSkillSpecial || this is TemplateItemSpecial) {
				
					switch (useEffectType) {
					
						case SpecialActionType.PlayerExitBattle:
							return CharacterActionType.PlayerExitBattle;
						case SpecialActionType.PlayerExitDungeon:
							return CharacterActionType.PlayerExitDungeon;
						case SpecialActionType.PlayerWarpDungeon:
							return CharacterActionType.PlayerWarpDungeon;
					
					}
				
				}
			
				return CharacterActionType.None;
        	}
        	
        }
        
        // -------------------------------------------------------------------------------
		// CalculatedEffect
		// -------------------------------------------------------------------------------
		public int CalculatedEffect(CharacterBase source, CharacterBase target=null, int level=1) {
			
			int totalPower = basePower + (level-1 * bonusPower);
			
			// -- Percentage based Healing (only on HP OR MP - not both)
			
			if (target != null && basePowerPercentage != 0) {
			
				if (recoveryType == RecoveryType.HP) {
					totalPower += (int)(target.MaxHP * basePowerPercentage);
				} else if (recoveryType == RecoveryType.MP) {
					totalPower += (int)(target.MaxMP * basePowerPercentage);
				}
			
			}
			
			// -- Percentage based Healing for levels (only on HP OR MP - not both)
			
			if (target != null && bonusPowerPercentage != 0) {
			
				if (recoveryType == RecoveryType.HP) {
					totalPower += (int)(target.MaxHP * (level-1 * bonusPowerPercentage));
				} else if (recoveryType == RecoveryType.MP) {
					totalPower += (int)(target.MaxMP * (level-1 * bonusPowerPercentage));
				}
			
			}
			
			// -- Point based Healing
			
			if (source != null && statPower != 0) {
			
				CharacterAttribute attrib = source.stats.attributes.FirstOrDefault(x => x.template == baseStat);
				if (attrib != null)
					totalPower += Mathf.RoundToInt(attrib.value * statPower);
						
			}
						
			// -- returns sum of both Percentage and Point based Healing
			
			return totalPower;
			
		}
		
  		// -------------------------------------------------------------------------------
		// PartyClassRequirementsMet
		// -------------------------------------------------------------------------------
		public bool PartyClassRequirementsMet {
      		get {	
      			var success = false;
      			foreach (CharacterBase character in Finder.party.characters) {
      				success = ClassRequirementsMet(character);
      			}
      			return success;
        	}
        }
        
        // -------------------------------------------------------------------------------
		// PartyStatRequirementsMet
		// -------------------------------------------------------------------------------
		public bool PartyStatRequirementsMet {
      		get {	
      			var success = false;
      			foreach (CharacterBase character in Finder.party.characters) {
      				success = StatRequirementsMet(character);
      			}
      			return success;
        	}
        }
        
  		// -------------------------------------------------------------------------------
		// RequirementsMet
		// -------------------------------------------------------------------------------
		public bool RequirementsMet(CharacterBase target) {
			return ClassRequirementsMet(target) && StatRequirementsMet(target);
		}
        
		// -------------------------------------------------------------------------------
		// ClassRequirementsMet
		// -------------------------------------------------------------------------------
		public bool ClassRequirementsMet(CharacterBase target) {
            if (characterClasses.Length == 0) return true;
			return characterClasses.Contains(target.template.characterClass);
		}
		
		// -------------------------------------------------------------------------------
		// StatRequirementsMet
		// -------------------------------------------------------------------------------
		public bool StatRequirementsMet(CharacterBase target) {
            
            if (statRequirements == null) return true;
			
			bool success = true;
			
			// -- Check Attribute Requirements
			foreach (CharacterAttribute req_attrib in statRequirements.attributeRequirements) {
				CharacterAttribute target_attrib = target.stats.attributes.FirstOrDefault(x => x.template == req_attrib.template);
				if (target_attrib != null && target_attrib.value >= req_attrib.value) {
					success = true;
				} else {
					success = false;
				}
			}
			
            return success;
            
		}		
		
		// -------------------------------------------------------------------------------
   
    }
    
    // ===================================================================================
    
}