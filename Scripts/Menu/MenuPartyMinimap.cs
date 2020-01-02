using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// 
	// ===================================================================================
    public class MenuPartyMinimap : MonoBehaviour {
        
        public Image mapImage;
        public MapRenderer mapRenderer;
        public Text txtDisplayName;
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private void OnEnable() {
            if (mapRenderer == null) {
                mapRenderer = new MapRenderer();
                float scaledHeight = mapImage.rectTransform.GetHeight();
            
            mapRenderer.Scale(scaledHeight, Finder.map.map);
            }
            DrawMap();
            Finder.navi.PlayerMoved += playerNav_PlayerMoved;
            Finder.navi.PlayerTurned += playerNav_PlayerTurned;
            Finder.map.FloorChanged += mapManager_FloorChanged;
        }
        
        // -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        void mapManager_FloorChanged() {
            if (gameObject.activeInHierarchy)
                DrawMap();
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        void playerNav_PlayerTurned(Vector3 obj) {
            if (gameObject.activeInHierarchy)
                DrawMap();
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        void playerNav_PlayerMoved(Vector3 obj) {
            if (gameObject.activeInHierarchy)
                DrawMap();
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private void OnDisable() {
        	if (Finder.navi != null) {
            	Finder.navi.PlayerMoved -= playerNav_PlayerMoved;
            	Finder.navi.PlayerTurned -= playerNav_PlayerTurned;
            	if (Finder.map != null)
            		Finder.map.FloorChanged -= mapManager_FloorChanged;
            }
        }
        
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private void DrawMap() {
            txtDisplayName.text = Finder.map.MapName;
            mapImage.preserveAspect = true;
            mapImage.sprite = Sprite.Create(mapRenderer.Texture, new Rect(0, 0, mapRenderer.Texture.width, mapRenderer.Texture.height), Vector2.zero);
            StartCoroutine(UpdateMap());
        }
		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private IEnumerator UpdateMap() {
        
            yield return new WaitForEndOfFrame();
            float scaledHeight = mapImage.rectTransform.GetHeight();
            
            mapRenderer.Scale(scaledHeight, Finder.map.map);
            mapRenderer.DrawFloor(Finder.party.MapExplorationInfo);
            
            if (Vector3Helper.CardinalDirection(Finder.party.transform.forward) == DirectionType.North) {
            	mapRenderer.DrawOverlay(Finder.party.transform, Finder.map.minimapSprites.playerNorth);
			} else if (Vector3Helper.CardinalDirection(Finder.party.transform.forward) == DirectionType.East) {
				mapRenderer.DrawOverlay(Finder.party.transform, Finder.map.minimapSprites.playerEast);
			} else if (Vector3Helper.CardinalDirection(Finder.party.transform.forward) == DirectionType.South) {
				mapRenderer.DrawOverlay(Finder.party.transform, Finder.map.minimapSprites.playerSouth);
			} else if (Vector3Helper.CardinalDirection(Finder.party.transform.forward) == DirectionType.West) {
				mapRenderer.DrawOverlay(Finder.party.transform, Finder.map.minimapSprites.playerWest);
			}

			foreach (MapPing ping in Finder.party.MapExplorationInfo.ExploredAreas[Finder.map.MapName]) {
				ping.loadTemplate();
				if (ping.template != null && ping.template.minimapIcon != null)
					mapRenderer.DrawOverlay(ping.Position, ping.template.minimapIcon);
			}
			
            mapImage.sprite = Sprite.Create(mapRenderer.Texture, new Rect(0, 0, mapRenderer.Texture.width, mapRenderer.Texture.height), Vector2.zero);
        }
		
		// -------------------------------------------------------------------------------
        
    }
}