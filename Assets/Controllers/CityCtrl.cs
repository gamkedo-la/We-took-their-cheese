using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityCtrl : MonoBehaviour {
	public int wealth = 0;
	// Use this for initialization
	public City city;
	public string name;
	void Start () {
		Button btn = gameObject.GetComponent<Button> ();
		btn.onClick.AddListener (displayCity);
	}
	public void addWealth(){
		city.money += 1;
	}
	void displayCity (){
		Debug.Log ("Display City has been Clicked");
		//set shop to city
		//display shop
		UIRouter.goTo ("Shop");
	}

	void OnEnable() {
		Debug.Log ("OnEnabled Called: " + name);
		loadCity ();
	}

	void initCity(){
		city = new City ();
		city.name = name;
		Game.Data.cities.Add (city);
	}

	public void loadCity(){
		city = Game.Data.cities.Find(x => x.name == name);
		if(city == null){
			initCity();
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
