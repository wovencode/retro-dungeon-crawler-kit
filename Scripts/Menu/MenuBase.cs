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
using UnityEngine.UI;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// MenuBase
	// =================================================================================== 
    public abstract class MenuBase<T> : MonoBehaviour {
    	
        public Transform panel;
        public GameObject btnLeave;
        public GameObject itemTemplatePrefab;
       
        protected List<T> content;
        
        protected abstract bool CanActivate(T item);
        protected abstract bool CanUpgrade(T item);
        protected abstract bool CanTrash(T item);
        protected abstract void DisposeItem(Transform item);
		protected abstract Sprite GetIcon(T item);
		protected abstract Sprite GetCurrencyIcon(T item);
        protected abstract string GetName(T item);
        protected abstract void LoadContent();
        
        protected virtual void TemplateInitialize(GameObject templateInstance, T item) { }
   		
   		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public virtual void OnEnable() {
            LoadContent();
            Clear();
            AddItemsToPanel(content);
        }

   		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected virtual void AddItemsToPanel(List<T> items) {
        	if (items != null) {
            foreach (T instance in items) {
            
                GameObject itemSlot = GameObject.Instantiate(itemTemplatePrefab) as GameObject;
                
                if (itemSlot != null) {
                    itemSlot.name = GetName(instance) + "_slot";
                    TemplateInitialize(itemSlot, instance);
                    itemSlot.transform.SetParent(panel, false);
                    itemSlot.SetActive(true);
                }
                
            }
            }
        }
   		
   		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected void LoadPage(){
        	LoadContent();
        	Clear();
        	AddItemsToPanel(content);
        }
        
   		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected void Clear(){
            foreach (Transform child in panel){
                DisposeItem(child);
                GameObject.Destroy(child.gameObject);
            }
        }

   		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        public virtual void OnDisable() {
            Clear();
        }

   		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        protected virtual void Refresh() {
            LoadContent();
            LoadPage();
        }
   		
        // -----------------------------------------------------------------------------------
		// TooltipRequestedHandler
		// -----------------------------------------------------------------------------------
        protected virtual void TooltipRequestedHandler(object obj) {
        	
        	TemplateSimple tmpl;
        	
        	if (obj is InstanceItem) {
        		tmpl = ((InstanceItem)obj).template;
        	} else if (obj is InstanceEquipment) {
        		tmpl = ((InstanceEquipment)obj).template;
        	} else if (obj is InstanceSkill) {
        		tmpl = ((InstanceSkill)obj).template;
        	} else {
        		tmpl = (TemplateSimple)obj;
        	}
        	
        	Finder.tooltip.Initialize(tmpl);
        	Finder.tooltip.Show();			
        }
        
        // -----------------------------------------------------------------------------------
        
    }
}