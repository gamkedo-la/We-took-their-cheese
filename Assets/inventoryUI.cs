using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryUI : MonoBehaviour {
	public Player player;
	public Transform itemList;
	public Transform shopItemPrefab;
	public Text gold;
	Transform itemUI;
	bool empty = true;
	bool fresh = true;
	public Canvas screen;

	// Use this for initialization
	void init () {
		Text textField;
		player = Game.Data.players.Find (x => x.name == "Mozzarella");
		foreach (Item item in Game.AllItems) { 
			itemUI = Instantiate (shopItemPrefab, itemList);
			//Set UI labels
			textField = itemUI.Find ("Name").GetComponent<Text>();
			textField.text = item.name;
			itemUI.name = item.name;

			textField = itemUI.Find ("Quantity").GetComponent<Text>();;
			textField.text = item.count.ToString ();; //TODO: Write util to change 1200 to 1.2k 

			textField = itemUI.Find ("Price").GetComponent<Text>();;
			textField.text = item.price.ToString ();; //TODO: make price equal to "city markup" + "base price"
			itemUI.gameObject.SetActive(false);
			//Set Icon
			//textField = itemUI.Find ("Price");
			//textField.text = item.name;
		}
	}

	void populate(){
		Text textField;
		foreach (Item item in player.items) {
			itemUI = itemList.Find (item.name);
			if (item.count < 1) {
				itemUI.gameObject.SetActive (false);
				continue;
			}
			itemUI.gameObject.SetActive (true);
			textField = itemUI.Find ("Quantity").GetComponent<Text>();;
			textField.text = item.count.ToString (); //TODO: Write util to change 1200 to 1.2k 

			textField = itemUI.Find ("Price").GetComponent<Text>();;
			textField.text = item.price.ToString (); //TODO: make price equal to "city sell" + "base price"

			ShopItemCtrl itemCtrl = itemUI.GetComponent<ShopItemCtrl>();
			itemCtrl.isCity = false;
		}
	}
	// Update is called once per frame
	void Update () {
		if (fresh == false && screen.enabled == false) {
			fresh = true;
		}
		if (screen.enabled == false) {
			return;
		}
		if (empty && Game.Data.players.Count > 0) {
			empty = false;
			init ();
		}
		if (fresh == true && screen.enabled == true) {
			fresh = false;
			populate ();
		}

		gold.text = player.money.ToString ();

	}
}
