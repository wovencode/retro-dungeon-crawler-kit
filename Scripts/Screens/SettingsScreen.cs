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
    // SETTINGS SCREEN
    // ===================================================================================
    public class SettingsScreen : MonoBehaviour {

        public Slider sfxSlider;
        public Slider bgmSlider;
        public Dropdown langDropdown;
        
         // ----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        private void OnEnable() {
        
            sfxSlider.value = Finder.settings.CurrentSettings.SFXVolume;
            bgmSlider.value = Finder.settings.CurrentSettings.BGMVolume;
            
            Finder.settings.CurrentSettings.PropertyChanged += CurrentSettings_PropertyChanged;
           	
           	Finder.audio.AdjustBGMVolume(Finder.settings.CurrentSettings.BGMVolume);
           	Finder.audio.AdjustSFXVolume(Finder.settings.CurrentSettings.SFXVolume);
           	
            langDropdown.options = Finder.txt.languages.Select(
            	x => new Dropdown.OptionData(x.name)
        		).ToList();
            
        }
        
        // ----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        private void OnDisable() {
        	if (Finder.settings != null)
            	Finder.settings.CurrentSettings.PropertyChanged -= CurrentSettings_PropertyChanged;
        }
        
        // ----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        private void CurrentSettings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            
            switch (e.PropertyName) {
                
                case "BGMVolume":
                    Finder.audio.AdjustBGMVolume(Finder.settings.CurrentSettings.BGMVolume);
                    break;
                case "SFXVolume":
                    Finder.audio.AdjustSFXVolume(Finder.settings.CurrentSettings.SFXVolume);
                    break;
                case "Language":
                	Finder.txt.adjustLanguage(langDropdown.value);
                	break;
               
            }
            
        }
        
        // ----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        private void Update()  {
        
            if (Finder.settings.CurrentSettings.SFXVolume != sfxSlider.value)
                Finder.settings.CurrentSettings.SFXVolume = sfxSlider.value;
                
            if (Finder.settings.CurrentSettings.BGMVolume != bgmSlider.value)
                Finder.settings.CurrentSettings.BGMVolume = bgmSlider.value;
           	
           	if (Finder.txt.language != langDropdown.value)
           		Finder.txt.adjustLanguage(langDropdown.value);
           	
        }
        
        // ----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        public void RestoreDefaults() {
        	
        	Finder.audio.AdjustBGMVolume(1.0f);
            Finder.audio.AdjustSFXVolume(0.5f);
            Finder.txt.adjustLanguage(0);
            
            sfxSlider.value = Finder.settings.CurrentSettings.SFXVolume;
            bgmSlider.value = Finder.settings.CurrentSettings.BGMVolume;
           	langDropdown.value = 0;
           	
        }
        
        // ----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        public void SaveChanges() {
            Finder.settings.Save();
        }
        
        // -----------------------------------------------------------------------------------
        
    }
    
}