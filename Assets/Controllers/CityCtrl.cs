﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CityCtrl : MonoBehaviour {
	// Use this for initialization
	public City city;
	void Start () {
		Button btn = gameObject.GetComponent<Button> ();
		btn.onClick.AddListener (displayCity);
	}

	public void addWealth(){
		city.money += 1000;
	}

	void displayCity (){
		
		UIRouter.shop.unpopulate();

		UIRouter.shop.city = city;//set shop to city
		Debug.Log ("City selected " + UIRouter.shop.city.name);

		UIRouter.shop.populate(Game.Data.players.Find(x => x.name == "Mozzarella"));
		UIRouter.goTo ("Shop");//display shop


	}

	void OnEnable() {
		Debug.Log ("OnEnabled Called: " + name);
		loadCity ();
	}

	void initCity(){
		city = new City ();
		city.name = name;
		//city.items = new List<Item> ();
		Game.Data.cities.Add (city);
	}

	public void loadCity(){
		City Savedcity = Game.Data.cities.Find(x => x.name == name);
		if(Savedcity == null){
			Game.Data.cities.Add (city);
		} else {
			city = Savedcity;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
