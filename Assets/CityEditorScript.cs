using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CityEditorScript : MonoBehaviour {

	Text cityName;
	void Awake (){
		cityName = gameObject.GetComponentInChildren<Text> ();
	}
	// Update is called once per frame
	void Update () {
		if (!Application.isPlaying && cityName.text != name) {
			Debug.Log ("Name is now: " + name);
			cityName.text = name;
		}
	}
}