using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class City {

	//list of items
	public string name = "Unnamed City";
	//current location //should location be saved?!
	//demand
	public int money = 0;
	public int maxMoney = 0;
	public List<Item> items;
}
