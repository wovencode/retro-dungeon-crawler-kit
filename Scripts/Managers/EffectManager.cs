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
	// EffectManager
	// ===================================================================================
    public class EffectManager : MonoBehaviour {
    	
    	public GameObject screenOverlay;
 		public Canvas canvas;
 		
		[Header("-=- UI Icons -=-")]
		public Sprite cancelButtonIcon;
		public Sprite worldmapTownIcon;
		public Sprite worldmapDungeonIcon;

    	[Header("-=- Flash Colors -=-")]
    	public FlashColors flashColors;
    	
 		// -------------------------------------------------------------------------------
		// Fade
		// -------------------------------------------------------------------------------    
       	public void Fade(bool startFaded) {
       		Flash flashComponent = screenOverlay.GetComponent<Flash>();
       		if (flashComponent) flashComponent.OnFlash(Color.black, startFaded, 0);
       	}
       	
 		// -------------------------------------------------------------------------------
		// SpawnEffect
		// -------------------------------------------------------------------------------    
       	public void SpawnEffect(Transform targetTransform, GameObject effect) {
       		
       		
       		GameObject hitEffect = (GameObject)Instantiate(effect, targetTransform);
       		
       		hitEffect.transform.SetParent(canvas.transform);
       		hitEffect.transform.SetAsLastSibling();
       		hitEffect.SetActive(true);
       		
       		
       	}
       	
  		// -------------------------------------------------------------------------------
		// StopAllEffects
		// -------------------------------------------------------------------------------    
       	public void StopAllEffects() {
       	
       	}
       	
  		// -------------------------------------------------------------------------------
		// StopEffect
		// -------------------------------------------------------------------------------    
       	public void StopEffect(GameObject effect) {
       	
       	}
       
    }
    
    // ===================================================================================
	// TINY CLASSES (ONLY USED BY EFFECT MANAGER)
	// ===================================================================================
   
   [Serializable]
    public class FlashColors {
    	public Color increaseHPColor;
    	public Color decreaseHPColor;
    	public Color increaseMPColor;
    	public Color increaseXPColor;
    	public Color increaseLVColor;
    }

    
    
}