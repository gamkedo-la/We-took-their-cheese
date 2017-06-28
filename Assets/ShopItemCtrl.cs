using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemCtrl : MonoBehaviour {
	public bool isCity = false;
	public void selectItem(){
		UIRouter.shop.selectItem (name, isCity);
	}
	void Start(){
		Button btn = gameObject.GetComponent<Button> ();
		btn.onClick.AddListener (selectItem);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
