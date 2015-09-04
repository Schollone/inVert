using UnityEngine;
using System.Collections;

public enum GameState { NullState, MainMenu, Game, Highscore, HighscoreInput }
public delegate void OnStateChangeHandler();

public class GameManager {

	protected GameManager() {}
	private static GameManager _instance = null;
	public event OnStateChangeHandler OnStateChange;
	public GameState gameState { get; private set; }

	private bool _playerIsAlive = false;

	public static GameManager Instance {
		get {
			if (GameManager._instance == null) {
				GameManager._instance = new GameManager();
			}
			return GameManager._instance;
		}
	}
	
	public void SetGameState(GameState state) {
		this.gameState = state;
		if (OnStateChange != null) {
			OnStateChange();
		}
	}

	public void OnApplicationQuit() {
		GameManager._instance = null;
	}
	

	public bool PlayerIsAlive {
		get {
			return _playerIsAlive;
		}
		set {
			_playerIsAlive = value;
		}
	}
}

