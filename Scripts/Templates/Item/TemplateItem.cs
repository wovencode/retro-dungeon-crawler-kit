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
    public abstract class TemplateItem : TemplateAdvanced {
    
    	[Header("-=- Costs -=-")]
    	public CurrencyCost _tradeCost;
		
    	public int _MaxStack;
    	    	
    	public virtual bool DepleteOnUse 		{ get { return true; } }
    	public override CurrencyCost tradeCost 	{ get { return _tradeCost; } }
    	public override int MaxStack 			{ get { return _MaxStack; } }
    	
    	public virtual ItemType itemType		{ get { return _itemType; } }
    	
    	protected virtual ItemType _itemType 	{ get; }
    	

    	
    	public override bool checkQuantity {
			get {
				return Finder.party.inventory.GetQuantity(this) < MaxStack;
			}
		}
    	
    }
    
    // ===================================================================================
    
}