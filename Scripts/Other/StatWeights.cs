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

	// ===================================================================================
	// 
	// ===================================================================================
    public class StatWeights : List<StatWeight> {
		
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void CalculatedAdjustedWeights() {
        
            var sorted = this.OrderBy(x => x.weight).ToList();
            
            for (int i = 0; i < sorted.Count(); i++) {
                if (i == 0) {
                    sorted[i].adjustedWeight = sorted[i].weight;
                } else {
                    sorted[i].adjustedWeight = sorted[i].weight + sorted[i - 1].adjustedWeight;
            	}
            }
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public TemplateMetaAttribute GetStatToIncrement() {
        	CalculatedAdjustedWeights();
            float d = UnityEngine.Random.Range(0.0f, 1.0f);
            var item = this.OrderBy(x => x.adjustedWeight).FirstOrDefault(x => d <= x.adjustedWeight);
            return item.template;
        }
        
        // -------------------------------------------------------------------------------
        
    }
        
}