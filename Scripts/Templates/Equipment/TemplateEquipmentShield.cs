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
	// TEMPLATE EQUIPMENT - SHIELD
	// ===================================================================================
    [Serializable]
    [CreateAssetMenu(fileName = "Unnamed Shield", menuName = "RDCK/Templates/Equipment/New Shield")]
    public class TemplateEquipmentShield : TemplateEquipment {
    	
    	[Header("-=- Visual/Audio -=-")]
		public AudioClip 				_critSFX;
		public AudioClip 				_hitSFX;
		public AudioClip				_missSFX;
		public GameObject 				_hitEffect;
		
		[Header("-=- Activation -=-")]
		public TemplateMetaCombatStyle 				_attackType;
    	public TemplateMetaElement 				_element;
    	public TemplateMetaSpecies					_strongVsSpecies;
       	public TemplateStatus[] 			_useBuffType; 
        public bool 					_removeBuff;
        
        public TemplateMetaEquipmentSlot _equipmentType;
        
        public override TemplateMetaEquipmentSlot equipmentType { get { return _equipmentType; } }
 
        public override GameObject hitEffect 		{ get { return _hitEffect; } }
		public override TemplateMetaCombatStyle attackType 		{ get { return _attackType; } }
    	public override TemplateMetaElement element 	{ get { return _element; } }
    	public override TemplateMetaSpecies strongVsSpecies		{ get { return _strongVsSpecies; } }
       	public override TemplateStatus[] useBuffType 	{ get { return _useBuffType; } }
        public override bool removeBuff				{ get { return _removeBuff; } }
		
		public override AudioClip critSFX			{ get { return _critSFX; } }
		public override AudioClip hitSFX			{ get { return _hitSFX; } }
		public override AudioClip missSFX			{ get { return _missSFX; } }
		
    }
    
    // ===================================================================================
    
}