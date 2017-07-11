using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour {

	public float secondsPerTick = 10f;
	public int consumeRangeMin = -10;
	public int consumeRangeMax = 10;

	private int tickCount = 0;

	void Start()
	{
		Debug.Log("GameTimer will tick every " + secondsPerTick + " seconds.");
		StartCoroutine("Tick");
	}

	// FIXME: what happens if store GUI is open? will changing city item quantities here cause bugs?
	private IEnumerator Tick() 
	{
		int quantityChange = 0;

		while(true)
		{
			yield return new WaitForSeconds(secondsPerTick);

			tickCount++;
			Debug.Log("GameTimer Month #" + tickCount);

			// update each city
			foreach (City nextCity in Game.AllCities) { 
				//Debug.Log("GameTimer next city: " + nextCity.name);

				// consume items over time
				foreach (Item nextItem in nextCity.items) {
					//Debug.Log("GameTimer next item: " + nextItem.name);

					// FIXME: set up a table for what each city consumes/produces
					// for now, cities just consume or produce randomly +/- 10
					quantityChange = UnityEngine.Random.Range(consumeRangeMin, consumeRangeMax+1);

					nextItem.count += quantityChange;

					// no negative values (TODO: cap at a maximum quantity?)
					if (nextItem.count < 0) nextItem.count = 0;

					Debug.Log(nextCity.name + (quantityChange>=0?" produced ":" consumed ") + quantityChange + " " + nextItem.name + " for a total of " + nextItem.count);

				} // items

			} // cities

		} // infinite coroutine loop with delay
	}

}
