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
	// TEMPLATE ITEM
	// ===================================================================================
    [Serializable]
    [CreateAssetMenu(fileName = "Unnamed ItemSpecial", menuName = "RDCK/Templates/Items/New ItemSpecial")]
    public class TemplateItemSpecial : TemplateItem {
 		
     	[Header("-=- Usage & Targeting -=-")]
		public CanUseLocation 										_useLocation;
		public bool 												_DepleteOnUse;
    	
		[Header("-=- Visual/Audio -=-")]
		public AudioClip 											_useSFX;
		public GameObject 											_hitEffect;
		
        [Header("-=- ItemSpecial -=-")]
        public SpecialActionType 									_useEffectType;
        
        public override SpecialActionType useEffectType 			{ get { return _useEffectType; } }
      
       	public override CanUseTarget targetType						{ get { return CanUseTarget.AtSelf; } }
		public override CanUseType useType							{ get { return CanUseType.IsAlive; } }
		public override CanUseLocation useLocation					{ get { return _useLocation; } }
		public override bool DepleteOnUse 							{ get { return _DepleteOnUse; } }
		
		public override AudioClip useSFX							{ get { return _useSFX; } }
		public override GameObject hitEffect 						{ get { return _hitEffect; } }
				
		protected override ItemType _itemType						{ get { return ItemType.Special; } }
		
    }
    
    // ===================================================================================
    
}