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
	// TEMPLATE ELEMENT
	// ===================================================================================
    [Serializable]
    [CreateAssetMenu(fileName = "Unnamed Element", menuName = "RDCK/Meta/New Element")]
    public class TemplateMetaElement : TemplateBase {
    
    	public Sprite				icon;
    	public TemplateMetaElement 	PrimaryRelation;
    	[Range(0,10)]public float 	PrimaryRelationWeight;
    	public TemplateMetaElement 	SecondaryRelation;
    	[Range(0,10)]public float 	SecondaryRelationWeight;
				
	}
	
    // ===================================================================================
    
}