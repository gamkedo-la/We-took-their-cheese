using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ScreenCtrl : MonoBehaviour {	
	// Use this for initialization
	public bool canQuickCancel = false;
	public AudioMixerSnapshot inMainWorld;

	Canvas screen;
	void Awake () { //Must be Awake so this happens before DebugSave.Start
		screen = gameObject.GetComponent<Canvas> ();
		UIRouter.screens.Add(screen);
	}

	public void runToOverWorld(){
		UIRouter.goTo ("OverWorld");
		inMainWorld.TransitionTo(1.5f);
	}

	// Update is called once per frame
	void Update () {
		if (canQuickCancel && screen.enabled && Input.GetKeyDown(KeyCode.Q)) {
			UIRouter.goTo ("OverWorld");
			inMainWorld.TransitionTo(1.5f);
			return;
		}
	}
}
