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
	// TEMPLATE SHOP
	// ===================================================================================
    [Serializable]
    [CreateAssetMenu(fileName = "Shop", menuName = "RDCK/Meta/New Shop")]
    public class TemplateMetaShop : TemplateBase {
    
		public Sprite imageShopkeeper;
        public Sprite imageBackground;
		public int level;
		public ShopType shopType;
		public bool canSell;
		public CurrencyType currencyType;
		public AudioClip music;
		public WelcomeText[] _welcomeText;
		
		
		// -------------------------------------------------------------------------------
		// description
		// -------------------------------------------------------------------------------
		public string welcomeText {
			get {
				return _welcomeText[Finder.txt.language].getRandom;
			}
		}
		
	}
	
	// -----------------------------------------------------------------------------------
	// WelcomeText
	// -----------------------------------------------------------------------------------
	[Serializable]
	public class WelcomeText {
		[TextArea]public string[] text;
		
		public string getRandom {
			get {
				return text[UnityEngine.Random.Range(0, text.Length)];
			}
		}
		
	}
	
    // ===================================================================================
    
}