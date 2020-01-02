// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// MENU PARTY WARP
	// ===================================================================================
    public class MenuPartyWarp : MenuBase<string> {
    
       
		// -------------------------------------------------------------------------------
		// CanConfirm
		// -------------------------------------------------------------------------------
  		protected override bool CanActivate(string tmpl) {
  			return true;
  		}
  
  		protected override bool CanUpgrade(string tmpl) {
  			return false;
  		}
  		
  		protected override bool CanTrash(string tmpl) {
  			return false;
  		}
  		
        protected override void LoadContent() {
            content = Finder.party.MapExplorationInfo.ExploredAreas.Keys.ToList();
            content.Sort();
        }

        protected override void TemplateInitialize(GameObject templateInstance, string item) {
        
            Button button = templateInstance.GetComponent<Button>();
            
            if (button != null) {

            	TemplateMetaDungeon map = DictionaryDungeon.Get(item);
                button.UpdateText(map.name);
                
                button.onClick.AddListener(new UnityEngine.Events.UnityAction(() =>
                    {
                        Finder.map.WarpDungeon(map);
                        
                        
                    }));
            }
        }

        protected override void DisposeItem(UnityEngine.Transform item)
        {
            Button button = item.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
            }
        }

		protected override Sprite GetCurrencyIcon(string item)
        {
            return null;
        }

        protected override Sprite GetIcon(string item)
        {
            return null;
        }


        protected override string GetName(string item)
        {
            return item;
        }


    }
}