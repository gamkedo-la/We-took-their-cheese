using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
public static class Game {

	public static GameSave Data;
	public static List<GameSave> savedGames = new List<GameSave>();

	public static void Save() {		
		Debug.Log ("Save has been called");
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
		bf.Serialize(file, savedGames);
		file.Close();
	}

	public static void Load() {
		Debug.Log (Application.persistentDataPath);
		if (File.Exists (Application.persistentDataPath + "/savedGames.gd")) {
			Debug.Log ("Save found! loading now ^_^");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			savedGames = (List<GameSave>)bf.Deserialize (file);
			file.Close ();
		} else {
			Debug.Log ("Save Not found!");
			Data = new GameSave ();
			savedGames.Add (Data);
		}
		//TODO: write stuff for multiple saves
		Data = savedGames[0];
		//if save was not selected select first save

		CityCtrl[] Cities = Object.FindObjectsOfType<CityCtrl> ();
		foreach (CityCtrl ctrl in Cities) {
			ctrl.enabled = true;
		}

		UIRouter.goTo ("OverWorld");
		Debug.Log ("Going to overworld from load");
		//enable all cities
	}
}
