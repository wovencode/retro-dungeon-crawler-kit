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
	// TEMPLATE ITEM CURATIVE
	// ===================================================================================
    [CreateAssetMenu(fileName = "ItemCurative", menuName = "RDCK/Templates/Items/New ItemCurative")]
    public class TemplateItemCurative : TemplateItem {

     	[Header("-=- Usage & Targeting -=-")]
    	public CanUseType 											_useType;
		public CanUseLocation 										_useLocation;
		public CanUseTarget 										_targetType;
		public bool 												_DepleteOnUse;
		
		[Header("-=- Visual/Audio -=-")]
		public AudioClip 											_useSFX;
		public GameObject 											_useEffect;
		
        [Header("-=- Power -=-")]
        public float 												_basePowerPercentage;
        public int 													_basePower;
		public TemplateMetaAttribute 								_baseStat;
		public float 												_statPower;
		public TemplateStatus[] 									_useBuffType; 
        public bool 												_removeBuff;
        
        [Header("-=- ItemCurative -=-")]
    	public RecoveryType 										_recoveryType;
		public TemplateMetaAttribute								_recoveryStat;
		
		public override CanUseType useType							{ get { return _useType; } }
		public override CanUseLocation useLocation					{ get { return _useLocation; } }
		public override CanUseTarget targetType						{ get { return _targetType; } }
		public override bool DepleteOnUse 							{ get { return _DepleteOnUse; } }
		
		public override AudioClip useSFX							{ get { return _useSFX; } }
		
		public override float basePowerPercentage					{ get { return _basePowerPercentage; } }
       	public override int basePower 								{ get { return _basePower; } }
		public override TemplateMetaAttribute baseStat 							{ get { return _baseStat; } }
		public override float statPower 							{ get { return _statPower; } }
		public override TemplateStatus[] useBuffType 				{ get { return _useBuffType; } }
        public override bool removeBuff								{ get { return _removeBuff; } }
				
    	public override RecoveryType recoveryType 					{ get { return _recoveryType; } }
    	public override TemplateMetaAttribute recoveryStat 			{ get { return _recoveryStat; } }
    		
    	protected override ItemType _itemType 						{ get { return ItemType.Curative; } }
    	
    }
    
    // ===================================================================================
    
}