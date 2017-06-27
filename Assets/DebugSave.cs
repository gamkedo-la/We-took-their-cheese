using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSave : MonoBehaviour {
	public List<GameSave> saves = Game.savedGames;
	public GameSave current = Game.Data;
	public List<Item> allItems;

	void Start (){ //Must be start so this is called after ScreenCtrl.Awake
		Game.Load ();
		saves = Game.savedGames;
		current = Game.Data;
		allItems = Game.AllItems;
	}
	public void Save(){
		//saves = Game.savedGames;
		//current = Game.Data;
		Game.Save();
	}
}
