// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// BattleManager
	// ===================================================================================
    public class BattleManager : MonoBehaviour {
		
		// ============================ INSPECTOR VARIABLES ==============================
		
		[Header("-=- Damage & Combat -=-")]
		public TemplateStatus SetStatusOnDefend;
    	[Range(0,100)]	public int runChanceNormal;
    	[Range(0,100)]	public int runChanceBoss;
    	[Range(0,10)]	public float maxDamageVariation;
    	[Range(0,1)]	public float minHitChance;
    	[Range(0,1)]	public float maxHitChance;
    	[Range(0,10)]	public float speciesFactor;
    	
		[Header("-=- Timing -=-")]
		[Range(0.1f,10f)]public float combatDelay;

		// ============================= CLASS VARIABLES =================================
		
		[Header("-=- Layout -=-")]
		public Transform monsterPartyFront;
		public Transform monsterPartyRear;
        public PartyMonster monsterParty;
        public Image battleBackground;
        public GameObject formationPrefab;
        
        public Transform monsterFormationRear;
        public Transform monsterFormationFront;
        public Transform heroFormationFront;
        public Transform heroFormationRear;
        
        public Button btnPlayerRun;
		public Button btnPlayerAttack;
		public Button btnPlayerItems;
		public Button btnPlayerSpell;
		public Button btnPlayerDefend;
		public Button btnPlayerSwitch;
		
        public MonsterEncounters[] monsterEncounters	{ get; set; }
        public int stepsSinceLastFight 					{ get; set; }
        public int monsterLevel 						{ get; set; }
        public float defaultEncounterRate 				{ get; set; }
        public float encounterRate 						{ get; set; }
        
        public bool InBattle 							{ get; private set; }
        public bool InBossBattle 						{ get; private set; }
        public bool IsGameOver 							{ get; private set; }
        public EncounterType encounterType				{ get; set; }
        
	    protected CharacterBase currentCharacter;
        protected List<CharacterBase> characterOrder;
        protected bool canFlee;
		
		// -------------------------------------------------------------------------------
		// Awake
		// -------------------------------------------------------------------------------
        void Awake() {
            encounterRate = defaultEncounterRate;
            Finder.navi.PlayerMoved += playerNavigation_PlayerMoved;
        }
        
 		// -------------------------------------------------------------------------------
		// RollForBattle
		// -------------------------------------------------------------------------------
 		public void RollForBattle(float encRate, int encLevel, int encAmountMin, int encAmountMax, bool encAmountScale, int poolId) {
            if (encounterRate + encRate > 0) {
                float val = UnityEngine.Random.Range(0f, 1.0f);
                if (val <= encounterRate + encRate)
                    StartBattle(poolId, encLevel, encAmountMin, encAmountMax, encAmountScale, true);
            }
        }
        
		// -------------------------------------------------------------------------------
		// setupEncounters
		// -------------------------------------------------------------------------------
        public void setupEncounters(int mLevel, float encRate, MonsterEncounters[] mapMonsterEncounters, Sprite background) {
        
         	monsterLevel 			= mLevel;
            encounterRate 			= encRate;
            defaultEncounterRate 	= encRate;
            monsterEncounters		= mapMonsterEncounters;
            
            if (background != null) {
            	battleBackground.sprite	= background;
        		battleBackground.canvasRenderer.SetAlpha(1f);
        	} else {
        		battleBackground.canvasRenderer.SetAlpha(0f);
        	}
        	
        }
        
		// -------------------------------------------------------------------------------
		// StartBattle
		// -------------------------------------------------------------------------------
        public bool StartBattle(int poolId, int encLevel, int encAmountMin, int encAmountMax, bool encAmountScale, bool CanFlee=true) {
        	
        	Finder.fx.Fade(true);
        	
        	InBattle = true;
            IsGameOver = false;
            stepsSinceLastFight = 0;
            encounterRate /= 2;
            canFlee = CanFlee;
            
            Finder.navi.enabled = false;
            Finder.ui.ActivateBattleCommands(true);
            
            Finder.party.BattlePreperations();
            
            Finder.ui.OverrideState(UIState.Battle);
            
			Finder.log.Clear();
            Finder.log.Show();
            
			CreateEnemies(poolId, encLevel, encAmountMin, encAmountMax, encAmountScale);
			
			Finder.audio.PlayBGM(encounterType);
			btnPlayerRun.interactable = true;
			btnPlayerRun.UpdateTextColor(Color.white);
			
			if (encounterType != EncounterType.Normal) {
            	InBossBattle = true;
            } else {
			 	InBossBattle = false;
            }
			
			SetupTurnOrder();
            
            NextCharacterTurn();
            
            return Finder.party.IsAlive;
            
        }
        
       	// -------------------------------------------------------------------------------
		// CalculateEncounterAmount
		// -------------------------------------------------------------------------------
        public int CalculateEncounterAmount(int encAmountMin, int encAmountMax, bool encAmountScale) {
        	
        	int amount = UnityEngine.Random.Range(encAmountMin,encAmountMax);
        	
        	if (encAmountScale)
        		amount += UnityEngine.Random.Range(0,Finder.party.characters.Count);
        	
        	amount = Mathf.Max(1, amount);
        	
        	return amount;
        	
        }
        
		// -------------------------------------------------------------------------------
		// CreateEnemies
		// -------------------------------------------------------------------------------
		private void CreateEnemies(int poolId, int encLevel, int encAmountMin, int encAmountMax, bool encAmountScale) {
            
            monsterPartyFront.gameObject.SetActive(true);
            monsterPartyRear.gameObject.SetActive(true);
            
            int encAmount = CalculateEncounterAmount(encAmountMin, encAmountMax, encAmountScale);
            
            for (int i = 1; i <= encAmount; i++)
            	CreateMonster(poolId, encLevel);
            	
        }
        
        // -------------------------------------------------------------------------------
		// CreateMonster
		// -------------------------------------------------------------------------------
        public void CreateMonster(int poolId, int encLevel) {
        
            if (monsterEncounters == null || monsterEncounters.Length < poolId)
                throw new InvalidOperationException("No Monster Types registered");
			 
            // ---- Select random Encounter
			
			MonsterEncounter enc = null;
			
			List<MonsterEncounter> encList = new List<MonsterEncounter>();

			encList.AddRange(monsterEncounters[poolId].monsterEncounter);
			encList = encList.OrderBy(x => x.encounterRate).ToList();
			
			enc = encList.FirstOrDefault(x => UnityEngine.Random.Range(0.0f, 1.0f) <= x.encounterRate);
			
			if (enc != null) {
			
				Transform transform;
				TemplateCharacterMonster tmpl = enc.monster;
				
				if ( (enc.useAcquisitionRules && tmpl.AcquisitionRequirementsMet) || !enc.useAcquisitionRules) {
					
					if (tmpl.defaultRank == RankType.Front) {
						transform = monsterPartyFront;
					} else {
						transform = monsterPartyRear;
					}
					
					GameObject newMonster = (GameObject)Instantiate(tmpl.prefab, transform);
			
					Image img = newMonster.GetComponent<Image>();
					img.sprite = tmpl.icon;
			
					// ---- Setup Monster Instance
			
					CharacterMonster dungeonMonster = new CharacterMonster();
			
					dungeonMonster.parent		= newMonster;
					dungeonMonster.template		= tmpl;
					dungeonMonster.Level		= monsterLevel + encLevel;
					dungeonMonster.lootContent	= tmpl.lootContent;
			
					dungeonMonster.DefaultInitialize();
			
					monsterParty.characters.Add(dungeonMonster);
			
					// -- Calculate Encounter Type
			
					if (enc.encounterType == EncounterType.Miniboss && encounterType != EncounterType.Boss && encounterType != EncounterType.Endboss) {
						encounterType = EncounterType.Miniboss;
					} else if (enc.encounterType == EncounterType.Boss && encounterType != EncounterType.Endboss) {
						encounterType = EncounterType.Boss;
					} else if (enc.encounterType == EncounterType.Endboss) {
						encounterType = EncounterType.Endboss;
					} else {
						encounterType = EncounterType.Normal;
					}
				
				}
        	
        	}
        		
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected void SetupTurnOrder() {
        	characterOrder = new List<CharacterBase>();
        	characterOrder.AddRange(Finder.party.characters);
        	characterOrder.AddRange(monsterParty.characters);
        	characterOrder.OrderByDescending(x => x.stats.Initiative);
        	currentCharacter = null;
        }
        
  		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected CharacterBase NextCharacter(CharacterBase currCharacter) {
        	var i = characterOrder.IndexOf(currCharacter);
        	i = (i == characterOrder.Count - 1) ? 0 : i +1;
        	return characterOrder[i];
        }
        
   		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected void NextCharacterTurn() {
        
        	if (InBattle) {
        		
				Finder.party.CalculateDerivedStats();
				monsterParty.CalculateDerivedStats();
				
				UpdateFormation();
				
				currentCharacter = NextCharacter(currentCharacter);
				
				if (currentCharacter.IsAlive)
					Finder.log.Add(currentCharacter.Name + Finder.txt.basicVocabulary.turnStart);
				
				currentCharacter.UpdateBuffDurations();
				
				CheckVictory(false);
				
				if (currentCharacter is CharacterHero) {
					UpdateBattleCommands();
					((CharacterHero)currentCharacter).HeroTurn();
				} else if (currentCharacter is CharacterMonster) {
					Finder.ui.ActivateBattleCommands(false);
					((CharacterMonster)currentCharacter).EnemyTurn();
					currentCharacter.BattleTurnFinished = true;
				}
				
				StartCoroutine(WaitForTurnCompletion());
								
        	}
        
        }
    	
     	// -------------------------------------------------------------------------------
		// WaitForTurnCompletion
		// -------------------------------------------------------------------------------  	
    	protected IEnumerator WaitForTurnCompletion() {
    	
    		while (!currentCharacter.BattleTurnFinished)
               	yield return null;
               	
            currentCharacter.BattleTurnFinished = false;
            CheckVictory();
    	
    	}
    	
    	// -------------------------------------------------------------------------------
		// CheckVictory
		// -------------------------------------------------------------------------------
    	protected void CheckVictory(bool autoNextTurn=true) {
        
			if (!monsterParty.IsAlive) {
				Finder.ui.ActivateBattleCommands(false);
				
				StartCoroutine(WonBattle());
				
			} else if (!Finder.party.IsAlive) {
				Finder.ui.ActivateBattleCommands(false);
			
				Finder.log.Add(Finder.txt.basicVocabulary.battleFail);
				StartCoroutine(GameOver());
				
			} else {
				if (autoNextTurn) Invoke("NextCharacterTurn", combatDelay);
			}
        	
        }
     
		// ===============================================================================
		// EVENTS
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        void playerNavigation_PlayerMoved(Vector3 obj) {
            stepsSinceLastFight++;
            if (encounterRate < defaultEncounterRate && stepsSinceLastFight >= Finder.config.stepsToResetFightChance) {
                encounterRate *= 2;
                if (encounterRate > defaultEncounterRate)
                    encounterRate = defaultEncounterRate;
            }
        }
                
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		private void OnDestroy() {
        	if (Finder.navi != null)
            	Finder.navi.PlayerMoved -= playerNavigation_PlayerMoved;
        }
        
    
		// ===============================================================================
		// END OF BATTLE FUNCTIONS
		// ===============================================================================

  		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected IEnumerator WonBattle() {
        
            yield return new WaitForSeconds(combatDelay);
			
			Finder.log.Add(Finder.txt.basicVocabulary.battleSuccess);
			            
            Finder.audio.StopBGM();
            
            if (encounterType == EncounterType.Normal) {
                Finder.audio.PlaySFX(SFX.JingleVictory);
            } else {
            	Finder.audio.PlaySFX(SFX.JingleVictoryBoss);
			}

            yield return new WaitForSeconds(combatDelay);

 			if (encounterType == EncounterType.Endboss) {
 			
            	Reset();
            	Finder.map.WarpOutro();
            	
            } else {

				var exp = monsterParty.TotalExperience/Finder.party.characters.Count;
            	Finder.party.AddExperience(exp);
				GainLoot();
				yield return new WaitForSeconds(combatDelay);
				Reset();
            	ReturnToNavigation();
            
            }
            
        }
        
  		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected void GainLoot() {
        
        	List<LootDrop> lootdrop = new List<LootDrop>();
			
			foreach (CharacterMonster monster in monsterParty.characters)
				lootdrop.AddRange(monster.lootContent);	
			
			RPGHelper.DropLoot(lootdrop);
        
        }
       	
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected void ReturnToNavigation() {
            
            Reset();
            
            Finder.party.BattlePreperations();
			Finder.ui.OverrideState(UIState.Dungeon);
			Finder.audio.PlayBGM(Finder.map.currentDungeonConfig.music);
			Finder.navi.enabled = true;
            
        }

 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void Reset() {
           
            InBattle 		= false;
            InBossBattle 	= false;
            
            DestroyEnemies();
            
        }
	
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------      
		private void UpdateBattleCommands() {
			
			if (!canFlee)
				btnPlayerRun.interactable 	= false;
			
			if (currentCharacter.IsSilenced)
				btnPlayerSpell.interactable = false;
			
			if (currentCharacter.IsStunned)
				ActivateBattleCommands(false);
			
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------      
		private void ActivateBattleCommands(bool p) {
			btnPlayerAttack.interactable 	= p;
			btnPlayerItems.interactable 	= p;
			btnPlayerSpell.interactable 	= p;
			btnPlayerDefend.interactable 	= p;
			btnPlayerSwitch.interactable	= p;
			btnPlayerRun.interactable 		= p;
		}
		
  		// -------------------------------------------------------------------------------
		// DestroyEnemies
		// -------------------------------------------------------------------------------      
        private void DestroyEnemies() {
        	
        	monsterParty.Clear();
        
            foreach (Transform t in monsterPartyFront)
     			Destroy(t.gameObject, Finder.battle.combatDelay);
 			
 			 foreach (Transform t in monsterPartyRear)
     			Destroy(t.gameObject, Finder.battle.combatDelay);
 			
 			monsterPartyFront.gameObject.SetActive(false);
 			monsterPartyRear.gameObject.SetActive(false);
 			
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------      
        private void ForceEndBattle() {
        	Reset();
			Invoke("ReturnToNavigation", Finder.battle.combatDelay);
        }
        
  		// ===============================================================================
		// FORMATION FUNCTIONS
		// ===============================================================================
      
        public void UpdateFormation() {
        	
        	Transform targetTransform;
        	
        	foreach (Transform t in monsterFormationRear)
     			Destroy(t.gameObject);
 			
 			foreach (Transform t in monsterFormationFront)
     			Destroy(t.gameObject);

       		foreach (Transform t in heroFormationRear)
     			Destroy(t.gameObject);
 			
 			foreach (Transform t in heroFormationFront)
     			Destroy(t.gameObject);
        
        	
        	foreach (CharacterBase character in Finder.party.characters) {
        	
        		if (character.IsRearguard) {
        			targetTransform = heroFormationRear;
        		} else {
        			targetTransform = heroFormationFront;
        		}
        		
        		GameObject newObj = (GameObject)Instantiate(formationPrefab, targetTransform);
        		FormationSlot slot = newObj.GetComponent<FormationSlot>();
				slot.Initialize(character);
				
        	}
        	
        	foreach (CharacterBase character in monsterParty.characters) {
        	
        		if (character.IsRearguard) {
        			targetTransform = monsterFormationRear;
        		} else {
        			targetTransform = monsterFormationFront;
        		}
        		
        		GameObject newObj = (GameObject)Instantiate(formationPrefab, targetTransform);
        		FormationSlot slot = newObj.GetComponent<FormationSlot>();
				slot.Initialize(character);
				
        	}
        	
        }
        
		// ===============================================================================
		// PLAYER MENU COMMANDS
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// PlayerAttack (Pre Selection)
		// -------------------------------------------------------------------------------
        public void PlayerAttack() {
            Finder.ui.ActivateBattleCommands(false);
            Finder.ui.ActivateBattleCommands(false);
			Finder.ui.Prompt(UIState.BattlePlayerAttack, false, true);
        }
        
 		// -------------------------------------------------------------------------------
		// ProcessPlayerAttack (Post Selection)
		// -------------------------------------------------------------------------------
        public void ProcessPlayerAttack() {
        
        	CharacterBase[] targets = Finder.ui.GetSelectedCharacters();
			
			if (targets != null) {
				currentCharacter.CommandAttack(targets);
           		currentCharacter.BattleTurnFinished = true;
        	} else {
        		PlayerCancel();
        	}
        
        }

		// -------------------------------------------------------------------------------
		// PlayerCastSpell (Pre Selection)
		// -------------------------------------------------------------------------------
        public void PlayerCastSpell() {
        	Finder.ui.ActivateBattleCommands(false);
			Finder.ui.ForceSelection(currentCharacter);
			Finder.ui.PushState(UIState.ShowSelectedCharacterAbilities);
        }

		// -------------------------------------------------------------------------------
		// PlayerCastSpell (Post Selection)
		// -------------------------------------------------------------------------------
        public void PlayerCastSpell(InstanceSkill instance, CharacterBase[] targets) {
			
			Finder.ui.ActivateBattleCommands(false);
			
			if (targets != null) {
			
                Finder.ui.PopState();
                Finder.ui.ActivateBattleCommands(false);
            
            	if (currentCharacter.CommandCastSpell(instance, targets)) {
            		if (instance.template.getCharacterActionType == CharacterActionType.PlayerExitBattle)  {
						ForceEndBattle();
					} else {
            			currentCharacter.BattleTurnFinished = true;
            		}
            	}
            				
            } else {
            	PlayerCancel();
            }
            
        }
        
		// -------------------------------------------------------------------------------
		// PlayerUseItem (Pre Selection)
		// -------------------------------------------------------------------------------
        public void PlayerUseItem() {
        	Finder.ui.ActivateBattleCommands(false);
            Finder.ui.PushState(UIState.Inventory);
        }
        
		// -------------------------------------------------------------------------------
		// PlayerUseItem (Post Selection)
		// -------------------------------------------------------------------------------
		public void PlayerUseItem(InstanceItem instance, CharacterBase[] targets) {
			
			Finder.ui.ActivateBattleCommands(false);
			
			if (targets != null) {
			
                Finder.ui.PopState();
                Finder.ui.ActivateBattleCommands(false);
            
            	if (currentCharacter.CommandUseItem(instance, targets)) {
            
            		if (instance.template.getCharacterActionType == CharacterActionType.PlayerExitBattle)  {
						ForceEndBattle();
					} else {
            			currentCharacter.BattleTurnFinished = true;
            		}
            	
            	}
            	
            } else {
            	PlayerCancel();
            }
            
        }
        
		// -------------------------------------------------------------------------------
		// PlayerDefend
		// -------------------------------------------------------------------------------
        public void PlayerDefend() {
			Finder.ui.ActivateBattleCommands(false);
           	currentCharacter.CommandDefend();
        	currentCharacter.BattleTurnFinished = true;
        }
        
 		// -------------------------------------------------------------------------------
		// PlayerSwitch
		// -------------------------------------------------------------------------------
        public void PlayerSwitch() {
        	Finder.ui.ActivateBattleCommands(false);
           	currentCharacter.CommandSwitch();
        	currentCharacter.BattleTurnFinished = true;
        }

		// -------------------------------------------------------------------------------
		// PlayerRun
		// -------------------------------------------------------------------------------
		public void PlayerRun() {
 			
 			Finder.ui.ActivateBattleCommands(false);
 			
 			bool success = currentCharacter.CommandPlayerRun();

            if (success)  {
				ForceEndBattle();
			} else {
				currentCharacter.BattleTurnFinished = true;
			}
			
        }
        
  		// -------------------------------------------------------------------------------
		// PlayerCancel
		// -------------------------------------------------------------------------------
        protected void PlayerCancel() {
        	Finder.ui.ActivateBattleCommands(true);
        }

      	// ===============================================================================
		// GAME OVER FUNCTIONS
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// GameOver
		// -------------------------------------------------------------------------------
        private IEnumerator GameOver() {
        	DestroyEnemies();
        	Finder.map.ClearMap();
            Finder.party.ForceClearBuffs();
            Finder.party.Clear();
            Finder.log.Clear();
            Finder.log.Hide();
            Finder.audio.StopBGM();
            Finder.audio.PlaySFX(SFX.GameOver);
            IsGameOver = true;
            Finder.ui.GameOver(true);
            Finder.ui.ActivateBattleCommands(false);
            yield return null;
        }
        
		// -------------------------------------------------------------------------------
		// GameOverReturnToTown
		// -------------------------------------------------------------------------------
        public void GameOverReturnToTown() {
        	
            Finder.audio.StopBGM();
            Finder.audio.StopSFX();
            ReturnToNavigation();
            Finder.ui.GameOver(false);
            
            Finder.party.RestoreHP(1);
            Finder.party.currencies.setResource(CurrencyType.Gold, (int)(Finder.party.currencies.getResource(CurrencyType.Gold) * Finder.config.deathGoldPenalty *-1));
            Finder.map.WarpTown(Finder.party.lastTown);
            IsGameOver = false;
        }
        
		// -------------------------------------------------------------------------------
		// GameOverLoadGame
		// -------------------------------------------------------------------------------
        public void GameOverLoadGame() {
        	
            Finder.audio.StopBGM();
            Finder.audio.StopSFX();
            ReturnToNavigation();
            Finder.ui.GameOver(false);
           
            Finder.save.LoadLastSave();
            IsGameOver = false;
        }

        // -------------------------------------------------------------------------------
        
    }
}