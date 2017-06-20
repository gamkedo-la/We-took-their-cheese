using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class City : MonoBehaviour {
	public int wealth = 0;
	// Use this for initialization

	void Start () {
		Button btn = gameObject.GetComponent<Button> ();
		btn.onClick.AddListener (displayCity);
	}

	void displayCity (){
		Debug.Log ("Display City has been Clicked");
		//set shop to city
		//display shop
		UIRouter.goTo ("Shop");


	}

	// Update is called once per frame
	void Update () {
		
	}
}
