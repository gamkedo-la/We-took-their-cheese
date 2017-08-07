using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

	const string CURRENCY_FORMAT_PREFIX = ""; // optional, could be "$"
	const string CURRENCY_FORMAT_SUFFIX = ""; // optional, could be "g"

	bool isBuying;
	Transform itemUI;
	float slope = 0f;
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

			textField = itemUI.Find ("Quantity").GetComponent<Text>();
			textField.text = shopNumberFormat(item.count);

			textField = itemUI.Find ("Price").GetComponent<Text>();

			int unitPrice = (int)(item.count*slope + item.price);
			Debug.Log(item.name + "unit price: " + unitPrice +"\n " + item.count +  "*" + slope + " + " + item.price);
			textField.text = shopNumberFormat(unitPrice); //TODO: make price equal to "city markup" + "base price"

			itemUI.gameObject.SetActive(false);

			//Set Icon
			icon = itemUI.Find ("Image").GetComponent<Image> ();
			iconRef = backendItemList.Find (item.name).Find ("Icon").GetComponent<Image>();
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
			if (itemUI == null) {
				Debug.LogError ("Could not find: " + item.name);
			}
			if (itemUI.gameObject.activeSelf) {
				itemUI.gameObject.SetActive (false);
			}

			itemUI = playerItemsList.Find (item.name);
			if (itemUI.gameObject.activeSelf) {
				itemUI.gameObject.SetActive (false);
			}
		}
	}

	// changes 1200 to 1.2k
	public string shopNumberFormat(int count) {
		if (count >= 1000000) return CURRENCY_FORMAT_PREFIX + (count / 1000000) + "M" + CURRENCY_FORMAT_SUFFIX;
		else if (count >= 1000) return CURRENCY_FORMAT_PREFIX + (count / 1000) + "k" + CURRENCY_FORMAT_SUFFIX;
		else return CURRENCY_FORMAT_PREFIX + count.ToString() + CURRENCY_FORMAT_SUFFIX;
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
			textField.text = shopNumberFormat(item.count);

			textField = itemUI.Find ("Price").GetComponent<Text>();
			float slope1 = (float)item.price / (float)item.maxAmount * -1;
			int unitPrice = (int)(item.count*slope1 + item.price);

			textField.text = unitPrice.ToString (); //TODO: make price equal to "city markup" + "base price"
			ShopItemCtrl itemCtrl = itemUI.GetComponent<ShopItemCtrl>();
			itemCtrl.isCity = true;
		}

		Item playerItem = player.items.Find (x => x.name == "Cheese");
		if(playerItem.count >= 1000) {
			Debug.Log("YOU RUINED THE ELF ECONOMY. VICTORY! TAKE THAT, ELVES");
			SceneManager.LoadScene("EndScene");
			return;
		}


		foreach (Item item in player.items) {
			itemUI = playerItemsList.Find (item.name);
			if (item.count < 1) {
				itemUI.gameObject.SetActive (false);
				continue;
			}
			itemUI.gameObject.SetActive (true);
			textField = itemUI.Find ("Quantity").GetComponent<Text>();;
			textField.text = shopNumberFormat(item.count);

			textField = itemUI.Find ("Price").GetComponent<Text>();;
			textField.text = shopNumberFormat(item.price);

			ShopItemCtrl itemCtrl = itemUI.GetComponent<ShopItemCtrl>();
			itemCtrl.isCity = false;
		}

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
		slope = (float)priceData.price / (float)priceData.maxAmount * -1;
		Text nameText = selectedItemUI.Find ("Name").GetComponent<Text> ();
		nameText.text = selectedItem.name;
		//TODO: toggle selected item ui on

		//Set Icon
		Image icon = selectedItemUI.Find ("Image").GetComponent<Image> ();
		Image iconRef = backendItemList.Find (selectedItem.name).Find ("Icon").GetComponent<Image> ();
		icon.sprite = iconRef.sprite;

	}

	public void buyItem(){
		Item cityItem = city.items.Find (x => x.name == selectedItem.name);
		if (isBuying) {
			int cost = (int)((cityItem.count - amount) * slope + selectedItem.price);
			Debug.Log (cost);
			if (player.money > cost * amount) {				
				player.money -=  cost * amount;
				city.money +=  cost * amount;

				Item playerItem = player.items.Find (x => x.name == selectedItem.name);
				selectedItem.count -= amount;
				playerItem.count += amount;
				//TODO: effect price
				populate (player);
			}
		} else {
			int cost = (int)((cityItem.count + amount) * slope + priceData.price);
			if (city.money > cost * amount) {								
				player.money += cost * amount;
				city.money -= cost * amount;


				selectedItem.count -= amount;
				cityItem.count += amount;
				//TODO: effect price
				populate (player);
			}
		}
	}
	void Update () {
		if (city != null && moneyUI != null) {
			if(Input.GetKeyDown(KeyCode.C)) { //Cheese cheat
				Item playerItem = player.items.Find (x => x.name == "Cheese");
				if (playerItem == null) {
					Debug.Log ("No cheese found");
					//player.items.Add(new 
				} else if(playerItem.count < 998) {
					playerItem.count = 998;
				} else {
					playerItem.count++;
				}
				Debug.Log("Cheese cheat, cheese now:" + playerItem.count);
				populate (player);
			}

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
				
				Item cityItem = city.items.Find (x => x.name == selectedItem.name);
				price = (int)((cityItem.count - amount)*slope + selectedItem.price);
			} else if(selectedItem != null){
				Item cityItem = city.items.Find (x => x.name == selectedItem.name);
				if (cityItem != null) {
					price = (int)((cityItem.count + amount)*slope + priceData.price);
					//price = (int)((cityItem.count + amount) * slope + selectedItem.price);
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
