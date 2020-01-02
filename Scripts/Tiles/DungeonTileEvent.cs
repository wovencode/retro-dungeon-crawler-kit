// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using WoCo.DungeonCrawler;
using UnityEngine;
using toinfiniityandbeyond.Tilemapping;
using System;
using System.Collections;
using System.Collections.Generic;

namespace WoCo.DungeonCrawler {
	
	// ===================================================================================
	// DUNGEON TILE EVENT
	// ===================================================================================
	[CreateAssetMenu (fileName = "Unnamed Tile Event", menuName = "RDCK/Tiles/New Tile Event")]
	public class DungeonTileEvent : DungeonTileBase {
		
		public GameObject objectPrefab;
		public float offsetHeight;
		public Texture2D minimapIcon;
		public bool facePlayer;
		public bool interactFromBothSides;
		
		[Header("-=- Image -=-")]
        public Sprite defaultImage;
        public Sprite interactedImage;
                   	
		[Header("-=- Event Settings -=-")]
		public bool HideOnCompletion;
		public bool TriggerOnce;
		public bool StartsDeactivated;
		
		[Header("-=- Event Chain -=-")]
		public EventChain eventChain;
		
    	
	}
	    
	// ===================================================================================
	
}
