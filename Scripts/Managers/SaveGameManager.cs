// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using System.Security.Cryptography;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// SAVE GAME MANAGER
	// ===================================================================================
    public class SaveGameManager : MonoBehaviour {
    	
    	[Header("-=- Saving -=-")]
    	public SavegameSettings savegameSettings;
    	
    	protected string filePathBase;
    	public DateTime StartDate 		{ get; set; }
        public DateTime SaveDate 		{ get; set; }
        public int LastLoadedIndex 		{ get; set; }
        public bool GameInProgress		{ get; set; }
        
        private const string cryptoKey = "Q3JpcHRvZ3JhZmlhcyBjb20gUmluamRhZWwgLyBBRVM=";
        private const int keySize = 256;
        private const int ivSize = 16;

		// -------------------------------------------------------------------------------
		// Start
		// -------------------------------------------------------------------------------
        public void Start() {
            LastLoadedIndex = -1;
            GameInProgress = false;
            filePathBase = Path.Combine(Application.persistentDataPath, "save{0}.dat");
        }
        
        // ==================== CREATE / RESTORE SAVESTATE FUNCTIONS =====================
        
		// -------------------------------------------------------------------------------
		// Save
		// -------------------------------------------------------------------------------
        public void SaveState(int index) {
        
            Savegame state = new Savegame();
            
            state.SaveDate 			= DateTime.Now;
            state.StartDate 		= StartDate;
            state.Index				= index;
            
      		// -------------------------------------------------------------------------------
			// Save: Current Location
			// -------------------------------------------------------------------------------
 
			state.mapName			= Finder.map.getCurrentMapName;
			state.locationType		= Finder.map.locationType;
			state.facingDirection	= Finder.navi.facingDirection;
     		state.X					= Finder.party.transform.position.x;
     		state.Y					= Finder.party.transform.position.z;					// z = Y
     		
     		// -------------------------------------------------------------------------------
			// Save: Last visited Town
			// -------------------------------------------------------------------------------
             if (Finder.party.lastTown != null)
           		state.lastTown				= Finder.party.lastTown.name;						

     		// -------------------------------------------------------------------------------
			// Save: Characters in Party
			// -------------------------------------------------------------------------------
			state.characters.Clear();
			
			foreach (CharacterBase character in Finder.party.characters) {
				state.characters.Add(character.SaveToSavegame());
			}

			// -------------------------------------------------------------------------------
			// Save: Party Equipment
			// -------------------------------------------------------------------------------
			state.equipment 			= Finder.party.equipment;
			
			// -------------------------------------------------------------------------------
			// Save: Party Inventory
			// -------------------------------------------------------------------------------
			state.inventory 			= Finder.party.inventory;
			
			// -------------------------------------------------------------------------------
			// Save: Interacted & Deactivated Objects
			// -------------------------------------------------------------------------------
			state.InteractedObjects		= Finder.party.InteractedObjects;
            state.DeactivatedObjects	= Finder.party.DeactivatedObjects;
            
 			// -------------------------------------------------------------------------------
			// Save: Exploration Data
			// -------------------------------------------------------------------------------
            state.MapExplorationInfo	= Finder.party.MapExplorationInfo;
            state.TownExplorationInfo	= Finder.party.TownExplorationInfo;
            
			// -------------------------------------------------------------------------------
			// Save: Currencies
			// -------------------------------------------------------------------------------
            state.gold 			= Finder.party.currencies.gold;
          
            SaveFile(index, state);
            LastLoadedIndex = index;
        }

		
		// -------------------------------------------------------------------------------
		// LoadState
		// -------------------------------------------------------------------------------
        public void LoadState(int index) {
        
            Savegame state = Load(index);
            
            if (state != null) {
            
				// -------------------------------------------------------------------------------
				// Restore: Currencies
				// -------------------------------------------------------------------------------
				Finder.party.currencies.gold 			= state.gold;
               
 				// -------------------------------------------------------------------------------
				// Restore: Interacted & Deactivated Objects
				// -------------------------------------------------------------------------------
                Finder.party.InteractedObjects		= state.InteractedObjects;
               	Finder.party.DeactivatedObjects		= state.DeactivatedObjects;
               	
  				// -------------------------------------------------------------------------------
				// Restore: Exploration Data
				// -------------------------------------------------------------------------------
               	Finder.party.MapExplorationInfo		= state.MapExplorationInfo;
               	Finder.party.TownExplorationInfo	= state.TownExplorationInfo;
                
				// -------------------------------------------------------------------------------
				// Restore: Party Equipment
				// -------------------------------------------------------------------------------
				Finder.party.equipment 				= state.equipment;
				Finder.party.equipment.LoadTemplates();
			
				// -------------------------------------------------------------------------------
				// Restore: Party Inventory
				// -------------------------------------------------------------------------------
 				Finder.party.inventory 				= state.inventory;
 				Finder.party.inventory.LoadTemplates();
 
                // -------------------------------------------------------------------------------
				// Restore: Characters in Party
				// -------------------------------------------------------------------------------
				Finder.party.characters.Clear();
				foreach (SavegameCharacter character in state.characters) {
					CharacterHero hero = new CharacterHero();
					hero.LoadFromSavegame(character);
					Finder.party.characters.Add(hero);
				}

                // -------------------------------------------------------------------------------
				// Restore: Last visited Town
				// -------------------------------------------------------------------------------
				if (!string.IsNullOrEmpty(state.lastTown))
					Finder.party.lastTown				= DictionaryTown.Get(state.lastTown);
                
                // -------------------------------------------------------------------------------
				// Restore: Current Location
				// -------------------------------------------------------------------------------
               	Location restoredLocation = new Location
               		{
               			locationType 	= state.locationType,
               			name	 		= state.mapName,
               			facingDirection = state.facingDirection,
               			X 				= state.X,
               			Y 				= state.Y
               		};
               		
				Finder.battle.Reset();
                Finder.ui.DeactivateAll();
                Finder.audio.StopBGM();
                Finder.audio.StopSFX();
                
                SaveDate = state.SaveDate;
                StartDate = state.StartDate;
                LastLoadedIndex = index;

                Finder.map.TeleportPlayer(restoredLocation);
                
                GameInProgress = true;
                
            }
        }

		// ============================== VARIOUS FUNCTIONS ==============================
		
 		// -------------------------------------------------------------------------------
		// SaveAsNew
		// -------------------------------------------------------------------------------
       	public void SaveAsNew() {
            int index = 0;
            List<Savegame> saves = GetAllSavegames();
            if (saves != null && saves.Count > 0)
                index = saves.Max(x => x.Index) + 1;

            SaveState(index);
        }
        
		// -------------------------------------------------------------------------------
		// LoadLastSave
		// -------------------------------------------------------------------------------
        public void LoadLastSave() {
            LoadState(LastLoadedIndex);
        }
    
 		// -------------------------------------------------------------------------------
		// Load
		// -------------------------------------------------------------------------------
        public Savegame Load(int index) {
            string targetFile = string.Format(filePathBase, index);
            return LoadFile(targetFile);
        }
        
 		// -------------------------------------------------------------------------------
		// GetAllSaves
		// -------------------------------------------------------------------------------
        public List<Savegame> GetAllSavegames() {
            List<Savegame> result = new List<Savegame>();
            string[] files = Directory.GetFiles(Application.persistentDataPath, "save*", SearchOption.TopDirectoryOnly);
            if (files != null && files.Length > 0) {
            	foreach (string file in files)
                	result.Add(LoadFile(file));
			}
            return result;
        }
        
 		// -------------------------------------------------------------------------------
		// HasSaveFiles
		// -------------------------------------------------------------------------------
        public bool HasSaveFiles() {
            string[] files = Directory.GetFiles(Application.persistentDataPath, "save*", SearchOption.TopDirectoryOnly);
            return files != null && files.Length > 0;
        }
        
        // ============================ SAVE & LOAD FUNCTIONS ============================
        
        // -------------------------------------------------------------------------------
		// SaveFile
		// -------------------------------------------------------------------------------
        public void SaveFile(int index, Savegame state) {
            
            byte[] key = Convert.FromBase64String(cryptoKey);
            string targetFile = string.Format(filePathBase, index);
			using (FileStream file = new FileStream(targetFile, FileMode.Create, FileAccess.Write))
			using (CryptoStream cryptoStream = CreateEncryptionStream(key, file)) {
    			WriteObjectToStream(cryptoStream, state);
			}
            
            /*
            in case encryption fails, you can use the unencrypted savegame code here:
            
            state.Index = index;
            using (FileStream fs = new FileStream(targetFile, FileMode.Create, FileAccess.Write)) {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, state);
                fs.Close();
            }
            */
        }
        
 		// -------------------------------------------------------------------------------
		// LoadFile
		// -------------------------------------------------------------------------------
        private Savegame LoadFile(string fileName) {
            
            byte[] key = Convert.FromBase64String(cryptoKey);
            
			using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			using (CryptoStream cryptoStream = CreateDecryptionStream(key, file)) {
    			return (Savegame)ReadObjectFromStream(cryptoStream);
			}
            
            /*
            in case encryption fails, you can use the unencrypted savegame code here:
            
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {
                BinaryFormatter bf = new BinaryFormatter();
                return bf.Deserialize(fs) as Savegame;
            }
            */
        }
        
        // ==============================  METHODS =======================================
        
     	// -------------------------------------------------------------------------------
		// WriteObjectToStream
		// -------------------------------------------------------------------------------
        public void WriteObjectToStream(Stream outputStream, object obj) {
			if (object.ReferenceEquals(null, obj)) return;
    
    		BinaryFormatter bf = new BinaryFormatter();
    		bf.Serialize(outputStream, obj);
		}

    	// -------------------------------------------------------------------------------
		// ReadObjectFromStream
		// -------------------------------------------------------------------------------
		public object ReadObjectFromStream(Stream inputStream) {
			BinaryFormatter binForm = new BinaryFormatter();
    		object obj = binForm.Deserialize(inputStream);
    		return obj;
		}
        
        // ========================= ENCRYPTION METHODS ==================================
        
     	// -------------------------------------------------------------------------------
		// CreateEncryptionStream
		// -------------------------------------------------------------------------------
        public CryptoStream CreateEncryptionStream(byte[] key, Stream outputStream) {
			
			byte[] iv = new byte[ivSize];

	   		var rng = new RNGCryptoServiceProvider();
			rng.GetNonZeroBytes(iv);

			outputStream.Write(iv, 0, iv.Length);

			Rijndael rijndael = new RijndaelManaged();
			rijndael.KeySize = keySize;

			CryptoStream encryptor = new CryptoStream(
				outputStream,
				rijndael.CreateEncryptor(key, iv),
				CryptoStreamMode.Write);
				
			return encryptor;
			
		}

		// -------------------------------------------------------------------------------
		// CreateDecryptionStream
		// -------------------------------------------------------------------------------
		public CryptoStream CreateDecryptionStream(byte[] key, Stream inputStream) {
			
			byte[] iv = new byte[ivSize];

			if (inputStream.Read(iv, 0, iv.Length) != iv.Length)
				throw new ApplicationException("Failed to read IV from stream.");
		
			Rijndael rijndael = new RijndaelManaged();
			rijndael.KeySize = keySize;

			CryptoStream decryptor = new CryptoStream(
				inputStream,
				rijndael.CreateDecryptor(key, iv),
				CryptoStreamMode.Read);
				
			return decryptor;
			
		}
        
        // -------------------------------------------------------------------------------
        
    }
}