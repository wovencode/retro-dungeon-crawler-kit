// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.ComponentModel;

namespace WoCo.DungeonCrawler {

 	// ===================================================================================
	// PartyPlayer
	// ===================================================================================
	[Serializable]
	public class PartyPlayer : PartyBase {
    	
    	public event PropertyChangedEventHandler PropertyChanged;
    	
		public PartyCurrencies currencies { get; set; }
        public PartyInventory inventory { get; set; }
		public PartyEquipment equipment { get; set; }

		public TemplateMetaTown lastTown { get; set; }
		
		public List<Interaction> InteractedObjects { get; set; }
        public List<Interaction> DeactivatedObjects { get; set; }
        
        public MapExplorationData MapExplorationInfo { get; set; }
		public TownExplorationData TownExplorationInfo { get; set; }

    	// -------------------------------------------------------------------------------
		// PartyPlayer
		// -------------------------------------------------------------------------------
     	public PartyPlayer() : base() {
            InteractedObjects		= new List<Interaction>();
            DeactivatedObjects		= new List<Interaction>();
            MapExplorationInfo 		= new MapExplorationData();
            TownExplorationInfo 	= new TownExplorationData();
            currencies				= new PartyCurrencies();
            inventory				= new PartyInventory();
           	equipment				= new PartyEquipment();
           	characters.Clear();
     	}
     	
  		// -------------------------------------------------------------------------------
		// DefaultInitialize
		// -------------------------------------------------------------------------------
        public override void DefaultInitialize() {
        	
        	currencies.DefaultInitialize();
        	
        	foreach (TemplateCharacterHero tmpl in Finder.config.startingCharacters) {
        		if (tmpl != null) {
					AddHero(tmpl);
        		}
        	}
        	
        }

		// ===============================================================================
		// FUNCTIONS
		// ===============================================================================
		
    	// -------------------------------------------------------------------------------
		// AddHero
		// -------------------------------------------------------------------------------
		public void AddHero(TemplateCharacterHero tmpl) {
			CharacterHero player = new CharacterHero
			{
				template = tmpl
			};
			player.DefaultInitialize();
			characters.Add(player);
			OnPropertyChanged("Member");
		}
		
    	// -------------------------------------------------------------------------------
		// DismissHero
		// -------------------------------------------------------------------------------
		public void DismissHero(TemplateBase tmpl) {
			
			if (tmpl is TemplateCharacterHero) {
				CharacterBase target = characters.FirstOrDefault(x => x.template == tmpl);
			
				if (target != null) {
					equipment.UnequipAll(target);
					characters.Remove(target);
					OnPropertyChanged("Member");
				}
			}
			
		}
		
     	// -------------------------------------------------------------------------------
		// ResetBattleStances
		// -------------------------------------------------------------------------------        
        public void ResetBattleStances() {
        	foreach (CharacterBase character in characters)
        		character.ResetBattleStances();
        }
        
     	// -------------------------------------------------------------------------------
		// SetFleeingStance
		// -------------------------------------------------------------------------------        
        public void SetFleeingStance() {
        	foreach (CharacterBase character in characters)
        		character.IsFleeing = true;
        }
          
 	    // ===============================================================================
		// SETTERS
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// AddExperience
		// -------------------------------------------------------------------------------
		public void AddExperience(int amount) {
			if (amount == 0) return;
			Finder.log.Add(Finder.txt.namesVocabulary.party + " " + Finder.txt.actionNames.gained + " " + amount + " " + Finder.txt.basicDerivedStatNames.XP);
			foreach (CharacterHero character in characters)
			{
        		if (character.IsAlive)
        			character.XP += amount;
			}
		}
		
		// -------------------------------------------------------------------------------
		// AddTown
		// -------------------------------------------------------------------------------
        public void AddTown(TemplateMetaTown town) {
			TownExplorationInfo.AddTown(town);
		}
		
	    // ===============================================================================
		// GETTERS
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// GetQuantity
		// -------------------------------------------------------------------------------
        public bool GetQuantity {
			get {
				return characters.Count < Finder.config.maxPartyMembers;
			}
		}
		
		// -------------------------------------------------------------------------------
		// getHasExploredTowns
		// -------------------------------------------------------------------------------
		public bool getHasExploredTowns(TemplateMetaTown[] towns) {
			if (towns == null || towns.Length == 0) return true;
			bool success = true;
			foreach (TemplateMetaTown town in towns)
				success = getHasExploredTown(town) ? success : false;
			return success;
		}		
		
		// -------------------------------------------------------------------------------
		// getHasExploredTown
		// -------------------------------------------------------------------------------
		public bool getHasExploredTown(TemplateMetaTown town) {
			if (town == null) return true;
			return TownExplorationInfo.GetTown(town.name) != null;
		}
		
		// -------------------------------------------------------------------------------
		// getHasExploredDungeons
		// -------------------------------------------------------------------------------
		public bool getHasExploredDungeons(TemplateMetaDungeon[] dungeons) {
			if (dungeons == null || dungeons.Length == 0) return true;
			bool success = true;
			foreach (TemplateMetaDungeon dungeon in dungeons)
				success = getHasExploredDungeon(dungeon) ? success : false;
			return success;
		}	
				
		// -------------------------------------------------------------------------------
		// getHasExploredDungeon
		// -------------------------------------------------------------------------------
		public bool getHasExploredDungeon(TemplateMetaDungeon map) {
			if (map == null) return true;
			return MapExplorationInfo.GetMapExplored(map.name);
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
        
    }

    // ===================================================================================
}