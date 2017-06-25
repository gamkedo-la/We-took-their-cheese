using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIRouter {
	public static List<Canvas> screens = new List<Canvas>();
	public static string location;
	static int lastNav = -1;
	public static void goTo (string name) {
		if (lastNav == Time.frameCount) {
			Debug.Log ("canceled goto: " + name);
			return;
		}

		foreach (Canvas screen in screens) {
			if (screen.name != name) {
				screen.enabled = false;
			} else {
				screen.enabled = true;
				location = name;
				lastNav = Time.frameCount;
			}
		}
	}
	public static ShopUI shop = null;
		
}
