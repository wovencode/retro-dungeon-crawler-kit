// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WoCo.DungeonCrawler;

namespace WoCo.DungeonCrawler {

    // ===================================================================================
	// Location
	// ===================================================================================
    [Serializable]
    public class Location {
    
    	[NonSerialized]private TemplateMetaBase _template;
    	
        protected string _name;
        
        public LocationType locationType;
        public TemplateMetaBase targetMap;
        public DirectionType facingDirection;
        public float X;
        public float Y;
        
        // -------------------------------------------------------------------------------
		// template
		// -------------------------------------------------------------------------------
    	public TemplateMetaBase template {
    		get { return _template; }
    		set { 
    			_template = value;
    			if (_template != null) _name = _template.name;
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
		// name
		// -------------------------------------------------------------------------------
        public string name {
        
        	get {
        		if (template != null) {
        			return template.name;
        		}
        		return _name;
        	}
        	
        	set {
        		if (value == null) {
        			_template = null;
        			_name = "";
        			locationType = LocationType.Worldmap;
        		} else {
        			if (locationType == LocationType.Dungeon) {
        				template = DictionaryDungeon.Get(value);
        			} else if (locationType == LocationType.Town) {
        				template = DictionaryTown.Get(value);
        			}
        		}
        	}
        	
        }
        
    	// -------------------------------------------------------------------------------
		// Equals
		// -------------------------------------------------------------------------------
        public override bool Equals(object obj) {
            Location l2 = obj as Location;
            if (l2 != null &&
                (l2.template == null && template == null ||l2.name == name ) &&
                l2.X == X &&
                l2.Y == Y)
                return true;

            return false;
        }
        
    	// -------------------------------------------------------------------------------
		// GetHashCode
		// -------------------------------------------------------------------------------
        public override int GetHashCode() {
            int hash = 13;
            hash = (hash * 7) + name.GetHashCode();
            hash = (hash * 7) + X.GetHashCode();
            hash = (hash * 7) + Y.GetHashCode();
            return hash;
        }
        
    	// -------------------------------------------------------------------------------
    
    }
    
    // -------------------------------------------------------------------------------
    
}