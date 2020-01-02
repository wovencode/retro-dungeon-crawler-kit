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
	// TEMPLATE STATUS
	// ===================================================================================
    [Serializable]
    [CreateAssetMenu(fileName = "Unnamed Status", menuName = "RDCK/Templates/New Status")]
    public class TemplateStatus : TemplateSimple {
    	
    	
    	public int duration;
    	public bool permanent;
    	public bool sticky;
    	public bool stayAfterBattle;
    	[Range(0,100)]public int removeByHit;
    	public StatModifiers statModifiers;
    	[Range(0,100)]public int buffAccuracy;
 		
		
 		
    }
    
    // ===================================================================================
    
}