using UnityEngine;
using System.Collections;

public class GameStateHandler : MonoBehaviour {

	private static GameStateHandler _instance = null;

	private GameManager GM;
	private GameObject _ui;

	public static GameStateHandler Instance {
		get {
			if(_instance == null) {
				_instance = GameObject.FindObjectOfType<GameStateHandler>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}

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
	}

	// Use this for initialization
	void Start () {
		GM = GameManager.Instance;
		GM.OnStateChange += HandleOnStateChange;

		_ui = GameObject.Find ("UI");
	}

	void OnLevelWasLoaded(int level) {
		GM = GameManager.Instance;
		_ui = GameObject.Find ("UI");
		if (GM.gameState == GameState.HighscoreInput && _ui != null) {
			_ui.transform.GetChild (0).gameObject.SetActive (true);
			_ui.transform.GetChild (1).gameObject.SetActive (false);
			_ui.transform.GetChild (2).gameObject.SetActive (false);
		}

		if (GM.gameState == GameState.Game) {
			GM.PlayerIsAlive = true;
			GlobalScoreManager.Reset();
			GlobalScoreManager.SetStartTime();
		}
	}

	void HandleOnStateChange () {
		_ui = GameObject.Find ("UI");

		Debug.Log(GM.gameState);

		if (GM.gameState == GameState.MainMenu && _ui != null) {
			_ui.transform.GetChild (0).gameObject.SetActive (false);
			_ui.transform.GetChild (1).gameObject.SetActive (false);
			_ui.transform.GetChild (2).gameObject.SetActive (true);
			
		} else if (GM.gameState == GameState.HighscoreInput && _ui != null) {
			Debug.LogWarning("Load Title Scene");
			//GM.OnStateChange -= HandleOnStateChange;
			Application.LoadLevel("TitleScene");

		} else if (GM.gameState == GameState.Highscore && _ui != null) {
			HighscoreManager.Instance.updatedHighscore = true;
			_ui.transform.GetChild (0).gameObject.SetActive (false);
			_ui.transform.GetChild (1).gameObject.SetActive (true);
			_ui.transform.GetChild (2).gameObject.SetActive (false);
			
		} else if (GM.gameState == GameState.Game && _ui != null) {
			_ui.transform.GetChild (0).gameObject.SetActive (false);
			_ui.transform.GetChild (1).gameObject.SetActive (false);
			_ui.transform.GetChild (2).gameObject.SetActive (false);

			//GM.OnStateChange -= HandleOnStateChange;
			Application.LoadLevel("GatesGroundScene");
		} 
	}

	public void OnApplicationQuit() {
		GameStateHandler._instance = null;
	}
}
