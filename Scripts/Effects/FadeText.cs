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
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {
	
	// ===================================================================================
	// FADE TEXT
	// ===================================================================================
    [RequireComponent(typeof(Text))]
    public class FadeText : MonoBehaviour {
    
        public Text fadeText;
		
		public bool autoStart;
		public bool reverse;
		public float delay = 0;
    	public float duration = 1;
    	public Color originalColor;
    	
    	protected float perSecond;
    	protected float startTime;
		
		// -------------------------------------------------------------------------------
		// OnEnable
		// -------------------------------------------------------------------------------    
		void OnEnable() {
			
			perSecond = fadeText.color.a+1 / duration;
			
			if (autoStart)
        		startTime = Time.time + delay;
		}
		
		// -------------------------------------------------------------------------------
		// OnDisable
		// -------------------------------------------------------------------------------    
		void OnDisable() {
		
			fadeText.color = originalColor;
			var col = fadeText.color;
			
			if (reverse) {
				col.a = 0;
			} else {
				col.a = 1;
			}
		}
		
		// -------------------------------------------------------------------------------
		// Update
		// -------------------------------------------------------------------------------    
		void Update() {
			if (Time.time >= startTime) {
            	var col = fadeText.color;
            	if (reverse) {
            		col.a += perSecond * Time.deltaTime;
            	} else {
            		col.a -= perSecond * Time.deltaTime;
            	}
            	fadeText.color = col;
        	}
		}
		
		// -------------------------------------------------------------------------------
		// OnFade
		// -------------------------------------------------------------------------------    
		public void OnFade() {
			fadeText.color = originalColor;
			var col = fadeText.color;
			
			if (reverse) {
				col.a = 1;
			} else {
				col.a = 0;
			}
			startTime = Time.time;
		}

        // -------------------------------------------------------------------------------
        
    }
}