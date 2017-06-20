using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverWorldCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (UIRouter.location == "OverWorld" && Input.GetKeyDown(KeyCode.Escape)) {
			UIRouter.goTo ("Inventory");
		}	
	}
}
