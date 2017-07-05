using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position += Time.deltaTime * (Input.GetAxis ("Horizontal") * Vector3.right + Input.GetAxis ("Vertical") * Vector3.up) * 400.0f;

	}
}
