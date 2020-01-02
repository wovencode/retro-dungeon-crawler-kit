// =======================================================================================
// RETRO DUNGEON CRAWLER KIT (Support Forum: www.dungeoncrawler.net)
//
//   --- DO NOT CHANGE ANYTHING BELOW THIS LINE (UNLESS YOU KNOW WHAT YOU ARE DOING) ---
// =======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoCo.DungeonCrawler {
    
    public enum ItemType {
    	None,
    	Regular,
    	Ability,
    	Attack,
    	Curative,
    	Special
    }
    
    public enum CurrencyType {
    	None,
    	Gold
    }
    
	public enum DirectionType {
		None, North, East, South, West
	}
	
    public enum CanUseType {
    	None,
    	IsHPNotMax,
    	IsMPNotMax,
    	IsStun,
    	IsSilence,
    	IsDepleteHP,
    	IsDepleteMP,
    	IsAlive,
    	IsDead
    }
    
    public enum CanUseLocation {
    	None,
    	Everywhere,
    	InBattle,
    	InNormalBattle,
    	InBossBattle,
    	InBattleAndDungeon,
    	InNormalBattleAndDungeon,
    	InTown,
    	InWorldmap,
    	InTownOrWorldmap,
    	InDungeon
    }
    
	public enum CanUseTarget {
    	None,
    	AtSelf,
    	AtTargetAlly,
    	AtTargetFrontrowAlly,
    	AtTargetRearrowAlly,
    	AtRandomAlly,
    	AtRandomFrontrowAlly,
    	AtRandomRearrowAlly,
    	AtAllAllies,
    	AtAllFrontrowAllies,
    	AtAllRearrowAllies,
    	AtTargetEnemy,
    	AtTargetFrontrowEnemy,
    	AtTargetRearrowEnemy,
    	AtRandomEnemy,
    	AtRandomFrontrowEnemy,
    	AtRandomRearrowEnemy,
    	AtAllEnemies,
    	AtAllFrontrowEnemies,
    	AtAllRearrowEnemies,
    	AtAll
    }
     
    public enum EncounterType {
    	Normal,
    	Miniboss,
    	Boss,
    	Endboss
    }
    
    public enum EditorType {
    	All,
    	Floor,
    	Wall,
    	FakeWall,
    	Door,
    	Stairs,
    	Start,
    	A1,
    	A2,
    	A3,
    	B1,
    	B2,
    	B3,
    	C1,
    	C2,
    	C3,
    	D1,
    	D2,
    	D3
    }

    public enum DerivedStatType {
        maxHP,
        maxMP,
        accuracy,
        resistance,
        critRate,
        critFactor,
        blockRate,
        blockFactor,
        initiative,
        attacks
    }
   
 	public enum SpecialActionType {
        None,
        PlayerExitBattle,
        PlayerExitDungeon,
        PlayerWarpDungeon
    }   
 	
    public enum CharacterActionType {
        None,
        Attack,
        Defend,
        Switch,
        PlayerRun,
        PlayerExitBattle,
        PlayerExitDungeon,
        PlayerWarpDungeon,
        Recover,
        Damage,
        LearnAbility,
        PassTurn,
        UseItem,
        UseAbility
    }    
 	
 	public enum CutsceneType {
 		Intro,
 		Outro,
 		Credits,
 		Cutscene
 	}
 	
    public enum RankType {
    	Front,
    	Rear
    }
    
    public enum RankTargetType {
    	All,
    	Front,
    	Rear
    }
    
    public enum MoveType {
    	None,
    	Up,
    	Down,
    	Left,
    	Right,
    }
    
    public enum BoolType {
    	Any,
    	True,
    	False
    }
    
    public enum ShopType {
    	None,
    	ShopItem,
    	ShopEquipment,
    	ShopItemAbility,
    	ShopInn,
    	ShopCharacter,
    	ShopItemCurative,
    }
    
    public enum RecoveryType {
    	None,
    	HP,
    	MP,
    	XP,
    	HPandMP
    }

	public enum LocationType {
		None,
		Town,
		Worldmap,
		Dungeon,
		Outro
	}
	
	public enum SaveMode {
        Save,
        Load
    }
    
   public enum SavegameSettings {
   		Always,
   		TownOnly
   }  
         
	public enum UIState {
		None,
		Town,
		Dungeon,
		Battle,
		BattlePlayerAttack,
		Menu,
		Inventory,
		InventorySell,
		EquipmentSell,
		CharacterSell,
		SelectCharacterToDisplayStatus,
		ShowSelectedCharacterStatus,
		SelectCharacterToDisplayAbilities,
		ShowSelectedCharacterAbilities,
		ProcessSelectedAbility,
		SelectCharacterToDisplayEquipment,
		ShowSelectedCharacterEquipment,
		SelectCharacterToSwitchFormation,
		SwitchSelectedCharacterFormation,
		ShopInside,
		ShopOutside,
		ShopEquipment,
		ShopItem,
		ShopInn,
		ShopCharacter,
		ShopCurative,
		ShopAbility,
		MinimapShow,
		MinimapHide,
		Worldmap,
		Title,
		Settings,
		Credits,
		DungeonWarp,
		SaveMenu,
		LoadMenu,
		OpeningScene,
		EndingScene,
		
	}
	
	public enum SFX {
        Equip,
        Unequip,
        WallBump,
        GameOver,
        Purchase,
        Sell,
        NormalFightWon,
        BossFightWon,
        Footstep,
        RunAway,
        JingleVictory,
        JingleVictoryBoss,
        ButtonClick,
        ButtonCancel
    }
    
}