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
		Debug.Log ("Name is now: " + gameObject.name);
		cityName.text = gameObject.name;
	}
}