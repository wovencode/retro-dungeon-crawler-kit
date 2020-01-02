// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using WoCo.DungeonCrawler;
using UnityEngine;
using toinfiniityandbeyond.Tilemapping;

namespace WoCo.DungeonCrawler {
	
	// ===================================================================================
	// DUNGEON TILE BASE
	// ===================================================================================
	public abstract class DungeonTileBase : ScriptableTile {

		[Header("-=- Basic Tile -=-")]
		public Sprite editorIcon;
		public EditorType editorType;
		public GameObject floorPrefab;
		public GameObject ceilingPrefab;
		public DirectionType facingDirection;
		
		public override bool IsValid {
			get {
				if(editorIcon == null)
					return false;
				return true;
			}
		}
		
		public override Sprite GetSprite (TileMap tilemap, Point position = default (Point)) {
			return editorIcon;
		}
		
		public override Texture2D GetIcon () {
			if (!IsValid) return null;
			return editorIcon.ToTexture2D();
		}
		
		private void OnValidate () {}
		
	}
	
	// ===================================================================================
	
}
