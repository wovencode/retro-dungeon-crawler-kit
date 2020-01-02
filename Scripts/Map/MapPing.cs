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
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

  	// -----------------------------------------------------------------------------------
	// MapPing
	// -----------------------------------------------------------------------------------
	[Serializable]
	public class MapPing {
	
    	[NonSerialized]private DungeonTileEvent _template;
        
    	protected string name;
    	protected float x;
    	protected float y;
    	
   		// -------------------------------------------------------------------------------
		// MapPing
		// -------------------------------------------------------------------------------      
    	public MapPing(Vector2 position, DungeonTileEvent tmpl) {
    		Position = position;
    		if (tmpl != null) {
    			template = tmpl;
    			name = tmpl.name;
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
		// MapPing
		// -------------------------------------------------------------------------------      
    	
    	public Vector2 Position {
    		get {
    			return new Vector2(x,y);
    		}
    		set {
    			x = value.x;
    			y = value.y;
    		}
    	}
    	
    	// -------------------------------------------------------------------------------
		// template
		// -------------------------------------------------------------------------------      
        public DungeonTileEvent template {
        	get { return _template; }
        	set {
        		if (value != null) {
        			_template = value;
        			name = _template.name;
        		}
        	}
        }
        
    	// -------------------------------------------------------------------------------
		// loadTemplate
		// -------------------------------------------------------------------------------
        public void loadTemplate() {
        	if (string.IsNullOrEmpty(name)) return;
       		template = (DungeonTileEvent)DictionaryTile.Get(name);
        }
    
    }
    
	// -------------------------------------------------------------------------------
	
}