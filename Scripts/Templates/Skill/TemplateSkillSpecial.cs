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
	// TEMPLATE SKILL - SPECIAL
	// ===================================================================================
    [Serializable]
    [CreateAssetMenu(fileName = "Unnamed SkillSpecial", menuName = "RDCK/Templates/Skills/New SkillSpecial")]
    public class TemplateSkillSpecial : TemplateSkill {
		
      	[Header("-=- Usage & Targeting -=-")]
    	public CanUseType 											_useType;
		public CanUseLocation 										_useLocation;
		public CanUseTarget 										_targetType;
		
		[Header("-=- Visual/Audio -=-")]
		public AudioClip 											_useSFX;
		public GameObject 											_hitEffect;
		
        [Header("-=- Ability Special -=-")]
        public int 													_baseManaCost;
        public int 													_bonusManaCost;
        
        [Header("-=- Ability Special -=-")]
        public SpecialActionType 									_useEffectType;
        
        public override SpecialActionType useEffectType 			{ get { return _useEffectType; } }
 
        public override CanUseTarget targetType						{ get { return _targetType; } }
		public override CanUseType useType							{ get { return _useType; } }
		public override CanUseLocation useLocation					{ get { return _useLocation; } }

		public override AudioClip useSFX							{ get { return _useSFX; } }
		public override GameObject hitEffect 						{ get { return _hitEffect; } }
	
        public override int baseManaCost 							{ get { return _baseManaCost; } }
		public override int bonusManaCost 							{ get { return _bonusManaCost; } }
        
    }
    
    // ===================================================================================
    
}