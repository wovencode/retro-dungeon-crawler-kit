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
	// TEMPLATE CHARACTER - HERO
	// ===================================================================================
    [CreateAssetMenu(fileName = "Unnamed Hero", menuName = "RDCK/Templates/Characters/New Hero")]
    public class TemplateCharacterHero : TemplateCharacterBase {
    	
    	[Header("-=- Costs -=-")]
		public CurrencyCost _tradeCost;
		
    	public override CurrencyCost tradeCost 	{ get { return _tradeCost; } }
		
    }
    
	// ===================================================================================
   
}