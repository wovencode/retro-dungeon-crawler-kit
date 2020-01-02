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
	// IEvent
	// -----------------------------------------------------------------------------------
    public interface IEvent {
    
    	Sprite icon { get; }
        string text { get; }        
        List<EventChoice> choices 		{ get;  }
        
        void OnClickChoice(int id);
        bool CheckEventCondition(EventCondition condition);

    }
    
    // -----------------------------------------------------------------------------------
    
}