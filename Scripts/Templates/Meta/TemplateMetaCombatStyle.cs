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
	// TEMPLATE COMBAT STYLE
	// ===================================================================================
    [Serializable]
    [CreateAssetMenu(fileName = "Unnamed CombatStyle", menuName = "RDCK/Meta/New CombatStyle")]
    public class TemplateMetaCombatStyle : ScriptableObject {
    	
    	public string[] _fullNameAttack;
    	public string[] _abbrevNameAttack;
    	public string[] _fullNameDefense;
    	public string[] _abbrevNameDefense;
    	public string[] _useText;
    	[Range(0,10)] public float frontAttackModifier;
    	[Range(0,10)] public float frontDefenseModifier;
    	[Range(0,10)] public float rearAttackModifier;
    	[Range(0,10)] public float rearDefenseModifier;
		
		// -------------------------------------------------------------------------------
		// fullNameAttack
		// -------------------------------------------------------------------------------
		public string fullNameAttack {
			get {
				return _fullNameAttack[Finder.txt.language];
			}
		}
		
		// -------------------------------------------------------------------------------
		// abbrevNameAttack
		// -------------------------------------------------------------------------------
		public string abbrevNameAttack {
			get {
				return _abbrevNameAttack[Finder.txt.language];
			}
		}
		
		// -------------------------------------------------------------------------------
		// fullNameDefense
		// -------------------------------------------------------------------------------
		public string fullNameDefense {
			get {
				return _fullNameDefense[Finder.txt.language];
			}
		}
		
		// -------------------------------------------------------------------------------
		// abbrevNameDefense
		// -------------------------------------------------------------------------------
		public string abbrevNameDefense {
			get {
				return _abbrevNameDefense[Finder.txt.language];
			}
		}
		
		// -------------------------------------------------------------------------------
		// useText
		// -------------------------------------------------------------------------------
		public string useText {
			get {
				return _useText[Finder.txt.language];
			}
		}
		
		// -------------------------------------------------------------------------------
		
	}
	
    // ===================================================================================
    
}