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
	// InstanceBase
	// ===================================================================================
    [Serializable]
    public abstract class InstanceBase {
    	
    	protected string _name;
        [NonSerialized] protected Sprite _icon;
    	
    	public virtual string name {
    		get { return _name; }
    		set { _name = value; }
    	}
    	
    	public virtual Sprite icon {
    		get { return _icon; }
    		set { _icon = value; }
    	}
    	
    	public virtual string fullName { get; }
		public virtual void loadTemplate() {}
		
    }
    
    // ===================================================================================
    
}