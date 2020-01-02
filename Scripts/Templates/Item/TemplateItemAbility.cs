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
	// TEMPLATE ITEM ABILITY
	// ===================================================================================
    [Serializable]
    [CreateAssetMenu(fileName = "Unnamed ItemAbility", menuName = "RDCK/Templates/Items/New ItemAbility")]
    public class TemplateItemAbility : TemplateItem {
    	
    	public bool _DepleteOnUse;
		public override bool DepleteOnUse 					{ get { return _DepleteOnUse; } }
		
		[Header("-=- ItemAbility -=-")]
        public TemplateSkill learnedAbility;
		
		protected override ItemType _itemType { get { return ItemType.Ability; } }
		
    }
    
    // ===================================================================================
    
}