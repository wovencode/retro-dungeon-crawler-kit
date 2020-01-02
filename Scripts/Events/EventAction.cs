// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoCo.DungeonCrawler {

	// -------------------------------------------------------------------------------
	// EventActionMinor
	// -------------------------------------------------------------------------------
    [Serializable]
    [CreateAssetMenu(fileName = "Unnamed EventAction", menuName = "RDCK/Events/New EventAction")]
    public class EventAction : ScriptableObject {

		[Header("-=- Play Sound -=-")]
		public AudioClip soundEffect;

		[Header("-=- Costs -=-")]
		public TemplateCharacterHero[] removeHeroes;
		public TemplateItem[] removeItems;
		public AbilityLevel[] castAbilities;
		public CurrencyType currencyType;
		public int removeCurrencyAmount;
		
		[Header("-=- Rewards -=-")]
		public TemplateCharacterHero[] addHeroes;
		public LootDrop[] addItems;
		public int addExperience;
		
		[Header("-=- Attack Party -=-")]
		public TemplateSkillAttack attackAbility;
		public int attackAbilityLevel;
		public InteractableEventTarget attackTarget;
		
		[Header("-=- Cure Party -=-")]
		public TemplateSkillCurative curativeAbility;
		public int curativeAbilityLevel;
		public InteractableEventTarget curativeTarget;
		
		[Header("-=- Manipulate other Event -=-")]
       	public InteractableEventAction[] events;
       	
		[Header("-=- Manipulate this Event -=-")]
		public BoolType eventActivation;
		public BoolType eventInteraction;
		       	
		[Header("-=- Start Combat -=-")]
		public bool startCombat;
		public int battlePoolId;
		public int battleEncounterLevel;
		public int battleEncounterAmountMin;
		public int battleEncounterAmountMax;
		public bool battleEncounterAmountScale;
		
		[Header("-=- Animate Event -=-")]
		public MoveType moveDirection;
		
		[Header("-=- Teleportation -=-")]
		public Location teleport;
		//public bool gotoOutro;
		
		[Header("-=- Other -=-")]
		public TemplateMetaShop openShop;
		
       	[Header("-=- Event Flow -=-")]
		public InteractableEventExecution continueEvent;
		public int gotoNodeIndex;
		
	
		
    }
    
    // ===================================================================================
    // TINY CLASSES
    // ===================================================================================
    
    [Serializable]
    public class InteractableEventAction {
    	public int targetX;
		public int targetY;
		public BoolType targetActivation;
		public BoolType targetInteraction;
    
    }
    
    public enum InteractableEventTarget {
    	OneRandom,
    	All
    }
    
    public enum InteractableEventExecution {
    	None,
    	GotoNextNode,
    	GotoNodeIndex,
    	CompleteEvent,
    	CancelEvent
    }
        
    
}