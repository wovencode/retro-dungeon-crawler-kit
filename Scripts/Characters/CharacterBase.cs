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
using WoCo.DungeonCrawler;

namespace WoCo.DungeonCrawler {
	
	// ===================================================================================
	// CharacterBase
	// ===================================================================================
    [Serializable]
    public class CharacterBase {
   		
   		public event PropertyChangedEventHandler PropertyChanged;
		public event EventHandler LevelUp;

   		public CharacterStats stats;
    	public List<InstanceSkill> abilities;
        public List<InstanceStatus> buffs;
        
        public AudioClip defaultCritSFX;
        public AudioClip defaultHitSFX;
        public AudioClip defaultMissSFX;
        public AudioClip defaultHurtSFX;
        public AudioClip defaultDieSFX;
        						
        public TemplateCharacterBase template;
        
        public bool IsRearguard 	{ get; set; }
		public bool IsFleeing 		{ get; set; }
		
		public CharacterActionType currentAction { get; set; }
		public GameObject parent 	{ get; set; }
		
		public bool BattleTurnFinished { get; set; }
		
        // ===============================================================================
		// INITALIZATION
		// ===============================================================================
        
		// -------------------------------------------------------------------------------
		// Awake
		// -------------------------------------------------------------------------------
        public CharacterBase() {
            stats 		= new CharacterStats();            
            abilities 	= new List<InstanceSkill>();
            buffs 		= new List<InstanceStatus>();
            
            ResetBattleStances();
        }
        
        // -------------------------------------------------------------------------------
		// DefaultInitialize
		// -------------------------------------------------------------------------------
        public virtual void DefaultInitialize() {
        	stats.Initialize();
            CalculateDefaults();
        }
        
        // -------------------------------------------------------------------------------
		// loadTemplates
		// -------------------------------------------------------------------------------
        public void loadTemplates() {
        	
        	foreach (InstanceSkill ability in abilities)
        		ability.loadTemplate();
        	
        	foreach (InstanceStatus buff in buffs)
        		buff.loadTemplate();
        	
        	stats.loadTemplates();
        	
        }
        
        // ===============================================================================
		// STATS
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// Name
		// -------------------------------------------------------------------------------
		public string Name {
			get { return template.name; }
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public int XP {
            
            get { return stats.XP; }
            
            set {
				stats.XP += value;
				
				if (Level < Finder.config.maxLevel && stats.XP >= stats.XPToNextLevel) {
					Level ++;
					stats.XPToNextLevel = RPGHelper.NextLvlExp(Level);
					stats.XP = 0;
					AttributePoints += Finder.config.attributePointsPerLvl;
					AbilityPoints += Finder.config.abilityPointsPerLvl;
					OnLevelUp();
					Finder.log.Add(Name+" "+Finder.txt.basicVocabulary.levelUp);
					LevelUpAbilities(true);
				} else {
					OnPropertyChanged("XP");
				}
				
            }
            
        }
        
		// -------------------------------------------------------------------------------
		// Level
		// -------------------------------------------------------------------------------
		public int Level {
            get { return stats.level; }
            set {
                stats.level = value;
                if (stats.level < 0) stats.level = 0;
                OnPropertyChanged("LV");
            }
        }

		// -------------------------------------------------------------------------------
		// AttributePoints
		// -------------------------------------------------------------------------------
		public int AttributePoints {
			get { return stats.attributePoints; }
			set {
				stats.attributePoints = value;
				if (stats.attributePoints < 0) stats.attributePoints = 0;
				OnPropertyChanged("AttributePoints");
			}
		}
		
		// -------------------------------------------------------------------------------
		// AbilityPoints
		// -------------------------------------------------------------------------------
		public int AbilityPoints {
			get { return stats.abilityPoints; }
			set {
				stats.abilityPoints = value;
				if (stats.abilityPoints < 0) stats.abilityPoints = 0;
				OnPropertyChanged("AbilityPoints");
			}
		}

		// -------------------------------------------------------------------------------
		// HP
		// -------------------------------------------------------------------------------
		public int HP {
            get { return stats.HP; }
            set {
                stats.HP = value;
                if (stats.HP < 0) stats.HP = 0;
                OnPropertyChanged("HP");
            }
        }
        
		// -------------------------------------------------------------------------------
		// MP
		// -------------------------------------------------------------------------------
        public int MP {
            get { return stats.MP; }
            set {
                stats.MP = value;
                if (stats.MP < 0) stats.MP = 0;
                OnPropertyChanged("MP");
            }
        }
        
		// -------------------------------------------------------------------------------
		// MaxHP
		// -------------------------------------------------------------------------------
        public int MaxHP {
            get { return stats.MaxHP; }
            set {
                stats.MaxHP = value;
                OnPropertyChanged("MaxHP");
            }
        }
        
		// -------------------------------------------------------------------------------
		// MaxMP
		// -------------------------------------------------------------------------------
        public int MaxMP {
            get { return stats.MaxMP; }
            set {
                stats.MaxMP = value;
                OnPropertyChanged("MaxMP");
            }
        }
		
		// -------------------------------------------------------------------------------
		// Attacks
		// -------------------------------------------------------------------------------
        public int Attacks {
            get { return Mathf.Max(1, stats.Attacks); }
        }
		
		// ===============================================================================
		// FUNCTIONS
		// ===============================================================================

		// -------------------------------------------------------------------------------
		// RestoreToFull
		// -------------------------------------------------------------------------------
 		public void RestoreToFull() {
            HP = MaxHP;
            MP = MaxMP;
            
            foreach (InstanceStatus buff in buffs) {
            	if (!buff.template.sticky)
            		InflictBuff(buff.template, 0, true, true);
            }
            
        }

		// -------------------------------------------------------------------------------
		// RestoreMP
		// -------------------------------------------------------------------------------
        public int RestoreMP(int amount) {
           	int value = amount;
           	if (MP + amount > MaxMP) {
           		value = MaxMP - MP;
           		MP = MaxMP;
        	} else {
        		MP += amount;
        	}
        	return value;
        }

      	// -------------------------------------------------------------------------------
		// RestoreHP
		// -------------------------------------------------------------------------------
		public int RestoreHP(int amount) {
           	int value = amount;
           	if (HP + amount > MaxHP) {
           		value = MaxHP - HP;
           		HP = MaxHP;
        	} else {
        		HP += amount;
        	}
        	return value;
        }
        
      	// -------------------------------------------------------------------------------
		// InflictDamage
		// -------------------------------------------------------------------------------
        public int InflictDamage(int amount) {
        	int value = amount;
        	if (HP - amount < 0) {
           		value = HP;
           		HP = 0;
           		Finder.audio.PlaySFX(defaultDieSFX);
        	} else {
        		HP -= amount;
        		Finder.audio.PlaySFX(defaultHurtSFX);
        	}
        	return value;
        }

        // -------------------------------------------------------------------------------
		// InflictBuffs
		// -------------------------------------------------------------------------------
      	public bool InflictBuffs(TemplateStatus[] buffs, int accuracy, bool force=false, bool remove=false) {
        	var success = false;
        	foreach (TemplateStatus buff in buffs) {
				 success = InflictBuff(buff, stats.Accuracy, force, remove);
			}
			return success;
        }
        
        // -------------------------------------------------------------------------------
		// InflictBuff
		// -------------------------------------------------------------------------------
        public bool InflictBuff(TemplateStatus buff, int accuracy, bool force=false, bool remove=false) {
        
			if (buff == null) return false;
			
        	if (remove) {
        	
        		foreach (InstanceStatus abuff in buffs) {
        			if (abuff.template == buff) {
        				abuff.remainingDuration = 0;
        			}
        		}
        		UpdateBuffs();
        		
        		return true;
        		
        	} else {
        	
        		if (CalculateOdds(accuracy + buff.buffAccuracy, stats.Resistance) || force) {
        			var tmpBuff = buffs.FirstOrDefault(x => x.template == buff);
        			if (tmpBuff != null) return false;
        			buffs.Add(new InstanceStatus { template=buff, remainingDuration = buff.duration });
        			Finder.log.Add(Name + Finder.txt.basicVocabulary.affectedBy + buff.name);
        			OnPropertyChanged("Buffs");
        			return true;
        		} else {
        			Finder.log.Add(Name + Finder.txt.basicVocabulary.unaffectedBy + buff.name);
        		}
        		
        	}
			
        	return false;
        	
        }
        
		// ===============================================================================
		// GETTERS
		// ===============================================================================
       
       	// -------------------------------------------------------------------------------
		// Element
		// -------------------------------------------------------------------------------
       	public TemplateMetaElement Element {
       		get {
       			var ele = (buffs.FirstOrDefault(x => x.template.statModifiers.other.changeElement == true && x.IsActive));
       			if (ele != null) return ele.template.statModifiers.other.element;
       			return template.element;
       		}
       	}
       
       	// -------------------------------------------------------------------------------
		// IsAlive
		// -------------------------------------------------------------------------------
        public bool IsAlive {
        	get {
        		return HP > 0;
        	}
        }
        
        // -------------------------------------------------------------------------------
		// IsSilenced
		// -------------------------------------------------------------------------------
        public bool IsSilenced {
        	get {
        		return (buffs.Any(x => x.template.statModifiers.special.silence == true && x.IsActive));
        	}
        }
        
       	// -------------------------------------------------------------------------------
		// IsStunned
		// -------------------------------------------------------------------------------
        public bool IsStunned {
        	get {
        		return (buffs.Any(x => x.template.statModifiers.special.stun == true && x.IsActive));
        	}
        }
        
      	// -------------------------------------------------------------------------------
		// IsDepleteHP
		// -------------------------------------------------------------------------------
        public bool IsDepleteHP {
        	get {
        		return (buffs.Any(x => x.template.statModifiers.other.depleteHP < 0 && x.IsActive));
        	}
        }

		// -------------------------------------------------------------------------------
		// IsDepleteMP
		// -------------------------------------------------------------------------------
        public bool IsDepleteMP {
        	get {
        		return (buffs.Any(x => x.template.statModifiers.other.depleteMP < 0 && x.IsActive));
        	}
        }
        
		// -------------------------------------------------------------------------------
		// HasAbility
		// -------------------------------------------------------------------------------
        public bool HasAbility(TemplateSkill tmpl, int level) {
        	return (abilities.Any(x => x.template == tmpl && x.level >= level));
        }
        
        // -------------------------------------------------------------------------------
		// getInLevelRange
		// -------------------------------------------------------------------------------
		public bool getInLevelRange(int minLevel, int maxLevel) {
			if (minLevel == 0 && maxLevel == 0) return true;
			return (Level >= minLevel || minLevel == 0) && (Level <= maxLevel || maxLevel == 0);
		}
        
      	// ===============================================================================
		// MISC FUNCTIONS
		// ===============================================================================
		
        // -------------------------------------------------------------------------------
		// BattlePreperations
		// -------------------------------------------------------------------------------
		public void BattlePreperations() {
		
			IsFleeing	= false;
			
			if (Finder.battle.SetStatusOnDefend)
				InflictBuff(Finder.battle.SetStatusOnDefend, 0, true, true);

			if (buffs != null && buffs.Count > 0) {
                buffs.ForEach(x => x.StopEffect());
                buffs.RemoveAll(x => !x.template.stayAfterBattle);
                CalculateDerivedStats();
            }
			
		}
		
       	// -------------------------------------------------------------------------------
		// UpdateBuffs
		// -------------------------------------------------------------------------------
		public void UpdateBuffs() {
			
			if (buffs.Count == 0) return;
			
            foreach (InstanceStatus buff in buffs) {
            	if (buff.IsActive) {
            		
            		Finder.log.Add(Name + Finder.txt.basicVocabulary.affectedBy + buff.template.name);
            	
            		if (buff.template.statModifiers.other.depleteHP != 0) {
            			int value = (int)(MaxHP * buff.template.statModifiers.other.depleteHP);
            			
            			if (value > 0) {
            				InflictDamage(value); 			
            			} else if (value < 0) {
            				RestoreHP(value);
            			}
            			            			
            		} else if (buff.template.statModifiers.other.depleteMP != 0) {
            			MP += (int)(MaxMP * buff.template.statModifiers.other.depleteMP);
            		}
            	}
            }
            
            OnPropertyChanged("Buffs");
            
		}
			
       	// -------------------------------------------------------------------------------
		// ResetBattleStances
		// -------------------------------------------------------------------------------
		public void ResetBattleStances() {				
		    IsFleeing 			= false;
		    BattleTurnFinished 	= false;
		}
			
        // -------------------------------------------------------------------------------
		// CanUseItem
		// -------------------------------------------------------------------------------
		public bool CanUseItem(TemplateItem item) {
            var inventoryItem = Finder.party.inventory.Items.FirstOrDefault(x => x.template == item);
            if (inventoryItem != null && !IsStunned && inventoryItem.Quantity > 0)
            	if (item != null) return item.CanUse(this);
            return false;
        }
           
        // -------------------------------------------------------------------------------
		// CanCastAbility
		// -------------------------------------------------------------------------------
        public bool CanCastAbility(TemplateSkill ability, int level) {
            if (ability != null && !IsSilenced && !IsStunned && IsAlive && level > 0)
           		return ability.CanUse(this, level);
            return false;
        }

 		// -------------------------------------------------------------------------------
		// CalculateOdds
		// -------------------------------------------------------------------------------
		protected bool CalculateOdds(float sourceVal, float targetVal) {
			float odds = Mathf.Clamp(sourceVal-targetVal, Finder.battle.minHitChance, Finder.battle.maxHitChance);
        	return (UnityEngine.Random.Range(0,1) <= odds);
        }
        
        // -------------------------------------------------------------------------------
		// CalculateHitType
		// -------------------------------------------------------------------------------      
        public int CalculateHitType(int damage, CharacterBase target, TemplateAdvanced activator=null) {
        	
        	AudioClip critSFX 	= defaultCritSFX;
            AudioClip hitSFX 	= defaultHitSFX;
            AudioClip missSFX 	= defaultMissSFX;
            
        	if (activator != null) {
            	critSFX		= activator.critSFX;
            	hitSFX 		= activator.hitSFX;
            	missSFX 	= activator.missSFX;
            }
            
            // -- Check for Glancing or Critical Hit
            
			if (CalculateOdds(stats.Accuracy, target.stats.BlockRate)) {
				Finder.audio.PlaySFX(missSFX);
				damage = (int)(damage * target.stats.BlockFactor);
			} else if (CalculateOdds(stats.CritRate, target.stats.BlockRate)) {
				Finder.audio.PlaySFX(hitSFX);
				damage = (int)(damage * target.stats.CritFactor);
			} else {
				Finder.audio.PlaySFX(hitSFX);
			}
			
			// -- Check for bonus damage against certain Species
			
			if (activator != null) {
				if (target.template.species == activator.strongVsSpecies) {
					damage = (int)(damage * Finder.battle.speciesFactor);
				}
			}
			
			return damage;
			
        }
        
        // -------------------------------------------------------------------------------
		// SpawnEffect
		// -------------------------------------------------------------------------------      
       protected void SpawnEffect(CharacterBase target, GameObject hitEffect) {
			if (target.parent != null && hitEffect != null) {
				Finder.fx.SpawnEffect(target.parent.transform, hitEffect);
			}
        }
        
      	// ===============================================================================
		// MENU COMMANDS
		// ===============================================================================

 		// -------------------------------------------------------------------------------
		// Attack
		// -------------------------------------------------------------------------------
        public bool CommandAttack(CharacterBase[] targets) {
            
            if (targets != null) 
                return ActivateAction(CharacterActionType.Attack, null, targets);
                
            return false;
        }
        
 		// -------------------------------------------------------------------------------
		// CommandDefend
		// -------------------------------------------------------------------------------
        public bool CommandDefend() {
			return ActivateAction(CharacterActionType.Defend, null, new CharacterBase[] { this } );
        }		

 		// -------------------------------------------------------------------------------
		// CommandSwitch
		// -------------------------------------------------------------------------------
        public bool CommandSwitch() {
			return ActivateAction(CharacterActionType.Switch, null, new CharacterBase[] { this } );
        }

 		// -------------------------------------------------------------------------------
		// CommandPlayerRun (CharacterHero only)
		// -------------------------------------------------------------------------------
        public bool CommandPlayerRun() {
			return ActivateAction(CharacterActionType.PlayerRun, null, new CharacterBase[] { this } );
        }   

      	// -------------------------------------------------------------------------------
		// CommandUseItem
		// -------------------------------------------------------------------------------
        public bool CommandUseItem(InstanceItem instance, CharacterBase[] targets) {
            var inventoryItem = Finder.party.inventory.Items.FirstOrDefault(x => x.template == instance.template);
            if (inventoryItem.Quantity > 0 && targets != null) {
            	Finder.audio.PlaySFX(inventoryItem.template.useSFX);
            	Finder.log.Add(Name+" "+Finder.txt.actionNames.uses+" "+inventoryItem.template.name);
            	if (inventoryItem.template.DepleteOnUse) inventoryItem.Quantity -= 1;
                if (inventoryItem.Quantity == 0) Finder.party.inventory.Items.Remove(inventoryItem);
                return ActivateAction(inventoryItem.template.getCharacterActionType, instance, targets);
            }
            return false;
        }

        // -------------------------------------------------------------------------------
		// CommandCastSpell
		// -------------------------------------------------------------------------------
        public bool CommandCastSpell(InstanceSkill instance, CharacterBase[] targets) {
            if (instance != null && targets != null) {
                MP -= instance.template.GetCost(instance.level);
                Finder.audio.PlaySFX(instance.template.useSFX);
              	if (instance.template is TemplateSkillAttack) {
                	Finder.log.Add(string.Format("{0} {1} {2}.", Name, instance.template.attackType.useText, instance.template.name));
            	} else {
            		Finder.log.Add(string.Format("{0} {1} {2}.", Name, Finder.txt.actionNames.casts, instance.template.name));
            	}
                return ActivateAction(instance.template.getCharacterActionType, instance, targets);
            }
            return false;
        }
        
        // -------------------------------------------------------------------------------
		// CommandSwitchFormation
		// -------------------------------------------------------------------------------
        public bool CommandSwitchFormation() {
        	return ActivateAction(CharacterActionType.Switch, null, new CharacterBase[] { this });
        }

      	// ===============================================================================
		// ACTIONS
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// ActivateAction
		// -------------------------------------------------------------------------------
        protected bool ActivateAction(CharacterActionType action, InstanceBase activator, CharacterBase[] targets) {
        	
        	iTweenEffects co = parent.GetComponent<iTweenEffects>();
        	
			if (co != null)
            	co.Tilt();

            switch (action) {
                
                case CharacterActionType.Attack:
                	return ActionAttack(targets);
                
                case CharacterActionType.Defend:
                	return ActionDefend();
                	
                case CharacterActionType.Switch:
                	return ActionSwitch();
               	
                case CharacterActionType.PlayerRun:
                	return ActionPlayerRun();
                
                case CharacterActionType.Recover:
                    return ActionRecover(activator, targets);

                case CharacterActionType.PlayerExitBattle:
                    return ActionPlayerExitBattle();

                case CharacterActionType.PlayerExitDungeon:
                	return ActionPlayerExitDungeon();
                	
                case CharacterActionType.PlayerWarpDungeon:
                	return ActionPlayerWarpDungeon();
                	
                case CharacterActionType.Damage:
                	return ActionDamage(activator, targets);
                	
				case CharacterActionType.LearnAbility:
					return ActionLearnAbility((InstanceItem)activator, targets);
				
				case CharacterActionType.PassTurn:
					return ActionPassTurn();
				
            }
		
			return false;
			            
        }
        
        // -------------------------------------------------------------------------------
		// ActionAttack
		// -------------------------------------------------------------------------------
		protected bool ActionAttack(CharacterBase[] targets) {

            int damage 							= 0;
            List<int> combatStyleDamage			= new List<int>();
			TemplateMetaElement element 		= null;
			TemplateEquipmentWeapon activator 	= null;
			
            activator = Finder.party.equipment.GetEquippedWeapon(this);
            
            GameObject hitEffect = null;
            
            if (activator != null) {
				element 	= activator.element;
				hitEffect 	= activator.hitEffect;
				damage 		= activator.CalculatedEffect(this);
			} else {
				element		= template.element;
				hitEffect 	= template.hitEffect;
			}
            
            // -- Repeat number of Attacks
            
            for (int i = 1; i <= Attacks; i++) {
            	
            	damage = 0;
            	
            	// -- iterate through all targets
            
				foreach (CharacterBase target in targets) { 
				
					if (target != null) {
						
						// -- Spawn effect at target
					
						SpawnEffect(target, hitEffect);
						
						// -- Inflict Buffs on target
						
						if (activator != null)
							target.InflictBuffs(activator.useBuffType, stats.Accuracy, activator.removeBuff);
					
						// -- Calculate damage types
					
						// -- A. Summarize all damage types for monsters who don't wield a weapon
					
						if (activator == null) {
						
							foreach (var tmpls in DictionaryCombatStyle.dict) {
								TemplateMetaCombatStyle tmpl = tmpls.Value;
								int attackValue = stats.combatStyles.FirstOrDefault(x => x.template == tmpl).attackValue;
								combatStyleDamage.Add(RPGHelper.CalculateFinalDamage(attackValue, tmpl, element, target));
							}
							damage += combatStyleDamage.Sum();
						
						// -- B. Use only the active damage type of the equipped weapon
						
						} else {
							TemplateMetaCombatStyle tmpl = activator.attackType;
							damage += RPGHelper.CalculateFinalDamage(stats.combatStyles.FirstOrDefault(x => x.template == tmpl).attackValue, tmpl, element, target);
						}
				
						// -- Check Glancing or Critical Hit and Species bonus damage
			
						damage += CalculateHitType(damage, target, activator);
			
						// -- Remove Buffs that can be removed by a hit
			
						buffs.RemoveAll(x => x.template.removeByHit > 0 && UnityEngine.Random.Range(1,100) <= x.template.removeByHit);
			
						// -- Finally inflict the damage
			
						damage = target.InflictDamage(damage);
			
						// -- Add feedback message to the log
			
						Finder.log.Add(string.Format("{0} {1} {2} {3} {4} {5}", Name, Finder.txt.actionNames.attacks, target.template.fullName, Finder.txt.basicVocabulary.causing, damage, Finder.txt.basicVocabulary.damage));
					
					}
				
				}
			
            }
                        
            return true;
        }
        
		// -------------------------------------------------------------------------------
		// ActionDamage
		// -------------------------------------------------------------------------------
		protected bool ActionDamage(InstanceBase activator, CharacterBase[] targets) {
			
			int amount = 0;
			TemplateAdvanced template = null;
			
			RPGHelper.DamageTargets(this, activator, amount, targets);
            
            if (activator is InstanceItem) {
            	template = ((InstanceItem)activator).template;
            } else if (activator is InstanceSkill) {
            	template = ((InstanceSkill)activator).template;
            }
                       
			return true;
		}
		
		// -------------------------------------------------------------------------------
		// ActionRecover
		// -------------------------------------------------------------------------------
		protected bool ActionRecover(InstanceBase activator, CharacterBase[] targets) {
			
			int amount = 0;
			TemplateAdvanced template = null;
			
			RPGHelper.RecoverTargets(this, activator, amount, targets);
			
			if (activator is InstanceItem) {
            	template = ((InstanceItem)activator).template;
            } else if (activator is InstanceSkill) {
            	template = ((InstanceSkill)activator).template;
            }
			            
            return true;
        }
        
  		// -------------------------------------------------------------------------------
		// ActionPlayerRun (CharacterHero only)
		// -------------------------------------------------------------------------------
        protected bool ActionPlayerRun() {
        	
        	if (this is CharacterHero) {
        	
            	Finder.party.ResetBattleStances();
        	
        		int val = UnityEngine.Random.Range(0, 100);
        		
        		int chance = 0;
        		
        		if (Finder.battle.encounterType == EncounterType.Normal) {
        			chance = Finder.battle.runChanceNormal;
        		} else if (Finder.battle.encounterType == EncounterType.Miniboss || Finder.battle.encounterType == EncounterType.Boss) {
        			chance = Finder.battle.runChanceBoss;
        		}
			
				if (val <= chance)  {
					Finder.fx.StopAllEffects();
					Finder.audio.PlaySFX(SFX.RunAway);
					Finder.log.Add(Finder.txt.basicVocabulary.runSuccess);
					return true;
				} else {
					Finder.party.SetFleeingStance();
					Finder.log.Add(Finder.txt.basicVocabulary.runFail);
				}
				
        	}
        	
        	return false;
		}  
		    
        // -------------------------------------------------------------------------------
		// ActionSwitch
		// -------------------------------------------------------------------------------
        protected bool ActionSwitch() {
        	
        	if (IsRearguard) {
        		IsRearguard = false;
        	} else {
        		IsRearguard = true;
        	}
        	
        	Finder.log.Add(string.Format("{0} {1} {2}.", Name, Finder.txt.formationNames.movedTo, IsRearguard ? Finder.txt.formationNames.rear : Finder.txt.formationNames.front));
        	        	
        	return true;
		}
		      
        // -------------------------------------------------------------------------------
		// ActionDefend
		// -------------------------------------------------------------------------------
        protected bool ActionDefend() {
        	
        	Finder.log.Add(Name+" "+Finder.txt.actionNames.defends);
        	        	
        	if (Finder.battle.SetStatusOnDefend != null)
       			InflictBuff(Finder.battle.SetStatusOnDefend, 0, true, false);
        	
        	return true;
		}
		
      	// -------------------------------------------------------------------------------
		// ActionPlayerExitBattle (CharacterHero only)
		// -------------------------------------------------------------------------------
		protected bool ActionPlayerExitBattle() {
			
			if (this is CharacterHero) {
				Finder.party.ResetBattleStances();
				Finder.fx.StopAllEffects();
				Finder.audio.PlaySFX(SFX.RunAway);
				return true;
			}
			
			return false;
		}
		
      	// -------------------------------------------------------------------------------
		// ActionPlayerExitDungeon (CharacterHero only)
		// -------------------------------------------------------------------------------
		protected bool ActionPlayerExitDungeon() {

			if (this is CharacterHero && Finder.party.lastTown != null) {
            	Finder.party.ResetBattleStances();
				Finder.ui.DeactivateAll();
           		Finder.map.WarpTown(Finder.party.lastTown);
            	return true;
            }
            
			return false;
		}
		
		// -------------------------------------------------------------------------------
		// ActionPlayerWarpDungeon (CharacterHero only)
		// -------------------------------------------------------------------------------
		protected bool ActionPlayerWarpDungeon() {

			if (this is CharacterHero) {
            	Finder.party.ResetBattleStances();
           		Finder.ui.OverrideState(UIState.DungeonWarp);
            	return true;
            }
            
			return false;
		}
		
		// -------------------------------------------------------------------------------
		// ActionLearnAbility
		// -------------------------------------------------------------------------------
		protected bool ActionLearnAbility(InstanceItem activator, CharacterBase[] targets) {

			foreach (CharacterBase target in targets) {
			
				TemplateItemAbility a = (TemplateItemAbility)activator.template;
					
				if (!target.abilities.Any(x => x.template == a.learnedAbility)) {
					target.abilities.Add(new InstanceSkill(a.learnedAbility, 1));
					return true;
				}
			
			}
			
            return false;
			
		}

		// -------------------------------------------------------------------------------
		// ActionPassTurn
		// -------------------------------------------------------------------------------
		protected bool ActionPassTurn() {
			
			Finder.ui.ActivateBattleCommands(false);
			
			if (IsAlive)
				Finder.log.Add(Name+" is passes the turn");
			
			BattleTurnFinished = true;
        	if (IsFleeing) IsFleeing = false;
			
            return true;
			
		}
		
		// ===============================================================================
		// LOAD/SAVE FUNCTIONS
		// ===============================================================================

      	// -------------------------------------------------------------------------------
		// LoadFromSavegame
		// -------------------------------------------------------------------------------
        public void LoadFromSavegame(SavegameCharacter character) {
        
        	template		= DictionaryCharacterHero.GetHero(character.templateName);
        	
        	IsRearguard	= character.IsRearguard;
        	        	
        	stats 		= character.stats;
        	abilities 	= character.abilities;
        	buffs 		= character.buffs;
        	
        	loadTemplates();
        }
        
        // -------------------------------------------------------------------------------
		// SaveToSavegame
		// -------------------------------------------------------------------------------
        public SavegameCharacter SaveToSavegame() {
        	
        	SavegameCharacter character = new SavegameCharacter();
        	
        	character.templateName		= template.name;
        	
        	character.IsRearguard		= IsRearguard;
        	        	
        	character.stats 			= stats;
        	character.abilities 		= abilities;
        	character.buffs 			= buffs;
        	
        	return character;
        	
        }
 
       	// ===============================================================================
		// DEFAULT FUNCTIONS
		// ===============================================================================

  		// -------------------------------------------------------------------------------
		// CalculateDefaults
		// -------------------------------------------------------------------------------
		public void CalculateDefaults() {
			
			if (template == null) return;
			
			defaultCritSFX	= template.critSFX;
			defaultHitSFX	= template.hitSFX;
        	defaultMissSFX	= template.missSFX;
        	defaultHurtSFX	= template.hurtSFX;
        	defaultDieSFX	= template.dieSFX;
        	
        	Level			+= template.level;
			IsRearguard		= (template.defaultRank == RankType.Front) ? false : true;
			
			// -- Default Inventory (from Character Template)
			foreach (TemplateItem item in template.defaultItems) {
            	Finder.party.inventory.AddItem(item, 1);
            }
            
            // -- Default Equipment (from Character Template)
            foreach (TemplateEquipment equipment in template.defaultEquipment) {
            	Finder.party.equipment.AddEquipment(equipment);
            	Finder.party.equipment.EquipItem(Finder.party.equipment.Equipment.FirstOrDefault(x => x.template.name == equipment.name), this);
            }
			
			// -- Default Attributes (from both Class and Species)
			
			List<CharacterAttributeWeight> attrib = new List <CharacterAttributeWeight>();
			
			if (template.characterClass != null &&  template.characterClass.attributes.Length > 0)
				attrib.AddRange(template.characterClass.attributes);
				
			if (template.species != null && template.species.attributes.Length > 0)
				attrib.AddRange(template.species.attributes);
			
			foreach (CharacterAttributeWeight default_attrib in attrib) {
				CharacterAttribute target_attrib = stats.attributes.FirstOrDefault(x => x.template == default_attrib.template);
				if (target_attrib != null) {
					target_attrib.value += default_attrib.value;
				}
			}
			
			// -- Default Abilities (from Class and Species)
			
			List<AbilityLevel> abil = new List <AbilityLevel>();
			
			if (template.characterClass != null &&  template.characterClass.defaultAbilities.Length > 0)
				abil.AddRange(template.characterClass.defaultAbilities);
			
			if (template.species != null && template.species.defaultAbilities.Length > 0)
				abil.AddRange(template.species.defaultAbilities);
			
			foreach (AbilityLevel ability in abil) {
				if (ability != null && !abilities.Any(x => x.template == ability.template))
            		abilities.Add(new InstanceSkill(ability.template, ability.level));
            }
			
			// -- Default Abilities (from Template)
            foreach (TemplateSkill ability in template.defaultAbilities) {
            	if (ability != null && !abilities.Any(x => x.template == ability))
            		abilities.Add(new InstanceSkill(ability, 1));
            }
			
			LevelUpAbilities(false);
			ScaleStats();
			CalculateDerivedStats();
			RestoreToFull();
			
		}
		
 		// -------------------------------------------------------------------------------
		// LevelUpAbilities
		// -------------------------------------------------------------------------------
		protected void LevelUpAbilities(bool currentLevelOnly=true) {
			
			List<AbilityList> abil = new List <AbilityList>();
			
			if (template.characterClass != null &&  template.characterClass.levelupAbilities.Length > 0)
				abil.AddRange(template.characterClass.levelupAbilities);
			
			if (template.characterClass != null &&  template.characterClass.levelupAbilities.Length > 0)
				abil.AddRange(template.species.levelupAbilities);
			
			if (currentLevelOnly) {
				
				// -- Used at Level-Up (from Class and Species)
				
				if (abil.Count >= Level && abil[Level] != null) {
					if (abil[Level].abilities.Count > 0) {
						foreach (AbilityLevel tmpl in abil[Level].abilities) {
							abilities.Add(new InstanceSkill(tmpl.template, tmpl.level));
							Finder.log.Add(Name + " " + Finder.txt.actionNames.learned + " " + tmpl.template.name);
						}
					}
				}
				
			} else {
				
				// -- Used when Initialized (from Class and Species)
				
				for (int i = 0; i < Level; i++) {
					if (abil.Count >= i+1) {
						if (abil[i].abilities.Count > 0) {
							foreach (AbilityLevel tmpl in abil[i].abilities) {
								abilities.Add(new InstanceSkill(tmpl.template, tmpl.level));
							}
						}
					}
				}
			
			}
		
		}

  		// -------------------------------------------------------------------------------
		// ScaleStats
		// -------------------------------------------------------------------------------
		protected void ScaleStats() {
           	
           	AttributePoints = 0;
           	AbilityPoints = 0;
           	
           	if (Level > 1) {
				AttributePoints = Level-1 * Finder.config.attributePointsPerLvl;
            	AbilityPoints 	= Level-1 * Finder.config.abilityPointsPerLvl;
            }
            
            // -- Scale Stats (according to Character Class and Species)
            
            StatWeights weights = new StatWeights();
            
            if (template.characterClass != null)
            	weights.AddRange(DictionaryCharacterClass.GetWeight(template.characterClass.name));
            
            if (template.species != null)
            	weights.AddRange(DictionarySpecies.GetWeight(template.species.name));
            
            while (AttributePoints > 0) {
            
                TemplateMetaAttribute scale_attrib = weights.GetStatToIncrement();
                CharacterAttribute target_attrib = stats.attributes.FirstOrDefault(x => x.template == scale_attrib);
                
                if (target_attrib != null)
                	target_attrib.value++;
                	
                AttributePoints--;
                
            }
            
			// -- Scale Abilities
			
			if (AbilityPoints > 0 && abilities.Count > 0) {
			
				while (AbilityPoints > 0) {
					InstanceSkill instance = abilities[UnityEngine.Random.Range(0, abilities.Count)];
					if (instance.level < instance.template.maxLevel) {
						instance.level++;
					}
					AbilityPoints--;
				}
			
			}
			
            stats.XP 	= stats.AttributesSum() * UnityEngine.Random.Range(1, 2 + Level);
            
        }
        	
    	// -------------------------------------------------------------------------------
		// CalculateDerivedStat
		// -------------------------------------------------------------------------------
		protected float CalculateDerivedStat(DerivedStatType type, float baseValue = 0) {
			
			float total = 0;
			
			// -- Attribute Bonus
			foreach (var tmpls in DictionaryAttribute.dict) {
            	DerivedStatModifier tmpl = tmpls.Value.statModifiers.FirstOrDefault(x => x.template == type);
            	if (tmpl != null) {
            		if (tmpl.percentageBased) {
            			total += baseValue * tmpl.value;
            		} else {
            			total += tmpl.value;
            		}
            	}
            }

			return total;
		
		}
		
		// -------------------------------------------------------------------------------
		// CalculateCombatStyle
		// -------------------------------------------------------------------------------
		protected int CalculateCombatStyle(TemplateMetaCombatStyle style, int baseValue = 0, bool attack = false) {
			
			int total = 0;
			
			// -- Attribute Bonus
			foreach (var tmpls in DictionaryAttribute.dict) {
            	CombatStyleModifier tmpl = tmpls.Value.combatStyleModifiers.FirstOrDefault(x => x.template == style);
            	if (tmpl != null) {
            		if (tmpl.percentageBased) {
            			if (attack) {
            				total += baseValue * tmpl.attackValue;
            			} else {
            				total += baseValue * tmpl.defenseValue;
            			}
            		} else {
            			if (attack) {
            				total += tmpl.attackValue;
            			} else {
            				total += tmpl.defenseValue;
            			}
            		}
            	}
            }

			return total;
		
		}
		
  		// -------------------------------------------------------------------------------
		// CalculateDerivedStats
		// -------------------------------------------------------------------------------
		public void CalculateDerivedStats() {
			
			// --------------------------------------------------------------------------- Reset Derived Stats
			
 			stats.MaxHP 			= 0;
            stats.MaxMP 			= 0;
            stats.Accuracy			= 0;
            stats.Resistance		= 0;
            stats.CritRate			= 0;
            stats.CritFactor		= 0;
            stats.BlockRate			= 0;
			stats.BlockFactor		= 0;
			stats.Initiative		= 0;
			stats.Attacks			= 0;
			
			// --------------------------------------------------------------------------- Reset Combat Styles
			
			foreach (CombatStyle style in stats.combatStyles) {
				style.attackValue 	= 0;
				style.defenseValue 	= 0;
			}
			
			// ---------------------------------------------------------------------------  Default Derived Stats
			
			List<DerivedStat> derived = new List <DerivedStat>();
			
			if (template.characterClass != null)
				derived.AddRange(template.characterClass.derivedStats);
			
			if (template.species != null)
				derived.AddRange(template.species.derivedStats);
			
			foreach (DerivedStat stat in derived) {
		
				switch (stat.template) {
				
					case DerivedStatType.maxHP:
						stats.MaxHP 			+= (int)stat.value;
						break;
        			case DerivedStatType.maxMP:
        				stats.MaxMP 			+= (int)stat.value;
        				break;
        			case DerivedStatType.accuracy:
        				stats.Accuracy 			+= (int)stat.value;
        				break;
        			case DerivedStatType.resistance:
        				stats.Resistance 		+= (int)stat.value;
        				break;
        			case DerivedStatType.critRate:
        				stats.CritRate 			+= stat.value;
        				break;
        			case DerivedStatType.critFactor:
        				stats.CritFactor 		+= stat.value;
        				break;
        			case DerivedStatType.blockRate:
        				stats.BlockRate 		+= stat.value;
        				break;
        			case DerivedStatType.blockFactor:
        				stats.BlockFactor 		+= stat.value;
        				break;
        			case DerivedStatType.initiative:
						stats.Initiative 		+= (int)stat.value;
						break;
					case DerivedStatType.attacks:
						stats.Attacks 			+= (int)stat.value;
						break;
						
				}
				
			}
			
			// ---------------------------------------------------------------------------  Bonus Derived Stats
			
 			stats.MaxHP 			+= (int)CalculateDerivedStat(DerivedStatType.maxHP, stats.MaxHP);
            stats.MaxMP 			+= (int)CalculateDerivedStat(DerivedStatType.maxMP, stats.MaxMP);
            stats.Accuracy			+= (int)CalculateDerivedStat(DerivedStatType.accuracy, stats.Accuracy);
            stats.Resistance		+= (int)CalculateDerivedStat(DerivedStatType.resistance, stats.Resistance);
            stats.CritRate			+= CalculateDerivedStat(DerivedStatType.critRate, stats.CritRate);
            stats.CritFactor		+= CalculateDerivedStat(DerivedStatType.critFactor, stats.CritFactor);
            stats.BlockRate			+= CalculateDerivedStat(DerivedStatType.blockRate, stats.BlockRate);
			stats.BlockFactor		+= CalculateDerivedStat(DerivedStatType.blockFactor, stats.BlockFactor);
			stats.Initiative		+= (int)CalculateDerivedStat(DerivedStatType.initiative, stats.Initiative);
			stats.Attacks			+= (int)CalculateDerivedStat(DerivedStatType.attacks, stats.Attacks);
			
			// --------------------------------------------------------------------------- Default Combat Styles
			
			List<CombatStyleModifier> styles = new List <CombatStyleModifier>();
			
			if (template.characterClass != null && template.characterClass.combatStyles.Length > 0)
				styles.AddRange(template.characterClass.combatStyles);
			
			if (template.species != null && template.species.combatStyles.Length > 0)
				styles.AddRange(template.species.combatStyles);
				
			foreach (var style in DictionaryCombatStyle.dict) {
            
            	CombatStyleModifier tmpl = styles.FirstOrDefault(x => x.template == style.Value);
            	if (tmpl != null) {
            		stats.combatStyles.FirstOrDefault(x => x.template == style.Value).attackValue += tmpl.attackValue;
            		stats.combatStyles.FirstOrDefault(x => x.template == style.Value).defenseValue += tmpl.defenseValue;
            	}
            	
            }
			
			// --------------------------------------------------------------------------- Bonus Combat Styles
			
			foreach (CombatStyle style in stats.combatStyles) {
				style.attackValue 	+= CalculateCombatStyle(style.template, style.attackValue, true);
				style.defenseValue 	+= CalculateCombatStyle(style.template, style.defenseValue, false);
			}
			
            // --------------------------------------------------------------------------- Apply Resistance Modifiers
            
            List<ElementalResistanceModifier> resis = new List <ElementalResistanceModifier>();
            
            if (template.characterClass != null && template.characterClass.resistances.Length > 0)
				resis.AddRange(template.characterClass.resistances);
			
			if (template.species != null && template.species.resistances.Length > 0)
				resis.AddRange(template.species.resistances);
            
            foreach (var ele in DictionaryElement.dict) {
            
            	ElementalResistanceModifier tmpl = resis.FirstOrDefault(x => x.template == ele.Value);
            	ElementalResistance resistance = stats.resistances.FirstOrDefault(x => x.template == ele.Value);
            	
            	if (tmpl != null) {
            	
            		float value = 0;
            		
            		if (tmpl.percentageBased) {
            			value = resistance.value * tmpl.value;
            		} else {
            			value = tmpl.value;
            		}
            		
            		resistance.value += value;
            	}
            }
            
 			// --------------------------------------------------------------------------- Apply Equipment Modifiers
 			
			if (Finder.party.equipment.Equipment.Count > 0) {
				foreach (InstanceEquipment item in Finder.party.equipment.Equipment.Where(x => x.IsEquipped && x.character == this))
					item.template.statModifiers.applyModifiers(this);
            }
			
			// --------------------------------------------------------------------------- Apply Buff Modifiers
            if (buffs != null) {
                foreach (InstanceStatus buff in buffs)
                	buff.template.statModifiers.applyModifiers(this);
            }
            
        }
        
        // ===============================================================================
		// OTHER
		// ===============================================================================

   		// -------------------------------------------------------------------------------
		// OnPropertyChanged
		// -------------------------------------------------------------------------------
   		protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler h = PropertyChanged;
            if (h != null)
                h(this, new PropertyChangedEventArgs(name));
        }
		
		// -------------------------------------------------------------------------------
		// OnLevelUp
		// -------------------------------------------------------------------------------
        protected void OnLevelUp() {
            EventHandler h = LevelUp;
            if (h != null)
                h(this, null);
        }
		
 		// -------------------------------------------------------------------------------
		// UpdateBuffDurations
		// -------------------------------------------------------------------------------
 		public void UpdateBuffDurations() {
            if (buffs != null && buffs.Count > 0) {
                buffs.ForEach(x => x.remainingDuration -= 1);
                buffs.ForEach(x => x.StopEffect());
                buffs.RemoveAll(x => !x.template.permanent && x.remainingDuration <= 0);
                CalculateDerivedStats();
            }
            UpdateBuffs();
        }
        
		// -------------------------------------------------------------------------------
		
    }
}