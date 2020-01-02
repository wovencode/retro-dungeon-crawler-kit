// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WoCo.DungeonCrawler {

    // ===================================================================================
    // SETTINGS MANAGER
    // ===================================================================================
    public class SettingsManager : MonoBehaviour {
    
        private string filePath;
        public Settings CurrentSettings { get; set; }
        public event EventHandler Loaded;
        
        // ----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        private void Start() {
            if (CurrentSettings == null) {
                CreateDefaultSettings();
                Save();
            }
            OnLoaded();
        }
        
        // ----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        protected void OnLoaded() {
            EventHandler temp = Loaded;
            if (temp != null)
                temp(this, null);
        }
        
        // ----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        private void CreateDefaultSettings() {
            CurrentSettings = new Settings();
            CurrentSettings.BGMVolume = 0.6f;
            CurrentSettings.SFXVolume = 1.0f;
        }
        
        // ----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        public void Save() {
            PlayerPrefs.SetFloat("BGMVolume", CurrentSettings.BGMVolume);
            PlayerPrefs.SetFloat("SFXVolume", CurrentSettings.SFXVolume);
                    
        }
        
        // ----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        private Settings Load() {
           	CurrentSettings = new Settings();
            CurrentSettings.BGMVolume = PlayerPrefs.GetFloat("BGMVolume");
            CurrentSettings.SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
            return CurrentSettings;
        }
        
        // ----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        public void LoadSavedSettings() {
        
            Settings settings = null;
            settings = Load();
            
            if (settings != null) {
                CopySettingsValues(settings);
            }
        }
        
        // ----------------------------------------------------------------------------------
		// 
		// -----------------------------------------------------------------------------------
        private void CopySettingsValues(Settings settings) {
            
            if (CurrentSettings == null) CurrentSettings = new Settings();

            CurrentSettings.BGMVolume = settings.BGMVolume;
            CurrentSettings.SFXVolume = settings.SFXVolume;
            
        }
    }

    

}