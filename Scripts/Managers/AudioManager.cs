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
	// AudioManager
	// ===================================================================================
    public class AudioManager : MonoBehaviour {
    
        private static SFX[] restrictOverlap = new SFX[] { SFX.WallBump };
        public AudioSource BGMSource;
        public List<AudioSource> SFXSources;
        
        [Header("--- Global Background Music ---")]
        public AudioClip musicWorldmap;
        public AudioClip musicBattleNormal;
        public AudioClip musicBattleMiniBoss;
        public AudioClip musicBattleBoss;
        public AudioClip musicBattleEndBoss;
        public AudioClip musicIntro;
        public AudioClip musicOutro;
		
		[Header("--- Battle Jingles ---")]
        public AudioClip jingleVictory;
        public AudioClip JingleVictoryBoss;
        public AudioClip runAway;
        public AudioClip gameOver;
		
        [Header("--- Item SFX ---")]
        public AudioClip Equip;
        public AudioClip Unequip; 
        public AudioClip Purchase;
        public AudioClip Sell;
                          
        [Header("--- Navigation SFX ---")]
        public AudioClip WallBump;
        public AudioClip footStep;
        
        [Header("--- UI SFX ---")]
        public AudioClip buttonClick;
        public AudioClip ButtonCancel;

        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------		
        private void Start() {
        
            if (Finder.settings != null && Finder.settings.CurrentSettings != null) {
                CalibrateSourcesFromSettings();
            } else if (Finder.settings != null) {
                Finder.settings.Loaded += settings_Loaded;
            }
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------		
        void settings_Loaded(object sender, EventArgs e) {
            CalibrateSourcesFromSettings();
            Finder.settings.Loaded -= settings_Loaded;
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------		
        private void CalibrateSourcesFromSettings() {
            BGMSource.volume = Finder.settings.CurrentSettings.BGMVolume;
            BGMSource.spatialBlend = 0;
            foreach (AudioSource source in SFXSources) {
                source.spatialBlend = 0;
                source.volume = Finder.settings.CurrentSettings.SFXVolume;
            }
        }

		// -------------------------------------------------------------------------------
		// PlayBGM (AudioClip)
		// -------------------------------------------------------------------------------		
		public void PlayBGM(AudioClip bgm) {
			if (bgm != null) {
		 		BGMSource.clip = bgm;
        		BGMSource.Play();
        	}
		}
		
		// -------------------------------------------------------------------------------
		// PlayBGM (EncounterType)
		// -------------------------------------------------------------------------------		
        public void PlayBGM(EncounterType encounterType) {
            switch (encounterType) {
                case EncounterType.Normal:
                    BGMSource.clip = musicBattleNormal;
                    BGMSource.Play();
                    break;
                case EncounterType.Miniboss:
                    BGMSource.clip = musicBattleNormal;
                    BGMSource.Play();
                    break;
                case EncounterType.Boss:
                    BGMSource.clip = musicBattleNormal;
                    BGMSource.Play();
                    break;
                case EncounterType.Endboss:
                    BGMSource.clip = musicBattleNormal;
                    BGMSource.Play();
                    break;
                default:
                    break;
            }
        }
        
		// -------------------------------------------------------------------------------
		// PlaySFX (AudioClip)
		// -------------------------------------------------------------------------------		
        public void PlaySFX(AudioClip sfx) {
        	if (sfx != null) {
				AudioSource source = SFXSources.FirstOrDefault(x => !x.isPlaying && !x.loop);
				if (source != null) {
					source.clip = sfx;
					source.Play();
				}
            }
        }
        
		// -------------------------------------------------------------------------------
		// PlaySFX (SFX)
		// -------------------------------------------------------------------------------		
 		public void PlaySFX(SFX sfx) {
        	
			AudioClip targetClip = GetClip(sfx);
			if (targetClip != null) {
			
				if (restrictOverlap.Contains(sfx)) {
					if (SFXSources.Any(x => x.isPlaying && x.clip == targetClip))
						return;
				}

				AudioSource source = SFXSources.FirstOrDefault(x => !x.isPlaying);
				if (source != null) {
					source.clip = targetClip;
					source.Play();
				}
		
			}
            
        }

		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------		
        public void PlayButtonClick() {
            PlaySFX(SFX.ButtonClick);
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------		
        private AudioClip GetClip(SFX sfx) {
        
            switch (sfx) {
            
                case SFX.Equip:
                    return Equip;
               	case SFX.Unequip:
                    return Unequip;     
                case SFX.WallBump:
                    return WallBump;
                case SFX.GameOver:
                    return gameOver;
                case SFX.Footstep:
                    return footStep;
                case SFX.RunAway:
                    return runAway;
                 case SFX.Purchase:
                    return Purchase;
                case SFX.Sell:
                    return Sell;
                case SFX.ButtonClick:
                    return buttonClick;
                case SFX.ButtonCancel:
                    return ButtonCancel;
                case SFX.JingleVictory:
                	return jingleVictory;
                case SFX.JingleVictoryBoss:
                	return JingleVictoryBoss;
                default:
                    break;

            }
			
            Debug.Log("Error: No clip available for: " + sfx.ToString());
        
        	return null;
        	
        }
        
		// -------------------------------------------------------------------------------
		// AdjustBGMVolume
		// -------------------------------------------------------------------------------		
        public void AdjustBGMVolume(float volume) {
        	BGMSource.volume = volume;
        }
        
   		// -------------------------------------------------------------------------------
		// AdjustSFXVolume
		// -------------------------------------------------------------------------------		
        public void AdjustSFXVolume(float volume) {
        	foreach (AudioSource sfxSource in SFXSources)
                sfxSource.volume = volume;
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------		
        public void StopBGM() {
            BGMSource.Stop();
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------		
        public void StopSFX() {
            SFXSources.ForEach(x => x.Stop());
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------		
        public void StopAll() {
            StopBGM();
            StopSFX();
        }
        
        // -------------------------------------------------------------------------------		
        
    }

}