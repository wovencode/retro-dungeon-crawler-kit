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
using System.ComponentModel;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// PARTY CURRENCIES
	// ===================================================================================
	[Serializable]
	public class PartyCurrencies {
    	
    	public event PropertyChangedEventHandler PropertyChanged;
    	
        public int gold;
        
        // -------------------------------------------------------------------------------
		// DefaultInitialize
		// -------------------------------------------------------------------------------
        public void DefaultInitialize() {
        
        	gold = Finder.config.startingGold;
        
        }
        
        // -------------------------------------------------------------------------------
		// canAfford
		// -------------------------------------------------------------------------------
        public bool canAfford(CurrencyType currencyType, int value) {
        
        	switch (currencyType) {
			
				case CurrencyType.Gold:
					return gold >= value;
					
			}
			
			return false;
        
        }
        
        // -------------------------------------------------------------------------------
		// getResource
		// -------------------------------------------------------------------------------
		public int getResource(CurrencyType currencyType) {
			
			switch (currencyType) {
			
				case CurrencyType.Gold:
					return gold;
					
			}
			
			return 0;
		}
		
		// -------------------------------------------------------------------------------
		// setResource
		// -------------------------------------------------------------------------------
		public void setResource(CurrencyType currencyType, int value) {
			
			switch (currencyType) {
			
				case CurrencyType.Gold:
					gold += value;
					if (gold < 0) gold = 0;
					OnPropertyChanged("Gold");
					break;
					
			}
			
		}
		
		// ===============================================================================
		// OTHER
		// ===============================================================================

   		// -------------------------------------------------------------------------------
		// OnPropertyChanged
		// -------------------------------------------------------------------------------
   		protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler h = this.PropertyChanged;
            if (h != null)
                h(this, new PropertyChangedEventArgs(name));
        }
        
        // -------------------------------------------------------------------------------
        
    }
  
}