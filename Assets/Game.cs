using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
public static class Game {

	public static GameSave Data;
	public static List<GameSave> savedGames = new List<GameSave>();
	public static List<Item> AllItems = new List<Item>();
	public static List<City> AllCities = new List<City>();

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
		Data = savedGames[0];//if save was not selected select first save

		if (Data.players == null) {//Fix old saves. 
			Data.players = new List<Player>();
		}
		if(Data.players.Count == 0){
			Player player1 = new Player("Mozzarella");
			foreach (Item i in AllItems) {
				player1.items.Add (new Item(i.name));
			}
			player1.items.Find(x => x.name == "Potatoes").count = 20;
			player1.money = 9700; //TODO: remove hardcoded starting cash
			Data.players.Add(player1);
		}

		CityCtrl[] Cities = Object.FindObjectsOfType<CityCtrl> ();
		foreach (CityCtrl ctrl in Cities) {
			ctrl.enabled = true;
			AllCities.Add(ctrl.city);
		}

		UIRouter.goTo ("OverWorld");
		Debug.Log ("Going to overworld from load");
	}
}
