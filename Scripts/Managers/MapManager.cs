// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using toinfiniityandbeyond.Tilemapping;
using WoCo.DungeonCrawler;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// MAP MANAGER
	// ===================================================================================
    public class MapManager : MonoBehaviour {
    
        public event Action FloorChanged;
        
		[HideInInspector]public LocationType locationType;
        [HideInInspector]public DungeonMap map;
		[HideInInspector]public TemplateMetaTown currentTownConfig;
		[HideInInspector]public TemplateMetaDungeon currentDungeonConfig;
		
		private List<Interactable> interactables;
       	private Vector3 startPos;
        private Vector3 startDir;
        
       	public Color mapFloorColor;
       	public MiniMapSprites minimapSprites;
        public Transform MapContainer;
        
		// -------------------------------------------------------------------------------
		// Start
		// -------------------------------------------------------------------------------
        void Start() {
            interactables = new List<Interactable>();
        }
        
  		// -------------------------------------------------------------------------------
		// MapName
		// -------------------------------------------------------------------------------
    	public string MapName {
    		get {
    			if (currentTownConfig != null && currentDungeonConfig == null) {
    				return currentTownConfig.name;
    			} else if (currentDungeonConfig != null) {
    				return currentDungeonConfig.name;
   	 			}
   	 			return "";
    		}
    	}
    	    
		// -------------------------------------------------------------------------------
		// UpdateInteractableDirection
		// -------------------------------------------------------------------------------
        public void UpdateInteractableDirection() {
        	if (locationType != LocationType.Dungeon) return;
        
            if (interactables != null && interactables.Count > 0) {
                Vector3 tmpForward;
                foreach (Interactable i in interactables) {
                	if (i.FacePlayer) {
                        tmpForward = (i.transform.position - Camera.main.transform.position).normalized;
                        Vector3 tmpDirection = new Vector3(tmpForward.x, 0, tmpForward.z);
                        if (tmpDirection != Vector3.zero)
                        	i.transform.forward = tmpDirection;
                    }
                }
            }
        }
        
		// -------------------------------------------------------------------------------
		// LoadFloor
		// -------------------------------------------------------------------------------
        public void LoadFloor() {
        
            interactables.Clear();
            
            if (currentDungeonConfig!=null)
                map = LoadDungeon(currentDungeonConfig);
                       
            if (map != null)  {
            
                LoadMapConfig();
                ClearMap();

                for (int x = 0; x < map.Size.x; x++) {
                    for (int y = 0; y < map.Size.y; y++) {
                    
                        ScriptableTile tile = map.GetTile(x, y);

                        if (tile is DungeonTileStart) {
                        	startPos = new Vector3(x, Finder.navi.baseHeightOffset, y);
                        	startDir = Vector3Helper.CardinalDirection(((DungeonTileStart)tile).facingDirection);
                        	CreateDungeonFloor(x, y, false, ((DungeonTileStart)tile).floorPrefab);
							CreateDungeonCeiling(x, y, ((DungeonTileStart)tile).ceilingPrefab);
							
                        } else if (tile is DungeonTileWall) {
                        	CreateDungeonFloor(x, y, true, ((DungeonTileWall)tile).floorPrefab);
                        	CreateDungeonWall(x, y, ((DungeonTileWall)tile));
                        	CreateDungeonCeiling(x, y, ((DungeonTileWall)tile).ceilingPrefab);
                        	
                        } else if (tile is DungeonTileFloor) {         
                        	CreateDungeonFloor(x, y, true, ((DungeonTileFloor)tile).floorPrefab, (DungeonTileFloor)tile);
                        	CreateDungeonCeiling(x, y, ((DungeonTileFloor)tile).ceilingPrefab, (DungeonTileFloor)tile);
                        	
                		} else if (tile is DungeonTileEvent) {    
                        	CreateDungeonObject(x, y, (DungeonTileEvent)tile);
                            CreateDungeonFloor(x, y, false, ((DungeonTileEvent)tile).floorPrefab);
                            CreateDungeonCeiling(x, y, ((DungeonTileEvent)tile).ceilingPrefab); 	
                		}
                      
                    }
                }
                
            } else {
            	throw new InvalidOperationException("tile map could not be loaded from map config!");
            }
        }
        
		// -------------------------------------------------------------------------------
		// LoadMapConfig
		// -------------------------------------------------------------------------------
        private void LoadMapConfig() {
            Finder.battle.setupEncounters(currentDungeonConfig.monsterLevel, currentDungeonConfig.encounterRate, currentDungeonConfig.monsterEncounters, currentDungeonConfig.battleBackground);
        }
        
		// ===============================================================================
		// DUNGEON OBJECT CREATION
		// ===============================================================================

		// -------------------------------------------------------------------------------
		// CreateDungeonFloor
		// -------------------------------------------------------------------------------
        private void CreateDungeonFloor(int x, int y, bool canStartFight, GameObject prefab, DungeonTileFloor tile=null) {
            if (prefab==null) return;
            
            GameObject go = GameObject.Instantiate(prefab) as GameObject;
    		go.name = string.Format("{0}_{1}", x, y);
            go.transform.position = new Vector3(x, Constants.BASEHEIGHT_FLOOR, y);
            go.transform.parent = MapContainer;
            
    		if (tile != null)
        		if (tile.facingDirection != DirectionType.None) go.transform.forward = Vector3Helper.CardinalDirection(tile.facingDirection);
        	
            if (tile != null && canStartFight) {
                if (tile.facingDirection != DirectionType.None) go.transform.forward = Vector3Helper.CardinalDirection(tile.facingDirection);
                go.AddComponent<OpenFloorCollisionHandler>();
            	OpenFloorCollisionHandler co = go.GetComponent<OpenFloorCollisionHandler>();
            	co.encounterRateModifier 	= tile.encounterRateModifier;
				co.monsterPoolID 			= tile.monsterPoolID;
				co.monsterAmountMin 		= tile.monsterAmountMin;
				co.monsterAmountMax 		= tile.monsterAmountMax;
				co.monsterAmountScale 		= tile.monsterAmountScale;
            }
            
        }
        
        // -------------------------------------------------------------------------------
		// CreateDungeonCeiling
		// -------------------------------------------------------------------------------
        private void CreateDungeonCeiling(int x, int y, GameObject prefab, DungeonTileFloor tile=null) {
            if (prefab==null) return;
            
            GameObject go = GameObject.Instantiate(prefab) as GameObject;
            go.name = string.Format("{0}_{1}", x, y);
            go.transform.position = new Vector3(x, Constants.BASEHEIGHT_CEILING, y);
            go.transform.parent = MapContainer;
            
           	if (tile != null)
           		if (tile.facingDirection != DirectionType.None) go.transform.forward = Vector3Helper.CardinalDirection(tile.facingDirection);
           
        }
        
		// -------------------------------------------------------------------------------
		// CreateDungeonWall
		// -------------------------------------------------------------------------------
        private void CreateDungeonWall(int x, int y, DungeonTileWall tile) {
            if (tile.wallPrefab==null) return;
            
            GameObject go = GameObject.Instantiate(tile.wallPrefab) as GameObject;
            go.transform.position = new Vector3(x, Constants.BASEHEIGHT_WALL, y);
            go.name = string.Format("{0}_{1}", x, y);
            go.transform.parent = MapContainer;
        
        	if (tile != null)
        		if (tile.facingDirection != DirectionType.None) go.transform.forward = Vector3Helper.CardinalDirection(tile.facingDirection);
        	
        }
        
		// -------------------------------------------------------------------------------
		// CreateDungeonObject
		// -------------------------------------------------------------------------------
        private void CreateDungeonObject(int x, int y, DungeonTileEvent tile) {
        	if (tile != null) {
        
				GameObject instance = (GameObject)GameObject.Instantiate(tile.objectPrefab);
				
				if (Vector3Helper.CardinalDirection(tile.facingDirection) != Vector3.zero)
					instance.transform.forward = Vector3Helper.CardinalDirection(tile.facingDirection);
					
				instance.transform.position = new Vector3(x, tile.offsetHeight, y);
				
				if (tile.facingDirection != DirectionType.None && !tile.interactFromBothSides)
					instance.transform.position -= instance.transform.forward/3;
				
				instance.transform.parent = MapContainer;
				instance.name = string.Format("{0}_{1}", x, y);
				interactables.Add(new Interactable { FacePlayer = tile.facePlayer, transform = instance.transform, ID = x.ToString() + y.ToString() });
				
				DungeonObjectEvent co = instance.GetComponent<DungeonObjectEvent>();
				
 				co.tile = tile;
 				
				co.Location = new Location { template = currentDungeonConfig, X = x, Y = y };
				
				if (Finder.party.InteractedObjects.Any(i => i.Location.Equals(co.Location)))
					co.SetInteraction(true);
				
				if (tile.StartsDeactivated || Finder.party.DeactivatedObjects.Any(i => i.Location.Equals(co.Location))) {
					co.Deactivate();
				} else {
					co.Activate();
				}
					
            }
            else
            {
            	Debug.LogErrorFormat("Error: No tile defined at position: {0}, {1}", x, y);
            }
            
        }      

   		// ===============================================================================
		// TELEPORT & MOVEMENT
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// TeleportPlayer
		// -------------------------------------------------------------------------------
		public void TeleportPlayer(Location location) {

			if (location == null || location.locationType == LocationType.None) return;
			
			if (location.template == null)
				location.template = location.targetMap;
			
            Finder.ui.DeactivateAll();
            
            // -- Teleport to Worldmap
            if (location.locationType == LocationType.Worldmap) {
            	WarpWorldmap();
            	return;
            }
            
            // -- Teleport to Town
			if (location.locationType == LocationType.Town) {
				WarpTown((TemplateMetaTown)location.template);
				return;
			}
			
			// -- Teleport to Outro
			if (location.locationType == LocationType.Outro) {
				WarpOutro();
				return;
			}
			
			// -- Teleport to Dungeon
			if (location.locationType == LocationType.Dungeon) {
           
           		WarpDungeon((TemplateMetaDungeon)location.template, false);

        		Finder.party.transform.position = new Vector3(location.X, Finder.navi.baseHeightOffset, location.Y);
            
            	if (Vector3Helper.CardinalDirection(location.facingDirection) != Vector3.zero)
            		Finder.party.transform.forward = Vector3Helper.CardinalDirection(location.facingDirection);
            	
            	Finder.navi.enabled = true;
            	
            }
            
        }
        
 		// -------------------------------------------------------------------------------
		// PrepareWarp
		// -------------------------------------------------------------------------------
        private void PrepareWarp() {
        	
        	Finder.ui.DeactivateAll();
        	Finder.navi.ResetFlags();
        	Finder.navi.enabled = false;
        	Finder.audio.StopBGM();
            Finder.audio.StopSFX();
        	Finder.fx.Fade(true);
        	ClearMap();
        	
        }
        
		// -------------------------------------------------------------------------------
		// WarpOutro
		// -------------------------------------------------------------------------------
        public void WarpOutro() {
        
            PrepareWarp();
            Finder.ui.OverrideState(UIState.EndingScene);
            Finder.audio.PlayBGM(Finder.audio.musicOutro);
            locationType = LocationType.Outro;

        }
        
		// -------------------------------------------------------------------------------
		// WarpWorldmap
		// -------------------------------------------------------------------------------
        public void WarpWorldmap() {
        
        	PrepareWarp();
            Finder.ui.OverrideState(UIState.Worldmap);
            Finder.audio.PlayBGM(Finder.audio.musicWorldmap);
            Finder.log.Hide();
           	locationType = LocationType.Worldmap;
            
        }
        
		// -------------------------------------------------------------------------------
		// WarpTown
		// -------------------------------------------------------------------------------
        public void WarpTown(TemplateMetaTown town) {
        	
        	if (town==null)
        		throw new ArgumentException("WarpTown failed: Template is empty!");

            PrepareWarp();
            Finder.party.AddTown(town);
            Finder.party.lastTown = town;
            currentTownConfig = town;
            Finder.ui.OverrideState(UIState.Town);
            Finder.audio.PlayBGM(town.music);
            Finder.log.Hide();
            locationType = LocationType.Town;
            
        }

		// -------------------------------------------------------------------------------
		// WarpDungeon
		// -------------------------------------------------------------------------------
        public void WarpDungeon(TemplateMetaDungeon map, bool start=true) {
        
        	if (map==null)
				throw new ArgumentException("WarpDungeon failed: Template is empty!");
			
			PrepareWarp();
			Finder.ui.OverrideState(UIState.Dungeon);
			currentDungeonConfig = map;
			LoadFloor();
			SetPlayerPosition(start);
			Finder.audio.PlayBGM(map.music);
			OnFloorChanged();
			Finder.log.Hide();
			locationType = LocationType.Dungeon;

        }
        
		// -------------------------------------------------------------------------------
		// SetPlayerPosition
		// -------------------------------------------------------------------------------
        public void SetPlayerPosition(bool start=true) {
        
        	if (start) {
            	Camera.main.transform.position = startPos;
            	
            	if (startDir != Vector3.zero)
            		Camera.main.transform.forward = startDir;
            }

            UpdateInteractableDirection();
            Finder.party.MapExplorationInfo.AddExploredSpace(MapName, new Vector2(Finder.party.transform.position.x, Finder.party.transform.position.z));
            
            Finder.navi.ResetFlags();
            Finder.navi.enabled = true;
            
        }
        
     	// ===============================================================================
		// MAP RELATED
		// ===============================================================================
        
		// -------------------------------------------------------------------------------
		// LoadDungeon
		// -------------------------------------------------------------------------------
        public DungeonMap LoadDungeon(TemplateMetaDungeon mapConfig) {
        
            DungeonMap map = null;
            
            if (mapConfig==null)
                throw new ArgumentException("No map configuration available!");
			
			if (mapConfig.mapFile==null)
				throw new ArgumentException("No map file available in map configuration: "+mapConfig);
			
			map = new DungeonMap(mapConfig.mapFile.height, mapConfig.mapFile.width);

            for (int x = 0; x < mapConfig.mapFile.width; x++) {
                for (int y = 0; y < mapConfig.mapFile.height; y++) {
                    ScriptableTile tile = mapConfig.mapFile.map[x + y * mapConfig.mapFile.width];
                    map.SetTile(x, y, tile);
                }
			}
			
			if (map == null)
				throw new ArgumentException("Loaded map is empty!");

            return map;
        }       

  		// -------------------------------------------------------------------------------
		// ClearMap
		// -------------------------------------------------------------------------------
        public void ClearMap() {
            if (MapContainer != null && MapContainer.childCount > 0) {
                for (int i = MapContainer.childCount - 1; i >= 0; i--)
                    GameObject.Destroy(MapContainer.GetChild(i).gameObject);
            }
            interactables.Clear();
        }
      
		// -------------------------------------------------------------------------------
		// OnFloorChanged
		// -------------------------------------------------------------------------------
        private void OnFloorChanged() {
            Action handler = FloorChanged;
            if (handler != null) handler();
        }
        
		// -------------------------------------------------------------------------------
		// HideEvents
		// -------------------------------------------------------------------------------
        public void HideEvents() {
            interactables.ForEach(x => x.Hide());
        }
        
		// -------------------------------------------------------------------------------
		// ShowEvents
		// -------------------------------------------------------------------------------
        public void ShowEvents() {
            interactables.ForEach(x => x.Show());
        }
        
		// -------------------------------------------------------------------------------
		// RemoveInteractables
		// -------------------------------------------------------------------------------
        private void RemoveInteractables(string id) {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentException("ID");

            interactables.RemoveAll(x => x.ID == id);
        }
        
        // -------------------------------------------------------------------------------
		// getCurrentMap
		// -------------------------------------------------------------------------------
        public TemplateMetaBase getCurrentMap {
        	get {
        		if (locationType == LocationType.Town) {
        			return currentTownConfig;
        		} else if (locationType == LocationType.Dungeon) {
        			return currentDungeonConfig;
        		}
        		return null;
        	}
        }
        
        // -------------------------------------------------------------------------------
		// getCurrentMap
		// -------------------------------------------------------------------------------
        public string getCurrentMapName {
        	get {
        		if (locationType == LocationType.Town) {
        			return currentTownConfig.name;
        		} else if (locationType == LocationType.Dungeon) {
        			return currentDungeonConfig.name;
        		} else if (locationType == LocationType.Worldmap) {
        			return Finder.txt.namesVocabulary.worldmap;
        		}
        		return "";
        	}
        }
        
        // ===============================================================================
		// TINY CLASSES (ONLY USED BY MAP MANAGER)
		// ===============================================================================
		
		// -------------------------------------------------------------------------------
		// Interactable
		// -------------------------------------------------------------------------------
        protected class Interactable {
        
        	public bool FacePlayer 		{ get; set; }
            public Transform transform 	{ get; set; }
            public string ID 			{ get; set; }
            
            public void Hide() {
            	DungeonObjectEvent e = transform.GetComponent<DungeonObjectEvent>();
            	if (e != null)
            		e.Hide();
            }
            
            public void Show() {
            	DungeonObjectEvent e = transform.GetComponent<DungeonObjectEvent>();
            	if (e != null)
            		e.Show();
            }
            
        }
        
        // -------------------------------------------------------------------------------
		// MiniMapSprites
		// -------------------------------------------------------------------------------
        [Serializable]
        public class MiniMapSprites {
        	public Texture2D playerNorth;
        	public Texture2D playerEast;
        	public Texture2D playerSouth;
        	public Texture2D playerWest;
        }
        	
        // -------------------------------------------------------------------------------
        
    }
}