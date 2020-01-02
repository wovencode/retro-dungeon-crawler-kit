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
	// EventNode
	// -------------------------------------------------------------------------------
    [Serializable]
    public class EventNode {
		
		public Sprite icon;
		[TextArea]public string text;
		
		public bool NoAutoStart;
		public EventCondition mainCondition;
		public EventCondition[] choiceConditions;
		public string[] choiceLabels;
		public EventAction[] choiceActions;
       	
    }
}