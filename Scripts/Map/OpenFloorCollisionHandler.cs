// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System.Collections;

namespace WoCo.DungeonCrawler {

    public class OpenFloorCollisionHandler : MonoBehaviour {
        
        public float encounterRateModifier;
		public int monsterPoolID;
		public int monsterLevelModifier;
		public int monsterAmountMin;
		public int monsterAmountMax;
		public bool monsterAmountScale;
		
        private void OnTriggerEnter(Collider col) {
        	PartyPlayer party = col.GetComponent<PartyPlayer>();
            if (party != null) {
                Finder.battle.RollForBattle(encounterRateModifier, monsterLevelModifier, monsterAmountMin, monsterAmountMax, monsterAmountScale, monsterPoolID);
            }
        }

    }
}