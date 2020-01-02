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
	// FLASH
	// ===================================================================================
    public class Flash : MonoBehaviour {
    	
    	[Range(0.1f, 10f)]public float flashDuration = 0.5f;
        public Text flashText;
		public Image flashImage;
		
		protected Sprite originalImage { get; set; }
		protected Color originalColor;
		protected bool isFlashing;
		protected bool startFade;
		
		// -------------------------------------------------------------------------------
		// Start
		// -------------------------------------------------------------------------------    
		void Start() {
			//originalImage = flashImage.sprite;
			originalColor = flashImage.color;
		}
		
		// -------------------------------------------------------------------------------
		// OnFlash
		// -------------------------------------------------------------------------------    
		public void OnFlash(Color color, bool startFaded=false, int value=0) {
			
			if (!isFlashing) {
				
				isFlashing = true;
				startFade = startFaded;
				
				float duration = flashDuration;
				flashImage.color = color;
				originalImage = flashImage.sprite;
				originalColor = flashImage.color;
				
				if (flashText != null && value != 0) {
					if (value == Mathf.Abs(value)) {
						flashText.text = "+" + value.ToString();
					} else {
						flashText.text = value.ToString();
					}
					flashText.gameObject.SetActive(true);
				}
			
				if (startFade) {
					flashImage.canvasRenderer.SetAlpha(1f);
				} else {
					flashImage.CrossFadeAlpha(1, flashDuration, false);
					duration *= 3;
				}
				
				if (startFade) {
					Invoke("FlashIn", flashDuration * duration);
				} else {
					Invoke("NormalizeFlash", flashDuration);
				}
			
			}
		}

        // -------------------------------------------------------------------------------
		// FlashIn
		// -------------------------------------------------------------------------------    
		protected void FlashIn() {
			flashImage.CrossFadeAlpha(0, flashDuration, false);
			Invoke("NormalizeFlash", flashDuration);
		}
		
        // -------------------------------------------------------------------------------
		// NormalizeFlash
		// -------------------------------------------------------------------------------    
		protected void NormalizeFlash() {
		
			if (isFlashing) {
			
				if (flashText != null) {
					flashText.text = "";
					flashText.gameObject.SetActive(false);
				}
				
				if (startFade) {
					flashImage.canvasRenderer.SetAlpha(0f);
				} else {
					flashImage.canvasRenderer.SetAlpha(1f);
				}
				
				if (originalImage != null) flashImage.sprite = originalImage;
				flashImage.color = Color.white; // originalColor;
				isFlashing = false;
				
			}
			
		}
        
        // -------------------------------------------------------------------------------
        
    }
}