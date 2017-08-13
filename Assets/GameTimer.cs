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
                    quantityChange = 0;
                    //first produce
                    if (nextItem.prouction > 0)
                    {
                        quantityChange = UnityEngine.Random.Range(0, nextItem.prouction);
                    }
                    else {
						if (nextItem.maxAmount > 0 && UnityEngine.Random.Range(1, 25) == 1) {
                            quantityChange = 1;
                            Debug.Log("randomly producing 1 " + nextItem.name);
                        }
                    }
					nextItem.count += quantityChange;
                    if (quantityChange > 0) { 
                        Debug.Log(nextCity.name + (quantityChange >= 0 ? " produced " : " consumed ") + quantityChange + " " + nextItem.name + " for a total of " + nextItem.count);
                    }
                    //then consume
                    quantityChange = 0;
                    quantityChange = nextItem.maxAmount / 10;
                    if (quantityChange < nextItem.count)
                    {
						if (nextCity.money < nextCity.maxMoney) {
							nextCity.money += quantityChange * nextItem.price / 5;
						}
                        nextItem.count -= quantityChange;
                    }
                    else {
						if (nextCity.money < nextCity.maxMoney) {
                        	nextCity.money += (quantityChange + (nextItem.count - quantityChange)) * nextItem.price;
						}
                    }
                    Debug.Log(nextCity.name + " consumed " + quantityChange + " " + nextItem.name + " for a total of " + nextItem.count);

                    // no negative values (TODO: cap at a maximum quantity?)
                    if (nextItem.count < 0) nextItem.count = 0;

					
				} // items

			} // cities

		} // infinite coroutine loop with delay
	}

}
