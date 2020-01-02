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
	// TEMPLATE SKILL - ATTACK
	// ===================================================================================
    [CreateAssetMenu(fileName = "Unnamed SkillAttack", menuName = "RDCK/Templates/Skills/New SkillAttack")]
    public class TemplateSkillAttack : TemplateSkill {

      	[Header("-=- Usage & Targeting -=-")]
    	public CanUseType 											_useType;
		public CanUseLocation 										_useLocation;
		public CanUseTarget 										_targetType;

        [Header("-=- Costs -=-")]
        public int 													_baseManaCost;
        public int 													_bonusManaCost;
		
		[Header("-=- Visual/Audio -=-")]
		public AudioClip 											_critSFX;
		public AudioClip 											_hitSFX;
		public AudioClip 											_missSFX;
		public GameObject 											_hitEffect;
		
        [Header("-=- Power -=-")]
        public int 													_basePower;
        public int													_bonusPower;
		public TemplateMetaAttribute 								_baseStat;
		[Range(-10,10)]public float 								_statPower;
		public TemplateMetaCombatStyle 								_attackType;
    	public TemplateMetaElement 									_element;
    	public TemplateMetaSpecies									_strongVsSpecies;
       	public TemplateStatus[] 									_useBuffType; 
        public bool 												_removeBuff;
        		
        public override CanUseTarget targetType						{ get { return _targetType; } }
		public override CanUseType useType							{ get { return _useType; } }
		public override CanUseLocation useLocation					{ get { return _useLocation; } }

		public override GameObject hitEffect 						{ get { return _hitEffect; } }
		
        public override int baseManaCost 							{ get { return _baseManaCost; } }
        public override int bonusManaCost 							{ get { return _bonusManaCost; } }
        
        public override int basePower 								{ get { return _basePower; } }
        public override int bonusPower 								{ get { return _bonusPower; } }
		public override TemplateMetaAttribute baseStat 				{ get { return _baseStat; } }
		public override float statPower 							{ get { return _statPower; } }
		
		public override TemplateMetaCombatStyle attackType 			{ get { return _attackType; } }
    	public override TemplateMetaElement element 				{ get { return _element; } }
    	public override TemplateMetaSpecies strongVsSpecies			{ get { return _strongVsSpecies; } }
       	public override TemplateStatus[] useBuffType 				{ get { return _useBuffType; } }
        public override bool removeBuff								{ get { return _removeBuff; } }
        
        public override AudioClip critSFX							{ get { return _critSFX; } }
		public override AudioClip hitSFX							{ get { return _hitSFX; } }
		public override AudioClip missSFX							{ get { return _missSFX; } }
        
    }
    
    // ===================================================================================
    
}