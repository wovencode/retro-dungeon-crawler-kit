// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// DungeonObjectEvent
	// ===================================================================================
	[RequireComponent(typeof(BoxCollider))]
    public class DungeonObjectEvent : MonoBehaviour, IEvent {
        
        public Animator animator;
        public Renderer eventRenderer;
        
        public DungeonTileEvent tile 			{ get; set; }
        
        public Sprite icon 						{ get; set; }
        public string text 						{ get; set; }
        public List<EventChoice> choices 		{ get; set; }
        
        public Location Location 				{ get; set; }
		public bool interacted 					{ get; set; }
		public bool deactivated 				{ get; set; }
        
        protected bool playerSteppedOnTile;
        protected int eventStep;
		protected DirectionType directionType;
		
		protected bool IsHidden;
		protected bool IsMoving;
    	protected float travelledDistance;
    	protected MoveType moveDirection;
    	protected EventNode currentNode;
 
        // -------------------------------------------------------------------------------
		// Update
		// -------------------------------------------------------------------------------		
		void Update() {
			
			if (!IsHidden) {
				if (tile.TriggerOnce && interacted && tile.HideOnCompletion) {
					if (eventRenderer != null) eventRenderer.enabled = false;
				} else if (!tile.TriggerOnce || !interacted) {
					if (eventRenderer != null) eventRenderer.enabled = true;
				}
			}
			
			if (tile.defaultImage != null && !interacted) {
				if (eventRenderer != null && eventRenderer is SpriteRenderer) ((SpriteRenderer)eventRenderer).sprite = tile.defaultImage;
			} else if (tile.interactedImage != null && interacted) {
				if (eventRenderer != null && eventRenderer is SpriteRenderer) ((SpriteRenderer)eventRenderer).sprite = tile.interactedImage;
			}
			
			
			if (IsMoving && moveDirection != MoveType.None)
        		StartCoroutine(AnimatedMovement(moveDirection));
			
		}
		
        // -------------------------------------------------------------------------------
		// OnTriggerStay
		// -------------------------------------------------------------------------------
 		protected virtual void OnTriggerStay(Collider col) {
 			if (col.GetComponent<PartyPlayer>() != null &&
 					!Finder.battle.InBattle &&
 					(!tile.TriggerOnce || !interacted) &&
 					Finder.navi.changedDirection &&
 					!playerSteppedOnTile &&
 					DungeonHelper.IsOppositeDirection(Finder.navi.facingDirection, tile.facingDirection)
 				) {
				StartEventNode();
            }
 		}
 		
        // -------------------------------------------------------------------------------
		// OnTriggerEnter
		// -------------------------------------------------------------------------------
        protected virtual void OnTriggerEnter(Collider col) {

        	if (col.GetComponent<PartyPlayer>() != null &&
        			!Finder.battle.InBattle &&
        			(!tile.TriggerOnce || !interacted) &&
        			(DungeonHelper.IsOppositeDirection(Finder.navi.facingDirection, tile.facingDirection, tile.interactFromBothSides) )
        		) {
				StartEventNode();
            }
        }
        
		// -------------------------------------------------------------------------------
		// OnTriggerExit
		// -------------------------------------------------------------------------------
        protected virtual void OnTriggerExit(Collider col) {
            QuitEvent();
        }
        
 		// -------------------------------------------------------------------------------
		// StartEventNode
		// -------------------------------------------------------------------------------
        protected void StartEventNode() {
        	
        	Finder.ui.PushState(UIState.MinimapHide);
        	
        	if (tile != null && tile.minimapIcon != null)
				Finder.party.MapExplorationInfo.AddMapPing(Finder.map.MapName, new Vector2(transform.position.x, transform.position.z), tile);

        	if (tile.eventChain != null) {
        		eventStep = -1;
        		choices = new List<EventChoice>();
        		NextEventNode();
        	} else {
        		CompleteEvent();
        	}
        }
        
		// -------------------------------------------------------------------------------
		// NextEventNode
		// -------------------------------------------------------------------------------
        protected void NextEventNode(int gotoIndex=-1) {
        
			if (gotoIndex <= -1) {
				eventStep++;
			} else {
				eventStep = gotoIndex;
			}
			
			currentNode = tile.eventChain.eventNodes[eventStep];
			
			if (CheckEventCondition(currentNode.mainCondition) &&
				(!currentNode.NoAutoStart || gotoIndex != -1)
				) {
				
				int i = 0;
				choices.Clear();
				
				foreach (string text in currentNode.choiceLabels) {
				
					bool enbl = true;
					if (currentNode.choiceConditions.Length != 0 &&
						currentNode.choiceConditions.Length >= i ) {
						enbl = CheckEventCondition(currentNode.choiceConditions[i]);
					}
					
					choices.Add(new EventChoice { label = text, enabled = enbl });
					i++;
				}
				
				text	= currentNode.text;
				icon	= currentNode.icon;
				
				StartCoroutine(ShowChoices());
					
			} else {
				NextEventNode();
			}
			
        }
        
		// ------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public void OnClickChoice(int id) {
			ExecuteEventActions(id);
		}

		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public bool CheckEventCondition(EventCondition condition) {
			
			if (condition == null) return true;
			
			bool party_success		= false;
			bool item_success 		= false;
			bool ability_success 	= false;
			bool event_success 		= false;
			
			// --------------------------------------------------------------------------- Required Heroes
			
			if (condition.requiredHeroes.Length > 0) {
				
				foreach (TemplateCharacterHero tmpl in condition.requiredHeroes) {
					party_success = Finder.party.HasMember(tmpl);
				
					if (party_success && !condition.requiresAllHeroes && condition.requiredHeroes.Length > 1)
						break;
				}
				
			} else {
				party_success = true;
			}
			
			// --------------------------------------------------------------------------- Required Items
			
			if (condition.items.Length > 0) {
			
				foreach (TemplateItem tmpl in condition.items) {
				
					item_success = (Finder.party.inventory.GetQuantity(tmpl) > 0) ? true : false;
			
					if (item_success && !condition.requiresAllItems && condition.items.Length > 1)
						break;
				
				}
				
			} else {
				item_success = true;
			}
			
			// --------------------------------------------------------------------------- Required Abilities
			
			if (condition.abilities.Length > 0) {
			
				foreach (AbilityLevel tmpl in condition.abilities) {
				
					CharacterBase character = Finder.party.HasAbility(tmpl.template, tmpl.level);
					ability_success = (character != null) ? true : false;
					
					if (ability_success)
						ability_success = Finder.party.CanCastAbility(tmpl.template, tmpl.level);
					
					if (ability_success && !condition.requiresAllAbilities && condition.abilities.Length > 1)
						break;
				
				}
				
			} else {
				ability_success = true;
			}
			
			// --------------------------------------------------------------------------- Required Events
			
			if (condition.events.Length > 0) {
			
				foreach (InteractableEventCondition tmpl in condition.events) {
				
					bool activate_success = true;
					bool interact_success = true;
				
					string name = string.Format("{0}_{1}", tmpl.targetX, tmpl.targetY);
					GameObject go = GameObject.Find(name);
					DungeonObjectEvent co = go.GetComponent<DungeonObjectEvent>();
				
					if (go == null || co == null)
						Debug.Log(string.Format("Error: No event for a condition at {0}/{1}", tmpl.targetX, tmpl.targetY));
				
					if (tmpl.targetActivated != BoolType.Any)
						activate_success = (co.deactivated && tmpl.targetActivated == BoolType.False) ||
										(!co.deactivated && tmpl.targetActivated == BoolType.True);
		
					if (tmpl.targetInteracted != BoolType.Any)
						interact_success = (co.deactivated && tmpl.targetActivated == BoolType.False) ||
										(!co.deactivated && tmpl.targetActivated == BoolType.True);
									
					event_success = (activate_success && interact_success) ? true : event_success;
				
					if (event_success == true && !condition.requiresAllEvents && condition.events.Length > 1)
						break;
			
				}
			} else {
				event_success = true;
			}
			
			// --------------------------------------------------------------------------- Validate Completion
			
			return party_success && item_success && ability_success && event_success;
			
		}
		
		// -------------------------------------------------------------------------------
		// ExecuteEventActions
		// -------------------------------------------------------------------------------
		protected void ExecuteEventActions(int id) {
			
			if (currentNode.choiceActions.Length == 0 ||
				currentNode.choiceActions.Length < id ||
				currentNode.choiceActions[id] == null) return;
			
			bool wait = false;
			
			// --------------------------------------------------------------------------- Play Sound
			
			Finder.audio.PlaySFX(currentNode.choiceActions[id].soundEffect);
			
			// --------------------------------------------------------------------------- Costs
			
			foreach (TemplateItem tmpl in currentNode.choiceActions[id].removeItems) {
				Finder.party.inventory.AddItem(tmpl, -1);
			}
			
			foreach (TemplateCharacterHero tmpl in currentNode.choiceActions[id].removeHeroes) {
				Finder.party.DismissHero(tmpl);
			}
			
			foreach (AbilityLevel tmpl in currentNode.choiceActions[id].castAbilities) {
				CharacterBase target = Finder.party.HasAbility(tmpl.template, tmpl.level);
				if (target != null)
					target.MP -= tmpl.template.GetCost(tmpl.level);
			}
			
			if (currentNode.choiceActions[id].removeCurrencyAmount != 0)
				Finder.party.currencies.setResource(currentNode.choiceActions[id].currencyType, currentNode.choiceActions[id].removeCurrencyAmount*-1);
			
			// --------------------------------------------------------------------------- Rewards
			
			foreach (TemplateCharacterHero tmpl in currentNode.choiceActions[id].addHeroes) {
				if (tmpl.checkQuantity) {
					Finder.party.AddHero(tmpl);
				}
			}
			
			if (currentNode.choiceActions[id].addItems.Length > 0)
				RPGHelper.DropLoot(currentNode.choiceActions[id].addItems.ToList());
			
			Finder.party.AddExperience(currentNode.choiceActions[id].addExperience);
			
			// --------------------------------------------------------------------------- Attack Party
			
			if (currentNode.choiceActions[id].attackAbility != null) {
			
				bool targetAll = false;
				
				if (currentNode.choiceActions[id].attackTarget == InteractableEventTarget.All)
					targetAll = true;
				
				InstanceSkill instance = new InstanceSkill(currentNode.choiceActions[id].attackAbility, currentNode.choiceActions[id].attackAbilityLevel);
				
				ActionDamage(instance, targetAll);
			
			}
			
			// --------------------------------------------------------------------------- Cure Party
			
			if (currentNode.choiceActions[id].curativeAbility != null) {
			
				bool targetAll = false;
				
				if (currentNode.choiceActions[id].attackTarget == InteractableEventTarget.All)
					targetAll = true;
				
				InstanceSkill instance = new InstanceSkill(currentNode.choiceActions[id].curativeAbility, currentNode.choiceActions[id].curativeAbilityLevel);
				
				ActionRecover(instance, targetAll);
			
			}
			
			// --------------------------------------------------------------------------- Manipulate other Event
			
			foreach (InteractableEventAction tmpl in currentNode.choiceActions[id].events) {
				
				string name = string.Format("{0}_{1}", tmpl.targetX, tmpl.targetY);
				GameObject go = GameObject.Find(name);
				DungeonObjectEvent co = go.GetComponent<DungeonObjectEvent>();
				
				if (go == null || co == null)
						Debug.Log(string.Format("Error: No event for condition at {0}/{1}", tmpl.targetX, tmpl.targetY));
					
				if (tmpl.targetActivation == BoolType.True) {
					co.Activate();
				} else if (tmpl.targetActivation == BoolType.False) {
					co.Deactivate();
				}
				
				if (tmpl.targetInteraction == BoolType.True) {
					co.AddInteraction();
				} else if (tmpl.targetInteraction == BoolType.False) {
					co.RemoveInteraction();
				}
				
			}
			
			// --------------------------------------------------------------------------- Manipulate this Event
			
			if (currentNode.choiceActions[id].eventActivation == BoolType.True) {
				Activate();
			} else if (currentNode.choiceActions[id].eventActivation == BoolType.False) {
				Deactivate();
			}
				
			if (currentNode.choiceActions[id].eventInteraction == BoolType.True) {
				AddInteraction();
			} else if (currentNode.choiceActions[id].eventInteraction == BoolType.False) {
				RemoveInteraction();
			}
			
			// --------------------------------------------------------------------------- Animate Event
			
			if (currentNode.choiceActions[id].moveDirection != MoveType.None) {
				moveDirection = currentNode.choiceActions[id].moveDirection;
				IsMoving = true;
			}
			
			// --------------------------------------------------------------------------- Teleportation
			
			if (currentNode.choiceActions[id].teleport.locationType != LocationType.None) {
				Finder.map.TeleportPlayer(currentNode.choiceActions[id].teleport);
			}
			
			// --------------------------------------------------------------------------- Open Shop
			
			if (currentNode.choiceActions[id].openShop != null) {
    			Finder.ui.OverrideShopState(currentNode.choiceActions[id].openShop);
    			Finder.ui.PushState(UIState.ShopOutside);
			}
			
			// --------------------------------------------------------------------------- Start Combat
			
			if (currentNode.choiceActions[id].startCombat) {
				
				int BattlePoolId 			= currentNode.choiceActions[id].battlePoolId;
				int BattleEncLevel 			= currentNode.choiceActions[id].battleEncounterLevel;
    			int BattleEncAmountMin 		= currentNode.choiceActions[id].battleEncounterAmountMin;
    			int BattleEncAmountMax		= currentNode.choiceActions[id].battleEncounterAmountMax;
    			bool BattleEncAmountScale 	= currentNode.choiceActions[id].battleEncounterAmountScale;
    			
    			Finder.battle.StartBattle(BattlePoolId, BattleEncLevel, BattleEncAmountMin, BattleEncAmountMax, BattleEncAmountScale, !tile.TriggerOnce);
    			
    			wait = true;
    			
			}
			
			// --------------------------------------------------------------------------- Complete Event
			
			StartCoroutine(WaitForCompletion(id, wait));

		}
		
		// -------------------------------------------------------------------------------
		// WaitForCompletion
		// -------------------------------------------------------------------------------  	
    	protected IEnumerator WaitForCompletion(int id, bool wait=true) {
    		
    		while ( (Finder.battle.InBattle || Finder.ui.currentState != UIState.Dungeon) && wait)
               	yield return null;
        	
        	if (!Finder.party.IsAlive) {
				QuitEvent();
			} else {
			
				if (currentNode.choiceActions[id].continueEvent == InteractableEventExecution.GotoNextNode) {
					NextEventNode();
				} else if (currentNode.choiceActions[id].continueEvent == InteractableEventExecution.GotoNodeIndex) {
					NextEventNode(currentNode.choiceActions[id].gotoNodeIndex);
				} else if (currentNode.choiceActions[id].continueEvent == InteractableEventExecution.CompleteEvent) {
					CompleteEvent();
				} else if (currentNode.choiceActions[id].continueEvent == InteractableEventExecution.CancelEvent) {
					QuitEvent();
				}
			
			}
        			
    	}
		
		// -------------------------------------------------------------------------------
		// ShowChoices
		// -------------------------------------------------------------------------------
        protected IEnumerator ShowChoices() {
        	
        	ChoicesPanel panel = Finder.ui.choicesPanel.GetComponent<ChoicesPanel>();
        	panel.Initialize(this);
        	Finder.ui.choicesPanel.gameObject.SetActive(true);
        	
           	while (Finder.ui.choicesPanel.gameObject.activeInHierarchy)
               	yield return null;
            	
        }
        
        // -------------------------------------------------------------------------------
		// CompleteEvent
		// -------------------------------------------------------------------------------
        protected void CompleteEvent() {
        	if (!interacted) AddInteraction();
        	QuitEvent();
        }
        
       	// -------------------------------------------------------------------------------
		// QuitEvent
		// -------------------------------------------------------------------------------
		protected void QuitEvent() {

			playerSteppedOnTile = false;
		
			if (interacted && !tile.TriggerOnce)
                interacted = false;
			
			Finder.ui.choicesPanel.gameObject.SetActive(false);
			
            eventStep = 0;
            
            Finder.ui.ActivateMenuButtons(true);
		
		}
		
		// -------------------------------------------------------------------------------
		// Activate
		// -------------------------------------------------------------------------------
 		public virtual void Activate() {
 			deactivated = false;
 			
            Finder.party.DeactivatedObjects.RemoveAll(x => x.Location == Location);
 			
 			this.gameObject.SetActive(true);
 		}
 		
 		// -------------------------------------------------------------------------------
		// Deactivate
		// -------------------------------------------------------------------------------
 		public virtual void Deactivate() {
 			deactivated = true;
 			
 			if (!Finder.party.DeactivatedObjects.Any(x => x.Location == Location)) {
                Finder.party.DeactivatedObjects.Add(new Interaction { Location = Location });
                
            }
 			
 			this.gameObject.SetActive(false);
 		}
 		
 		// -------------------------------------------------------------------------------
		// SetInteraction
		// -------------------------------------------------------------------------------		
  		public void SetInteraction(bool state) {
  			
  			interacted = state;
  		
  			if (interacted && currentNode != null && currentNode.choiceActions != null) {
  				if (currentNode.choiceActions.Any(x => x.moveDirection != MoveType.None)) {
					moveDirection = currentNode.choiceActions.FirstOrDefault(x => x.moveDirection != MoveType.None).moveDirection;
					IsMoving = true;
				}
			}
  		} 
  		
  		// -------------------------------------------------------------------------------
		// AddInteraction
		// -------------------------------------------------------------------------------
 		public void AddInteraction() {
 			if (tile.TriggerOnce) interacted = true;
 			if (!Finder.party.InteractedObjects.Any(x => x.Location == Location)) {
                Finder.party.InteractedObjects.Add(new Interaction { Location = Location });
 			
            }
 		}
 		
  		// -------------------------------------------------------------------------------
		// RemoveInteraction
		// -------------------------------------------------------------------------------
 		public void RemoveInteraction() {
 			if (tile.TriggerOnce) interacted = false;
 			Finder.party.InteractedObjects.RemoveAll(x => x.Location == Location);
 			
 		}
 		
		// -------------------------------------------------------------------------------
		// ActionDamage
		// -------------------------------------------------------------------------------
		protected void ActionDamage(InstanceBase activator, bool targetAll=false) {
			
			int amount = 0;
			
			CharacterBase[] targets;
			
			if (targetAll) {
				targets = Finder.party.characters.ToArray();
			} else {
				targets = RPGHelper.getRandomPlayer();
			}
			
			RPGHelper.DamageTargets(null, activator, amount, targets);
            
		}
		
		// -------------------------------------------------------------------------------
		// ActionRecover
		// -------------------------------------------------------------------------------
		protected void ActionRecover(InstanceBase activator, bool targetAll=false) {
			
			int amount = 0;
			
			CharacterBase[] targets;
			
			if (targetAll) {
				targets = Finder.party.characters.ToArray();
			} else {
				targets = RPGHelper.getRandomPlayer();
			}
			
			RPGHelper.RecoverTargets(null, activator, amount, targets);
			   	    
        }
        
        // -------------------------------------------------------------------------------
		// AnimatedMovement
		// -------------------------------------------------------------------------------
    	protected IEnumerator AnimatedMovement(MoveType moveDirection) {
    		
    		travelledDistance += Time.deltaTime;
    		var moveDir = interacted ? 1 : -1;
    		
    		switch (moveDirection) {
    		
    			case MoveType.Up:
    				transform.Translate(Vector3.up * Time.deltaTime * moveDir);
    				break;
    				
    			case MoveType.Down:
    				transform.Translate(Vector3.down * Time.deltaTime * moveDir);
    				break;
    				
    			case MoveType.Left:
    				transform.Translate(Vector3.left * Time.deltaTime * moveDir);
    				break;
    				
    			case MoveType.Right:
    				transform.Translate(Vector3.right * Time.deltaTime * moveDir);
    				break;

    		}

    		while (travelledDistance < 1)
    			yield return null;
    		
    		IsMoving = false;
    	}
 
        // -------------------------------------------------------------------------------
		// Show
		// -------------------------------------------------------------------------------		
  		public void Show() {
  			IsHidden = false;
  			if (eventRenderer != null) eventRenderer.enabled = true;
  		}
  		
        // -------------------------------------------------------------------------------
		// Hide
		// -------------------------------------------------------------------------------		
  		public void Hide() {
  			IsHidden = true;
  			if (eventRenderer != null) eventRenderer.enabled = false;
  		}
  		
     	// -------------------------------------------------------------------------------
		
    }
}