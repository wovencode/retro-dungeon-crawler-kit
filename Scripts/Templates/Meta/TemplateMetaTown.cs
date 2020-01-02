// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// TEMPLATE META TOWN
	// ===================================================================================
    [CreateAssetMenu(fileName = "Unnamed Town", menuName = "RDCK/Meta/New Town")]
    [Serializable]
    public class TemplateMetaTown : TemplateMetaBase {
    
		[Header("-=- Visuals -=-")]
		public Sprite imageBackground;
		
		[Header("-=- Shops -=-")]
		public TemplateMetaShop[] shops;

    }
    
    // ===================================================================================

}