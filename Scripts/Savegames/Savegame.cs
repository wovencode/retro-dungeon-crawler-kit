// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;
using WoCo.DungeonCrawler;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// SAVEGAME
	// ===================================================================================
    [Serializable]
    public class Savegame {
    
    	public int Index;
    	public DateTime StartDate;
        public DateTime SaveDate;
        
        
        // -------------------------------------------------------------------------------
		// Current Location
		// -------------------------------------------------------------------------------
        public string mapName;
		public DirectionType facingDirection;
		public LocationType locationType;
        public float X;
        public float Y;
		
       	// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public PartyInventory inventory;
		public PartyEquipment equipment;
		
		public int gold;

		public string lastTown;
		
		public List<Interaction> InteractedObjects;
        public List<Interaction> DeactivatedObjects;
        
        public MapExplorationData MapExplorationInfo;
		public TownExplorationData TownExplorationInfo;
        public List<SavegameCharacter> characters;
        
        // -------------------------------------------------------------------------------
		// Savegame
		// -------------------------------------------------------------------------------
     	public Savegame() {
            InteractedObjects		= new List<Interaction>();
            DeactivatedObjects		= new List<Interaction>();
            MapExplorationInfo 		= new MapExplorationData();
            TownExplorationInfo 	= new TownExplorationData();
            
            inventory				= new PartyInventory();
           	equipment				= new PartyEquipment();
           	characters 				= new List<SavegameCharacter>();   
           	
           	gold					= 0;
           	
     	}
		
    }
    
  	// ===================================================================================
    
}