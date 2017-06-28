using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player {
	public string name = "Forever Provolone";
	//current location 
	//demand
	public int money = 0;
	public List<Item> items = new List<Item>();
	public Player(string argName){
		name = argName;
	}
}
