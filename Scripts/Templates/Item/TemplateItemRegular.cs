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
    [CreateAssetMenu(fileName = "ItemRegular", menuName = "RDCK/Templates/Items/New ItemRegular")]
    public class TemplateItemRegular : TemplateItem {
    	

		protected override ItemType _itemType { get { return ItemType.Regular; } }
		
    }
    
    // ===================================================================================
    
}