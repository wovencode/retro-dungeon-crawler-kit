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

	// -------------------------------------------------------------------------------
	// EventChain
	// -------------------------------------------------------------------------------
    [CreateAssetMenu (fileName = "Unnamed Event Chain", menuName = "RDCK/Events/New EventChain")]
    [Serializable]
    public class EventChain : ScriptableObject {
		
		public EventNode[] eventNodes;
       	
    }
    
}