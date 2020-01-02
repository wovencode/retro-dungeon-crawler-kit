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

    // -------------------------------------------------------------------------------
	// CutsceneScreen
	// -------------------------------------------------------------------------------
    public class CutsceneScreen : MonoBehaviour {
    	
    	public Text txtDisplay;
        public Image foreground;
        public Image background;
        
        public CutsceneType sceneType { get; set; }
        
		private int index;
		private int sceneindex;
		private TemplateMetaCutscene cutscene;
        
        // -------------------------------------------------------------------------------
		// OnEnable
		// -------------------------------------------------------------------------------
        public void OnEnable() {
        	index = 0;
        	sceneindex = 0;
        	
        	if (sceneType == CutsceneType.Outro) {
        		cutscene = Finder.txt.outroScene;
        	} else if (sceneType == CutsceneType.Intro) {
        		cutscene = Finder.txt.introScene;
        	} else if (sceneType == CutsceneType.Credits) {
        		cutscene = Finder.txt.creditsScene;
        	}
        	UpdateScene();
        	
        }
        
        // -------------------------------------------------------------------------------
		// UpdateScene
		// -------------------------------------------------------------------------------
        protected void UpdateScene() {
        
        	txtDisplay.GetComponent<FadeText>().OnFade();
        	txtDisplay.text 	= cutscene.cutsceneContent[sceneindex].text[index].text;
        	
        	if (foreground != null && cutscene.cutsceneContent[sceneindex].foreground != null) {
        		foreground.sprite 	= cutscene.cutsceneContent[sceneindex].foreground;
        		foreground.gameObject.SetActive(true);
        	} else {
        		foreground.gameObject.SetActive(false);
        	}
        	
        	if (background != null && cutscene.cutsceneContent[sceneindex].background != null) {
        		background.sprite 	= cutscene.cutsceneContent[sceneindex].background;
        		background.gameObject.SetActive(true);
        	} else {
        		background.gameObject.SetActive(false);
        	}
        
        }
        
        // -------------------------------------------------------------------------------
		// OnNextPressed
		// -------------------------------------------------------------------------------
        public void OnNextPressed() {
            index++;
            if (index < cutscene.cutsceneContent[sceneindex].text.Length) {
                UpdateScene();
            } else {
            	sceneindex++;
            	if (sceneindex < cutscene.cutsceneContent.Length) {
            		index = 0;
            		UpdateScene();
            	} else {
            
					OnSkipPressed();
                }
            }
        }
        
        // -------------------------------------------------------------------------------
		// OnSkipPressed
		// -------------------------------------------------------------------------------
        public void OnSkipPressed() {
        
        	if (sceneType == CutsceneType.Intro) {
				Finder.map.WarpWorldmap();
			} else if (sceneType == CutsceneType.Outro) {
				Finder.ui.PushState(UIState.Credits);
			} else {
				Finder.ui.PushState(UIState.Title);
			}
					
			gameObject.SetActive(false);
        
        }
        
        // -------------------------------------------------------------------------------
          
    }
}