using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCtrl : MonoBehaviour {	
	// Use this for initialization
	public bool canQuickCancel = false;
	Canvas screen;
	void Awake () { //Must be Awake so this happens before DebugSave.Start
		screen = gameObject.GetComponent<Canvas> ();
		UIRouter.screens.Add(screen);
	}

	public void runToOverWorld(){
		UIRouter.goTo ("OverWorld");
	}

	// Update is called once per frame
	void Update () {
		//if (canQuickCancel && screen.enabled && Input.GetKeyDown(KeyCode.Escape)) {
		if (canQuickCancel && screen.enabled && Input.GetKeyDown(KeyCode.Q)) {
			UIRouter.goTo ("OverWorld");
			return;
		}
	}
}
