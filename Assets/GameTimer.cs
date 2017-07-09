using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {

	public float secondsPerTick = 10f;
	private int tickCount = 0;

	void Start()
	{
		Debug.Log("GameTimer will tick every " + secondsPerTick + " seconds.");
		StartCoroutine("Tick");
	}

	private IEnumerator Tick()
	{
		while(true)
		{

			yield return new WaitForSeconds(secondsPerTick);

			tickCount++;
			Debug.Log("GameTimer Tick #" + tickCount);

			// TODO: make each city consume items as time passes

		}
	}

}
