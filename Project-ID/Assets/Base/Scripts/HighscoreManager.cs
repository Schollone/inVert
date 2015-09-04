using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class HighscoreManager : MonoBehaviour {
	
	private static HighscoreManager _instance = null;

	public int nameLength = 3;
	public int maxPlayers = 20;
	public bool updatedHighscore = false;
	
	private List<Player> _players;
	private char[] _playerName;
	private int _cursor;

	void Awake() {
		if(_instance == null) {
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		} else {
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance) {
				Destroy(this.gameObject);
			}
		}

		_players = new List<Player> ();
		_playerName = new char[nameLength];
		_cursor = 0;
		
		for (int i = 0; i < _playerName.Length; i++) {
			_playerName[i] = '_';
		}
		Debug.Log ("Awake HighscoreManager");
		Load();
	}

	public static HighscoreManager Instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<HighscoreManager>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}

	public void OnApplicationQuit() {
		HighscoreManager._instance = null;
	}
	
	public void SavePlayer(string name, float score, float time) {
		Player p = new Player ();
		p.name = name;
		p.score = score;
		p.time = time;

		Player[] tmpPlayers = _players.ToArray();

		bool playerAdded = false;

		for (int i = 0; i < tmpPlayers.Length; i++) {

			if (p.score >= tmpPlayers[i].score) {
				_players.Insert(i, p);
				playerAdded = true;
				if (_players.Count >= maxPlayers) {
					//_players.RemoveAt(maxPlayers);
				}
				break;
			}
		}

		if (!playerAdded || tmpPlayers.Length == 0) {
			_players.Add(p);
		}

		Player[] players = _players.ToArray();
		for (int j = maxPlayers; j < players.Length; j++) {
			_players.RemoveAt(j);
		}

		updatedHighscore = true;
		Save();
	}

	public void AddLetter(char letter) {
		if (_cursor < nameLength) {
			_playerName[_cursor] = letter;
			if (_cursor < (nameLength-1) ) {
				_cursor++;
			}
		}
	}

	public void DeleteLetter() {
		if (_cursor > 0) {
			_cursor--;
		}
		_playerName[_cursor] = '_';
	}

	public string getPlayerName() {
		string s = "";
		foreach(char c in _playerName) {
			s += c;
		}
		return s;
	}

	public List<Player> getPlayers() {
		return _players;
	}

	public void Save() {
		Debug.Log("Save Data");
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/highscore.dat", FileMode.OpenOrCreate);

		HighscoreData data = new HighscoreData();
		data.players = _players;

		bf.Serialize(file, data);
		file.Close();
	}

	public void Load() {
		Debug.Log("Load Data");
		if (File.Exists(Application.persistentDataPath + "/highscore.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/highscore.dat", FileMode.Open);
			HighscoreData data = (HighscoreData) bf.Deserialize(file);
			file.Close();

			_players = data.players;
		}
	}
}

[Serializable]
public struct Player {
	public string name;
	public float score;
	public float time;
}

[Serializable]
class HighscoreData {
	public List<Player> players;
}
