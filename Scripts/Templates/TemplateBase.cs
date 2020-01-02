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
	// TEMPLATE BASE
	// ===================================================================================
    [Serializable]
    public abstract class TemplateBase : ScriptableObject {
    
    	[Header("-=- Basic -=-")]
    	public string[] _fullName;
    	
    	// -------------------------------------------------------------------------------
		// fullName
		// -------------------------------------------------------------------------------
		public string fullName {
			get {
				if (_fullName.Length  > 0 && _fullName.Length >= Finder.txt.language && _fullName[Finder.txt.language] != null) {
					return _fullName[Finder.txt.language];
				} else {
					Debug.LogWarning("fullName missing for language ID:"+Finder.txt.language+" on "+this.name);
					return "-missing name-";
				}
			}
		}
    	
		// -------------------------------------------------------------------------------
      
    }
    
    // ===================================================================================
    
}