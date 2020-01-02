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
	// TEMPLATE EQUIPMENT - WEAPON
	// ===================================================================================
    [CreateAssetMenu(fileName = "Weapon", menuName = "RDCK/Templates/Equipment/New Weapon")]
    public class TemplateEquipmentWeapon : TemplateEquipment {
    	
    	[Header("-=- Visual/Audio -=-")]
    	public AudioClip 												_critSFX;
		public AudioClip 												_hitSFX;
		public AudioClip 												_missSFX;
		public GameObject 												_hitEffect;
		
		[Header("-=- Weapon -=-")]
		public int 														_basePower;
		public TemplateMetaAttribute 									_baseStat;
		public float 													_statPower;
		public TemplateMetaCombatStyle 									_attackType;
    	public TemplateMetaElement 										_element;
    	public TemplateMetaSpecies										_strongVsSpecies;
       	public TemplateStatus[] 										_useBuffType; 
        public bool 													_removeBuff;
        
        public TemplateMetaEquipmentSlot _equipmentType;
        
        public override TemplateMetaEquipmentSlot equipmentType 		{ get { return _equipmentType; } }
        
        public override int basePower 									{ get { return _basePower; } }
		public override TemplateMetaAttribute baseStat 					{ get { return _baseStat; } }
		public override float statPower 								{ get { return _statPower; } }

		public override TemplateMetaCombatStyle attackType 				{ get { return _attackType; } }
    	public override TemplateMetaElement element 					{ get { return _element; } }
    	public override TemplateMetaSpecies strongVsSpecies				{ get { return _strongVsSpecies; } }
       	public override TemplateStatus[] useBuffType 					{ get { return _useBuffType; } }
        public override bool removeBuff									{ get { return _removeBuff; } }
        
		public override GameObject hitEffect 							{ get { return _hitEffect; } }
		public override AudioClip critSFX								{ get { return _critSFX; } }
		public override AudioClip hitSFX								{ get { return _hitSFX; } }
		public override AudioClip missSFX								{ get { return _missSFX; } }
		
		public string GetDescription(string separation = "\n", bool showNull = false) {
            
            string text = "";
            
            text += string.Format("{0} {1}{2}", Finder.txt.skillVocabulary.basePower, basePower, separation);
            text += string.Format("{0} {1}{2}", Finder.txt.skillVocabulary.attackType, attackType.fullNameAttack, separation);
            if (baseStat != null) 	text += string.Format("{0} {1}{2}", Finder.txt.skillVocabulary.statInfluence, baseStat.fullName, separation);
        	if (statPower != 0) 	text += string.Format("{0} {1}{2}", Finder.txt.skillVocabulary.statPower, (statPower * 100).ToString(), separation);
            
            if (!string.IsNullOrEmpty(text))
                text = "<b>"+Finder.txt.namesVocabulary.statistics+"</b>" + separation + text;
            
            return text;
        }
		
    }
    
    // ===================================================================================
    
}