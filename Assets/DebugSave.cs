using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSave : MonoBehaviour {
	public List<GameSave> saves = Game.savedGames;
	public GameSave current = Game.Data;
	void Awake (){
		Game.Load ();
		saves = Game.savedGames;
		current = Game.Data;
	}
	public void Save(){
		//saves = Game.savedGames;
		//current = Game.Data;
		Game.Save();
	}
}
