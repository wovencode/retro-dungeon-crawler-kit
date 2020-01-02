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
	// TEMPLATE EQUIPMENT - ARMOR
	// ===================================================================================
    [Serializable]
    [CreateAssetMenu(fileName = "Equipment", menuName = "RDCK/Templates/Equipment/New Armor")]
    public class TemplateEquipmentArmor : TemplateEquipment {
    	
    	[Header("-=- Equipment Armor -=-")]
    	public TemplateMetaEquipmentSlot _equipmentType;
        
        public override TemplateMetaEquipmentSlot equipmentType { get { return _equipmentType; } }
		
    }
	
	// ===================================================================================
	
}