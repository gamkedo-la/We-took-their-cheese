using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour {
	
	Canvas screen;
	public City city;
	public Text moneyUI;
	// Use this for initialization
	void Start () {
		screen = gameObject.GetComponent<Canvas> ();
		UIRouter.shop = this;
	}
	public void runToOverWorld(){
		UIRouter.goTo ("OverWorld");
		city = null;
	}
	void Update () {
		if (city != null && moneyUI != null) {
			moneyUI.text = city.money.ToString ();
		}
	}

	public void DEBUGaddWealth(){
		city.money += 1;
	}
}
