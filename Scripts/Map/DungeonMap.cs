// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System;
using System.Collections;

using WoCo.DungeonCrawler;
using toinfiniityandbeyond.Tilemapping;


namespace WoCo.DungeonCrawler {

	// -------------------------------------------------------------------------------
	// DungeonMap
	// -------------------------------------------------------------------------------
    public class DungeonMap {
    
        private ScriptableTile[,] tiles;

        public Vector2 Size { get; private set; }

        public DungeonMap(int height, int width) {
            this.Size = new Vector2(width, height);
            tiles = new ScriptableTile[width, height];
        }

        public void SetTile(int x, int y, ScriptableTile scriptableTile) {
            tiles[x, y] = scriptableTile;
        }

        public ScriptableTile GetTile(int x, int y) {
            return tiles[x, y];
        }
        
    }
}