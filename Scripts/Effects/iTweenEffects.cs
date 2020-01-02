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
	// iTweenEffects
	// ===================================================================================
    public class iTweenEffects : MonoBehaviour {
    	
    	[Range(0f, 100f)]public float shakeDuration;
    	[Range(0f, 100f)]public float shakeStrength;
    	
    	[Range(0f, 100f)]public float tiltDuration;
    	[Range(0f, 100f)]public float tiltStrength;
    	public MoveType	tiltDirection;
    	
    	public GameObject targetObject;
    	
    	// -------------------------------------------------------------------------------
		// Awake
		// -------------------------------------------------------------------------------    
		public void Awake() {
			iTween.Init(targetObject);
		}
    	
		// -------------------------------------------------------------------------------
		// Shake
		// -------------------------------------------------------------------------------    
		public void Shake() {
			iTween.PunchRotation(targetObject, iTween.Hash("y", shakeStrength, "z", shakeStrength, "duration", shakeDuration));
		}
		
		// -------------------------------------------------------------------------------
		// Tilt
		// -------------------------------------------------------------------------------    
		public void Tilt() {
			
			switch (tiltDirection) {
			
				case MoveType.Up:
					iTween.PunchPosition(targetObject, iTween.Hash("y", tiltStrength, "duration", tiltDuration));
					break;
					
				case MoveType.Down:
					iTween.PunchPosition(targetObject, iTween.Hash("y", tiltStrength*-1, "duration", tiltDuration));
					break;
				
			}
				
		}
		
		// -------------------------------------------------------------------------------
		// Tilt
		// -------------------------------------------------------------------------------    
		public void Tilt(Vector3 vector) {
			iTween.PunchPosition(targetObject, vector, tiltDuration);
		}
		
        // -------------------------------------------------------------------------------    
        
    }
}