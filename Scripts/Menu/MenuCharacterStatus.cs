// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

namespace WoCo.DungeonCrawler {

	// ===================================================================================
	// 
	// ===================================================================================
    public class MenuCharacterStatus : MonoBehaviour {
    	
    	public CharacterBase character { get; set; }
    	
		public Text headerText;
		
		public Text txtLabelClass;
		public Text txtLabelSpecies;
		public Text txtLabelElement;
		public Text txtLabelFormation;
		public Text txtLabelLevel;
        public Text txtLabelHP;
        public Text txtLabelMP;
        public Text txtLabelXP;
        public Text txtLabelNext;
        
        public Text txtLabelAttributePoints;
		
		public Text txtClass;
		public Text txtSpecies;
		public Text txtElement;
		public Text txtFormation;
        public Text txtLevel;
        public Text txtHP;
        public Text txtMP;
        public Text txtXP;
        public Text txtNext;
        
        public Text txtAttributePoints;
		
        public Text txtLabelAcc;
        public Text txtLabelRes;
        public Text txtLabelCritRate;
        public Text txtLabelCritFactor;
        public Text txtLabelBlockRate;
        public Text txtLabelBlockFactor;
		public Text txtLabelInitiative;
		public Text txtLabelAttacks;
       
        public Text txtAcc;
        public Text txtRes;
        public Text txtCritRate;
        public Text txtCritFactor;
        public Text txtBlockRate;
        public Text txtBlockFactor;
        public Text txtInitiative;
        public Text txtAttacks;
        
        public Text[] txtAttackStyleLabel;
        public Text[] txtAttackStyleValue;
 
        public Text[] txtDefenseStyleLabel;
        public Text[] txtDefenseStyleValue;
       
        public Text[] txtAttributeLabel;
        public Text[] txtAttributeValue;
        
        public Text[] txtResistanceLabel;
        public Text[] txtResistanceValue;
       
        public GameObject btnBack;
        public GameObject statButtonContainer;
        
        protected string currentStat;
        protected int i;
        
   		// -------------------------------------------------------------------------------
		// OnEnable
		// -------------------------------------------------------------------------------
        private void OnEnable() {
            EnableStatButtons(character.AttributePoints > 0);
            Label();
            Refresh();
        }
        
   		// -------------------------------------------------------------------------------
		// Label
		// -------------------------------------------------------------------------------
        private void Label() {
        
        	headerText.text					= character.Name;
        	
        	txtLabelClass.text				= Finder.txt.namesVocabulary.charclass;
        	txtLabelSpecies.text			= Finder.txt.namesVocabulary.species;
        	txtLabelElement.text			= Finder.txt.namesVocabulary.element;
        	txtLabelFormation.text			= Finder.txt.formationNames.formation;
			txtLabelLevel.text 				= Finder.txt.basicDerivedStatNames.level;
			txtLabelXP.text 				= Finder.txt.basicDerivedStatNames.experience;
			txtLabelNext.text 				= Finder.txt.basicDerivedStatNames.nextLevelXP;
			txtLabelAttributePoints.text 	= Finder.txt.basicDerivedStatNames.attributePoints;
			
			
			// -- Labels: Attributes
			i = 0;
			foreach (CharacterAttribute attrib in character.stats.attributes) {
				txtAttributeLabel[i].text = attrib.template.fullName;
				i++;
			}
			
			// -- Labels: Combat Styles
			i = 0;
			foreach (CombatStyle style in character.stats.combatStyles) {
				txtAttackStyleLabel[i].text = style.template.fullNameAttack;
				txtDefenseStyleLabel[i].text = style.template.fullNameDefense;
				i++;
			}
			
			// -- Labels: Derived Stats
			txtLabelHP.text 			= Finder.txt.basicDerivedStatNames.health;
			txtLabelMP.text 			= Finder.txt.basicDerivedStatNames.mana;
			txtLabelAcc.text 			= Finder.txt.getDerivedStatName(DerivedStatType.accuracy,false);
			txtLabelRes.text 			= Finder.txt.getDerivedStatName(DerivedStatType.resistance,false);
			txtLabelCritRate.text 		= Finder.txt.getDerivedStatName(DerivedStatType.critRate,false);
			txtLabelCritFactor.text 	= Finder.txt.getDerivedStatName(DerivedStatType.critFactor,false);
			txtLabelBlockRate.text 		= Finder.txt.getDerivedStatName(DerivedStatType.blockRate,false);
			txtLabelBlockFactor.text 	= Finder.txt.getDerivedStatName(DerivedStatType.blockFactor,false);
			txtLabelInitiative.text 	= Finder.txt.getDerivedStatName(DerivedStatType.initiative,false);
			txtLabelAttacks.text 		= Finder.txt.getDerivedStatName(DerivedStatType.attacks,false);
			
			// -- Labels: Resistances
			i = 0;
			foreach (var element in DictionaryElement.dict) {
				txtResistanceLabel[i].text	= element.Value.name;
				i++;
			}
			
        }
        
   		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private void Refresh() {
        
            character.CalculateDerivedStats();
            
            if (character.template.characterClass != null)
            	txtClass.text 			= character.template.characterClass.fullName;
            	
            if (character.template.species != null)
            	txtSpecies.text			= character.template.species.fullName;
            	
            if (character.template.element != null)
            	txtElement.text 		= character.template.element.fullName;
            
            txtFormation.text 		= (character.IsRearguard) ? Finder.txt.formationNames.rear : Finder.txt.formationNames.front;
            txtLevel.text 			= character.Level.ToString();
            txtHP.text 				= string.Format("{0} / {1}", character.HP, character.MaxHP);
            txtMP.text 				= string.Format("{0} / {1}", character.MP, character.MaxMP);
            txtXP.text 				= character.stats.XP.ToString();
            txtNext.text 			= character.stats.XPToNextLevel.ToString();

			// -- Values: Attributes 
            i = 0;
			foreach (CharacterAttribute attrib in character.stats.attributes) {
				txtAttributeValue[i].text = attrib.value.ToString();
				i++;
			}
			
			// -- Values: Combat Styles (Derived Combat Style + Equipped Weapon Power)
            i = 0;
			foreach (CombatStyle style in character.stats.combatStyles) {
				
				int attackValue = style.attackValue;
				
				TemplateEquipmentWeapon equip = Finder.party.equipment.GetEquippedWeapon(character);
				
				if (equip != null) {
					attackValue += equip.basePower;
				}
				
				txtAttackStyleValue[i].text = attackValue.ToString();
				txtDefenseStyleValue[i].text = style.defenseValue.ToString();
				i++;
			}
			
			// -- Values: Derived Stats
			
            txtAttributePoints.text 	= character.AttributePoints.ToString();
            
            txtAcc.text 				= character.stats.Accuracy.ToString()		+ " %";
            txtRes.text 				= character.stats.Resistance.ToString()		+ " %";
            txtCritRate.text 			= character.stats.CritRate.ToString()		+ " %";
            txtCritFactor.text 			= character.stats.CritFactor.ToString()		+ " %";
            txtBlockRate.text 			= character.stats.BlockRate.ToString()		+ " %";
            txtBlockFactor.text 		= character.stats.BlockFactor.ToString()	+ " %";
            txtInitiative.text 			= character.stats.Initiative.ToString();
            txtAttacks.text 			= character.stats.Attacks.ToString();
            
            // -- Values: Resistances
            i = 0;
			foreach (var element in DictionaryElement.dict) {
				txtResistanceValue[i].text	= (character.stats.resistances.FirstOrDefault(x => x.template == element.Value).value*100).ToString() + " %";
				i++;
			}
            
            
        }

  		// -------------------------------------------------------------------------------
		// 
		// -------------------------------------------------------------------------------
        private void EnableStatButtons(bool active) {
            statButtonContainer.SetActive(active);
        }
        
  		// -------------------------------------------------------------------------------
		// UpgradeRequestedHandler
		// -------------------------------------------------------------------------------
        public void UpgradeRequestedHandler(string stat) {
        	if (character.AttributePoints > 0) {
        		currentStat = stat;
           		Finder.confirm.Show(Finder.txt.commandNames.upgrade+Finder.txt.seperators.dash+Finder.txt.basicVocabulary.areYouSure, Finder.txt.basicVocabulary.yes, Finder.txt.basicVocabulary.no, () => UpgradeConfirmedHandler(), null);
            }
        }
        
        // -------------------------------------------------------------------------------
		// UpgradeConfirmedHandler
		// -------------------------------------------------------------------------------
        protected void UpgradeConfirmedHandler() {
         	 if (character.AttributePoints > 0) {
                
                TemplateMetaAttribute upgrade_attrib = DictionaryAttribute.Get(currentStat);
                if (upgrade_attrib != null) {
                	
                	CharacterAttribute target_attrib = character.stats.attributes.FirstOrDefault(x => x.template == upgrade_attrib);
                	if (target_attrib != null) {
                		target_attrib.value++;
                		character.AttributePoints -= 1;
                	}
                }
                
                Refresh();
                
            }
        }
  		
        // -------------------------------------------------------------------------------
  
    }
}