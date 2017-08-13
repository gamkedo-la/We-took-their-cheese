using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPam : MonoBehaviour {
	public Texture2D cursor;
	public Vector3 positionOffset = Vector3.zero;
	// Use this for initialization
	void Start () {
		Cursor.SetCursor(cursor, positionOffset, CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () {
        float minBorderY = -82.3f;
        float maxBorderY = 2620f;
        float minBorderX = -670f;
        float maxBorderX = 2625f;

        Vector3 projectedCamPos = transform.position + Time.deltaTime * (Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.up) * 400.0f;

        if (projectedCamPos.x > maxBorderX || projectedCamPos.x < minBorderX || projectedCamPos.y > maxBorderY || projectedCamPos.y < minBorderY)
        {

        } else
        {
            transform.position += Time.deltaTime * (Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.up) * 400.0f;
        }
	}
}
