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

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// InventorySlot
	// ===================================================================================
    [Serializable]
    public abstract class InventorySlot : InstanceBase {
    	
        public virtual int Quantity { get; set; }
        public virtual int Level { get; set; }
        
        protected string _equipee;
    	
    	// -------------------------------------------------------------------------------
    	//
    	// -------------------------------------------------------------------------------
    	public CharacterBase character {
    		get {
    			if (_equipee != "")
    				return Finder.party.GetMember(_equipee);
    			else
    				return null;
    		}
    		set {
    			if (value != null)
    				_equipee = value.Name;
    			else
    				_equipee = "";
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
    	//
    	// -------------------------------------------------------------------------------
    	public bool IsEquipped {
    		get { return (character != null); }
    	}
        
        // -------------------------------------------------------------------------------
    	
    }
    
    // ===================================================================================
    
}