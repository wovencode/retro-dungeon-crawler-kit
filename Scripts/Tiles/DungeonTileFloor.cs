// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using WoCo.DungeonCrawler;
using UnityEngine;
	
namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// DUNGEON TILE FLOOR
	// ===================================================================================
	[CreateAssetMenu (fileName = "Unnamed Tile Floor", menuName = "RDCK/Tiles/New Tile Floor")]
	public class DungeonTileFloor : DungeonTileBase {
		
		[Header("-=- Floor Tile -=-")]
		[Range(0,99)]public int monsterPoolID;
		[Range(-1,1)]public float encounterRateModifier;
		public int monsterAmountMin;
		public int monsterAmountMax;
		public bool monsterAmountScale;
		public int monsterLevelModifier;
		
	}
	
	// ===================================================================================
		
}
