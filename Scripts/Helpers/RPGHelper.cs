using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using WoCo.DungeonCrawler; 

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// RPG HELPER
	// ===================================================================================
    public static class RPGHelper {
		
		// -------------------------------------------------------------------------------
		// getRandomPlayer
		// -------------------------------------------------------------------------------      
		public static CharacterBase[] getRandomPlayer() {
		
			System.Random rnd = new System.Random();
			CharacterBase tmpChar = Finder.party.characters[rnd.Next(Finder.party.characters.Count)];
			
			if (tmpChar.IsAlive)
					return new CharacterBase[] { tmpChar };
			
			while(!tmpChar.IsAlive)
			{
			
				tmpChar = Finder.party.characters[rnd.Next(Finder.party.characters.Count)];
				
				if (tmpChar.IsAlive)
					return new CharacterBase[] { tmpChar };
				
			}
			
			return new CharacterBase[] { null };
		}
		
		// -----------------------------------------------------------------------------------
		// getPercentageValue
		// -----------------------------------------------------------------------------------
		public static float getPercentageValue(int Value, int MaxValue) {
			return (Value != 0 && MaxValue != 0) ? (float)Value / (float)MaxValue : 0;
		}
		
		// -------------------------------------------------------------------------------
		// NextLvlExp
		// -------------------------------------------------------------------------------
        public static int NextLvlExp(int lvl) {
        	return (int)((lvl+1 * Finder.config.nextLevelExpBase) * Finder.config.nextLevelExpFactor);
        }
        
        // -------------------------------------------------------------------------------
		// DamageTargets
		// -------------------------------------------------------------------------------
        public static void DamageTargets(CharacterBase source, InstanceBase activator, int amount, CharacterBase[] targets) {
        
        	foreach (CharacterBase target in targets) {
				
				if (target != null) {
					
					int level = 0;
					int accuracy = 0;
					TemplateAdvanced template = null;
					
					if (activator is InstanceItem) {
            			template = ((InstanceItem)activator).template;
            			//level = ((InstanceItem)activator).level;
            		} else if (activator is InstanceSkill) {
            			template = ((InstanceSkill)activator).template;
            			level = ((InstanceSkill)activator).level;
            		}
            		
					amount += template.CalculatedEffect(source, target, level);
					
					if (source != null)
						accuracy 	= source.stats.Accuracy;
					
					amount = RPGHelper.CalculateFinalDamage(amount, template.attackType, template.element, target);
					
					if (source != null)
						amount += source.CalculateHitType(amount, target, template);
			
					target.InflictBuffs(template.useBuffType, accuracy, false, template.removeBuff);

					if (target.parent != null && template.hitEffect != null)
						Finder.fx.SpawnEffect(target.parent.transform, template.hitEffect);
					
					amount = target.InflictDamage(amount);
					
					Finder.log.Add(string.Format("{0} {1} {2} {3}", target.Name, Finder.txt.actionNames.takes, amount, Finder.txt.basicVocabulary.damage));
				
				}
            
            }
        
        }
        
        // -------------------------------------------------------------------------------
		// RecoverTargets
		// -------------------------------------------------------------------------------
        public static void RecoverTargets(CharacterBase source, InstanceBase activator, int amount, CharacterBase[] targets) {
        
        	foreach (CharacterBase target in targets) {
            	            	
            	if (target != null) {
            	
            		int level = 0;
            		int accuracy = 0;
            		string text_before 	= "";
            		string text_after 	= "";	
					TemplateAdvanced template = null;
					
					if (activator is InstanceItem) {
            			template = ((InstanceItem)activator).template;
            			//level = ((InstanceItem)activator).level;
            		} else if (activator is InstanceSkill) {
            			template = ((InstanceSkill)activator).template;
            			level = ((InstanceSkill)activator).level;
            		}
					
					amount = template.CalculatedEffect(source, target, level);
					
					if (target.parent != null && template.hitEffect != null)
						Finder.fx.SpawnEffect(target.parent.transform, template.hitEffect);
					
					if (source != null)
						accuracy = source.stats.Accuracy;
					
					target.InflictBuffs(template.useBuffType, accuracy, true, template.removeBuff);
					
					// -- Apply Stat Boost
					if (template.recoveryStat != null) {
						CharacterAttribute attrib = target.stats.attributes.FirstOrDefault(x => x.template == template.recoveryStat);	
						if (attrib != null)
							attrib.value += amount;
							Finder.log.Add(string.Format("{0} {1} {2} {3}", target.Name, Finder.txt.actionNames.gained, amount, template.recoveryStat.fullName));
					
					}
					
					// -- Apply Recovery
					switch (template.recoveryType) {
					
						case RecoveryType.HP:
							amount 				= target.RestoreHP(amount);
							text_before 		= Finder.txt.actionNames.recovered;
							text_after 			= Finder.txt.basicDerivedStatNames.HP;
							break;
					
						case RecoveryType.MP:
							amount 				= target.RestoreMP(amount);
							text_before 		= Finder.txt.actionNames.recovered;
							text_after 			= Finder.txt.basicDerivedStatNames.MP;
							break;
						
						case RecoveryType.HPandMP:
							target.RestoreMP(amount);
							amount 				= target.RestoreHP(amount);
							text_before 		= Finder.txt.actionNames.recovered;
							text_after 			= Finder.txt.basicDerivedStatNames.HP + " & " + Finder.txt.basicDerivedStatNames.MP;
							break;
						
						case RecoveryType.XP:
							target.XP 			+= amount;
							text_before 		= Finder.txt.actionNames.gained;
							text_after			= Finder.txt.basicDerivedStatNames.XP;
							break;
							
					}
										
					if (template.recoveryType != RecoveryType.None)
						Finder.log.Add(string.Format("{0} {1} {2} {3}", target.Name, text_before, amount, text_after));
				
				}
        	}
        
        }
        
        // -------------------------------------------------------------------------------
		// CalculateFinalDamage
		// -------------------------------------------------------------------------------
		public static int CalculateFinalDamage(int damage, TemplateMetaCombatStyle attackType, TemplateMetaElement element, CharacterBase target, bool IsRearguard = false) {
			
			int defense = 0;
				
			damage = (int)(damage * RPGHelper.getElementalRelation(element, target));

			defense = target.stats.combatStyles.FirstOrDefault(x => x.template == attackType).defenseValue;
			
			if (IsRearguard) {
				damage = (int)(damage * attackType.rearAttackModifier);
			} else {
				damage = (int)(damage * attackType.frontAttackModifier);
			}
			
			if (target.IsRearguard) {
				defense = (int)(defense * attackType.rearDefenseModifier);
			} else {
				defense = (int)(defense * attackType.frontDefenseModifier);
			}
			
            float variance = UnityEngine.Random.Range(1-Finder.battle.maxDamageVariation, 1+Finder.battle.maxDamageVariation);
            
            damage = (int)(damage * variance);

            damage -= defense;
            
            if (damage <= 0) damage = 1;
            
            return damage;
        }
        
 		// -------------------------------------------------------------------------------
		// getCanUseType
		// -------------------------------------------------------------------------------
		public static void DropLoot(List<LootDrop> lootdrop) {       
        	
        	List<InventorySlot> lootList;
        	
        	lootList = new List<InventorySlot>();
        	
        	InventorySlot tmpl = null;
        	
        	bool uniqueDrop = false;
        	
        	int quantity = 0;
        	int[] resourceCount = new int[Enum.GetNames(typeof(CurrencyType)).Length];
			
			if (lootdrop != null) {
			
				foreach (LootDrop loot in lootdrop) {
		
					if (
						UnityEngine.Random.value <= loot.dropChance &&
						(!loot.useAcquisitionRules ||
						(loot.useAcquisitionRules &&
						(loot.lootObject.PartyClassRequirementsMet || !loot.useClassRequirementRules) &&
						(loot.lootObject.PartyStatRequirementsMet || !loot.useStatRequirementRules) &&
						(loot.lootObject.AcquisitionRequirementsMet || !loot.useAcquisitionRules)
						))) {
					
						if (loot.minQuantity > 0 && loot.maxQuantity > 0) {
							quantity = UnityEngine.Random.Range(loot.minQuantity, loot.maxQuantity);
						} else {
							quantity = Math.Max(loot.minQuantity, loot.maxQuantity);
							if (quantity <= 0) quantity = 1;
						}
										
						if (loot.lootObject is TemplateResource && (loot.uniqueDrop == false || uniqueDrop == false)) {
						
							if (quantity > 0) {
								Finder.party.currencies.setResource(((TemplateResource)loot.lootObject).currencyType, quantity);
								resourceCount[(int)((TemplateResource)loot.lootObject).currencyType] += quantity;
								uniqueDrop = loot.uniqueDrop;
							}
						
						} else if (loot.lootObject is TemplateEquipment && (loot.uniqueDrop == false || uniqueDrop == false)) {
							TemplateEquipment obj = (TemplateEquipment)loot.lootObject;
							Finder.party.equipment.AddEquipment(obj);
							
							tmpl = new InstanceEquipment { template = obj };
							lootList.Add(tmpl);
							
							uniqueDrop = loot.uniqueDrop;
						
						} else if (loot.lootObject is TemplateItem && (loot.uniqueDrop == false || uniqueDrop == false)) {
							
							TemplateItem obj = (TemplateItem)loot.lootObject;
							Finder.party.inventory.AddItem(obj, quantity);
							
							tmpl = new InstanceItem { template = obj, Quantity = quantity };
							lootList.Add(tmpl);
							
							uniqueDrop = loot.uniqueDrop;
						}

					}
				}
			
				int i = 0;
			
				foreach (int value in resourceCount) {
					if (value > 0) {						
						tmpl = new InstanceResource { currencyType = (CurrencyType)i, Quantity = value };
						lootList.Add(tmpl);
					}
					i++;
				}
        	
        	}
        	
        	if (lootList.Count > 0)
        		lootList.Sort((x,y) => y.Quantity.CompareTo(x.Quantity));
        		Finder.loot.Show(lootList);

        }
        
 		// -------------------------------------------------------------------------------
		// getCanUseType
		// -------------------------------------------------------------------------------
		public static bool getCanUseType(CanUseType useType, CharacterBase target) {
        	
			switch (useType) {
				case CanUseType.IsHPNotMax:
					return (target.IsAlive && target.HP < target.MaxHP);
				case CanUseType.IsMPNotMax:
					return (target.IsAlive && target.MP < target.MaxMP);
				case CanUseType.IsStun:
					return target.IsStunned;
				case CanUseType.IsSilence:
					return target.IsSilenced;
				case CanUseType.IsDepleteHP:
					return target.IsDepleteHP;
				case CanUseType.IsDepleteMP:
					return target.IsDepleteMP;
				case CanUseType.IsAlive:
					return target.IsAlive;
				case CanUseType.IsDead:
					return !target.IsAlive;					
			}
        	
			return false;
        }
        
        // -------------------------------------------------------------------------------
		// getCanUseLocation
		// -------------------------------------------------------------------------------
        public static bool getCanUseLocation(CanUseLocation useLocation) {
        
        	switch (useLocation) {
        		case CanUseLocation.Everywhere:
        			return true;
				case CanUseLocation.InBattle:
					return Finder.battle.InBattle;
				case CanUseLocation.InNormalBattle:
					return Finder.battle.InBattle && !Finder.battle.InBossBattle;
				case CanUseLocation.InTown:
					return Finder.map.locationType == LocationType.Town && !Finder.battle.InBattle;
				case CanUseLocation.InTownOrWorldmap:
					return (Finder.map.locationType == LocationType.Town || Finder.map.locationType == LocationType.Worldmap) && !Finder.battle.InBattle;
				case CanUseLocation.InDungeon:
					return Finder.map.locationType != LocationType.Town && !Finder.battle.InBattle;
				case CanUseLocation.InBattleAndDungeon:
					return Finder.map.locationType != LocationType.Town && (!Finder.battle.InBattle || !Finder.battle.InBossBattle || (Finder.battle.InBattle || Finder.battle.InBossBattle));
				case CanUseLocation.InNormalBattleAndDungeon:
					return Finder.map.locationType != LocationType.Town && (!Finder.battle.InBattle || (Finder.battle.InBattle && !Finder.battle.InBossBattle));
				case CanUseLocation.InWorldmap:
					return Finder.map.locationType == LocationType.Worldmap;
			}
        
        	return false;
        
        }
        
        // -------------------------------------------------------------------------------
		// getElementalRelation
		// -------------------------------------------------------------------------------
        public static float getElementalRelation(TemplateMetaElement sourceElement, CharacterBase target) {
        	
        	float factor = 1;
        	float modifier = 1;
        	
        	if (sourceElement != null) {
	        	if (sourceElement.PrimaryRelation != null && target.Element == sourceElement.PrimaryRelation) {
					factor = sourceElement.PrimaryRelationWeight;
				} else if (sourceElement.SecondaryRelation != null && target.Element == sourceElement.SecondaryRelation) {
					factor = sourceElement.SecondaryRelationWeight;
				}
			
				modifier = factor - target.stats.resistances.FirstOrDefault(x => x.template == sourceElement).value;
			}

			return modifier;
        	
        }
        
        // -------------------------------------------------------------------------------
        
    }
}
