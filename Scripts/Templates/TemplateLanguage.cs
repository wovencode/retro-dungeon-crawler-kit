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
	// TEMPLATE LANGUAGE
	// ===================================================================================
    [Serializable]
    [CreateAssetMenu(fileName = "Unnamed Language", menuName = "RDCK/Templates/New Language")]
    public class TemplateLanguage : ScriptableObject {
 
		[Header("-=- Basic -=-")]
		public Seperators					seperators;
		public BasicVocabulary 				basicVocabulary;
		[Header("-=- Names -=-")]
		public NamesVocabulary 				namesVocabulary;
		[Header("-=- Skills -=-")]
		public SkillVocabulary 				skillVocabulary;
		[Header("-=- Basic Derived Stats -=-")]
		public BasicDerivedStatNames 		basicDerivedStats;
		[Header("-=- More Derived Stats -=-")]
    	public DerivedStatNames[] 			statNames;
		[Header("-=- Formation Names -=-")]
		public FormationNames 				formationNames;
		[Header("-=- Command Names -=-")]
		public CommandNames 				commandNames;
    	[Header("-=- Button Names -=-")]
		public ButtonNames 					buttonNames;
    	[Header("-=- Action Names -=-")]
		public ActionNames 					actionNames;
    	[Header("-=- Currency Names -=-")]
    	public CurrencyNames[] 				currencyNames;
    	[Header("-=- Special Item Descriptions -=-")]
    	public SpecialItemDescriptions 		specialItemDescriptions;
    	[Header("-=- Use Type Descriptions -=-")]
    	public UseTypeDescriptions[] 		useTypeDescriptions;
    	[Header("-=- Use Location Descriptions -=-")]
    	public UseLocationDescriptions[] 	useLocationDescriptions;
    	[Header("-=- Use Target Descriptions -=-")]
    	public UseTargetDescriptions[] 		useTargetDescriptions;
    	
    }
    
	// ===================================================================================
	// TINY CLASSES - ONLY USED BY LANGUAGE TEMPLATE
	// ===================================================================================

	// ----------------------------------------------------------------------------------
	// Seperators
	// -----------------------------------------------------------------------------------	
	[Serializable]
    public class Seperators {
		public string space;
		public string dash;
		public string colon;
	}	
	
	// ----------------------------------------------------------------------------------
	// FormationNames
	// -----------------------------------------------------------------------------------	
	[Serializable]
    public class FormationNames {
		public string formation;
		public string movedTo;
		public string front;
		public string rear;
	}		

	// ----------------------------------------------------------------------------------
	// CommandNames
	// -----------------------------------------------------------------------------------	
	[Serializable]
    public class CommandNames {
		public string dismiss;
		public string hire;
		public string sell;
		public string buy;
		public string use;
		public string cast;
		public string equip;
		public string unequip;
		public string rest;
		public string upgrade;
		public string trash;
		public string save;
		
	}
	
	// ----------------------------------------------------------------------------------
	// ButtonNames
	// -----------------------------------------------------------------------------------	
	[Serializable]
    public class ButtonNames {
		public string Status;
    	public string Inventory;
    	public string Equipment;
    	public string Skills;
    	public string Options;
    	public string Minimap;
    	public string Save;
    	public string Load;
    	public string Quit;
    	public string Credits;
    	public string NewGame;
    	public string Switch;
    	public string cancel;
    	public string revert;
	}
	
	// ----------------------------------------------------------------------------------
	// ActionNames
	// -----------------------------------------------------------------------------------	
	[Serializable]
    public class ActionNames {
		public string gained;
		public string recovered;
		public string learned;
		public string uses;
		public string defends;
		public string attacks;
		public string takes;
		public string casts;
	}
	
	// ----------------------------------------------------------------------------------
	// UseTypeDescriptions
	// -----------------------------------------------------------------------------------
 	[Serializable]
    public class UseTypeDescriptions {
    	public CanUseType useType;
    	public string description;
    }	
    
    // ----------------------------------------------------------------------------------
	// UseLocationDescriptions
	// -----------------------------------------------------------------------------------
 	[Serializable]
    public class UseLocationDescriptions {
    	public CanUseLocation useLocation;
    	public string description;
    }	
    
    // ----------------------------------------------------------------------------------
	// UseTargetDescrptions
	// -----------------------------------------------------------------------------------
 	[Serializable]
    public class UseTargetDescriptions {
    	public CanUseTarget useTarget;
    	public string description;
    }	
	
	// ----------------------------------------------------------------------------------
	// AttributeNames
	// -----------------------------------------------------------------------------------
 	[Serializable]
    public class CurrencyNames {
    	public CurrencyType currencyType;
    	public string abbrev;
    	public string name;
    	public Sprite icon;
    }	
	
	// ----------------------------------------------------------------------------------
	// SpecialItemDescriptions
	// -----------------------------------------------------------------------------------
 	[Serializable]
    public class SpecialItemDescriptions {
    	public string PlayerExitBattle;
    	public string PlayerExitDungeon;
    	public string PlayerWarpDungeon;
    	public string CharacterLearnSkill;
    }	
	
    // ----------------------------------------------------------------------------------
	// DerivedStatNames
	// -----------------------------------------------------------------------------------
 	[Serializable]
    public class DerivedStatNames {
    	public DerivedStatType statType;
    	public string abbrev;
    	public string name;
    }
    
     // ----------------------------------------------------------------------------------
	// BasicDerivedStatNames
	// -----------------------------------------------------------------------------------
 	[Serializable]
    public class BasicDerivedStatNames {
    	
		public string level;
		public string LV;
		public string experience;
		public string XP;
		public string nextLevelXP;
		public string attributePoints;
		public string skillPoints;
		public string health;
		public string HP;
		public string mana;
		public string MP;
		public string fullAttack;
		public string abbrevAttack;
		public string fullDefense;
		public string abbrevDefense;
    }
    
    // ----------------------------------------------------------------------------------
	// SkillsVocabulary
	// -----------------------------------------------------------------------------------
 	[Serializable]
    public class SkillVocabulary {
    	public string manaCost;
		public string basePower;
		public string bonusPower;
		public string statInfluence;
		public string statPower;
		public string attackType;
		public string recoveryType;
		public string perLevel;
		public string useType;
		public string useLocation;
		public string useTarget;
    }

	// ----------------------------------------------------------------------------------
	// BasicVocabulary
	// -----------------------------------------------------------------------------------
 	[Serializable]
    public class BasicVocabulary {
		public string cannotSell;
		public string cannotDismiss;
		public string cannotAfford;
		public string maxStackReached;
		public string purchased;
		public string hired;
		public string dismissed;
		public string levelUp;
		public string runFail;
		public string runSuccess;
		public string damage;
		public string requirementsNotMet;
		public string soldFor;
		public string saved;
		public string noNeedToRest;
		public string lootFail;
		public string toInteract;
		public string effects;
		public string causing;
		public string returnToTown;
		public string affectedBy;
		public string unaffectedBy;
		public string areYouSure;
		public string yes;
		public string no;
		public string battleFail;
		public string battleSuccess;
		public string turnStart;
    }
    
    // ----------------------------------------------------------------------------------
	// NamesVocabulary
	// -----------------------------------------------------------------------------------
 	[Serializable]
    public class NamesVocabulary {
    	public string party;
		public string worldmap;
		public string statistics;
		public string requirements;
		public string resistance;
		public string element;
		public string charclass;
		public string species;
		public string savegame;
		public string loadgame;
    }
        
    // ===================================================================================
    
}