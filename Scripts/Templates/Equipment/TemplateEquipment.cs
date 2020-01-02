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
	// TEMPLATE EQUIPMENT
	// ===================================================================================
    [Serializable]
    public abstract class TemplateEquipment : TemplateAdvanced {
    
		[Header("-=- Costs -=-")]
		public CurrencyCost _tradeCost;
		
    	[Header("-=- Equipment -=-")]
        public StatModifiers 			statModifiers;
		public TemplateStatus[] 			equipBuffType;
		
    	public override CurrencyCost tradeCost 	{ get { return _tradeCost; } }
		
		public virtual TemplateMetaEquipmentSlot equipmentType { get; }
		
		
		
       	// -------------------------------------------------------------------------------
		// CanEquip
		// -------------------------------------------------------------------------------
		public virtual bool CanEquip(CharacterBase target) {
			return !target.IsStunned && target.IsAlive && RequirementsMet(target);
        }
		
		// -------------------------------------------------------------------------------
		// CanUnequip
		// -------------------------------------------------------------------------------
		public virtual bool CanUnequip(CharacterBase target) {
			return true;
        }
		
    }
	
	// ===================================================================================
	
}