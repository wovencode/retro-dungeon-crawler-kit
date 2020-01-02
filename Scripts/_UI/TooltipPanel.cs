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
	// TOOLTIP PANEL
	// ===================================================================================
    public class TooltipPanel : MonoBehaviour {
    	
    	public GameObject panel;
    	public Text headerText;
        public Image image;
        public Text descriptionText;
        public Text contentText;
        
    	public Transform ElementContent;
    	public Transform ClassContent;
       	public GameObject ButtonIconPrefab;
       	
       	protected TooltipPanel hoverPanel;
       	
       	// -------------------------------------------------------------------------------
		// Show
		// -------------------------------------------------------------------------------
        public void Show() {
            panel.SetActive(true);
        }
        
		// -------------------------------------------------------------------------------
		// Hide
		// -------------------------------------------------------------------------------
        public void Hide() {
            panel.SetActive(false);
        }
        
  		// -------------------------------------------------------------------------------
		// Initialize
		// -------------------------------------------------------------------------------
     	public void Initialize(TemplateSimple tmpl) {
       	
       		if (tmpl == null) return;
        	
        	hoverPanel = GetComponent<TooltipPanel>();
        	
        	headerText.text 		= tmpl.fullName;
        	image.sprite 			= tmpl.icon;
        	descriptionText.text 	= tmpl.description;
        	contentText.text		= "";
        	
        	if (tmpl is TemplateEquipment)
        		RefreshClasses((TemplateAdvanced)tmpl);
        	
        	if (tmpl is TemplateAdvanced)
				RefreshAdvanced((TemplateAdvanced)tmpl);
        	
        	if (tmpl is TemplateEquipment && ((TemplateEquipment)tmpl).statModifiers != null)
				RefreshEquipment((TemplateEquipment)tmpl);
        	
       		if (tmpl is TemplateItemSpecial || tmpl is TemplateSkillSpecial)
       			RefreshSpecial((TemplateAdvanced)tmpl);
       		
       		if (tmpl is TemplateCharacterBase)
       			RefreshCharacter((TemplateCharacterBase)tmpl);
       		
       		if (tmpl is TemplateItemAbility)
       			RefreshItemAbility((TemplateItemAbility)tmpl);
       			
       		if (tmpl is TemplateItemCurative || tmpl is TemplateSkillCurative)
       			RefreshCurative((TemplateAdvanced)tmpl);
       			
       		if (tmpl is TemplateItemAttack || tmpl is TemplateSkillAttack)
       			RefreshAttack((TemplateAdvanced)tmpl);
       		
       	}
       	
		// -------------------------------------------------------------------------------
		// RefreshClasses
		// -------------------------------------------------------------------------------
        public void RefreshClasses(TemplateAdvanced tmpl) {

            foreach (Transform t in ClassContent) {
     			Destroy(t.gameObject);
 			}
 			
 			foreach (KeyValuePair<string, TemplateCharacterClass> entry in DictionaryCharacterClass.dict) {
 				GameObject newObj = (GameObject)Instantiate(ButtonIconPrefab, ClassContent);
 				newObj.GetComponentInChildren<Button>().GetComponentInChildren<Image>().sprite = entry.Value.icon;
 				newObj.GetComponentInChildren<Button>().interactable = false;
 				if (tmpl.characterClasses != null && tmpl.characterClasses.Length > 0) {
 					newObj.GetComponentInChildren<Button>().interactable = tmpl.characterClasses.Any( x => x.name == entry.Key);
 				} else if (tmpl.characterClasses.Length == 0) {
 					newObj.GetComponentInChildren<Button>().interactable = true;
 				}
 			} 
 			  
        }
        
        // -------------------------------------------------------------------------------
		// RefreshClass
		// -------------------------------------------------------------------------------
        public void RefreshClass(TemplateCharacterClass characterClass) {
        
        	foreach (Transform t in ClassContent) {
     			Destroy(t.gameObject);
 			}
 			
 			foreach (KeyValuePair<string, TemplateCharacterClass> entry in DictionaryCharacterClass.dict) {
 				GameObject newObj = (GameObject)Instantiate(ButtonIconPrefab, ClassContent);
 				newObj.GetComponentInChildren<Button>().GetComponentInChildren<Image>().sprite = entry.Value.icon;
 				newObj.GetComponentInChildren<Button>().interactable = (characterClass.name == entry.Key) ? true : false;
 			} 
        
        }
        
        // -------------------------------------------------------------------------------
		// RefreshElements
		// -------------------------------------------------------------------------------
        public void RefreshElements(TemplateMetaElement element) {
        
        	foreach (Transform t in ElementContent) {
     			Destroy(t.gameObject);
 			}
			
 			foreach (var ele in DictionaryElement.dict) {
 				GameObject newObj = (GameObject)Instantiate(ButtonIconPrefab, ElementContent);
 				newObj.GetComponentInChildren<Button>().GetComponentInChildren<Image>().sprite = ele.Value.icon;
 				newObj.GetComponentInChildren<Button>().interactable = (element == ele.Value) ? true : false;
 			}
        
        }
        
        // -------------------------------------------------------------------------------
		// RefreshEquipment
		// -------------------------------------------------------------------------------
        public void RefreshEquipment(TemplateEquipment tmpl) {
        	
        	if (tmpl is TemplateEquipmentWeapon) {
        		contentText.text += ((TemplateEquipmentWeapon)tmpl).GetDescription();
        	} else {
       	 		contentText.text += ((TemplateEquipment)tmpl).statModifiers.GetDescription();
        	}
        	
        	RefreshElements(tmpl.element);
        }
        
        // -------------------------------------------------------------------------------
		// RefreshAdvanced
		// -------------------------------------------------------------------------------
        public void RefreshAdvanced(TemplateAdvanced tmpl) {
        	contentText.text += tmpl.statRequirements.GetDescription();
        }
        
        // -------------------------------------------------------------------------------
		// RefreshEffects
		// -------------------------------------------------------------------------------
        public void RefreshEffects(TemplateAdvanced tmpl) {
        
        	var text = "";
        
        	text += "<b>"+Finder.txt.basicVocabulary.effects + Finder.txt.seperators.colon + "</b>\n";
        	
        	if (tmpl is TemplateSkill)
        		text += Finder.txt.skillVocabulary.manaCost			+ " " + ((TemplateSkill)tmpl).baseManaCost + " + " + ((TemplateSkill)tmpl).bonusManaCost + " " + Finder.txt.skillVocabulary.perLevel + "\n";
        	
        	// -- Base Power
        	
        	if (tmpl.basePower != 0)
        		text += Finder.txt.skillVocabulary.basePower + " " + tmpl.basePower;
        	
        	if (tmpl.basePowerPercentage != 0) {
        		if (tmpl.basePower != 0) text += " + ";
        		text += tmpl.basePowerPercentage + "%";
        	}
        	text += "\n";
        	
        	// -- Bonus Power (Abilities only)
        	
        	if (tmpl is TemplateSkill) {
        	
				if (((TemplateSkill)tmpl).bonusPower != 0)
				text += Finder.txt.skillVocabulary.bonusPower 		+ " ";
			
				if (((TemplateSkill)tmpl).bonusPower != 0)
					text +=  ((TemplateSkill)tmpl).bonusPower;
				
				// -- Bonus Power Percentage (Curative Abilities only)
				
				if (tmpl is TemplateSkillCurative) {
				
					if (((TemplateSkillCurative)tmpl).bonusPowerPercentage != 0) {
						if (((TemplateSkill)tmpl).bonusPower != 0) text += " + ";
						text += ((TemplateSkill)tmpl).bonusPowerPercentage + "%";
					}
					text += "\n";
				
				}
        	
        	}
        	
        	if (tmpl.baseStat != null && tmpl.statPower != 0) {
        	text += Finder.txt.skillVocabulary.statInfluence 	+ Finder.txt.seperators.colon + tmpl.baseStat.fullName 									+ "\n";
        	text += Finder.txt.skillVocabulary.statPower 		+ Finder.txt.seperators.colon + (tmpl.statPower * 100).ToString() 						+ "%\n";
        	}
        	
        	text += Finder.txt.skillVocabulary.useType 			+ Finder.txt.seperators.colon + Finder.txt.getUseTypeDescription(tmpl.useType) 			+ "\n";
        	text += Finder.txt.skillVocabulary.useLocation 		+ Finder.txt.seperators.colon + Finder.txt.getLocationTypeDescription(tmpl.useLocation) + "\n";
        	text += Finder.txt.skillVocabulary.useTarget 		+ Finder.txt.seperators.colon + Finder.txt.getTargetTypeDescription(tmpl.targetType) 	+ "\n";
        	
        	if (!string.IsNullOrEmpty(text))
        		contentText.text += text + "\n";
        	
        }
        
        // -------------------------------------------------------------------------------
		// RefreshCurative
		// -------------------------------------------------------------------------------
        public void RefreshCurative(TemplateAdvanced tmpl) {
        	RefreshEffects(tmpl);
        	if (tmpl.recoveryType != RecoveryType.None)
        	contentText.text += Finder.txt.skillVocabulary.recoveryType		+ " " + tmpl.recoveryType.ToString() + "\n";
        }
        
        // -------------------------------------------------------------------------------
		// RefreshAttack
		// -------------------------------------------------------------------------------
        public void RefreshAttack(TemplateAdvanced tmpl) {
        	RefreshEffects(tmpl);
        	contentText.text += Finder.txt.skillVocabulary.attackType 		+ " " + tmpl.attackType.ToString() + "\n";
        	RefreshElements(tmpl.element);
        }
        
		// -------------------------------------------------------------------------------
		// RefreshSpecial
		// -------------------------------------------------------------------------------
        public void RefreshSpecial(TemplateAdvanced tmpl) {
        	
        	var text = "";
        	
        	switch (tmpl.useEffectType) {
        	
        		case SpecialActionType.PlayerExitBattle:
        			text = Finder.txt.specialItemDescriptions.PlayerExitBattle;
        			break;
        			
        		case SpecialActionType.PlayerExitDungeon:
        			text = Finder.txt.specialItemDescriptions.PlayerExitDungeon;
        			break;
        			
        		case SpecialActionType.PlayerWarpDungeon:
        			text = Finder.txt.specialItemDescriptions.PlayerWarpDungeon;
        			break;
        			
        	}
        	
        	contentText.text += text + "\n";
        
        }
        
        // -------------------------------------------------------------------------------
		// RefreshAbility
		// -------------------------------------------------------------------------------
        public void RefreshItemAbility(TemplateItemAbility tmpl) {
        	contentText.text += Finder.txt.specialItemDescriptions.CharacterLearnSkill + "\n" + tmpl.learnedAbility.fullName;
        }
        
         // -------------------------------------------------------------------------------
		// RefreshCharacter
		// -------------------------------------------------------------------------------
        public void RefreshCharacter(TemplateCharacterBase tmpl) {
        	contentText.text += tmpl.characterClass.GetDescription();
        	RefreshElements(tmpl.element);
        	RefreshClass(tmpl.characterClass);
        }
        
        // -------------------------------------------------------------------------------
        
    }
}