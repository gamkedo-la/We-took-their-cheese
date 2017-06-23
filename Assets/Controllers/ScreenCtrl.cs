using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCtrl : MonoBehaviour {	
	// Use this for initialization
	public bool canQuickCancel = false;
	Canvas screen;
	void Start () {
		screen = gameObject.GetComponent<Canvas> ();
		UIRouter.screens.Add(screen);
	}

	public void runToOverWorld(){
		UIRouter.goTo ("OverWorld");
	}

	// Update is called once per frame
	void Update () {
		if (canQuickCancel && screen.enabled && Input.GetKeyDown(KeyCode.Escape)) {
			UIRouter.goTo ("OverWorld");
			return;
		}
	}
}
