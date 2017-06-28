using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {
	
	Canvas screen;
	public City city;
	public Player player;
	public int amount;
	public Text amountUI;
	public Text amountInputFieldUI;
	public Text moneyUI;
	public Text playerMoneyUI;
	public Item selectedItem;
	public Transform cityItemsList;
	public Transform playerItemsList;
	public Transform shopItemPrefab;

	Transform itemUI;
	// Use this for initialization
	void Start () {
		screen = gameObject.GetComponent<Canvas> ();
		UIRouter.shop = this;
		Transform itemUI;
		Text textField;
		amountInputFieldUI.text = "0";
		foreach (Item item in Game.AllItems) { 
			itemUI = Instantiate (shopItemPrefab, cityItemsList);
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

			//set player copy
			itemUI = Instantiate(itemUI, playerItemsList);
			itemUI.name = item.name;
		}
	}

	public void runToOverWorld(){
		UIRouter.goTo ("OverWorld");
		city = null;
	}
	public void unpopulate(){		
		foreach (Item item in city.items) {
			itemUI = cityItemsList.Find (item.name);
			if (itemUI.gameObject.activeSelf) {
				itemUI.gameObject.SetActive (false);
			}

			itemUI = playerItemsList.Find (item.name);
			if (itemUI.gameObject.activeSelf) {
				itemUI.gameObject.SetActive (false);
			}
		}
	}

	public void populate(Player selectedPlayer){
		Text textField;
		player = selectedPlayer;
		foreach (Item item in city.items) {
			itemUI = cityItemsList.Find (item.name);
			if (item.count < 1) {
				itemUI.gameObject.SetActive (false);
				continue;
			}

			itemUI.gameObject.SetActive (true);
			textField = itemUI.Find ("Quantity").GetComponent<Text>();
			textField.text = item.count.ToString ();; //TODO: Write util to change 1200 to 1.2k 

			textField = itemUI.Find ("Price").GetComponent<Text>();
			textField.text = item.price.ToString ();; //TODO: make price equal to "city markup" + "base price"
			ShopItemCtrl itemCtrl = itemUI.GetComponent<ShopItemCtrl>();
			itemCtrl.isCity = true;
			//Set Icon
			//textField = itemUI.Find ("Price");
			//textField.text = item.name;


		}


		foreach (Item item in player.items) {
			itemUI = playerItemsList.Find (item.name);
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

	//TODO: ENUMS!!
	public void selectItem(string itemName, bool isCity){
		if (isCity) {
			selectedItem = city.items.Find (x => x.name == itemName); //I can feel the judgement. 
			//set item to buy

		}
	}

	public void buyItem(){
		
		if(player.money > selectedItem.price * amount){
			player.money -= selectedItem.price * amount;
			Item playerItem = player.items.Find (x => x.name == selectedItem.name);
			selectedItem.count -= amount;
			playerItem.count += amount;
			//TODO: effect price
			populate(player);
		}
	}
	void Update () {
		if (city != null && moneyUI != null) {
			moneyUI.text = city.money.ToString ();
			if (selectedItem == null) {
				amountUI.text = "0";
			} else {
				amountUI.text = selectedItem.count.ToString ();
			}
			if (amountInputFieldUI.text != "") {
				amount = int.Parse (amountInputFieldUI.text);
			}
		}
		if (player != null && playerMoneyUI != null) {
			playerMoneyUI.text = player.money.ToString ();
		}
	}

	public void DEBUGaddWealth(){
		city.money += 1;
	}
}
