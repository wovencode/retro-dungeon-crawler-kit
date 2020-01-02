// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// MAP RENDERER
	// ===================================================================================
    public class MapRenderer {
    
        public Texture2D Texture { get; set; }
        public int LocalScale { get; private set; }
        public Vector2 Size { get; set; }
		public DungeonMap Map { get; set; }
		
 		
 		// -------------------------------------------------------------------------------
		// Scale
		// -------------------------------------------------------------------------------
        public void Scale(float scaledHeight, DungeonMap map) {

        	Map = map;
        	Size = map.Size;
        	
        	LocalScale = (int)(scaledHeight / Size.x);
            
            if (LocalScale < 1)
                throw new InvalidOperationException("Target scale is too small.");
			
			Texture = new Texture2D((int)Size.x, (int)Size.y, TextureFormat.ARGB32, false);
			
            if (Texture.height != scaledHeight)
                Texture.Resize((int)scaledHeight, (int)scaledHeight);
            
        }
        
 		// -------------------------------------------------------------------------------
		// DrawFloor
		// -------------------------------------------------------------------------------
        public void DrawFloor(MapExplorationData data) {
        	
        	if (!data.ExploredAreas.ContainsKey(Finder.map.MapName))
                throw new ArgumentException("No Map Data");
        	
            Clear();
            
            List<MapPing> mapInfo = data.ExploredAreas[Finder.map.MapName];
            
            foreach (MapPing info in mapInfo) {
                if (LocalScale > 1) {
                
                    int newX = (int)Math.Round(info.Position.x) * LocalScale;
                    int newY = (int)Math.Round(info.Position.y) * LocalScale;
                    
                    for (int x = newX; x <= newX + LocalScale; x++) {
                        for (int y = newY; y <= newY + LocalScale; y++) {
                            Texture.SetPixel(x, y, Finder.map.mapFloorColor);
                        }
                    }
                            
                } else if (LocalScale == 1) {
                    Texture.SetPixel((int)info.Position.x, (int)info.Position.y, Finder.map.mapFloorColor);
            	}
            }
            
            Texture.Apply();
        }
        
 		// -------------------------------------------------------------------------------
		// DrawOverlay
		// -------------------------------------------------------------------------------
        internal void DrawOverlay(Transform transform, Texture2D tex) {
            if (LocalScale == 0)
                throw new InvalidOperationException("Must call DrawFloor first!");

            DrawOverlay(new Vector2(transform.position.x, transform.position.z), tex);

        }

 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void DrawOverlay(Vector2 position, Texture2D texture) {
        	
            int i = 0;
            int j = 0;
            int newX = (int)Math.Round(position.x) * LocalScale;
            int newY = (int)Math.Round(position.y) * LocalScale;
                        
            int width = (int)((texture.width / 2) + LocalScale / 2);
            int height = (int)((texture.height / 2) + LocalScale / 2);
            
            for (int x = newX; x <= newX + width; x++) {
                for (int y = newY; y <= newY + height; y++) {
                    Color32 c = texture.GetPixel(i, j);
                    if (c.a != 0) {
                        Texture.SetPixel(
                            x - (texture.width / 2) + LocalScale / 2,
                            y - (texture.height / 2) + LocalScale / 2,
                            c);
                    }
                    j++;
                }
                i++;
                j = 0;
            }
           
            Texture.Apply();
        }
        
 		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public void Clear() {
            for (int i = 0; i < Texture.width; i++)
                for (int j = 0; j < Texture.height; j++)
                    Texture.SetPixel(i, j, Color.black);
            Texture.Apply();
        }
        
        // -------------------------------------------------------------------------------
        
    }
}