// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// OBJECT FINDER UTILITY
	// ===================================================================================
	public class Finder {
		
		protected static ConfirmationBox			cache_confirmationBox;
		protected static PartyPlayer				cache_party;
		protected static TextManager 				cache_textManager;
		protected static EffectManager 				cache_fxManager;
        protected static AudioManager 				cache_audioManager;
        protected static BattleManager 				cache_battleManager;
        protected static MapManager 				cache_mapManager;
        protected static UIManager 					cache_uiManager;
		protected static NavigationManager			cache_navigationManager;
		protected static SettingsManager			cache_settingsManager;
		protected static ConfigManager				cache_configManager;
		protected static LogManager					cache_logManager;
		protected static SaveGameManager			cache_savegameManager;
		protected static TooltipPanel				cache_TooltipPanel;
		protected static MenuLootPanel				cache_LootPanel;
		
		// -------------------------------------------------------------------------------
		// MenuLootPanel (loot)
		// -------------------------------------------------------------------------------
		public static MenuLootPanel loot {
			get {
				if (cache_LootPanel == null)
					cache_LootPanel = GameObject.FindObjectOfType<MenuLootPanel>();
				return cache_LootPanel;
			}
		}
		
		// -------------------------------------------------------------------------------
		// ConfirmationBox (confirm)
		// -------------------------------------------------------------------------------
		public static ConfirmationBox confirm {
			get {
				if (cache_confirmationBox == null)
					cache_confirmationBox = GameObject.FindObjectOfType<ConfirmationBox>();
				return cache_confirmationBox;
			}
		}
		
		// -------------------------------------------------------------------------------
		// TooltipPanel (tooltip)
		// -------------------------------------------------------------------------------
		public static TooltipPanel tooltip {
			get {
				if (cache_TooltipPanel == null)
					cache_TooltipPanel = GameObject.FindObjectOfType<TooltipPanel>();
				return cache_TooltipPanel;
			}
		}
		
		// -------------------------------------------------------------------------------
		// SaveGameManager (save)
		// -------------------------------------------------------------------------------
		public static SaveGameManager save {
			get {
				if (cache_savegameManager == null)
					cache_savegameManager = GameObject.FindObjectOfType<SaveGameManager>();
				return cache_savegameManager;
			}
		}

		// -------------------------------------------------------------------------------
		// PartyPlayer (party)
		// -------------------------------------------------------------------------------
		public static PartyPlayer party {
			get {
				if (cache_party == null)
					cache_party = GameObject.FindObjectOfType<PartyPlayer>();
				return cache_party;
			}
		}

		// -------------------------------------------------------------------------------
		// LogManager (log)
		// -------------------------------------------------------------------------------
		public static LogManager log {
			get {
				if (cache_logManager == null)
					cache_logManager = GameObject.FindObjectOfType<LogManager>();
				return cache_logManager;
			}
		}

		// -------------------------------------------------------------------------------
		// TextManager (txt)
		// -------------------------------------------------------------------------------
		public static TextManager txt {
			get {
				if (cache_textManager == null)
					cache_textManager = GameObject.FindObjectOfType<TextManager>();
				return cache_textManager;
			}
		}
		
		// -------------------------------------------------------------------------------
		// NavigationManager (navi)
		// -------------------------------------------------------------------------------
		public static NavigationManager navi {
			get {
				if (cache_navigationManager == null)
					cache_navigationManager = GameObject.FindObjectOfType<NavigationManager>();
				return cache_navigationManager;
			}
		}

		// -------------------------------------------------------------------------------
		// ConfigManager (config)
		// -------------------------------------------------------------------------------
		public static ConfigManager config {
			get {
				if (cache_configManager == null)
					cache_configManager = GameObject.FindObjectOfType<ConfigManager>();
				return cache_configManager;
			}
		}		
		
		// -------------------------------------------------------------------------------
		// BattleManager (battle)
		// -------------------------------------------------------------------------------
		public static BattleManager battle {
			get {
				if (cache_battleManager == null)
					cache_battleManager = GameObject.FindObjectOfType<BattleManager>();
				return cache_battleManager;
			}
		}	
		
		// -------------------------------------------------------------------------------
		// SettingsManager (settings)
		// -------------------------------------------------------------------------------
		public static SettingsManager settings {
			get {
				if (cache_settingsManager == null)
					cache_settingsManager = GameObject.FindObjectOfType<SettingsManager>();
				return cache_settingsManager;
			}
		}	
		// -------------------------------------------------------------------------------
		// EffectManager (fx)
		// -------------------------------------------------------------------------------
		public static EffectManager fx {
			get {
				if (cache_fxManager == null)
					cache_fxManager = GameObject.FindObjectOfType<EffectManager>();
				return cache_fxManager;
			}
		}		
		
		// -------------------------------------------------------------------------------
		// AudioManager (audio)
		// -------------------------------------------------------------------------------
		public static AudioManager audio {
			get {
				if (cache_audioManager == null)
					cache_audioManager = GameObject.FindObjectOfType<AudioManager>();
				return cache_audioManager;
			}
		}		

		// -------------------------------------------------------------------------------
		// UIManager (ui)
		// -------------------------------------------------------------------------------
		public static UIManager ui {
			get {
				if (cache_uiManager == null)
					cache_uiManager = GameObject.FindObjectOfType<UIManager>();
				return cache_uiManager;
			}
		}		

		// -------------------------------------------------------------------------------
		// MapManager (map)
		// -------------------------------------------------------------------------------
		public static MapManager map {
			get {
				if (cache_mapManager == null)
					cache_mapManager = GameObject.FindObjectOfType<MapManager>();
				return cache_mapManager;
			}
		}
		
		// -------------------------------------------------------------------------------
	
	}

}