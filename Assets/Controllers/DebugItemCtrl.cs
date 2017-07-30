using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DebugItemCtrl : MonoBehaviour {
	Text itemName;
	Text itemPrice;
	public int price;
	public Item data;
	void Awake () { //Must be Awake so this happens before DebugSave.Start		
		data = Game.AllItems.Find(x => x.name == name);
		if (data == null) {			
			initItem ();
		}

		itemName= gameObject.transform.Find("Name").GetComponent<Text>();
		itemPrice= gameObject.transform.Find("Price/PriceText").GetComponent<Text>();
	}

	void initItem (){
		//Debug.Log ("Registering item: " + name);
		data = new Item(name);
		data.price = price;
		Game.AllItems.Add (data);
	}

	// Update is called once per frame
	void Update () {
		//update data
		data.name = name;
		data.price = price;

		//update ui
		itemName.text = data.name;
		itemPrice.text = data.price.ToString ();
	}
}