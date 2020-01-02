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
	// SELECTION PANEL
	// ===================================================================================
    public class SelectionPanel : MonoBehaviour {
    	
    	public GameObject buttonPrefab;
    	public Transform content;
    	
    	protected UIState sourceState;
    	protected bool party;
    	protected bool aliveOnly;
    	public CharacterBase[] selectedCharacters;
    	
    	public bool selected { get; set; }
    	public bool cancelled { get; set; }
    	
  		// -------------------------------------------------------------------------------
		// ShowPrompt
		// -------------------------------------------------------------------------------
    	public void ShowPrompt(UIState source, bool Party, bool AliveOnly, RankTargetType rankTargetType=RankTargetType.All) {
        	
        	selectedCharacters = new CharacterBase[Constants.MAX_PARTY_MEMBERS];
        	sourceState 	= source;
        	party 			= Party;
        	aliveOnly 		= AliveOnly;
        	
        	foreach (Transform t in content)
     			Destroy(t.gameObject);

 			if (party) {
 			
				// -- Iterate and Add party members
				foreach (CharacterBase character in Finder.party.characters) {
					if ( (character.IsRearguard && rankTargetType == RankTargetType.Rear || !character.IsRearguard && rankTargetType == RankTargetType.Front || rankTargetType == RankTargetType.All) && (aliveOnly && character.IsAlive) || !aliveOnly) {
						GameObject newObj = (GameObject)Instantiate(buttonPrefab, content);
						ButtonCharacterSelect button = newObj.GetComponent<ButtonCharacterSelect>();
						button.Initialize(this, character);
					}
				}
			
			} else {
			
				// -- Iterate and Add enemies				
				foreach (CharacterBase character in Finder.battle.monsterParty.characters) {
					if ( (character.IsRearguard && rankTargetType == RankTargetType.Rear || !character.IsRearguard && rankTargetType == RankTargetType.Front || rankTargetType == RankTargetType.All) && (aliveOnly && character.IsAlive) || !aliveOnly) {
						GameObject newObj = (GameObject)Instantiate(buttonPrefab, content);
						ButtonCharacterSelect button = newObj.GetComponent<ButtonCharacterSelect>();
						button.Initialize(this, character);
					}
				}
				
			}
			
			// -- Add the Cancel button
			
			GameObject cancelObj = (GameObject)Instantiate(buttonPrefab, content);
			cancelObj.GetComponentInChildren<Text>().text = Finder.txt.buttonNames.cancel;
			ButtonCharacterSelect cancelButton = cancelObj.GetComponent<ButtonCharacterSelect>();
			cancelButton.icon.sprite = Finder.fx.cancelButtonIcon;
			cancelButton.selectionPanel = this;
			cancelButton.characterBase = null;
			
			gameObject.SetActive(true);
			
			StartCoroutine(ShowSelection());
        	
        }
		
        // -------------------------------------------------------------------------------
		// ShowSelection
		// -------------------------------------------------------------------------------
        protected IEnumerator ShowSelection() {
           	while (!selected)
               	yield return null;
        }
           
        // -------------------------------------------------------------------------------
		// OnSelect
		// -------------------------------------------------------------------------------
        public void OnSelect(CharacterBase characterBase) {
        	
        	if (characterBase != null) {
        		selectedCharacters[0] = characterBase;
        		cancelled = false;
        	} else {
        		selectedCharacters = null;
        		cancelled = true;
        	}
        	
        	this.gameObject.SetActive(false);
        	selected = true;
        	
        	Finder.ui.ConfirmSelection(sourceState);
        	
        }
        
        // -------------------------------------------------------------------------------
        // GetFirstSelectedCharacter
        // -------------------------------------------------------------------------------
		public CharacterBase GetFirstSelectedCharacter() {
        	
        	selected = false;
        	
        	if (cancelled) {
        		cancelled = false;
        		return null;
        	} else {
        		return selectedCharacters[0];
        	}
        }
        
        // -------------------------------------------------------------------------------
        // GetSelectedCharacters
        // -------------------------------------------------------------------------------
		public CharacterBase[] GetSelectedCharacters() {
        	selected = false;
        	cancelled = false;
        	return selectedCharacters;
        }
        
        // -------------------------------------------------------------------------------
		// OverrideSelection
		// -------------------------------------------------------------------------------
		public void OverrideSelection(UIState sourcestate, CharacterBase[] target) {
			selectedCharacters = new CharacterBase[Constants.MAX_PARTY_MEMBERS];
			sourceState = sourcestate;
        	selectedCharacters = target;
        	cancelled = false;
        	selected = true;
        	Finder.ui.ConfirmSelection(sourceState);
        }	
        
        // -------------------------------------------------------------------------------
        
    }
    
    // -------------------------------------------------------------------------------
    
}