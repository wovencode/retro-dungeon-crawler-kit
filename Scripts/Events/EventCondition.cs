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
	// EventCondition
	// -------------------------------------------------------------------------------
    [Serializable]
    [CreateAssetMenu(fileName = "Unnamed EventCondition", menuName = "RDCK/Events/New EventCondition")]
    public class EventCondition : ScriptableObject {
		
		
		[Header("-=- Party Conditions -=-")]
		public TemplateCharacterHero[] requiredHeroes;
		public bool requiresAllHeroes;
		
       	[Header("-=- Item Conditions -=-")]
       	public TemplateItem[] items;
   		public bool requiresAllItems;
   		
   		[Header("-=- Ability Conditions -=-")]
   		public AbilityLevel[] abilities;
   		public bool requiresAllAbilities;
   		
   		[Header("-=- Event Conditions -=-")]
   		public InteractableEventCondition[] events;
		public bool requiresAllEvents;
		
    }
    
    // ===================================================================================
    // TINY CLASSES
    // ===================================================================================
    
    [Serializable]
    public class InteractableEventCondition {
    	public int targetX;
		public int targetY;
		public BoolType targetActivated;
		public BoolType targetInteracted;
    
    }
    
    
    
    // -----------------------------------------------------------------------------------
    
}