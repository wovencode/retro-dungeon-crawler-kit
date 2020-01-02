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
	// CUTSCENE
	// ===================================================================================
	[Serializable]
    public class Cutscene {
		public CutsceneText[] text;
		public Sprite background;
		public Sprite foreground;
	}	
	
	// -----------------------------------------------------------------------------------
	// CutsceneText
	// -----------------------------------------------------------------------------------
 	[Serializable]
 	public class CutsceneText {
 		[TextArea(10,5)] public string[] _text;
 		
 		public string text {
 			get {
 					return _text[Finder.txt.language];
 			}
 		}
 		
 	}
 	    
    // ===================================================================================
    
}

