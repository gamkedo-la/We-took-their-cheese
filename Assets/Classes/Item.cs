using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
	//list of items
	public string name = "Unnamed Item";
	//current location //should location be saved?!
	//demand
	public int price = 0;
	public int count = 0;
	public Item(string itemName){
		name = itemName;
	}
}
