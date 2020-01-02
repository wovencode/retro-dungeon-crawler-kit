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

namespace WoCo.DungeonCrawler {

    [Serializable]
    public class MapExplorationData {
    
        public Dictionary<string, List<MapPing>> ExploredAreas { get; set; }

 		// -------------------------------------------------------------------------------
		// MapExplorationData
		// -------------------------------------------------------------------------------
		public MapExplorationData()  {
            ExploredAreas = new Dictionary<string, List<MapPing>>();
        }

		// -------------------------------------------------------------------------------
		// GetFloorData
		// -------------------------------------------------------------------------------
        private List<MapPing> GetFloorData(string mapName) {
            List<MapPing> floorData = null;
            if (!ExploredAreas.ContainsKey(mapName)) {
                floorData = new List<MapPing>();
                ExploredAreas.Add(mapName, floorData);
            } else {
                floorData = ExploredAreas[mapName];
            }
            return floorData;
        }
		
		// -------------------------------------------------------------------------------
		// GetMapExplored
		// -------------------------------------------------------------------------------
   		public bool GetMapExplored(string name) {
            return (ExploredAreas.ContainsKey(name));
        }
        
 		// -------------------------------------------------------------------------------
		// GetExploredMapCount
		// -------------------------------------------------------------------------------
        public int GetExploredMapCount {
        	get {
        		return ExploredAreas.Count;
        	}
        }
		
		// -------------------------------------------------------------------------------
		// AddExploredSpace
		// -------------------------------------------------------------------------------
        public void AddExploredSpace(string mapName, Vector2 pos) {
            
            pos = pos.Round(0);
            List<MapPing> floorData = this.GetFloorData(mapName);

            if (!floorData.Any(x => x.Position.x == pos.x && x.Position.y == pos.y)) {
                floorData.Add(new MapPing(pos, null));
            }
        }

		// -------------------------------------------------------------------------------
		// AddMapPing
		// -------------------------------------------------------------------------------
		public void AddMapPing(string mapName, Vector2 pos, DungeonTileEvent tile) {
			
			pos = pos.Round(0);
            List<MapPing> floorData = this.GetFloorData(mapName);
            MapPing info = floorData.FirstOrDefault(x => x.Position.x == pos.x && x.Position.y == pos.y);
            
            if (info != null) {
                if (info.template == tile) return;
                floorData.Remove(info);
            }

            floorData.Add(new MapPing(pos, tile));
		}
		
		// -------------------------------------------------------------------------------
		
    }
}