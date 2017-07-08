﻿using System.Collections;
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
	public Text transactionPrice;
	public Text transactionTotal;
	public Text playerMoneyUI;
	public Text buyButton;
	public Item selectedItem;
	public Item priceData;
	public Transform selectedItemUI;
	public Text unitPrice;
	public Transform cityItemsList;
	public Transform playerItemsList;
	public Transform shopItemPrefab;
	public Transform backendItemList;

	bool isBuying;
	Transform itemUI;
	float slope;
	// Use this for initialization
	void Start () {
		screen = gameObject.GetComponent<Canvas> ();
		UIRouter.shop = this;
		Transform itemUI;
		Text textField;
		Image icon;
		Image iconRef;
		amountInputFieldUI.text = "1";

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
			icon = itemUI.Find ("Image").GetComponent<Image> ();
			iconRef = backendItemList.Find (item.name).Find ("Icon").GetComponent<Image> ();
			icon.sprite = iconRef.sprite;

			//set player copy
			itemUI = Instantiate(itemUI, playerItemsList);
			itemUI.name = item.name;
		}
		selectedItem = null;
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
		amount = 1;
		foreach (Item item in city.items) {
			itemUI = cityItemsList.Find (item.name);
			if (itemUI == null) {
				Debug.LogError ("Item not found in city list " + item.name + ". Perhaps spelled wrong in City?");
			}
			if (item.count < 1) {
				Debug.Log (item.name);
				itemUI.gameObject.SetActive (false);
				continue;
			}

			itemUI.gameObject.SetActive (true);
			textField = itemUI.Find ("Quantity").GetComponent<Text>();
			textField.text = item.count.ToString ();; //TODO: Write util to change 1200 to 1.2k 

			textField = itemUI.Find ("Price").GetComponent<Text>();
			int price = (int)(amount * (amount * slope + item.price + item.price * 0.1));
			textField.text = price.ToString (); //TODO: make price equal to "city markup" + "base price"
			ShopItemCtrl itemCtrl = itemUI.GetComponent<ShopItemCtrl>();
			itemCtrl.isCity = true;



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
		selectedItem = null;

	}

	//TODO: ENUMS!!
	public void selectItem(string itemName, bool isCity){
		isBuying = isCity;
		if (isCity) {
			selectedItem = city.items.Find (x => x.name == itemName); //I can feel the judgement. 
			priceData = selectedItem;
			buyButton.text = "Buy";

		} else {
			selectedItem = player.items.Find (x => x.name == itemName);
			priceData =  city.items.Find (x => x.name == itemName);
			buyButton.text = "Sell";
		}
		amountInputFieldUI.text = "1";
		slope = priceData.price / (-priceData.maxAmount);
		Text nameText = selectedItemUI.Find ("Name").GetComponent<Text> ();
		nameText.text = selectedItem.name;
		//TODO: toggle selected item ui on

		//Set Icon
		Image icon = selectedItemUI.Find ("Image").GetComponent<Image> ();
		Image iconRef = backendItemList.Find (selectedItem.name).Find ("Icon").GetComponent<Image> ();
		icon.sprite = iconRef.sprite;

	}

	public void buyItem(){
		if (isBuying) {
			if (player.money > selectedItem.price * amount) {
				int cost = (int)(amount * (int)(amount * slope + selectedItem.price + selectedItem.price * 0.1));//%10 markup
				player.money -= cost;
				city.money += cost;

				Item playerItem = player.items.Find (x => x.name == selectedItem.name);
				selectedItem.count -= amount;
				playerItem.count += amount;
				//TODO: effect price
				populate (player);
			}
		} else {
			if (city.money > selectedItem.price * amount) {				
				Item cityItem = city.items.Find (x => x.name == selectedItem.name);
				int cost = (int)(amount * (int)(amount * slope + cityItem.price));//%10 markup
				player.money += cost;
				city.money -= cost;


				selectedItem.count -= amount;
				cityItem.count += amount;
				//TODO: effect price
				populate (player);
			}
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
			} else {
				amountInputFieldUI.text = "1";
			}
			int price = 0;
			if (isBuying && selectedItem != null) {
				price = (int)(amount * slope + selectedItem.price + selectedItem.price * 0.1);

			} else if(selectedItem != null){
				Item cityItem = city.items.Find (x => x.name == selectedItem.name);
				if (cityItem != null) {
					price = (int)(amount * slope + cityItem.price);
					//TODO: handle prices, or no sales when no demand
				}
			}
			transactionPrice.text = price.ToString ();
			unitPrice.text = price.ToString ();
			transactionTotal.text = (amount * price).ToString () +" g";
		}
		if (player != null && playerMoneyUI != null) {
			playerMoneyUI.text = player.money.ToString ();
		}
	}

	public void DEBUGaddWealth(){
		city.money += 100;
	}
}
