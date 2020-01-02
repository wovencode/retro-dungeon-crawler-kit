// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

    // ===================================================================================
    // SETTINGS
    // ===================================================================================
    [Serializable]
    public class Settings : INotifyPropertyChanged {
        
        private float bgmVolume;
        private float sfxVolume;
        private int language;
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        // ----------------------------------------------------------------------------------
		// Language
		// -----------------------------------------------------------------------------------
        public int Language {
            get { return language; }
            set { 
            	if (language != value) { language = value; OnPropertyChanged("Language"); }
            	}
        }
        
		// ----------------------------------------------------------------------------------
		// BGMVolume
		// -----------------------------------------------------------------------------------
        public float BGMVolume {
            get { return bgmVolume; }
            set {
            	if (bgmVolume != value) { bgmVolume = value; OnPropertyChanged("BGMVolume"); }
            	}
        }
        
		// ----------------------------------------------------------------------------------
		// SFXVolume
		// -----------------------------------------------------------------------------------
        public float SFXVolume {
            get { return sfxVolume; }
            set {
            	if (sfxVolume != value) { sfxVolume = value; OnPropertyChanged("SFXVolume"); }
            	}
        }
        
		// ----------------------------------------------------------------------------------
		// OnPropertyChanged
		// -----------------------------------------------------------------------------------
        protected void OnPropertyChanged(string name) {
            PropertyChangedEventHandler h = PropertyChanged;
            if (h != null)
                h(this, new PropertyChangedEventArgs(name));
        }
    }
	
	// ===================================================================================
	
}