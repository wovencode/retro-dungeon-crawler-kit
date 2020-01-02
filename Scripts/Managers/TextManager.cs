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

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// TEXT MANAGER
	// ===================================================================================
	[Serializable]
    public class TextManager : MonoBehaviour {
		
		// ============================== INSPECTOR PROPERTIES ===========================
		
		[Header("-=- Languages -=-")]
		public TemplateLanguage[] languages;
		
    	[Header("-=- Intro/Outro -=-")]
    	public TemplateMetaCutscene introScene;
    	public TemplateMetaCutscene outroScene;
    	public TemplateMetaCutscene creditsScene;
    	
    	[Header("-=- Library Folders (in Resources) -=-")]
    	public FolderNames folderNames;
    	
		// =============================== CLASS PROPERTIES ==============================
		
		public int language { get; private set; }
		
   		// -------------------------------------------------------------------------------
		// adjustLanguage
		// -------------------------------------------------------------------------------		
		public void adjustLanguage(int lang) {
			language = lang;
		}
		
   		// -------------------------------------------------------------------------------
		// getCurrencyName
		// -------------------------------------------------------------------------------
    	public string getCurrencyName(CurrencyType currencyType, bool abbrev=true) {
    		return abbrev ? languages[language].currencyNames.FirstOrDefault(x => x.currencyType == currencyType).abbrev : languages[language].currencyNames.FirstOrDefault(x => x.currencyType == currencyType).name;
    	}
    	
    	// -------------------------------------------------------------------------------
		// getCurrencyIcon
		// -------------------------------------------------------------------------------
    	public Sprite getCurrencyIcon(CurrencyType currencyType) {
    		return languages[language].currencyNames.FirstOrDefault(x => x.currencyType == currencyType).icon;
    	}
   		
    	// -------------------------------------------------------------------------------
		// getDerivedStatName
		// -------------------------------------------------------------------------------
    	public string getDerivedStatName(DerivedStatType statType, bool abbrev=true) {
    		return abbrev ? languages[language].statNames.FirstOrDefault(x => x.statType == statType).abbrev : languages[language].statNames.FirstOrDefault(x => x.statType == statType).name;
    	} 	
    	
    	// -------------------------------------------------------------------------------
		// getUseTypeDescription
		// -------------------------------------------------------------------------------
    	public string getUseTypeDescription(CanUseType useType) {
    		return languages[language].useTypeDescriptions.FirstOrDefault(x => x.useType == useType).description;
    	} 	
    	
    	// -------------------------------------------------------------------------------
		// getLocationTypeDescription
		// -------------------------------------------------------------------------------
    	public string getLocationTypeDescription(CanUseLocation useLocation) {
    		return languages[language].useLocationDescriptions.FirstOrDefault(x => x.useLocation == useLocation).description;
    	} 	
    	
    	// -------------------------------------------------------------------------------
		// getTargetTypeDescription
		// -------------------------------------------------------------------------------
    	public string getTargetTypeDescription(CanUseTarget useTarget) {
    		return languages[language].useTargetDescriptions.FirstOrDefault(x => x.useTarget == useTarget).description;
    	} 	
    	
    	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
    	public Seperators seperators {
    		get {
    			return languages[language].seperators;	
    		}
    	}
    	
       	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
    	public BasicVocabulary basicVocabulary {
    		get {
    			return languages[language].basicVocabulary;	
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
    	public NamesVocabulary namesVocabulary {
    		get {
    			return languages[language].namesVocabulary;	
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
    	public SkillVocabulary skillVocabulary {
    		get {
    			return languages[language].skillVocabulary;	
    		}
    	}
    	
       	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
    	public BasicDerivedStatNames basicDerivedStatNames {
    		get {
    			return languages[language].basicDerivedStats;	
    		}
    	}
    	
      	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
    	public ButtonNames buttonNames {
    		get {
    			return languages[language].buttonNames;	
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
    	public ActionNames actionNames {
    		get {
    			return languages[language].actionNames;	
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
    	public CommandNames commandNames {
    		get {
    			return languages[language].commandNames;	
    		}
    	}
    	
      	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
    	public FormationNames formationNames {
    		get {
    			return languages[language].formationNames;	
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
    	public SpecialItemDescriptions specialItemDescriptions {
    		get {
    			return languages[language].specialItemDescriptions;	
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
    	public UseTypeDescriptions[] useTypeDescriptions {
    		get {
    			return languages[language].useTypeDescriptions;	
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
    	public UseLocationDescriptions[] useLocationDescriptions {
    		get {
    			return languages[language].useLocationDescriptions;	
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
    	public UseTargetDescriptions[] useTargetDescriptions {
    		get {
    			return languages[language].useTargetDescriptions;	
    		}
    	}
    	    	
    }
    
    // ===================================================================================
	// TINY CLASSES - ONLY USED BY TEXT MANAGER
	// ===================================================================================
	
	[Serializable]
    public class FolderNames {
    	public string folderLanguages;
    	public string folderSkills;
    	public string folderCharacterClasses;
    	public string folderDungeons;
    	public string folderEquipment;
    	public string folderGossip;
    	public string folderItems;
    	public string folderTowns;
    	public string folderHeroes;
    	public string folderTiles;
    	public string folderStatus;
    	public string folderLootDrops;
    	public string folderEncounters;
    	public string folderElements;
    	public string folderAttributes;
    	public string folderEquipmentSlots;
    	public string folderCombatStyles;
    	public string folderCombatStances;
    	public string folderSpecies;
    }
   
    // ===================================================================================
}