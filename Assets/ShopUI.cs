using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour {

	Canvas screen;
	// Use this for initialization
	void Start () {
		screen = gameObject.GetComponent<Canvas> ();
	}
	public void runToOverWorld(){
		UIRouter.goTo ("OverWorld");
	}
	void Update () {

	}
}
