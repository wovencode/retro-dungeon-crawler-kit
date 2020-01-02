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
	// TEMPLATE SKILL - CURATIVE
	// ===================================================================================
    [CreateAssetMenu(fileName = "Unnamed SkillCurative", menuName = "RDCK/Templates/Skills/New SkillCurative")]
    public class TemplateSkillCurative : TemplateSkill {

     	[Header("-=- Usage & Targeting -=-")]
    	public CanUseType 											_useType;
		public CanUseLocation 										_useLocation;
		public CanUseTarget 										_targetType;

    	[Header("-=- Costs -=-")]
    	public int 													_baseManaCost;
    	public int 													_bonusManaCost;

		[Header("-=- Visual/Audio -=-")]
		public AudioClip 											_useSFX;
		public GameObject 											_useEffect;
		
        [Header("-=- Power -=-")]
        public float 												_basePowerPercentage;
        public int 													_basePower;
        public float												_bonusPowerPercentage;
        public int													_bonusPower;
		public TemplateMetaAttribute 								_baseStat;
		[Range(-10,10)]public float 								_statPower;
		public TemplateStatus[] 									_useBuffType; 
        public bool 												_removeBuff;
        public RecoveryType											_recoveryType;
        public TemplateMetaAttribute								_recoveryStat;
        
		public override CanUseType useType							{ get { return _useType; } }
		public override CanUseLocation useLocation					{ get { return _useLocation; } }
		public override CanUseTarget targetType						{ get { return _targetType; } }		

		public override AudioClip useSFX							{ get { return _useSFX; } }
		
		public override float basePowerPercentage					{ get { return _basePowerPercentage; } }
       	public override int basePower 								{ get { return _basePower; } }
       	public override int bonusPower 								{ get { return _bonusPower; } }
       	public override float bonusPowerPercentage 					{ get { return _bonusPowerPercentage; } }
		public override TemplateMetaAttribute baseStat 				{ get { return _baseStat; } }
		public override float statPower 							{ get { return _statPower; } }
		public override TemplateStatus[] useBuffType 				{ get { return _useBuffType; } }
        public override bool removeBuff								{ get { return _removeBuff; } }

		public override RecoveryType recoveryType 					{ get { return _recoveryType; } }
		public override TemplateMetaAttribute recoveryStat 			{ get { return _recoveryStat; } }
		
		public override int baseManaCost 							{ get { return _baseManaCost; } }
		public override int bonusManaCost 							{ get { return _bonusManaCost; } }
		
    }
    
    // ===================================================================================
    
}