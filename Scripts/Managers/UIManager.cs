// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// UI MANAGER
	// ===================================================================================
    public class UIManager : MonoBehaviour {
		
		public bool ShowNavigationArrows;

        public GameObject playerParty;
        public GameObject navControls;
        
        public GameObject battleFormation;
        public GameObject battleCommands;
        public GameObject battleBackground;
        public GameObject gameOver;
		
		public GameObject goldPanel;
		public GameObject shortcutMenuButton;
        public GameObject shortcutMenu;
        
        public GameObject characterAbilities;
        public GameObject playerItems;
        public GameObject characterStatus;
        
        public GameObject characterEquipment;
        public GameObject playerMap;
        
		public GameObject panelWorldmap;
		public GameObject panelWorldmapNavigation;
		
        public GameObject panelTown;
        public GameObject panelTownNavigation;

		public GameObject logPanel;
		public GameObject _selectionPanel;
		
        public GameObject shopOutside;
        public GameObject shopInside;
       
        public GameObject titleScreen;
        public GameObject settingsScreen;
        public GameObject dungeonWarpScreen;

        public GameObject saveMenuScreen;

        public GameObject shopInventoryPanel;
        public GameObject choicesPanel;
        
        public GameObject cutScene;
        
		public SelectionPanel selectionPanel;
		
		public bool MapOpened { get; set; }
        public UIState currentState { get; set; }
        
        private bool popState;
        private UIState previousState;
        private Stack<UIState> stateStack = new Stack<UIState>();
        
      
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private void Start() {
        	selectionPanel = _selectionPanel.GetComponent<SelectionPanel>();
            StartCoroutine(OnApplicationStarted());
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private IEnumerator OnApplicationStarted() {
            yield return new WaitForEndOfFrame();
            OverrideState(UIState.Title);
        }

 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void PushState(string state) {
        	popState = true;
            UIState newState = (UIState)Enum.Parse(typeof(UIState), state);
            PushState(newState);
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void PushState(UIState state) {
            if (currentState != state) {
            	popState = true;
                stateStack.Push(currentState);
                UpdateUIState(state);
            }
        }
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void OverrideState(string state) {
            UIState newState = (UIState)Enum.Parse(typeof(UIState), state);
            OverrideState(newState);
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void OverrideState(UIState state) {
            stateStack.Clear();
            stateStack.Push(state);
            UpdateUIState(state);
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void PopState() {
            if (stateStack.Count > 0) {
                if (popState) {
                	popState = false;
                	UIState state = stateStack.Pop();
                	UpdateUIState(state);
                } else {
                	UIState state = stateStack.Peek();
                	UpdateUIState(state);
                }
            } else {
                UpdateUIState(UIState.None);
            }
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void RevertState() {
        	if (stateStack.Count > 0) {
                UIState state = stateStack.Peek();
                UpdateUIState(state);
            } else {
                UpdateUIState(UIState.None);
            }
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private void UpdateUIState(UIState state) {
            previousState = currentState;
            currentState = state;
            LoadCurrentState();
        }
        
        // ===============================================================================
        // UI STATE FUNCTIONS
        // ===============================================================================
        
 		// -------------------------------------------------------------------------------
		// LoadCurrentState
		// -------------------------------------------------------------------------------
        private void LoadCurrentState() {
        
            DeactivateAll();

            switch (currentState)  {
            
                case UIState.Town:
                    LoadStateTown();
                    break;
                    
                case UIState.Dungeon:
                   	LoadStateDungeon();
                    break;
                    
                case UIState.Battle:
                    LoadStateBattle();
                    break;
                    
                case UIState.Menu:
                    LoadStateShortcuts();
                    break;
                  
                case UIState.ShowSelectedCharacterStatus:
                	LoadState_ShowSelectedCharacterStatus();
                	break;
                
                case UIState.Inventory:
                	ShowInventory();
                    break;
                    
                case UIState.InventorySell:
                	LoadStateInventorySell();
                    break;
                    
                 case UIState.CharacterSell:
                	LoadStateCharacterSell();
                    break;
                    
                case UIState.SelectCharacterToSwitchFormation:
                	SelectCharacterToSwitchFormation();
                	break;
                
                case UIState.SwitchSelectedCharacterFormation:
                	LoadState_ProcessStateSwitchFormation();
                	break;
                
                case UIState.ShowSelectedCharacterAbilities:
                	LoadState_ShowSelectedCharacterAbilities();
                    break;
               
                case UIState.ShopEquipment:
                	LoadState_ShopOutside(ShopType.ShopEquipment);
                    break;
                
                case UIState.ShopInn:
                	LoadState_ShopOutside(ShopType.ShopInn);
                    break;
                    
                case UIState.ShopCharacter:
                	LoadState_ShopOutside(ShopType.ShopCharacter);
                    break;
                    
                case UIState.ShopCurative:
                	LoadState_ShopOutside(ShopType.ShopItemCurative);
                    break;
                    
                case UIState.ShopAbility:
                	LoadState_ShopOutside(ShopType.ShopItemAbility);
                    break;
                
                case UIState.ShopItem:
                	LoadState_ShopOutside(ShopType.ShopItem);
                    break;
                    
                case UIState.ShopOutside:
                	LoadState_ShopOutside(ShopType.None);
                    break;
                    
                case UIState.ShopInside:
                	LoadState_ShopInside(true);
                    break;
                
                case UIState.MinimapShow:
                    LoadStateMinimapShow();
                    break;
                    
                 case UIState.MinimapHide:
                    LoadStateMinimapHide();
                    break;
                    
                case UIState.Worldmap:
                	LoadStateWorldmap();
                    break;
                
                case UIState.Title:
                    LoadStateTitleScreen();
                    break;
                    
                case UIState.Settings:
                    LoadStateSettingsScreen();
                    break;
                    
                case UIState.DungeonWarp:
                    LoadStateDungeonWarp();
                    break;

                case UIState.SaveMenu:
                    LoadStateSaveMenu();
                    break;

                case UIState.LoadMenu:
                    LoadStateLoadMenu();
                    break;
                    
                case UIState.Credits:
                    LoadStateCredits();
                    break;
                    
                case UIState.OpeningScene:
                    LoadStateIntro();
                    break;
                    
                case UIState.EndingScene:
                    LoadStateOutro();
                    break;
            }
        }
		
		// ========================= LOCATION RELATED STATES =============================
		
		// -------------------------------------------------------------------------------
		// LoadStateWorldmap
		// -------------------------------------------------------------------------------
		protected void LoadStateWorldmap() {
			panelWorldmap.SetActive(true);
            playerParty.SetActive(true);
            ActivateShortcutButtons(true);
            panelWorldmapNavigation.SetActive(true);
		}
		
 		// -------------------------------------------------------------------------------
		// LoadStateTown
		// -------------------------------------------------------------------------------
		protected void LoadStateTown() {
			panelTown.SetActive(true);
            ActivateShortcutButtons(true);
            playerParty.SetActive(true);
            panelTownNavigation.SetActive(true);
		}
		
 		// -------------------------------------------------------------------------------
		// LoadStateDungeon
		// -------------------------------------------------------------------------------
		protected void LoadStateDungeon() {
			ActivateShortcutButtons(true);
            ActivateNaviCommands(true);
            playerParty.SetActive(true);
            
            Finder.map.ShowEvents();
		}
		
 		// -------------------------------------------------------------------------------
		// LoadStateBattle
		// -------------------------------------------------------------------------------
		protected void LoadStateBattle() {
		
			battleFormation.SetActive(true);
			battleCommands.SetActive(true);
			battleBackground.SetActive(true);
			playerParty.SetActive(true);
            
            Finder.map.HideEvents();
            
            if (Finder.battle.IsGameOver) {
            	battleFormation.SetActive(false);
            	battleCommands.SetActive(false);
            	battleBackground.SetActive(false);
                gameOver.SetActive(true);
            }
		}
		
		// =========================== STATUS RELATED STATES =============================
		
 		// -------------------------------------------------------------------------------
		// SelectCharacterToDisplayStatus
		// -------------------------------------------------------------------------------
		public void SelectCharacterToDisplayStatus() {
			ActivateMenuButtons(false);
			Prompt(UIState.SelectCharacterToDisplayStatus, true, false);
		}
		
		// -------------------------------------------------------------------------------
		// LoadState_ShowSelectedCharacterStatus
		// -------------------------------------------------------------------------------
		protected void LoadState_ShowSelectedCharacterStatus() {
		
			CharacterBase target = GetFirstSelectedCharacter();
			
            if (target != null) {
			
				MenuCharacterStatus menu = characterStatus.GetComponent<MenuCharacterStatus>();
            	if (menu != null) menu.character = target;
            	
				characterStatus.SetActive(true);
				
				goldPanel.SetActive(true);
            	playerParty.SetActive(true);

			} else {
				PopState();
			}
			
		}
		
 		// -------------------------------------------------------------------------------
		// SelectCharacterToSwitchFormation
		// -------------------------------------------------------------------------------
		public void SelectCharacterToSwitchFormation() {
			
			ActivateMenuButtons(false);
			Prompt(UIState.SelectCharacterToSwitchFormation, true, false);
            
		}
		
		// -------------------------------------------------------------------------------
		// LoadState_ProcessStateSwitchFormation (Post Selection)
		// -------------------------------------------------------------------------------
		protected void LoadState_ProcessStateSwitchFormation() {
		
			CharacterBase target = GetFirstSelectedCharacter();
			
            if (target != null)
				target.CommandSwitchFormation();

			PopState();
			
		}
		
		// ========================= INVENTORY RELATED STATES ============================
		
 		// -------------------------------------------------------------------------------
		// ShowInventory
		// -------------------------------------------------------------------------------
		public void ShowInventory() {
			
			ActivateMenuButtons(false);
			
            MenuPartyInventory menu = playerItems.GetComponent<MenuPartyInventory>();
            menu.IsSell = false;
            
            goldPanel.SetActive(true);
            playerItems.SetActive(true);
            playerParty.SetActive(true);
            
		}
		
		// -------------------------------------------------------------------------------
		// ProcessStateInventory (Post Selection)
		// -------------------------------------------------------------------------------
		protected void ProcessStateInventory() {
		
			MenuPartyInventory menu = playerItems.GetComponent<MenuPartyInventory>();
            if (menu != null) menu.ActivationRequestHandler();
			
		}
		
		// -------------------------------------------------------------------------------
		// LoadStateInventorySell
		// -------------------------------------------------------------------------------
		protected void LoadStateInventorySell() {
		
            MenuPartyInventory menu = playerItems.GetComponent<MenuPartyInventory>();
            menu.parent = shopOutside.GetComponent<ShopOutsidePanel>();
            menu.IsSell = true;
            shopOutside.GetComponent<ShopOutsidePanel>().IsSell = true;

            shopInventoryPanel.SetActive(false);
            playerItems.SetActive(true);
           	LoadState_ShopInside(false);
            
		}
		
		// -------------------------------------------------------------------------------
		// LoadStateCharacterSell
		// -------------------------------------------------------------------------------
		protected void LoadStateCharacterSell() {
		
        	shopInventoryPanel.SetActive(false);
            ShopCharacter shop = shopInside.GetComponent<ShopCharacter>();
           	shop.IsSell = true;
           	
            LoadState_ShopInside(true);
            
		}
		
		// ============================ SHOP RELATED STATES ==============================
		
		// -------------------------------------------------------------------------------
		// OverrideShopState
		// required when accessing a shop from within a dungeon map
		// -------------------------------------------------------------------------------
		public void OverrideShopState(TemplateMetaShop templateShop) {
			if (templateShop != null) {
				ShopOutsidePanel shopPanel = shopOutside.GetComponent<ShopOutsidePanel>();
				shopPanel.shop = templateShop;
			}
		}
		
		// -------------------------------------------------------------------------------
		// LoadStateShopOutside
		// required when accessing a shop from the menu based town
		// -------------------------------------------------------------------------------
		protected void LoadState_ShopOutside(ShopType shopType) {
			
			if (shopType != ShopType.None) {
				ShopOutsidePanel shopPanel = shopOutside.GetComponent<ShopOutsidePanel>();
				shopPanel.shop = Finder.map.currentTownConfig.shops.FirstOrDefault(x => x.shopType == shopType);
            }
            
            shopOutside.SetActive(true);
            playerParty.SetActive(true);
		}

		// -------------------------------------------------------------------------------
		// LoadState_ShopInside
		// -------------------------------------------------------------------------------
		protected void LoadState_ShopInside(bool buy=true) {
			goldPanel.SetActive(true);
			
            shopInventoryPanel.SetActive(buy);
            shopInside.SetActive(buy);
            
            shopOutside.SetActive(true);
            playerParty.SetActive(true);
		}
		
		// ========================== ABILITY RELATED STATES =============================
		
		// -------------------------------------------------------------------------------
		// SelectCharacterToDisplayAbilities
		// -------------------------------------------------------------------------------
		public void SelectCharacterToDisplayAbilities() {
			ActivateMenuButtons(false);
			Prompt(UIState.SelectCharacterToDisplayAbilities, true, false);
		}
		
		// -------------------------------------------------------------------------------
		// LoadState_ShowSelectedCharacterAbilities
		// -------------------------------------------------------------------------------
		protected void LoadState_ShowSelectedCharacterAbilities() {
			
			CharacterBase target = GetFirstSelectedCharacter();
			
            if (target != null) {
            
            	MenuCharacterSkills menu = characterAbilities.GetComponent<MenuCharacterSkills>();
				menu.character = target;
				
				characterAbilities.SetActive(true);
				playerParty.SetActive(true);
				
            } else {
            	PopState();
            }
             
		}
		
		// -------------------------------------------------------------------------------
		// LoadState_ProcessSelectedAbility
		// -------------------------------------------------------------------------------
		protected void LoadState_ProcessSelectedAbility() {
		
			CharacterBase[] targets = GetSelectedCharacters();
			
            if (targets != null) {
            
            	MenuCharacterSkills menu = characterAbilities.GetComponent<MenuCharacterSkills>();
            	menu.ActivationRequestHandler(targets);
				
            } else {
            	PopState();
            }
             
		}
		
		// ========================= EQUIPMENT RELATED STATES ============================
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public void SelectCharacterToDisplayEquipment() {
			ActivateMenuButtons(false);
			Prompt(UIState.SelectCharacterToDisplayEquipment, true, false);
		}
		
		// -------------------------------------------------------------------------------
		// LoadState_ShowSelectedCharacterEquipment
		// -------------------------------------------------------------------------------
		protected void LoadState_ShowSelectedCharacterEquipment() {
		
			CharacterBase target = GetFirstSelectedCharacter();
			
            if (target != null) {
            
				MenuCharacterEquipment menu = characterEquipment.GetComponent<MenuCharacterEquipment>();
				
				menu.character = target;

				characterEquipment.SetActive(true);
				
				goldPanel.SetActive(true);
				playerParty.SetActive(true);
			
            } else {
            	PopState();
            }
            
		}
		
		// =============================== MINIMAP STATES ================================
		
		// -------------------------------------------------------------------------------
		// LoadStateMinimapShow
		// -------------------------------------------------------------------------------
		protected void LoadStateMinimapShow() {
			MapOpened = true;
            playerMap.SetActive(true);
        	ActivateShortcutButtons(true);
            ActivateNaviCommands(true);
            playerParty.SetActive(true);
		}
		
		// -------------------------------------------------------------------------------
		// LoadStateMinimapHide
		// -------------------------------------------------------------------------------
		protected void LoadStateMinimapHide() {
			MapOpened = false;
            playerMap.SetActive(false);
        	ActivateShortcutButtons(true);
            ActivateNaviCommands(true);
            playerParty.SetActive(true);
		}
		
		// ================================ OTHER STATES =================================
		
		// -------------------------------------------------------------------------------
		// LoadStateShortcuts
		// -------------------------------------------------------------------------------
		protected void LoadStateShortcuts() {
			goldPanel.SetActive(true);
            shortcutMenuButton.SetActive(true);
            playerParty.SetActive(true);
		}
		
		// -------------------------------------------------------------------------------
		// LoadStateTitleScreen
		// -------------------------------------------------------------------------------
		protected void LoadStateTitleScreen() {
			titleScreen.SetActive(true);
		}

		// -------------------------------------------------------------------------------
		// LoadStateSettingsScreen
		// -------------------------------------------------------------------------------
		protected void LoadStateSettingsScreen() {
			settingsScreen.SetActive(true);
		}

		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		protected void LoadStateDungeonWarp() {
			dungeonWarpScreen.SetActive(true);
		}

		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		protected void LoadStateSaveMenu() {
			MenuPartySave saveMenu = saveMenuScreen.GetComponent<MenuPartySave>();
            if (saveMenu != null) saveMenu.Mode = SaveMode.Save;
            saveMenuScreen.SetActive(true);
		}

		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		protected void LoadStateLoadMenu() {
			MenuPartySave loadMenu = saveMenuScreen.GetComponent<MenuPartySave>();
            if (loadMenu != null) loadMenu.Mode = SaveMode.Load;
            saveMenuScreen.SetActive(true);
		}
		
		// -------------------------------------------------------------------------------
		// LoadStateCredits
		// -------------------------------------------------------------------------------
		protected void LoadStateCredits() {
			CutsceneScreen screen = cutScene.GetComponent<CutsceneScreen>();
            if (screen != null) screen.sceneType = CutsceneType.Credits;
			cutScene.SetActive(true);
		}
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		protected void LoadStateIntro() {
			CutsceneScreen screen = cutScene.GetComponent<CutsceneScreen>();
            if (screen != null) screen.sceneType = CutsceneType.Intro;
			cutScene.SetActive(true);
		}

		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		protected void LoadStateOutro() {
			CutsceneScreen screen = cutScene.GetComponent<CutsceneScreen>();
            if (screen != null) screen.sceneType = CutsceneType.Outro;
			cutScene.SetActive(true);
		}

  		// ===============================================================================
  		// ACTIVATE FUNCTIONS
  		// ===============================================================================
		
		public void ActivateCharacterStatus(bool p) {
			characterStatus.SetActive(false);
		}
		
		// -------------------------------------------------------------------------------
		// ActivateMenuButtons
		// -------------------------------------------------------------------------------
       	public void ActivateMenuButtons(bool p) {
        	ActivateBattleCommands(p);
        	ActivateShortcutButtons(p);
        	ActivateNaviCommands(p);
        }
       
		// -------------------------------------------------------------------------------
		// ActivateShortcutButtons
		// -------------------------------------------------------------------------------
        private void ActivateShortcutButtons(bool p) {
        	shortcutMenuButton.SetActive(p);
            shortcutMenu.SetActive(false);
        }
        
  		// -------------------------------------------------------------------------------
		// ActivateNaviCommands
		// -------------------------------------------------------------------------------
         public void ActivateNaviCommands(bool p) {
         	
         	Finder.navi.ResetFlags();
         	
         	if (Finder.battle.InBattle) {
            	navControls.SetActive(false);
            	Finder.navi.enabled = false;
            } else {
            	if (ShowNavigationArrows) navControls.SetActive(p);
            	Finder.navi.enabled = p;
            }
            
        }  
            
 		// -------------------------------------------------------------------------------
		// ActivateBattleCommands
		// -------------------------------------------------------------------------------
         public void ActivateBattleCommands(bool p) {
         	if (Finder.battle.InBattle) {
         		battleFormation.SetActive(p);
            	battleCommands.SetActive(p);
            } else {
            	battleFormation.SetActive(false);
            	battleCommands.SetActive(false);
            }
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public void ActivateNavigationPanels(bool p) {
        	panelWorldmapNavigation.SetActive(p);
        	panelTownNavigation.SetActive(p);
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
		public void ActivateWorldmap(bool p) {
        	panelWorldmap.SetActive(p);
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void ActivateDungeonMenuState() {
            UpdateUIState(UIState.Menu);
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void ActivatePreviousState() {
            UpdateUIState(previousState);
        }
        
 		// -------------------------------------------------------------------------------
		// DeactivateAll
		// -------------------------------------------------------------------------------
        public void DeactivateAll() {
        	
        	Finder.navi.enabled = false;
        	
        	ActivateMenuButtons(false);
        	battleFormation.SetActive(false);
        	battleBackground.SetActive(false);
        	playerParty.SetActive(false);
        	panelWorldmap.SetActive(false);
            navControls.SetActive(false);
            gameOver.SetActive(false);
            logPanel.SetActive(false);
            characterAbilities.SetActive(false);
            playerItems.SetActive(false);
            characterStatus.SetActive(false);
            characterEquipment.SetActive(false);
            playerMap.SetActive(false);
            panelTown.SetActive(false);
			goldPanel.SetActive(false);
            shopOutside.SetActive(false);
            shopInside.SetActive(false);
            titleScreen.SetActive(false);
            
            settingsScreen.SetActive(false);
            dungeonWarpScreen.SetActive(false);
            saveMenuScreen.SetActive(false);
            choicesPanel.SetActive(false);
            cutScene.SetActive(false);
        }

       
		public void GameOver(bool active) {
            gameOver.SetActive(active);
        }
        

  		// ===============================================================================
  		// CHARACTER SELECTION FUNCTIONS
  		// ===============================================================================

 		// -------------------------------------------------------------------------------
        // ConfirmSelection
        // -------------------------------------------------------------------------------
        public void ConfirmSelection(UIState sourceState) {
        	
        	selectionPanel.gameObject.SetActive(false);
        	ActivateShortcutButtons(false);
			ActivateNavigationPanels(false);
			
        	switch (sourceState) {
        		
        		case UIState.SelectCharacterToSwitchFormation:
        			LoadState_ProcessStateSwitchFormation();
        			break;
        		
        		case UIState.SelectCharacterToDisplayStatus:
        			LoadState_ShowSelectedCharacterStatus();
        			break;
        	
        		case UIState.SelectCharacterToDisplayEquipment:
        			LoadState_ShowSelectedCharacterEquipment();
                    break;
                
                case UIState.SelectCharacterToDisplayAbilities:
                	LoadState_ShowSelectedCharacterAbilities();
                	break;

                case UIState.ShowSelectedCharacterAbilities:
                	LoadState_ProcessSelectedAbility();
                	break;
                	
                case UIState.BattlePlayerAttack:
                	Finder.battle.ProcessPlayerAttack();
                	break;
                	
                case UIState.Inventory:
                	ProcessStateInventory();
                	break;
                    
        	}
        	
        }
        
        // -------------------------------------------------------------------------------
        // GetFirstSelectedCharacter
        // -------------------------------------------------------------------------------
		public CharacterBase GetFirstSelectedCharacter() {
        	return selectionPanel.GetFirstSelectedCharacter();
        }
        
        // -------------------------------------------------------------------------------
        // GetSelectedCharacters
        // -------------------------------------------------------------------------------
		public CharacterBase[] GetSelectedCharacters() {
        	return selectionPanel.GetSelectedCharacters();
        }
        
       	// -------------------------------------------------------------------------------
        // Prompt
        // -------------------------------------------------------------------------------
        public void Prompt(UIState sourceState, bool party, bool alive, RankTargetType rankTargetType=RankTargetType.All) {
        	selectionPanel.ShowPrompt(sourceState, party, alive, rankTargetType);
        	StartCoroutine(WaitForPromptSelection());
        }
        
        // -------------------------------------------------------------------------------
        // ForceSelection (One Target)
        // -------------------------------------------------------------------------------
        public void ForceSelection(CharacterBase character, UIState sourceState = UIState.None) {
        	if (sourceState == UIState.None) sourceState = currentState;
        	selectionPanel.OverrideSelection(sourceState, new CharacterBase[] { character });
        }
        
        // -------------------------------------------------------------------------------
        // ForceSelection (Multiple Targets)
        // -------------------------------------------------------------------------------
        public void ForceSelection(CharacterBase[] characters, UIState sourceState = UIState.None) {
        	if (sourceState == UIState.None) sourceState = currentState;
        	selectionPanel.OverrideSelection(sourceState, characters);
        }
        
        // -------------------------------------------------------------------------------
        // StartPromptSelection
        // -------------------------------------------------------------------------------
        public IEnumerator WaitForPromptSelection() {
        	while (selectionPanel.gameObject.activeInHierarchy)
               	yield return null;
        }

        // -------------------------------------------------------------------------------
        
    }
}